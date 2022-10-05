using System.Diagnostics.CodeAnalysis;
using Fido2NetLib;
using HotChocolate.Language;

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

            _ => throw ThrowHelper.EnumMember_ParseValue_IsInvalid(this, typeof(TEnum).Name)
        };
    }

    /// <inheritdoc />
    protected override TEnum ParseLiteral(StringValueNode valueSyntax)
    {
        if (TryDeserialize(valueSyntax.Value, out var value))
        {
            return (TEnum) value;
        }
        throw ThrowHelper.EnumMember_ParseLiteral_IsInvalid(this, typeof(TEnum).Name);
    }

    /// <inheritdoc />
    protected override StringValueNode ParseValue(TEnum runtimeValue)
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
            case TEnum e:
                resultValue = Serialize(e);
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

    private string Serialize(TEnum runtimeValue)
    {
        return EnumNameMapper<TEnum>.GetName(runtimeValue);
    }

    private bool TryDeserialize(string resultValue, [NotNullWhen(true)] out TEnum? runtimeValue)
    {
        if (EnumNameMapper<TEnum>.TryGetValue(resultValue, out var result))
        {
            runtimeValue = result;
            return true;
        }
        runtimeValue = null;
        return false;
    }
}
