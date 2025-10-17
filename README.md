# Summary of the Article:
The Byte Magazine article "A Travesty Generator for Micros" by Hugh Kenner and Joseph O'Rourke (1984) describes a program that analyzes an input text to generate new text with the same n-length character sequence statistics. Using Markov chains (n-grams of characters), the generator mimics the "texture" and style of the sample, creating humorous or parodic imitations without true understanding. The original code was written in Pascal and included in the article so readers could experiment with different input texts.
## Key Concepts:
* Reads and analyzes a sample text.
* Records distribution of n-length sequences (n-grams).
* Generates new text by probabilistically selecting the next character, matching the observed patterns.
* Produces "travesties"—texts statistically similar but not meaningful replications of the original content.

This is a modern C# Implementation: A Full-Featured Travesty (Markov) Text Generator which reads a text file, processes n-gram statistics, and generates a new, statistically similar output. Features include configurable n-gram length, adjustable output length, and file, GUI or console output.
