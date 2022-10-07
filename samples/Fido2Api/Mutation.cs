using System.Text;
using Fido2NetLib;
using Fido2NetLib.Development;
using Fido2NetLib.Objects;
using HotChocolate.Resolvers;

namespace Fido2Api;

// ReSharper disable once ClassNeverInstantiated.Global
public class Mutation
{
    private static readonly DevelopmentInMemoryStore DemoStorage = new();

    private string FormatException(Exception e)
    {
        return e.InnerException is not null ? $"{e.Message} ({e.InnerException.Message})" : e.Message;
    }

    public CredentialCreateOptions MakeCredentialOptions(
        IFido2 fido2,
        IHttpContextAccessor httpContextAccessor,
        string? username,
        string displayName,
        AttestationConveyancePreference attType,
        AuthenticatorAttachment? authType,
        bool requireResidentKey,
        UserVerificationRequirement userVerification)
    {
        try
        {
            if (string.IsNullOrEmpty(username))
            {
                username = $"{displayName} (Usernameless user created at {DateTime.UtcNow})";
            }

            // 1. Get user from DB by username (in our example, auto create missing users)
            var user = DemoStorage.GetOrAddUser(username, () => new()
            {
                DisplayName = displayName,
                Name = username,
                Id = Encoding.UTF8.GetBytes(username) // byte representation of userID is required
            });

            // 2. Get user existing keys by username
            var existingKeys = DemoStorage.GetCredentialsByUser(user)
                .Select(c => c.Descriptor).ToList();

            // 3. Create options
            AuthenticatorSelection authenticatorSelection = new()
            {
                RequireResidentKey = requireResidentKey,
                UserVerification = userVerification
            };

            if (authType.HasValue)
            {
                authenticatorSelection.AuthenticatorAttachment = authType;
            }

            AuthenticationExtensionsClientInputs exts = new()
            {
                Extensions = true,
                UserVerificationMethod = true,
            };

            var options = fido2.RequestNewCredential(user, existingKeys,
                authenticatorSelection, attType, exts);

            // 4. Temporarily store options, session/in-memory cache/redis/db
            httpContextAccessor.HttpContext!.Session.SetString("fido2.attestationOptions",
                options.ToJson());

            // 5. return options to client
            return options;
        }
        catch (Exception e)
        {
            // Error handling to be reviewed within your code
            throw new GraphQLException(FormatException(e));
        }
    }

    public async Task<AttestationVerificationSuccess> MakeCredential(
        IFido2 fido2,
        IHttpContextAccessor httpContextAccessor,
        AuthenticatorAttestationRawResponse attestationResponse,
        CancellationToken cancellationToken)
    {
        try
        {
            // 1. get the options we sent the client
            var jsonOptions =
                httpContextAccessor.HttpContext!.Session.GetString(
                    "fido2.attestationOptions");
            var options = CredentialCreateOptions.FromJson(jsonOptions);

            // 2. Create callback so that lib can verify credential id is unique to this user
            IsCredentialIdUniqueToUserAsyncDelegate callback = static async (args, cancellationToken) =>
            {
                var users =
                    await DemoStorage.GetUsersByCredentialIdAsync(args.CredentialId,
                        cancellationToken);
                return users.Count <= 0;
            };

            // 2. Verify and make the credentials
            var success = await fido2.MakeNewCredentialAsync(attestationResponse, options,
                callback, cancellationToken: cancellationToken);

            // 3. Store the credentials in db
            DemoStorage.AddCredentialToUser(options.User, new()
            {
                Descriptor = new PublicKeyCredentialDescriptor(success.Result!.CredentialId),
                PublicKey = success.Result.PublicKey,
                UserHandle = success.Result.User.Id,
                SignatureCounter = success.Result.Counter,
                CredType = success.Result.CredType,
                RegDate = DateTime.Now,
                AaGuid = success.Result.Aaguid
            });

            // 4. return the result to client
            return success.Result;
        }
        catch (Exception e)
        {
            // Error handling to be reviewed within your code
            throw new GraphQLException(FormatException(e));
        }
    }

    public AssertionOptions MakeAssertionOptions(
        IResolverContext context,
        IFido2 fido2,
        IHttpContextAccessor httpContextAccessor,
        string? username,
        UserVerificationRequirement userVerification = UserVerificationRequirement.Discouraged)
    {
        try
        {
            var existingCredentials = new List<PublicKeyCredentialDescriptor>();

            if (!string.IsNullOrEmpty(username))
            {
                // 1. Get user from DB
                var user = DemoStorage.GetUser(username) ?? throw new ArgumentException("Username was not registered");

                // 2. Get registered credentials from database
                existingCredentials = DemoStorage.GetCredentialsByUser(user).Select(c => c.Descriptor).ToList();
            }

            var exts = new AuthenticationExtensionsClientInputs()
            {
                UserVerificationMethod = true
            };

            // 3. Create options
            var options = fido2.GetAssertionOptions(
                existingCredentials,
                userVerification,
                exts
            );

            // 4. Temporarily store options, session/in-memory cache/redis/db
            httpContextAccessor.HttpContext!.Session.SetString("fido2.assertionOptions", options.ToJson());

            // 5. Return options to client
            return options;
        }
        catch (Exception e)
        {
            // Error handling to be reviewed within your code
            throw new GraphQLException(FormatException(e));
        }
    }

    public async Task<AssertionVerificationResult> MakeAssertion(
        IFido2 fido2,
        IHttpContextAccessor httpContextAccessor,
        AuthenticatorAssertionRawResponse clientResponse,
        CancellationToken cancellationToken)
    {
        try
        {
            // 1. Get the assertion options we sent the client
            var jsonOptions =
                httpContextAccessor.HttpContext!.Session.GetString(
                    "fido2.assertionOptions");
            var options = AssertionOptions.FromJson(jsonOptions);

            // 2. Get registered credential from database
            var creds = DemoStorage.GetCredentialById(clientResponse.Id) ??
                        throw new Exception("Unknown credentials");

            // 3. Get credential counter from database
            var storedCounter = creds.SignatureCounter;

            // 4. Create callback to check if userhandle owns the credentialId
            IsUserHandleOwnerOfCredentialIdAsync callback = static async (args, cancellationToken) =>
            {
                var storedCreds = await DemoStorage.GetCredentialsByUserHandleAsync(args.UserHandle, cancellationToken);
                return storedCreds.Exists(c => c.Descriptor.Id.SequenceEqual(args.CredentialId));
            };

            // 5. Make the assertion
            var res = await fido2.MakeAssertionAsync(clientResponse, options,
                creds.PublicKey, storedCounter, callback,
                cancellationToken: cancellationToken);

            // 6. Store the updated counter
            DemoStorage.UpdateCounter(res.CredentialId, res.Counter);

            // 7. return the result to client
            return res;
        }
        catch (Exception e)
        {
            // Error handling to be reviewed within your code
            throw new GraphQLException(FormatException(e));
        }
    }
}
