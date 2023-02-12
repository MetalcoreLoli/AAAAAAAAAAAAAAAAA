namespace Labs.Lib;

public static partial class StringExtensions
{

    ///<summary>
    /// Методов поиска граней в строке <paramref name="value"/>
    ///</summary>
    ///<param name="value"> Строка, в которой ищутся грани</param>
    ///<returns>Возвращает массив граней</returns>
    public static IEnumerable<string> GetAllBorders(this string value)
    {
        for (int i = 1; i < value.Length; i++) 
        {
            // сравниваем префикс и суффикс
            if (value.Substring(0, i) == value.Substring(value.Length - i, i)) 
            {
                //так как грань - это подстрока, которая одновременно 
                //префикс и суффикс, то без разнице что добавлять в 
                //список. в данном случае добавляется префикс
                yield return value.Substring(0,i);
            }
        }
    }

    public static IEnumerable<int> GetSubstrings(this string pattern, string text, IMethodOfSearchInString methodOfSearch) => 
        methodOfSearch.SearchSubstring(pattern, text);

    public static IEnumerable<int> GetSubstrings(this string pattern, string text) =>  
        pattern.GetSubstrings(text, new BorderStringSearchMethod());

    public static BlocksStringSearchMethod BlocksMethod => new();
    
    public static BorderStringSearchMethod BordersMethod => new();

}
