using System;
using System.Collections.Generic;
using System.Text;
using GuitarNote.Services;

namespace GuitarNote.Models;

public class ChordLine
{
    public string OriginalText { get; }
    public string Text { get; private set; }
    public List<(int position, Chord chord)> Chords { get; private set; }

    public ChordLine(string input)
    {
        OriginalText = input;
        (Text, Chords) = Parse(input);
    }

    private (string, List<(int, Chord)>) Parse(string input)
    {
        var sb = new StringBuilder();
        var chords = new List<(int, Chord)>();
        var index = 0;

        while (index < input.Length)
        {
            if (input[index] == '[')
            {
                (index, var chord) = ExtractChord(input, index);
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

    private (int index, Chord chord) ExtractChord(string input, int startIndex)
    {
        var end = input.IndexOf(']', startIndex);
        if (end == -1)
            throw new InvalidChordLineException("Missing closing bracket for chord.");
                
        var chord = input.AsSpan(startIndex + 1, end - startIndex - 1).Trim();
        return chord.IsEmpty 
            ? throw new InvalidChordLineException("Chord cannot be empty.") 
            : (end + 1, ChordParser.ParseChord(chord.ToString()));
    }
}

public class InvalidChordLineException : Exception
{
    public InvalidChordLineException(string message) : base(message) { }
}