namespace SandlotWizards.CommandLineParser.Parsing;
public static class Tokenizer
{
    public static List<string> Tokenize(string input)
    {
        return input.Split(' ').ToList();
    }
}
