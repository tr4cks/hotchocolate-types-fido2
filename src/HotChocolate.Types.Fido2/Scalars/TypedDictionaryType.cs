using System.Diagnostics.CodeAnalysis;
using HotChocolate.Language;
using HotChocolate.Types.Fido2.Utilities;
using HotChocolate.Utilities;

namespace HotChocolate.Types.Fido2.Scalars;

internal abstract class TypedDictionaryType<TRuntimeType> :
    ScalarType<TRuntimeType, ObjectValueNode>
{
    private readonly DictionaryToObjectValueConverter _dictionaryToObjectValueConverter =
        new();
    private readonly ObjectValueToDictionaryConverter _objectValueToDictConverter = new();

    protected TypedDictionaryType(
        NameString name,
        string? description = null,
        BindingBehavior bind = BindingBehavior.Explicit) : base(name, bind)
    {
        Description = description;
    }

    /// <inheritdoc />
    public override IValueNode ParseResult(object? resultValue)
    {
        return resultValue switch
        {
            null => NullValueNode.Default,

            IReadOnlyDictionary<string, object?> d =>
                _dictionaryToObjectValueConverter.Convert(d),

            TRuntimeType value => ParseValue(value),

            _ => throw ThrowHelper.TypedDictionary_ParseValue_IsInvalid(this,
                typeof(TRuntimeType).Name)
        };
    }

    /// <inheritdoc />
    protected override TRuntimeType ParseLiteral(ObjectValueNode valueSyntax)
    {
        var dict = _objectValueToDictConverter.Convert(valueSyntax);
        if (TryDeserialize(dict, out var value))
        {
            return value;
        }
        throw ThrowHelper.TypedDictionary_ParseLiteral_IsInvalid(this,
            typeof(TRuntimeType).Name);
    }

    /// <inheritdoc />
    protected override ObjectValueNode ParseValue(TRuntimeType runtimeValue)
    {
        if (TrySerialize(runtimeValue, out var dict))
        {
            return _dictionaryToObjectValueConverter.Convert(dict);
        }
        throw ThrowHelper.TypedDictionary_ParseValue_IsInvalid(this,
            typeof(TRuntimeType).Name);
    }

    /// <inheritdoc />
    public override bool TrySerialize(object? runtimeValue, out object? resultValue)
    {
        switch (runtimeValue)
        {
            case null:
                resultValue = null;
                return true;
            case TRuntimeType t when TrySerialize(t, out var value):
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
            case IReadOnlyDictionary<string, object?> d
                when TryDeserialize(d, out var value):
                runtimeValue = value;
                return true;
            default:
                runtimeValue = null;
                return false;
        }
    }

    protected abstract bool TrySerialize(
        TRuntimeType runtimeValue,
        [NotNullWhen(true)]
        out IReadOnlyDictionary<string, object?>? resultValue);

    protected abstract bool TryDeserialize(
        IReadOnlyDictionary<string, object?> resultValue,
        [NotNullWhen(true)]
        out TRuntimeType? runtimeValue);
}
