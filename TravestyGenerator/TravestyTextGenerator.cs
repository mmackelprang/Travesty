using System.Text;

namespace TravestyGenerator;

/// <summary>
/// Travesty Text Generator using Markov chains (n-grams)
/// Based on the 1984 Byte Magazine article by Hugh Kenner and Joseph O'Rourke
/// </summary>
public class TravestyTextGenerator
{
    private readonly int _order;
    private readonly Dictionary<string, List<char>> _chains;
    private readonly Random _random;
    private string _sourceText = string.Empty;
    private List<string> _keys = new List<string>();

    /// <summary>
    /// Initializes a new instance of the TravestyTextGenerator
    /// </summary>
    /// <param name="order">The order level (n-gram size) for analysis. Higher values produce more coherent but less creative output.</param>
    public TravestyTextGenerator(int order = 3)
    {
        if (order < 1 || order > 10)
        {
            throw new ArgumentException("Order must be between 1 and 10", nameof(order));
        }
        
        _order = order;
        _chains = new Dictionary<string, List<char>>();
        _random = new Random();
    }

    /// <summary>
    /// Analyzes the input text and builds the Markov chain model
    /// </summary>
    /// <param name="text">The source text to analyze</param>
    public void AnalyzeText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Input text cannot be empty", nameof(text));
        }

        if (text.Length < _order + 1)
        {
            throw new ArgumentException($"Input text must be at least {_order + 1} characters long", nameof(text));
        }

        _sourceText = text;
        _chains.Clear();
        _keys.Clear();

        // Build the Markov chain by analyzing n-grams
        for (int i = 0; i <= text.Length - _order - 1; i++)
        {
            string key = text.Substring(i, _order);
            char nextChar = text[i + _order];

            if (!_chains.ContainsKey(key))
            {
                _chains[key] = new List<char>();
                _keys.Add(key);
            }

            _chains[key].Add(nextChar);
        }
    }

    /// <summary>
    /// Generates new text based on the analyzed Markov chain
    /// </summary>
    /// <param name="length">The desired length of the generated text</param>
    /// <returns>The generated text</returns>
    public string Generate(int length)
    {
        if (_chains.Count == 0)
        {
            throw new InvalidOperationException("No text has been analyzed. Call AnalyzeText first.");
        }

        if (length < 1)
        {
            throw new ArgumentException("Length must be positive", nameof(length));
        }

        var output = new StringBuilder();

        // Start with a random key from the source text
        string currentKey = GetRandomKey();
        output.Append(currentKey);

        // Generate characters until we reach the desired length
        while (output.Length < length)
        {
            if (!_chains.ContainsKey(currentKey))
            {
                // If we hit a dead end, pick a new random starting point
                currentKey = GetRandomKey();
                output.Append(' '); // Add space as separator
                output.Append(currentKey);
                continue;
            }

            // Get possible next characters for the current key
            List<char> possibleChars = _chains[currentKey];
            
            // Randomly select one based on the distribution in the source text
            char nextChar = possibleChars[_random.Next(possibleChars.Count)];
            output.Append(nextChar);

            // Update the key by sliding the window
            currentKey = currentKey.Substring(1) + nextChar;
        }

        return output.ToString();
    }

    /// <summary>
    /// Gets a random key from the analyzed chains
    /// </summary>
    private string GetRandomKey()
    {
        if (_keys.Count == 0)
        {
            throw new InvalidOperationException("No chains available");
        }

        int index = _random.Next(_keys.Count);
        return _keys[index];
    }
}
