using System;
using GuitarNote.Models;
using static System.Enum;

namespace GuitarNote.Services;

public class ChordParser
{
    public static Chord ParseChord(string chordString)
    {
        var note = ParseNote(chordString);

        var noteLength = note.GetName().Length;
        
        if (chordString.Length == noteLength)
            return new Chord(note, "maj");
        
        var span = chordString.AsSpan(noteLength);
        var type = ParseType(span);
        var bass = ParseBass(span);
        
        return new Chord(note, type.ToString(), bass);
    }

    private static Note ParseNote(string chordString)
    {
        if (chordString.Length >= 2 && TryParse<Note>(chordString.AsSpan(0, 2), out var note) 
            || chordString.Length >= 1 && TryParse<Note>(chordString.AsSpan(0, 1), out note));
        else
            throw new ArgumentException("chord string was null or empty");

        return note;
    }

    private static ReadOnlySpan<char> ParseType(ReadOnlySpan<char> chordSpan)
    {
        if (chordSpan.IsEmpty)
            throw new ArgumentException("chord string was null or empty");
        
        var enumerator = chordSpan.Split('/');
        
        enumerator.MoveNext();
        var type = chordSpan.Slice(enumerator.Current.Start.Value,
            enumerator.Current.End.Value - enumerator.Current.Start.Value);
        
        return type;
    }

    private static Note ParseBass(ReadOnlySpan<char> chordSpan)
    {
        if (chordSpan.IsEmpty)
            throw new ArgumentException("chord string was null or empty");

        var enumerator = chordSpan.Split('/');
        enumerator.MoveNext();
        
        if (!enumerator.MoveNext())
            return Note.None;
        
        var bass = chordSpan.Slice(
            enumerator.Current.Start.Value,
            enumerator.Current.End.Value - enumerator.Current.Start.Value
        );
        
        return !TryParse<Note>(bass, out var note) 
            ? throw new ArgumentException($"invalid root note: {bass.ToString()}") 
            : note;
    }
}