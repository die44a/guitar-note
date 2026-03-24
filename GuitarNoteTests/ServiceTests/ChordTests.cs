using GuitarNote.Models;

namespace GuitarNoteTests.ServiceTests;

[TestFixture]
public class ChordTests
{
    [Test]
    public void GetNotes_MajorChord()
    {
        var chord = new Chord(Note.C, "maj");
        var notes = chord.GetNotes();
        Assert.That(notes, Is.EquivalentTo([Note.C, Note.E, Note.G]));
    }
    
    [Test]
    public void GetNotes_Minor7Chord()
    {
        var chord = new Chord(Note.A, "m7"); 
        var notes = chord.GetNotes();
        Assert.That(notes, Is.EquivalentTo([Note.A, Note.C, Note.E, Note.G]));
    }

    [Test]
    public void GetNotes_Major7Chord_Transpose()
    {
        var chord = new Chord(Note.D, "maj7");
        var transposedChord = chord.Transpose(2);
        var notes = transposedChord.GetNotes();
        Assert.That(notes, Is.EquivalentTo([Note.E, Note.GSharp, Note.B, Note.DSharp]));
    }
}