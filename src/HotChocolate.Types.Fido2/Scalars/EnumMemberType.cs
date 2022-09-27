using System.Diagnostics.CodeAnalysis;
using HotChocolate.Language;
using HotChocolate.Types.Fido2.Extensions;

namespace HotChocolate.Types.Fido2.Scalars;

public class EnumMemberType<TEnum> : ScalarType<TEnum, StringValueNode> where TEnum : struct, Enum
{
    public EnumMemberType(NameString name, string? description = null, BindingBehavior bind = BindingBehavior.Explicit) : base(name, bind)
    {
        Description = description;
    }

    /// <inheritdoc />
    protected override bool IsInstanceOfType(StringValueNode valueSyntax)
    {
        return TryDeserialize(valueSyntax.Value, out _);
    }

    /// <inheritdoc />
    public override IValueNode ParseResult(object? resultValue)
    {
        return resultValue switch
        {
            null => NullValueNode.Default,

            string s when TryDeserialize(s, out _) =>
                new StringValueNode(s),

            TEnum value => ParseValue(value),

            _ => throw ThrowHelper.EnumMember_ParseValue_IsInvalid(this)
        };
    }

    /// <inheritdoc />
    protected override TEnum ParseLiteral(StringValueNode valueSyntax)
    {
        if (TryDeserialize(valueSyntax.Value, out var value))
        {
            return (TEnum) value;
        }
        throw ThrowHelper.EnumMember_ParseLiteral_IsInvalid(this);
    }

    /// <inheritdoc />
    protected override StringValueNode ParseValue(TEnum runtimeValue)
    {
        if (TrySerialize(runtimeValue, out var value))
        {
            return new(value);
        }
        throw ThrowHelper.EnumMember_ParseValue_IsInvalid(this);
    }

    /// <inheritdoc />
    public override bool TrySerialize(object? runtimeValue, out object? resultValue)
    {
        switch (runtimeValue)
        {
            case null:
                resultValue = null;
                return true;
            case TEnum e when TrySerialize(e, out var value):
                resultValue = value;
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

    private bool TrySerialize(TEnum runtimeValue, [NotNullWhen(true)] out string? resultValue)
    {
        resultValue = runtimeValue.GetEnumMemberValue();
        return resultValue is not null;
    }

    private bool TryDeserialize(string resultValue, [NotNullWhen(true)] out TEnum? runtimeValue)
    {
        runtimeValue = EnumExtensions.GetEnumFromEnumMemberValue<TEnum>(resultValue);
        return runtimeValue is not null;
    }
}
