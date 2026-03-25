using System;
using GuitarNote.Models;
using static System.Enum;

namespace GuitarNote.Services;

public static class ChordParser
{
    public static Chord ParseChord(string chordString)
    {
        if (string.IsNullOrEmpty(chordString))
            throw new InvalidChordException("chord string was null or empty");
        
        var note = ParseNote(chordString);

        var noteLength = note.GetName().Length;
        
        if (chordString.Length == noteLength)
            return new Chord(note, "maj");
        
        var span = chordString.AsSpan(noteLength);
        var type = ParseType(span);
        var bass = ParseBass(span);
        
        return new Chord(note, type.ToString(), bass);
    }

    private static Note ParseNote(ReadOnlySpan<char> chordSpan)
    {
        if (chordSpan.IsEmpty)
            throw new InvalidChordException("chord string was null or empty");
        
        if (!TryParse<Note>(chordSpan[..1], ignoreCase: true,out var note))
            throw new InvalidChordException($"note cannot be found");

        if (chordSpan.Length == 1)
            return note;
        
        if (chordSpan[1] == '#')
            note = (Note)(((int)note + 1 + 12) % 12);
        if (chordSpan[1] == 'b')
            note = (Note)(((int)note - 1 + 12) % 12);

        return note;
    }

    private static ReadOnlySpan<char> ParseType(ReadOnlySpan<char> chordSpan)
    {
        if (chordSpan.IsEmpty)
            throw new InvalidChordException("chord string was null or empty");
        
        var enumerator = chordSpan.Split('/');
        
        enumerator.MoveNext();
        var type = chordSpan.Slice(enumerator.Current.Start.Value,
            enumerator.Current.End.Value - enumerator.Current.Start.Value);
        
        return type.Length == 0
            ? "maj"
            : type;;
    }

    private static Note ParseBass(ReadOnlySpan<char> chordSpan)
    {
        if (chordSpan.IsEmpty)
            throw new InvalidChordException("chord string was null or empty");

        var enumerator = chordSpan.Split('/');
        enumerator.MoveNext();
        
        if (!enumerator.MoveNext())
            return Note.None;
        
        var bass = chordSpan.Slice(
            enumerator.Current.Start.Value,
            enumerator.Current.End.Value - enumerator.Current.Start.Value
        );
        
        return ParseNote(bass);
    }
}

public class InvalidChordException : Exception
{
    public InvalidChordException(string message) : base(message) { }
}