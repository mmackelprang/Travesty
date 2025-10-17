# Travesty Generator

A modern C# implementation of the classic "Travesty Generator" described in the Byte Magazine article "A Travesty Generator for Micros" by Hugh Kenner and Joseph O'Rourke (November 1984).

## About

The Travesty Generator uses Markov chains (n-grams of characters) to analyze an input text and generate new text with the same statistical character sequence patterns. The result mimics the "texture" and style of the sample text, creating humorous or parodic imitations without true understanding of the content.

### Key Concepts

* **Analyzes sample text**: Reads and processes the input to build a statistical model
* **N-gram sequences**: Records the distribution of n-length character sequences
* **Probabilistic generation**: Generates new text by randomly selecting the next character based on observed patterns
* **Statistical mimicry**: Produces "travesties"—texts that are statistically similar but not meaningful replications of the original

## Features

* Command-line interface for easy automation
* Input from file or direct text
* Configurable n-gram order (1-10) for different levels of coherence
* Adjustable output length
* Output to console or file
* Sample text files included for experimentation

## Requirements

* .NET 8.0 or later

## Building the Project

```bash
cd TravestyGenerator
dotnet build
```

## Usage

### Basic Syntax

```bash
dotnet run -- [options]
```

Or if you've built the project:

```bash
./TravestyGenerator [options]
```

### Command-Line Options

| Option | Description | Default |
|--------|-------------|---------|
| `-i, --input <file>` | Path to input text file | - |
| `-t, --text <text>` | Input text directly from command line | - |
| `-o, --output <file>` | Path to output file (if not specified, writes to console) | console |
| `-l, --length <number>` | Length of generated output in characters | 500 |
| `-n, --order <number>` | Order level (n-gram size) for analysis (1-10) | 3 |
| `-h, --help` | Display help information | - |

### Examples

#### Generate from a file to console

```bash
dotnet run -- -i ../SampleTexts/shakespeare.txt -l 1000
```

#### Generate with custom order level

```bash
dotnet run -- -i ../SampleTexts/moby-dick.txt -l 800 -n 5
```

#### Generate from direct text input

```bash
dotnet run -- -t "The quick brown fox jumps over the lazy dog" -l 200
```

#### Generate and save to file

```bash
dotnet run -- -i ../SampleTexts/genesis.txt -o output.txt -l 2000 -n 4
```

#### Using the built executable

```bash
cd TravestyGenerator/bin/Debug/net8.0
./TravestyGenerator -i ../../../../SampleTexts/constitution.txt -l 500
```

## Understanding the Order Parameter

The order parameter (`-n` or `--order`) controls the n-gram size used for analysis:

* **Order 1**: Very random, character-by-character generation. Output will be chaotic.
* **Order 2-3**: Balanced randomness. Creates somewhat recognizable patterns but still creative.
* **Order 4-6**: More coherent. Output resembles the source style more closely.
* **Order 7+**: Very coherent. Output may contain longer verbatim sequences from source.

**Recommendation**: Start with order 3 (default) and experiment from there.

## Sample Text Files

The repository includes several sample texts in the `SampleTexts` directory:

* **shakespeare.txt**: Hamlet's "To be or not to be" soliloquy
* **genesis.txt**: The Creation story from Genesis
* **moby-dick.txt**: Opening paragraphs of Moby-Dick
* **constitution.txt**: Preamble and Article I of the US Constitution
* **pangrams.txt**: Various pangrams and programming quotes

Feel free to experiment with your own text files!

## How It Works

1. **Analysis Phase**: The program reads the input text and builds a dictionary of n-grams (character sequences of length n) and their possible following characters.

2. **Generation Phase**: Starting with a random n-gram from the source text, the program:
   - Looks up possible next characters
   - Randomly selects one based on the frequency distribution
   - Appends it to the output
   - Slides the window forward by one character
   - Repeats until the desired length is reached

3. **Statistical Fidelity**: The output maintains the same statistical character sequence patterns as the input, creating text that "feels" similar but is not a direct copy.

## Examples of Generated Text

### Shakespeare (order 3)
```
To be wish'd. To sleep, perchance and thoulsuch an undiscove wish'oppresolu_ts makes, 
That takes when's conquers pake coward ther pati_er we know nottent mthe dread of times...
```

### Moby-Dick (order 4)
```
Call me Ishmael. Some years ago—never mind involuntarily the ocean with a philosophical 
flourish Cato throws himself upon his sword; I quietly take to the ship...
```

## License

See LICENSE file for details.

## Original Article

Based on "A Travesty Generator for Micros" by Hugh Kenner and Joseph O'Rourke, published in Byte Magazine, November 1984.
