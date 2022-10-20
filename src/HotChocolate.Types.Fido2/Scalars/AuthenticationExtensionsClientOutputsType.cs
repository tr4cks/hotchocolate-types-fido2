using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Fido2NetLib.Objects;

namespace HotChocolate.Types.Fido2.Scalars;

// ReSharper disable once ClassNeverInstantiated.Global
internal class AuthenticationExtensionsClientOutputsType :
    TypedDictionaryType<AuthenticationExtensionsClientOutputs>
{
    public AuthenticationExtensionsClientOutputsType() :
        base(WellKnownScalarTypes.AuthenticationExtensionsClientOutputs)
    {
        Description = ScalarResources
            .AuthenticationExtensionsClientOutputsType_Description;
    }

    protected override bool TrySerialize(
        AuthenticationExtensionsClientOutputs runtimeValue,
        [NotNullWhen(true)]
        out IReadOnlyDictionary<string, object?>? resultValue)
    {
        Dictionary<string, object?> result = new();
        if (runtimeValue.Example is not null)
        {
            result["example.extension"] = runtimeValue.Example;
        }
        result["appid"] = runtimeValue.AppID;
        result["authnSel"] = runtimeValue.AuthenticatorSelection;
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

    protected override bool TryDeserialize(
        IReadOnlyDictionary<string, object?> resultValue,
        [NotNullWhen(true)]
        out AuthenticationExtensionsClientOutputs? runtimeValue)
    {
        try
        {
            var stringValue = JsonSerializer.Serialize(resultValue);
            runtimeValue =
                JsonSerializer.Deserialize<AuthenticationExtensionsClientOutputs>(
                    stringValue)!;
            return true;
        }
        catch (Exception)
        {
            runtimeValue = null;
            return false;
        }
    }
}
