namespace Labs.Lib;

public static partial class StringExtentions
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

    public class BorderStringSearchMethod : MethodOfSearchInString
    {
        private IEnumerable<int> MaxBorderArray(String str)
        {
            int n = str.Length;
            var borders = new int[n];
            borders[0] = -1;

            for (int i = 0; i < n - 1; i++)
            {
                int tmp = borders[i];

                while ((tmp > -1) && (str[i + 1] != str[tmp + 1])) tmp = borders[tmp];

                if (str[i + 1] == str[tmp + 1])
                    borders[i + 1] = tmp + 1;
                else
                    borders[i + 1] = -1;
            }

            return borders;
        }

        private IEnumerable<int> GetRightBorders(int[] borders, int patternLength)
        {
            for (int i = 0; i < borders.Length; i++)
            {
                if ((borders[i] + 1) == patternLength)
                    yield return i;
            }
        }

        public override IEnumerable<int> SearchSubstring(String pattern, String sample)
        {
            String str = JoinRows(pattern, sample);

            var borders = MaxBorderArray(str).ToArray();
            int lengthPattern = pattern.Length;

            return GetRightBorders(borders, lengthPattern);
        }
    }
}
