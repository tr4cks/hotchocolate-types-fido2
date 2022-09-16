using System.Diagnostics.CodeAnalysis;
using HotChocolate.Language;

namespace HotChocolate.Types.Fido2.Scalars;

public class Base64Type : ScalarType<byte[], StringValueNode>
{
    public Base64Type() : base(WellKnownScalarTypes.Base64)
    {
        Description = ScalarResources.Base64Type_Description;
    }

    /// <inheritdoc />
    protected override bool IsInstanceOfType(StringValueNode valueSyntax)
    {
        try
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Convert.FromBase64String(valueSyntax.Value);
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }

    // todo
    /// <inheritdoc />
    public override IValueNode ParseResult(object? resultValue)
    {
        return resultValue switch
        {
            null => NullValueNode.Default,

            // todo: review
            string s when TryDeserialize(s, out _) =>
                new StringValueNode(s),

            byte[] value => ParseValue(value),

            _ => throw ThrowHelper.Base64_ParseValue_IsInvalid(this)
        };
    }

    // todo: review
    /// <inheritdoc />
    protected override byte[] ParseLiteral(StringValueNode valueSyntax)
    {
        if (TryDeserialize(valueSyntax.Value, out var value))
        {
            return value;
        }
        throw ThrowHelper.Base64_ParseLiteral_IsInvalid(this);
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

    private string Serialize(byte[] runtimeValue) => Convert.ToBase64String(runtimeValue);

    // todo: NotNullWhen?
    private bool TryDeserialize(string resultValue, [NotNullWhen(true)] out byte[]? runtimeValue)
    {
        try
        {
            runtimeValue = Convert.FromBase64String(resultValue);
            return true;
        }
        catch (FormatException)
        {
            runtimeValue = null;
            return false;
        }
    }
}
