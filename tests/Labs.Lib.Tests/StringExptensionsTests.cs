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
    [MemberData(nameof(GetRightIndexOfSubstringsTestsData))]
    public void Returns_Right_Index_Of_Substrings_If_Pattern_Was_Found(string pattern, string text, int[] excepted)
    {
        //act
        var result = 
            pattern.GetSubstrings(text, new StringExtensions.BorderStringSearchMethod());

        //assert
        result.Should().BeEquivalentTo(excepted);
    }
    
    [Theory]
    [MemberData(nameof(GetIndexesOfSubstringsTestsData))]
    public void Returns_Indexes_Of_Substrings_If_Pattern_Was_Found(string pattern, string text, int[] excepted)
    {
        //act
        var result = 
            pattern.GetSubstrings(text, StringExtensions.BlocksMethod);

        //assert
        result.Should().BeEquivalentTo(excepted);
    }

    [Fact]
    public void Transmutation_Of_Blocks_Should_Return_Borders()
    {
        var pattern = "aba";
        var text = "ababa";
        var patternText = pattern + "$" + text;

        var excepted = StringExtensions.BordersMethod.GetTable(patternText).ToArray();    
        var blocks = StringExtensions.BlocksMethod.GetTable(patternText).ToArray();

        //act
        var result = StringExtensions.Transmutation.FromBlocksToBorders(blocks, (patternText).Length);

        //assert
        result.Should().BeEquivalentTo(excepted);
    }

    [Fact]
    public void Transmutation_Of_Borders_Should_Return_Blocks()
    {
        var pattern = "aba";
        var text = "ababa";
        var patternText = pattern + "$" + text;

        var borders = StringExtensions.BordersMethod.GetTable(patternText).ToArray();
        var excepted  = StringExtensions.BlocksMethod.GetTable(patternText).ToArray();

        //act
        var result = StringExtensions.Transmutation.FromBordersToBlocks(borders, (patternText).Length);

        //assert
        result.Should().BeEquivalentTo(excepted);
    }

    #endregion

    //ab abc 
    //ab  - 2
    //abcadb - 6
    //ab - 2

    #region MemberData
        
    public static IEnumerable<object[]> GetIndexesOfSubstringsTestsData = new List<object[]> ()
    {               
        new object[] {"aba","ababa", new int[] {0, 2}},
        new object[] {"ab","abbab", new int[] {0, 3}},
    };

    public static IEnumerable<object[]> GetRightIndexOfSubstringsTestsData = new List<object[]> ()
    {               
        new object[] {"ABAAB","ABAABABAABAAB", new int[] {4, 9, 12}},
                    // 0  1  2  3 4 5 6  7  8
                    // a  b  a  $ a b a  b  a
                    //-1 -1 -1 -1 0 1 2  1  2
        new object[] {"aba","ababa", new int[] {2, 4}},

                    // 0  1  2 3 4  5 6 7
                    // a  b  $ a b  b a b
                    //-1 -1 -1 0 1 -1 0 1
        new object[] {"ab","abbab", new int[] {1, 4}},
        
    };
    
    public static IEnumerable<object[]> TransmutationTestData = new List<object[]> ()
    {               
        new object[] {new int[] {4, 6}, new int[] {6, 8}},
        new object[] {new int[] {2, 5}, new int[] {4, 7}},
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
