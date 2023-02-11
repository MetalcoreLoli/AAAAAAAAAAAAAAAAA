// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Labs.Lib;

[Serializable]
public class Data
{
    public string Pattern { get; set; }
    public string Text { get; set; }

    public override string ToString() => $"{Pattern}${Text}";
}

internal static class Program
{
    private static string ArrayToString(this IEnumerable<int> source, char sep) =>
        source.Aggregate("", (acc, i) => acc + i + sep);
   
    private static async Task Main(string[] args)
    {
        if (args.Count() < 1) throw new Exception("A file path not provided");
        var filePath = args[0];

        using var fileStream =  File.OpenRead(filePath);
        var testDatas = await JsonSerializer.DeserializeAsync<Data[]>(fileStream);
        foreach (var data in testDatas!)
        {
            System.Console.WriteLine(data);
            System.Console.WriteLine($"borders: {data.Pattern.GetSubstrings(data.Text, StringExtensions.BordersMethod).ArrayToString(' ')}");
            System.Console.WriteLine($"borders: {data.Text.GetAllBorders().Aggregate("", (acc, i ) => acc + i + " ")}");
            System.Console.WriteLine($"blocks: {data.Pattern.GetSubstrings(data.Text, StringExtensions.BlocksMethod).ArrayToString(' ')}");
            System.Console.WriteLine();
        }

    }
}
