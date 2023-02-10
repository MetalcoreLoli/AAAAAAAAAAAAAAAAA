namespace Labs.Lib;

public static partial class StringExtensions
{
    public static class Transmutation
    {
        public static IEnumerable<int> FromBlocksToBorders(int[] blocks, int n)
        {
            var borders = new int[blocks.Length];
            borders[0] = 0;
            for(var i = 1; i < n; i++)
            {
                var bpRight = borders[i - 1];
                while (bpRight > 0 && blocks[i - bpRight] <= bpRight) 
                    bpRight = borders[bpRight - 1];
                borders[i] = (blocks[i - bpRight] > bpRight) ? bpRight + 1: 0;
            }
            return borders.Select(i => i - 1);
        }

        public static IEnumerable<int> FromBordersToBlocks(int[] brs, int n)
        {
            int ValGrow(int[] borders, int stringLength, int nVal, int i0) 
            {
                int nLeft = i0 - 1; 
                int nRight = stringLength; 
                int nL = nVal - i0;
                while (nLeft < nRight - 1)
                {
                    int nMid = (nLeft + nRight) >> 1;
                    if (nL + nMid == borders[nMid]) 
                        nLeft = nMid; 
                    else 
                        nRight = nMid;
                }
                return nLeft - i0 + 1;
            }

            // data normalization
            var borders = brs.Select(d => d + 1).ToArray();

            int leftIdx = 0, rightIdx = 0;
            var blocks = new int[borders.Length];
            for (var i = 1; i < n; i++)
            {
                blocks[i] = 0;
                if (i >= rightIdx)
                {
                    blocks[i] = ValGrow(borders, n, 1, i);
                    leftIdx = i;
                    rightIdx = leftIdx + blocks[i];
                }
                else 
                {
                    int j = i - leftIdx;
                    if (blocks[j] < rightIdx - i) 
                        blocks[i] = blocks[j];
                    else 
                    {
                        blocks[i] = rightIdx - i + ValGrow(borders, n, rightIdx - i + 1, rightIdx);
                        leftIdx = i; 
                        rightIdx = leftIdx + blocks[i];
                    }
                }
            }

            return blocks;
        }
    }
}
