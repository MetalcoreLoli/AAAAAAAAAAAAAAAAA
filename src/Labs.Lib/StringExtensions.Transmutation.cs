namespace Labs.Lib;

public static partial class StringExtensions
{
    public static class Transmutation
    {
        public static IEnumerable<int> FromBlocksToBorders(int[] blocks, int[] borders)
        {
            var patternLenght = borders.First() - blocks.First();
            return blocks.Select(block => block + patternLenght);
        }

        public static IEnumerable<int> FromBordersToBlocks(int[] blocks, int[] borders)
        {
            var patternLenght = Math.Abs(borders.First() - blocks.First());
            return borders.Select(border => border - patternLenght);
        }

    }
}
