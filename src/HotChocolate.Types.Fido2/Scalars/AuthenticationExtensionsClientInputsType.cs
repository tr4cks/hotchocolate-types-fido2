using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2.Scalars;

internal class AuthenticationExtensionsClientInputsType : TypedDictionaryType<AuthenticationExtensionsClientInputs>
{
    // todo: add description
    public AuthenticationExtensionsClientInputsType() : base(WellKnownScalarTypes.AuthenticationExtensionsClientInputs)
    {
    }

    protected override bool TrySerialize(AuthenticationExtensionsClientInputs runtimeValue, [NotNullWhen(true)] out IReadOnlyDictionary<string, object?>? resultValue)
    {
        Dictionary<string, object?> result = new();
        if (runtimeValue.Example is not null)
        {
            result["example.extension"] = runtimeValue.Example;
        }
        if (runtimeValue.AppID is not null)
        {
            result["appid"] = runtimeValue.AppID;
        }
        if (runtimeValue.AuthenticatorSelection is not null)
        {
            result["authnSel"] = runtimeValue.AuthenticatorSelection;
        }
        if (runtimeValue.Extensions is not null)
        {
            // ReSharper disable once StringLiteralTypo
            result["exts"] = runtimeValue.Extensions;
        }
        if (runtimeValue.UserVerificationMethod is not null)
        {
            result["uvm"] = runtimeValue.UserVerificationMethod;
        }
        resultValue = result;
        return true;
    }

    protected override bool TryDeserialize(IReadOnlyDictionary<string, object?> resultValue, [NotNullWhen(true)] out AuthenticationExtensionsClientInputs? runtimeValue)
    {
        try
        {
            var stringValue = JsonSerializer.Serialize(resultValue);
            runtimeValue = JsonSerializer.Deserialize<AuthenticationExtensionsClientInputs>(stringValue)!;
            return true;
        }
        catch (Exception)
        {
            runtimeValue = null;
            return false;
        }
    }
}
