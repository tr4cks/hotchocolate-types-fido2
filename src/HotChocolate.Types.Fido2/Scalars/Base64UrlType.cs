using System.Diagnostics.CodeAnalysis;
using HotChocolate.Language;
using Microsoft.IdentityModel.Tokens;

namespace HotChocolate.Types.Fido2.Scalars;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Base64UrlType : ScalarType<byte[], StringValueNode>
{
    public Base64UrlType() : base(WellKnownScalarTypes.Base64Url)
    {
        Description = ScalarResources.Base64UrlType_Description;
    }

    /// <inheritdoc />
    protected override bool IsInstanceOfType(StringValueNode valueSyntax)
    {
        try
        {
            Base64UrlEncoder.DecodeBytes(valueSyntax.Value);
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }

    /// <inheritdoc />
    public override IValueNode ParseResult(object? resultValue)
    {
        return resultValue switch
        {
            null => NullValueNode.Default,

            string s when TryDeserialize(s, out _) =>
                new StringValueNode(s),

            byte[] value => ParseValue(value),

            _ => throw ThrowHelper.Base64Url_ParseValue_IsInvalid(this)
        };
    }

    /// <inheritdoc />
    protected override byte[] ParseLiteral(StringValueNode valueSyntax)
    {
        if (TryDeserialize(valueSyntax.Value, out var value))
        {
            return value;
        }
        throw ThrowHelper.Base64Url_ParseLiteral_IsInvalid(this);
    }

    /// <inheritdoc />
    protected override StringValueNode ParseValue(byte[] runtimeValue)
    {
        return new(Serialize(runtimeValue));
    }

    /// <inheritdoc />
    public override bool TrySerialize(object? runtimeValue, out object? resultValue)
    {
        switch (runtimeValue)
        {
            case null:
                resultValue = null;
                return true;
            case byte[] value:
                resultValue = Serialize(value);
                return true;
            default:
                resultValue = null;
                return false;
        }
    }

    /// <inheritdoc />
    public override bool TryDeserialize(object? resultValue, out object? runtimeValue)
    {
        switch (resultValue)
        {
            case null:
                runtimeValue = null;
                return true;
            case string s when TryDeserialize(s, out var value):
                runtimeValue = value;
                return true;
            default:
                runtimeValue = null;
                return false;
        }
    }

    private string Serialize(byte[] runtimeValue) =>
        Base64UrlEncoder.Encode(runtimeValue);

    private bool TryDeserialize(
        string resultValue,
        [NotNullWhen(true)] out byte[]? runtimeValue)
    {
        try
        {
            runtimeValue = Base64UrlEncoder.DecodeBytes(resultValue);
            return true;
        }
        catch (FormatException)
        {
            runtimeValue = null;
            return false;
        }
    }
}
