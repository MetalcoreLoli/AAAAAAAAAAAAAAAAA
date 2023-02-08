namespace Labs.Lib;

public static partial class StringExtensions
{
    public class BlocksStringSearchMethod : MethodOfSearchInString 
    {
        private BlocksStringSearchMethod() {}

        private int Cmp(String str, int pos1, int pos2) 
        {
            int n = str.Length;

            int eqLen = 0;
            while (pos1 < n && pos2 < n && str[pos1++] == str[pos2++]) 
                ++eqLen;

            return eqLen;
        }

        private IEnumerable<int> Blocks(string str) {
            var blocks = new int[str.Length];
            int n = str.Length;
            int left = 0, right = 0;

            for (int i = 1; i < n; i++) 
            {
                blocks[i] = 0;
                if (i >= right) 
                {
                    blocks[i] = Cmp(str, 0, i);

                    if (blocks[i] > 0) {
                        right = i + blocks[i];
                        left = i;
                    }
                } 
                else 
                {
                    int k = i - left;

                    if (blocks[k] < right - i)
                        blocks[i] = blocks[k];
                    else 
                    {
                        blocks[i] = right - i;
                        left = i;
                        int complement = Cmp(str, right - i, right);
                        if (complement > 0)
                        {
                            blocks[i] += complement;
                            right = i + blocks[i];
                        }
                    }
                }
            }
            return blocks;
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

        public IEnumerable<int> GetBlocks(int[] data, int patternLen)
        {
            for (int i = 0; i < data.Length; i++) {
                if (data[i] == patternLen)
                    yield return (i - patternLen);
            }
        }

        public override IEnumerable<int> SearchSubstring(string pattern, string sample)
        {
            var str = JoinRows(pattern, sample);
            var blocks = Blocks(str).ToArray();

            var result = GetBlocks(blocks, pattern.Length).ToList();

            return IndexForma switch       
            {
                IndexType.Programmer => result.Select(i => i - 1),
                IndexType.Human => result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

