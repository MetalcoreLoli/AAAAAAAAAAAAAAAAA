using System.Runtime.CompilerServices;

namespace Labs.Lib;

public static partial class StringExtentions
{
    public interface IMethodOfSearchInString
    {
        IEnumerable<int> SearchSubstring(string pattern, string sample);
    }
    
    public abstract class MethodOfSearchInString : IMethodOfSearchInString
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected String JoinRows(String s1, String s2) => s1 + "$" + s2;


        public abstract IEnumerable<int> SearchSubstring(string pattern, string sample);
    }

    public static class TransmutationOfSearchResults 
    {
        public static IEnumerable<int> FromBlocksToBorder(int[] blocks, int[] borders)
        {
            borders[0] = 0;
            for (int i = 1; i < blocks.Length; i++)
            {

            }
            return null;
        }

        public static IEnumerable<int> FromBordersToBlocks(int[] blocks, int[] borders)
        {
            return null;
        }

    }
}

