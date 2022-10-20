using System.Diagnostics;
using HotChocolate.Language;
using HotChocolate.Utilities;

namespace HotChocolate.Types.Fido2.Utilities;

internal class ConverterContext
{
    public IValueNode? ValueNode { get; set; }
}

internal class DictionaryToObjectValueConverter : DictionaryVisitor<ConverterContext>
{
    public ObjectValueNode Convert(IReadOnlyDictionary<string, object?> dictionary)
    {
        ConverterContext context = new();
        VisitObject(dictionary, context);
        var result = context.ValueNode as ObjectValueNode;
        Debug.Assert(result != null, nameof(result) + " != null");
        return result;
    }

    protected override void VisitObject(
        IReadOnlyDictionary<string, object?> dictionary,
        ConverterContext context)
    {
        List<ObjectFieldNode> fieldNodes = new();
        foreach (var field in dictionary)
        {
            ConverterContext valueContext = new();
            VisitField(field, valueContext);
            Debug.Assert(valueContext.ValueNode != null,
                "valueContext.ValueNode != null");
            fieldNodes.Add(new(field.Key, valueContext.ValueNode));
        }
        context.ValueNode = new ObjectValueNode(fieldNodes);
    }

    protected override void VisitList(
        IReadOnlyList<object> list,
        ConverterContext context)
    {
        List<IValueNode> valueNodes = new();
        foreach (var value in list)
        {
            ConverterContext valueContext = new();
            Visit(value, valueContext);
            Debug.Assert(valueContext.ValueNode != null,
                "valueContext.ValueNode != null");
            valueNodes.Add(valueContext.ValueNode);
        }
        context.ValueNode = new ListValueNode(valueNodes);
    }

    protected override void VisitValue(object value, ConverterContext context)
    {
        context.ValueNode = value switch
        {
            null => NullValueNode.Default,
            string s => new StringValueNode(s),
            short s => new IntValueNode(s),
            int i => new IntValueNode(i),
            long l => new IntValueNode(l),
            float f => new FloatValueNode(f),
            double d => new FloatValueNode(d),
            decimal d => new FloatValueNode(d),
            bool b => new BooleanValueNode(b),
            sbyte s => new IntValueNode(s),
            byte b => new IntValueNode(b),
            _ => throw new NotSupportedException(
                string.Format(TypeResources.TypeConversion_ConvertNotSupported, 
                    value.GetType().Name, nameof(IValueNode)))
        };
    }
}
