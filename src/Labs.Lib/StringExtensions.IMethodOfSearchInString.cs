using System.Runtime.CompilerServices;

namespace Labs.Lib;

public static partial class StringExtensions
{
    public interface IMethodOfSearchInString
    {
        IEnumerable<int> SearchSubstring(string pattern, string sample);
    }
    
    public abstract class MethodOfSearchInString : IMethodOfSearchInString
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected String JoinRows(String s1, String s2) => s1 + "$" + s2;

        public abstract IEnumerable<int> GetTable(string str);

        /// <summary>
        /// Поиск <paramref name="pattern"/> в <paramref name="sample"/>
        /// </summary>
        /// <param name="pattern">Шаблон</param>
        /// <param name="sample">Строка</param>
        /// <returns>Индексы всех найденных подстрок</returns>
        public abstract IEnumerable<int> SearchSubstring(string pattern, string sample);
    }
}
