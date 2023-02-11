using System.Runtime.CompilerServices;

namespace Labs.Lib;

public static partial class StringExtensions
{
    public class BlocksStringSearchMethod : MethodOfSearchInString 
    {
        private BlocksStringSearchMethod() {}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsIndexInsideOfString(string str, int idx) => str.Length > idx;

        private int Cmp(string str, int patternPosition, int substringPosition) 
        {
            int n = str.Length;

            int eqLen = 0;
            while (patternPosition < n && substringPosition < n 
                    && str[patternPosition++] == str[substringPosition++]) 
                ++eqLen;

            return eqLen;
        }

        private int LengthOfMatchedSubstring(string str, int startOfSubstring) => 
            Cmp(str, 0, startOfSubstring);

        ///<summary>
        /// Метод возвращает таблицу блоков.
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
                if (i >= rightIdx) 
                {
                    blocks[i] = LengthOfMatchedSubstring(str, i);
                    var isSubstringExist = blocks[i] > 0;

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

        public enum IndexType
        {
            Human, Programmer
        }

        public IndexType IndexForma { get; }


        public BlocksStringSearchMethod(IndexType indexForma)
        {
            IndexForma = indexForma;
        }


        public override IEnumerable<int> SearchSubstring(string pattern, string text)
        {
            var str = JoinRows(pattern, text);
            var blocks = GetTable(str).ToArray();

            var result = GetStartIndexiesOfSubstrings(blocks, pattern.Length).ToList();

            return IndexForma switch       
            {
                IndexType.Programmer => result.Select(i => i - 1),
                IndexType.Human => result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

