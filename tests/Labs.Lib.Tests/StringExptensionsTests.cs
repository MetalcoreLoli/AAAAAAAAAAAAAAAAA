namespace Labs.Lib.Tests;

public class StringExtenstionsTests
{
    #region Tests

    [Theory]
    [MemberData(nameof(BordersTestsData))]
    public void Returns_Border_For_String(string value, string[] excepted)
    {

    }

    [Theory]
    [MemberData(nameof(SufixesTestsData))]
    public void Returns_Array_Of_Sufixes_For_String(string value, string[] excepted)
    {

    }

    [Theory]
    [MemberData(nameof(PrefixesTestsData))]
    public void Returns_Array_Of_Prefixes_For_String(string value, string[] excepted)
    {

    }
        
    #endregion



    #region MemberData
        
    public static IEnumerable<object[]> SufixesTestsData = new List<object[]> ()
    {
        new object[] {"ab", new string[] {}},
        new object[] {"abba", new string[] {}},
    };

    public static IEnumerable<object[]> PrefixesTestsData = new List<object[]> ()
    {
        new object[] {"ab", new string[] {}},
        new object[] {"abba", new string[] {}},
    };

    public static IEnumerable<object[]> BordersTestsData = new List<object[]> ()
    {
    };
    #endregion
}
