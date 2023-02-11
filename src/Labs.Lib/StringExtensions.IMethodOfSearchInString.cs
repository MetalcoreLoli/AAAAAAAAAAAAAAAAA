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
        ///<summary>
        /// Метод конкатенирует строки <paramref name="pattern"/> и <paramref name="text"/> и ставит между 
        /// <paramref name="sep"/>
        ///</summary>
        ///<param name="pattern"> паттерн </param>
        ///<param name="text"> текст </param>
        ///<param name="sep"> не алфавитный символ </param>
        ///<returns> 
        /// Возвращает строку <paramref name="pattern"/> + <paramref name="sep"/> + <paramref name="text"/> 
        ///</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected string JoinStrings(string pattern, string text, char sep) => pattern + sep + text;

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
