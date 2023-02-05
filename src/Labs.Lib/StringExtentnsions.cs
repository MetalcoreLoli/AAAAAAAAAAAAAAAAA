namespace Labs.Lib;

public static class StringExtentions
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
}
