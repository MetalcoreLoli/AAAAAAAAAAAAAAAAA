namespace Labs.Lib;

public static partial class StringExtensions
{
    public class BorderStringSearchMethod : MethodOfSearchInString
    {
        ///<summary>
        /// Метод преобразовывает <paramref name="borders"/> в индексы в строке
        ///</summary>
        ///<param name="borders"> Таблица граней </param>
        ///<param name="patternLength"> Длина шаблона </param>
        private IEnumerable<int> GetRightBorders(int[] borders, int patternLength)
        {
            for (int i = 0; i < borders.Length; i++)
            {
                if ((borders[i] + 1) == patternLength)
                    yield return (i - patternLength - 1);
            }
        }

        ///<summary>
        /// Метод возвращает таблицу граней.
        ///</summary>
        ///<param name="str"> 
        ///     Данный параметр должен соответствовать следующей форме:
        ///     шаблон + 'не_алфавитный_символ' + строка
        ///</param>
        public override IEnumerable<int> GetTable(String str)
        {
            var n = str.Length;
            var borders = new int[n];

            borders[0] = -1;

            for (var i = 0; i < n - 1; i++)
            {
                var prevPatternIdx = borders[i];

                var patternIdx = prevPatternIdx + 1;
                var charIdx = i + 1;

                while (prevPatternIdx > -1 && str[charIdx] != str[patternIdx])
                {
                    prevPatternIdx = borders[prevPatternIdx];
                    patternIdx = prevPatternIdx + 1;
                }

                borders[charIdx] = str[charIdx] == str[patternIdx] ? patternIdx : -1;
            }

            return borders;
        }


        public override IEnumerable<int> SearchSubstring(String pattern, String sample)
        {
            String str = JoinStrings(pattern, sample);

            var borders = GetTable(str).ToArray();
            int lengthPattern = pattern.Length;

            return GetRightBorders(borders, lengthPattern);
        }
    }
}

