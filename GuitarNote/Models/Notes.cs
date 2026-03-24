namespace GuitarNote.Models;

public static class NotesExtension
{
    private static readonly string[] Names =
        ["C","C#","D","D#","E","F","F#","G","G#","A","A#","B"];

    public static string GetName(this Note note)
        => Names[(int)note];

    public static Note Transpose(this Note note, int semitones)
        => (Note)(((int)note + semitones + 12) % 12);
}

public enum Note
{
    C = 0, 
    BSharp = 0,       
    CSharp = 1, 
    Db = 1,         
    D = 2,
    DSharp = 3, 
    Eb = 3,         
    E = 4, 
    Fb = 4,            
    F = 5, 
    ESharp = 5,       
    FSharp = 6, 
    Gb = 6,           
    G = 7,
    GSharp = 8, 
    Ab = 8,          
    A = 9,
    ASharp = 10, 
    Bb = 10,         
    B = 11, 
    Cb = 11         
}