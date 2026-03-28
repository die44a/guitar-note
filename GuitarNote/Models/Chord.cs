using System;
using System.Linq;

namespace GuitarNote.Models;

public class Chord
{
    public Note Note { get; }
    public Note? SlashBass {get; }
    public string Type { get; }
    public int[] Intervals { get; }

    public Chord(Note note, string type, Note? slashBass = Note.None)
    {
        Note = note;
        Type = type;
        Intervals = GetIntervals(type);
        SlashBass = slashBass;
    }
    
    public Chord Transpose(int semitones)
    {
        var newRoot = Note.Transpose(semitones);
        var newBass = SlashBass?.Transpose(semitones);
        return new Chord(newRoot, Type, newBass);
    }
    
    public Note[] GetNotes()
        => Intervals.Select(i => Note.Transpose(i)).ToArray();
    
    private static int[] GetIntervals(string type) => type switch
    {
        "maj" or "M" => [0, 4, 7],
        "maj7" or "M7" => [0, 4, 7, 11],
        "6" or "maj6" => [0, 4, 7, 9],
        "maj9" => [0, 4, 7, 11, 14],
        "m" or "min" => [0, 3, 7],
        "m7" or "min7" => [0, 3, 7, 10],
        "m6" or "min6" => [0, 3, 7, 9],
        "m9" or "min9" => [0, 3, 7, 10, 14],
        "dim" => [0, 3, 6],
        "dim7" => [0, 3, 6, 9],
        "m7b5" => [0, 3, 6, 10],
        "7" or "dom7" => [0, 4, 7, 10],
        "9" or "dom9" => [0, 4, 7, 10, 14],
        "11" or "dom11" => [0, 4, 7, 10, 14, 17],
        "13" or  "dom13" => [0, 4, 7, 10, 14, 18],
        "aug" or "+" => [0, 4, 8],
        "aug7" or "+7" => [0, 4, 8, 11],
        _ => []
    };

    public override int GetHashCode()
    {
        return HashCode.Combine(Note, Type, SlashBass);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Chord other)
            return false;
        
        return Note == other.Note 
               && Type == other.Type 
               && SlashBass == other.SlashBass;
    }
    
    public static bool operator ==(Chord left, Chord right) => Equals(left, right);
    public static bool operator !=(Chord left, Chord right) => !Equals(left, right);
}
