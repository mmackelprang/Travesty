using System.Text;

namespace TravestyGenerator;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var options = ParseArguments(args);
            
            if (options.ShowHelp)
            {
                ShowHelp();
                return;
            }

            // Get input text
            string inputText = GetInputText(options);
            
            if (string.IsNullOrWhiteSpace(inputText))
            {
                Console.WriteLine("Error: Input text is empty.");
                return;
            }

            // Generate travesty
            var generator = new TravestyTextGenerator(options.OrderLevel);
            generator.AnalyzeText(inputText);
            string output = generator.Generate(options.OutputLength);

            // Output result
            if (!string.IsNullOrEmpty(options.OutputFile))
            {
                File.WriteAllText(options.OutputFile, output);
                Console.WriteLine($"Output written to: {options.OutputFile}");
            }
            else
            {
                Console.WriteLine(output);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    static Options ParseArguments(string[] args)
    {
        var options = new Options();
        
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-h":
                case "--help":
                    options.ShowHelp = true;
                    break;
                case "-i":
                case "--input":
                    if (i + 1 < args.Length)
                    {
                        options.InputFile = args[++i];
                    }
                    break;
                case "-t":
                case "--text":
                    if (i + 1 < args.Length)
                    {
                        options.InputText = args[++i];
                    }
                    break;
                case "-o":
                case "--output":
                    if (i + 1 < args.Length)
                    {
                        options.OutputFile = args[++i];
                    }
                    break;
                case "-l":
                case "--length":
                    if (i + 1 < args.Length && int.TryParse(args[++i], out int length))
                    {
                        options.OutputLength = length;
                    }
                    break;
                case "-n":
                case "--order":
                    if (i + 1 < args.Length && int.TryParse(args[++i], out int order))
                    {
                        options.OrderLevel = order;
                    }
                    break;
            }
        }
        
        return options;
    }

    static string GetInputText(Options options)
    {
        if (!string.IsNullOrEmpty(options.InputText))
        {
            return options.InputText;
        }
        
        if (!string.IsNullOrEmpty(options.InputFile))
        {
            if (!File.Exists(options.InputFile))
            {
                throw new FileNotFoundException($"Input file not found: {options.InputFile}");
            }
            return File.ReadAllText(options.InputFile);
        }
        
        throw new ArgumentException("No input provided. Use -i for file or -t for text.");
    }

    static void ShowHelp()
    {
        Console.WriteLine(@"Travesty Generator - A Markov Chain Text Generator
Based on the 1984 Byte Magazine article by Hugh Kenner and Joseph O'Rourke

Usage:
  TravestyGenerator [options]

Options:
  -i, --input <file>      Input file path
  -t, --text <text>       Input text directly
  -o, --output <file>     Output file path (if not specified, writes to console)
  -l, --length <number>   Length of output text in characters (default: 500)
  -n, --order <number>    Order level (n-gram size) for analysis (default: 3, range: 1-10)
  -h, --help              Show this help message

Examples:
  TravestyGenerator -i sample.txt -l 1000
  TravestyGenerator -i shakespeare.txt -o output.txt -l 2000 -n 5
  TravestyGenerator -t ""The quick brown fox"" -l 200
");
    }
}

class Options
{
    public string? InputFile { get; set; }
    public string? InputText { get; set; }
    public string? OutputFile { get; set; }
    public int OutputLength { get; set; } = 500;
    public int OrderLevel { get; set; } = 3;
    public bool ShowHelp { get; set; }
}
