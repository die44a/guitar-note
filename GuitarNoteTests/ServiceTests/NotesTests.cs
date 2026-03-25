using GuitarNote.Models;

namespace GuitarNoteTests.ServiceTests;

[TestFixture]
public class NotesTests
{
    [TestCase(Note.C, "C")]
    [TestCase(Note.CSharp, "C#")]
    [TestCase(Note.D, "D")]
    [TestCase(Note.DSharp, "D#")]
    [TestCase(Note.E, "E")]
    [TestCase(Note.F, "F")]
    [TestCase(Note.FSharp, "F#")]
    [TestCase(Note.G, "G")]
    [TestCase(Note.GSharp, "G#")]
    [TestCase(Note.A, "A")]
    [TestCase(Note.ASharp, "A#")]
    [TestCase(Note.B, "B")]
    [TestCase(Note.Bb, "A#")]
    [TestCase(Note.Db, "C#")]
    [TestCase(Note.Eb, "D#")]
    [TestCase(Note.Gb, "F#")]
    public void CheckGetName(Note note, string expected)
    {
        var name = note.GetName();
        Assert.That(name, Is.EqualTo(expected));
    }

    [TestCase(Note.C, 1, Note.CSharp)]
    [TestCase(Note.C, 2, Note.D)]
    [TestCase(Note.C, 3, Note.DSharp)]
    [TestCase(Note.C, 4, Note.E)]
    [TestCase(Note.C, 5, Note.F)]
    [TestCase(Note.C, 6, Note.FSharp)]
    [TestCase(Note.C, 7, Note.G)]
    [TestCase(Note.C, 8, Note.GSharp)]
    [TestCase(Note.C, 9, Note.A)]
    [TestCase(Note.C, 10, Note.ASharp)]
    [TestCase(Note.C, 11, Note.B)]
    [TestCase(Note.C, 12, Note.C)] 

    [TestCase(Note.C, -1, Note.B)]
    [TestCase(Note.C, -2, Note.ASharp)]
    [TestCase(Note.C, -3, Note.A)]
    [TestCase(Note.C, -4, Note.GSharp)]
    [TestCase(Note.C, -5, Note.G)]
    [TestCase(Note.C, -6, Note.FSharp)]
    [TestCase(Note.C, -7, Note.F)]
    [TestCase(Note.C, -8, Note.E)]
    [TestCase(Note.C, -9, Note.DSharp)]
    [TestCase(Note.C, -10, Note.D)]
    [TestCase(Note.C, -11, Note.CSharp)]
    [TestCase(Note.C, -12, Note.C)] 

    [TestCase(Note.Bb, 1, Note.B)]
    [TestCase(Note.Db, 2, Note.Eb)]
    [TestCase(Note.Eb, 3, Note.FSharp)] 
    [TestCase(Note.Gb, 4, Note.ASharp)] 

    [TestCase(Note.F, -5, Note.C)]
    [TestCase(Note.Cb, 1, Note.C)]
    [TestCase(Note.A, 14, Note.B)] 
    [TestCase(Note.DSharp, -13, Note.D)] 

    public void CheckTranspose(Note note, int semitone, Note expected)
    {
        var newNote = note.Transpose(semitone);
        Assert.That(newNote, Is.EqualTo(expected));
    }
}