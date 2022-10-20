using HotChocolate.Types.Fido2.Utilities;
using HotChocolate.Utilities;

namespace HotChocolate.Types.Fido2.UnitTests;

public class UtilitiesTests
{
    private readonly ObjectValueToDictionaryConverter _objectValueToDictionaryConverter =
        new();

    [Fact]
    public void DictionaryToObjectValueConverterTest()
    {
        DictionaryToObjectValueConverter converter = new();
        Dictionary<string, object?> originalDictionary = new()
        {
            {
                "object", new Dictionary<string, object?>
                {
                    {"nullValue", null},
                    {"listOfIntegers", new List<object> {1, 2, 3, 4, 5}},
                    {
                        "listOfObjects", new List<object>
                        {
                            new Dictionary<string, object?>
                            {
                                {"int", 42},
                                {"bool", true}
                            }
                        }
                    }
                }
            },
            {"string", "Hello World!"}
        };
        var result = converter.Convert(originalDictionary);

        var dictionaryForTesting = _objectValueToDictionaryConverter.Convert(result);
        Assert.Equal(originalDictionary, dictionaryForTesting);
    }
}
