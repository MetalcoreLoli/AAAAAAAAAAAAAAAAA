using System.Runtime.CompilerServices;

namespace Labs.Lib;

public static partial class StringExtensions
{
    public class BlocksStringSearchMethod : MethodOfSearchInString 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsIndexInsideOfString(string str, int idx) => str.Length > idx;

        ///<summary>
        /// Метод сравнивает подстроки
        ///</summary>
        ///<param name="str"> Текст, в котором сравниваются подстроки </param>
        ///<param name="s1Position"> Начало первой подстроки </param>
        ///<param name="s2Position"> Начало второй подстроки </param>
        ///<returns> Возвращает длину подстроки, если подстроки не равны, то вернется 0 </returns>
        private int Cmp(string str, int s1Position, int s2Position) 
        {
            int n = str.Length;

            int eqLen = 0;
            while (s1Position < n && s2Position < n 
                    && str[s1Position++] == str[s2Position++]) 
                ++eqLen;

            return eqLen;
        }

        ///<summary>
        /// Длина следующей совпавшей с паттерном подстроки
        ///</summary>
        ///<param name="str"> паттерн + 'не_алфавитный_символ' + текст</param>
        ///<param name="startOfSubstring"> индекс в <paramref name="str"/> откуда начинать сравнение. <param>
        ///<returns>
        ///  Вернет длину подстроки или 0, если подстрока не будет совпадать с паттерном.
        ///</returns>
        private int LengthOfNextMatchedSubstring(string str, int startOfSubstring) => 
            Cmp(str, 0, startOfSubstring);

        ///<summary>
        /// Метод возвращает таблицу блоков. 
        /// Блок - это длина подстроки
        ///</summary>
        ///<param name="str"> 
        ///     Данный параметр должен соответствовать следующей форме:
        ///     шаблон + 'не_алфавитный_символ' + строка
        ///</param>
        public override IEnumerable<int> GetTable(string str) {
            var blocks = new int[str.Length];
            int leftIdx = 0, rightIdx = 0;

            for (int i = 1; i < str.Length; i++) 
            {
                blocks[i] = 0;
                // проверяет дошли ли до конца текущей подстроки
                if (i >= rightIdx) 
                {
                    // ищем длину следующей подстроки
                    blocks[i] = LengthOfNextMatchedSubstring(str, i);
                    var isSubstringExist = blocks[i] > 0;

                    // если подстрока совпадающая с паттерном существует, то
                    // смещаем начало текущей подстроки(leftIdx) к началу найденной подстроки(i)
                    // также смещаем конец текущей подстроки(rightIdx) в конец найденной подстроки(i + blocks[i])
                    if (isSubstringExist) 
                    {
                        rightIdx = i + blocks[i];
                        leftIdx = i;
                    }
                } 
                else 
                {
                    int k = i - leftIdx;

                    if (blocks[k] < rightIdx - i)
                        blocks[i] = blocks[k];
                    else 
                    {
                        blocks[i] = rightIdx - i;
                        leftIdx = i;
                        int complement = Cmp(str, rightIdx - i, rightIdx);
                        if (complement > 0)
                        {
                            blocks[i] += complement;
                            rightIdx = i + blocks[i];
                        }
                    }
                }
            }
            return blocks;
        }

        ///<summary>
        /// Метод преобразует <paramref name="blocks"/> в индексы начал подстрок
        ///</summary>
        ///<param name="blocks"> Таблица блоков </param>
        ///<param name="patternLen"> Длина шаблона </param>
        private IEnumerable<int> GetStartIndexiesOfSubstrings(int[] blocks, int patternLen)
        {
            for (int i = 0; i < blocks.Length; i++) {
                if (blocks[i] == patternLen)
                    yield return (i - patternLen);
            }
        }



        public override IEnumerable<int> SearchSubstring(string pattern, string text)
        {
            var str = JoinStrings(pattern, text);
            var blocks = GetTable(str).ToArray();

            var result = GetStartIndexiesOfSubstrings(blocks, pattern.Length).ToList();

            return result.Select(i => i - 1);
        }
    }
}

