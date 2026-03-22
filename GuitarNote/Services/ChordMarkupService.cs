using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarNote.Services;

public class ChordMarkupService
{
}

public class ChordLine
{
    public string Text { get; private set; }
    public List<(int position, string chord)> Chords { get; private set; }

    public ChordLine(string input)
    {
        (Text, Chords) = Parse(input);
    }

    private (string, List<(int, string)>) Parse(string input)
    {
        var sb = new StringBuilder();
        var chords = new List<(int, string)>();
        int index = 0;

        while (index < input.Length)
        {
            if (input[index] == '[')
            {
                int end = input.IndexOf(']', index);
                if (end == -1)
                    throw new InvalidChordLineException("Missing closing bracket for chord.");
                
                var chord = input.Substring(index + 1, end - index - 1).Trim();
                if (string.IsNullOrEmpty(chord))
                    throw new InvalidChordLineException("Chord cannot be empty.");
                
                index = end + 1;
                
                chords.Add((sb.Length, chord));
                continue;
            }

            if (input[index] == ']')
                throw new InvalidChordLineException("Missing opening bracket for chord.");
            
            sb.Append(input[index]);
            index++;
        }

        return (sb.ToString(), chords); 
    }
}

public class InvalidChordLineException : Exception
{
    public InvalidChordLineException(string message) : base(message) { }
}