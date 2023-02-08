namespace Labs.Lib;

public static partial class StringExtensions
{

    public static string[] GetAllBorders(this string value)
    {
        var res = new List<string>();
        for (int i = 1; i < value.Length; i++) {

            if (value.Substring(0, i) == value.Substring(value.Length - i, i)) {
                res.Add(value.Substring(0,i));
            }
        }

        res.Sort();
        return res.ToArray();
    }

    public static IEnumerable<int> GetSubstrings(this string pattern, string text, IMethodOfSearchInString methodOfSearch) => 
        methodOfSearch.SearchSubstring(pattern, text);

    public static IEnumerable<int> GetSubstrings(this string pattern, string text) =>  
        pattern.GetSubstrings(text, new BorderStringSearchMethod());

    public static BlocksStringSearchMethod BlocksHumanMethod => new(BlocksStringSearchMethod.IndexType.Human);
    public static BlocksStringSearchMethod BlocksProgrammerMethod => new(BlocksStringSearchMethod.IndexType.Programmer);
    
    public static BorderStringSearchMethod BordersMethod => new();

}
