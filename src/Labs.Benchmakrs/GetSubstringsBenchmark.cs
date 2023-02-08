using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Labs.Lib;

namespace Labs.Benchmarks;

[MemoryDiagnoser, Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical), RankColumn]
public class GetSubstringBenchmark
{
    [ParamsSource(nameof(Patterns))]
    public string Pattern { get; set; }
    
    [ParamsSource(nameof(Texts))]
    public string Text { get; set; }


    [Benchmark]
    public void GetSubstringsByBorders()
    {
        Pattern.GetSubstrings(Text, StringExtensions.BordersMethod);
    }
    
    [Benchmark]
    public void GetSubstringsByBlocks()
    {
        Pattern.GetSubstrings(Text, StringExtensions.BordersMethod);
    }

    public IEnumerable<string> Patterns => new List<string>() 
    {
        "ab",
        "aba",
        "abaab",
        "ab",
        "aba",
        "abaab",
        "abc",
        "in",
        "tui",
        "Vivat"
    };

    public IEnumerable<string> Texts => new List<string> ()
    {
        "abbabab",
        "abaabaab",
        "abaababaabaab",
        "abaababaabaababaabaab",
        "abcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcb",
        "abcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcb",
        "abcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcbabcabacbcacbacabcbacbcacbabcabacbcacbacabcbabcabacabcbacbcacbabcabacabcbabcabacbcacbabcabacabcbacbcacbacabcb",
        @"
            Pater noster, qui es in caelis,
            sanctificētur nomen tuum.
            Adveniat regnum tuum.
            Fiat voluntas tua,
            sicut in caelo, et in terrā.
            Panem nostrum quotidiānum da nobis hodie,
            et dimitte nobis debĭta nostra,
            sicut et nos dimittĭmus debitorĭbus nostris.
            Et ne nos indūcas in tentatiōnem,
            sed libĕra nos a malo.
            Amen.
        ",
        @"
            Ave, Maria, gratiā plena;
            Domĭnus tecum:
            benedicta tu in mulierĭbus,
            et benedictus fructus ventris tui, Iesus.
            Sancta Maria, Mater Dei,
            ora pro nobis peccatorĭbus,
            nunc et in horā mortis nostrae.
            Amen.
        ",
        @"
            1. Gaudeāmus igĭtur,
            Juvĕnes dum sumus!
            Post jucundam juventūtem,
            Post molestam senectūtem
            Nos habēbit humus!
            
            2. Ubi sunt, qui ante nos
             In mundo fuēre?
             Vadĭte ad supĕros,
             Transĭte ad infĕros,
             Hos si vis vidēre!

            3. Vita nostra brevis est,
            Brevi finiētur.
            Venit mors velocĭter,
            Rapit nos atrocĭter,
            Nemini parcētur!
            
            4. Vivat Academia!
            Vivant professōres!
            Vivat membrum quodlĭbet!
            Vivant membra quaelĭbet!
            Semper sint in flore!

            5. Vivant omnes virgĭnes
            Gracĭles, formōsae!
            Vivant et muliĕres
            Tenĕrae, amabĭles,
            Bonae, laboriōsae!

            6. Vivat et Respublĭca
            Et qui illam regunt! 
            Vivat nostra civĭtas,
            Maecenātum carĭtas,
            Qui nos hic protĕgunt!

            7. Pereat tristitia,
            Pereant dolōres! 
            Pereat diabŏlus,
            Quivis antiburschius
            Atque irrisōres!

            8. Quis confluxus hodie
            Academicōrum?
            E longinquo convenērunt,
            Protĭnusque successērunt
            In commūne forum.

            9. Vivat nostra sociĕtas,
            Vivant studiōsi!
            Crescat unā verĭtas,
            Floreat fraternĭtas,
            Patriae prosperĭtas!

            10. Alma Mater floreat,
            Quae nos educāvit;
            Caros et commilitōnes,
            Dissĭtas in regiōnes
            Sparsos, congregāvit
        "
    };
    
}
