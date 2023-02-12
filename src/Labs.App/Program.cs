// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Labs.Lib;

[Serializable]
public class Data
{
    public string Pattern { get; set; }
    public string Text { get; set; }
    public int TaskNumber { get; set; }

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

        System.Console.WriteLine("Enter task number: ");
        if (!int.TryParse(Console.ReadLine(), out var  selectedTaskNumber))
            throw new Exception("asdasdasd adas dasd ada da a dasdas das dasd asda");
            
        switch (selectedTaskNumber)
        {
            case 2: 
                {
                    foreach (var data in testDatas!)
                    {
                        System.Console.WriteLine(data.Text);
                        System.Console.WriteLine($"results: {data.Text.GetAllBorders().Aggregate("", (acc, i) => acc + i + " ")}");
                        System.Console.WriteLine();
                    }
                }
                break;
            case 3: 
                {
                    foreach (var data in testDatas!.Where(d => d.Pattern.Length > 0))
                    {
                        System.Console.WriteLine(data);
                        System.Console.WriteLine($"borders: {data.Pattern.GetSubstrings(data.Text, StringExtensions.BordersMethod).ArrayToString(' ')}");
                        System.Console.WriteLine();
                    }
                }
                break;
            case 4: 

                {
                    foreach (var data in testDatas!.Where(d => d.Pattern.Length > 0))
                    {
                        System.Console.WriteLine(data);
                        System.Console.WriteLine($"blocks: {data.Pattern.GetSubstrings(data.Text, StringExtensions.BlocksMethod).ArrayToString(' ')}");
                        System.Console.WriteLine();
                    }
                }
                break;
            case 7: 
                {
                    foreach (var data in testDatas!.Where(d => d.Pattern.Length > 0))
                    {
                        try
                        {
                            System.Console.WriteLine(data);
                            var borders = StringExtensions.BordersMethod.GetTable(data.ToString());
                            var blocks = StringExtensions.BlocksMethod.GetTable(data.ToString());
                            System.Console.WriteLine($"borders: {borders.ArrayToString(' ')}");
                            System.Console.WriteLine($"blocks: {blocks.ArrayToString(' ')}");
                            System.Console.WriteLine($"from borders to blocks: {StringExtensions.Transmutation.FromBordersToBlocks(borders.ToArray(), data.ToString().Length).ArrayToString(' ')}");
                            System.Console.WriteLine($"from blocks to borders: {StringExtensions.Transmutation.FromBlocksToBorders(blocks.ToArray(), data.ToString().Length).ArrayToString(' ')}");
                            System.Console.WriteLine();
                            
                        }
                        catch (System.Exception)
                        {
                            continue;
                        }
                    }
                }
                break;
            default:
                throw new Exception("asdasdasd adas dasd ada da a dasdas das dasd asda");
        }
    }
}
