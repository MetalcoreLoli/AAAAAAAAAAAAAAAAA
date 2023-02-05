namespace Labs.Lib.Tests;

public class StringExtenstionsTests
{
    #region Tests

    [Theory]
    [MemberData(nameof(BordersTestsData))]
    public void Returns_Border_For_String(string value, string[] excepted)
    {
        //act
        var result = value.GetAllBorders();

        //assert 
        result.Should().BeEquivalentTo(excepted);
    }

    [Theory]
    [MemberData(nameof(SufixesTestsData))]
    public void Returns_Array_Of_Sufixes_For_String(string value, string[] excepted)
    {

    }
    #endregion



    #region MemberData
        
    public static IEnumerable<object[]> SufixesTestsData = new List<object[]> ()
    {
        new object[] {},
    };

    public static IEnumerable<object[]> BordersTestsData = new List<object[]> ()
    {
        new object[] {"ab", new string[] {}},
        new object[] {"abba", new string[] {"a"}},
        new object[] {"abbabaab", new string[] {"ab"}},
        new object[] {"abbabaabbaababba", new string[] {"a", "abba"}},
        new object[] {"abbabaabbaababbabaababbaabbabaab", new string[] {"ab", "abbabaab"}},
        new object[] {"abcab", new string[] {"ab"}},
        new object[] {"abcabacabcbacbcacbabcabacabcb", new string[] {"abcabacabcb"}},
        new object[]
        {
            "abcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcbabcabacbcacbacabcbacbcacbabcabacbcacbacabcbabcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcb",
            new string[] {"abcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcb"}
        },
        new object[] {"aba", new string[] {"a"}},
        new object[] {"abaab", new string[] {"ab"}},
        new object[] {"abaabaab", new string[] {"ab", "abaab"}},
        new object[] {"abaababaabaab", new string[] {"ab", "abaab"}},
        new object[] {"abaababaabaababaabaab", new string[] {"ab", "abaab", "abaababaabaab"}},
        new object[] {"abaababaabaababaabaababaababaabaab", new string[] {"ab", "abaab", "abaababaabaab"}},
    };
    #endregion
}
