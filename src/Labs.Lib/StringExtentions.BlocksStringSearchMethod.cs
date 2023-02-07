namespace Labs.Lib;

public static partial class StringExtentions
{
    public class BlocksStringSearchMethod : MethodOfSearchInString {
        public enum IndexType
        {
            Human, Programmer
        }

        public IndexType IndexForma { get; }

        private BlocksStringSearchMethod() {}

        public BlocksStringSearchMethod(IndexType indexForma)
        {
            IndexForma = indexForma;
        }

        private int[] block;

        private int cmp(String str, int pos1, int pos2) {
            int n = str.Length, dist;

            if (pos1 >= n || pos2 >= n) return 0;

            dist = (n - pos1 < n - pos2) ? (n - pos1) : (n - pos2);
            int j = 0;
            while (j < dist && str[pos1 + j] == str[pos2 + j]) j++;

            return j;
        }

        private int cmp2(String str, int pos1, int pos2) {
            int n = str.Length, dist;

            int eqLen = 0;
            while (pos1 < n && pos2 < n && str[pos1++] == str[pos2++]) ++eqLen;

            return eqLen;
        }

        private void blocArrays(String str) {
            int n = str.Length;
            int left = 0;
            int right = 0;
            block[0] = 0;
            for (int i = 1; i < n; i++) {
                block[i] = 0;
                if (i >= right) {
                    block[i] = cmp2(str, 0, i);

                    if (block[i] > 0) {
                        right = i + block[i];
                        left = i;
                    }
                } else {
                    int k = i - left;

                    if (block[k] < right - i)
                        block[i] = block[k];
                    else {
                        block[i] = right - i;
                        left = i;
                        int complement = cmp2(str, right - i, right);
                        if (complement > 0){
                            block[i] = block[i] + complement;
                            right = i + block[i];
                        }
                    }
                }
            }
        }

        public override IEnumerable<int> SearchSubstring(string pattern, string sample)
        {
            String str = JoinRows(pattern, sample);

            block = new int[str.Length];
            blocArrays(str);

            int lengthPattern = pattern.Length;
            var result = new List<int>();

            for (int i = 0; i < block.Length; i++) {
                if (block[i] == lengthPattern)
                    result.Add(i - lengthPattern);
            }

            return IndexForma switch       
            {
                IndexType.Programmer => result.Select(i => i - 1),
                IndexType.Human => result,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

