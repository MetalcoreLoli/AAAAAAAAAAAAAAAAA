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

        public static IEnumerable<int> FromBordersToBlocks(int[] blocks, int[] borders)
        {
            var patternLenght = Math.Abs(borders.First() - blocks.First());
            return borders.Select(border => border - patternLenght);
        }

    }
}
