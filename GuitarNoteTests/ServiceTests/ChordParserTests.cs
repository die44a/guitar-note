using GuitarNote.Models;
using GuitarNote.Services;

namespace GuitarNoteTests.ServiceTests;

[TestFixture]
public class ChordParserTests
{
    private void CheckChordParser(string chord, Chord expected)
    {
        var actual = ChordParser.ParseChord(chord);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Note, Is.EqualTo(expected.Note));
            Assert.That(actual.Type, Is.EqualTo(expected.Type).IgnoreCase);
            Assert.That(actual.SlashBass, Is.EqualTo(expected.SlashBass));
        }
    }

    private void CheckThrowsException(string chord, string? expectedMessage = null)
    {
        var ex = Assert.Throws<InvalidChordException>(() => ChordParser.ParseChord(chord));
        if (expectedMessage != null)
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public void EmptyChord()
        => CheckThrowsException("", "chord string was null or empty");
    
    [Test]
    public void NoNote()
        =>  CheckThrowsException("none", "note cannot be found");
    
    [Test]
    public void WrongNote()
    {
        var chord = "T#";
        CheckThrowsException(chord,  "note cannot be found");
    }
    
    [Test]
    public void OnlySimpleNote()
    {
        var chord = "C";
        var expected = new Chord(Note.C, "maj");
        CheckChordParser(chord, expected);
    }
    
    [Test]
    public void SimpleNoteAndDefaultType()
    {
        var chord = "Cmaj";
        var expected = new Chord(Note.C, "maj");
        CheckChordParser(chord, expected);
    }

    [Test]
    public void OnlyNote()
    {
        var chord = "C#";
        var expected = new Chord(Note.CSharp, "maj");   
        CheckChordParser(chord, expected);
    }

    [Test]
    public void NoteAndType()
    {
        var chord = "C#maj7";
        var expected = new Chord(Note.CSharp, "maj7");
        CheckChordParser(chord, expected);
    }
    
    [Test]
    public void UnknownType()
    {
        var chord = "C#maj5";
        var expected = new Chord(Note.CSharp, "maj5");
        CheckChordParser(chord, expected);
    }

    [Test]
    public void WrongNoteWithType()
    {
        var chord = "K#maj7";
        CheckThrowsException(chord,  "note cannot be found");
    }

    [Test]
    public void NoteWithSlashBass()
    {
        var chord = "C#/F";
        var expected = new Chord(Note.CSharp, "maj", Note.F);
        CheckChordParser(chord, expected);
    }

    [Test]
    public void WrongNoteWithSlashBass()
    {
        var chord = "T#/F";
        CheckThrowsException(chord, "note cannot be found");
    }

    [Test]
    public void NoteWithTypeAndSlash()
    {
        var chord = "C#maj7/G";
        var expected = new Chord(Note.CSharp, "maj7", Note.G);
        CheckChordParser(chord, expected);
    }

    [Test]
    public void WrongNoteWithTypeAndSlash()
    {
        var chord = "T#maj7/G";
        CheckThrowsException(chord, "note cannot be found");
    }

    [Test]
    public void NoteWithFlat()
    {
        var chord = "Gb";
        var expected = new Chord(Note.FSharp, "maj");
        CheckChordParser(chord, expected);
    }

    [Test]
    public void LongChord()
    {
        var chord = "C#maj7/G#";
        var expected = new Chord(Note.CSharp, "maj7", Note.GSharp);
        CheckChordParser(chord, expected);
    }
    
    [Test]
    public void CaseSensitiveNote()
    {
        var chord = "c";
        var expected = new Chord(Note.C, "maj");
        CheckChordParser(chord, expected);
    }
    
    [Test]
    public void CaseSensitiveNoteWithSharp()
    {
        var chord = "c#";
        var expected = new Chord(Note.CSharp, "maj");
        CheckChordParser(chord, expected);
    }

    [Test]
    public void CaseSensitiveNoteWithType()
    {
        var chord = "c#MaJ7";
        var expected = new Chord(Note.CSharp, "maj7");
        CheckChordParser(chord, expected);
    }
    
    [Test]
    public void CaseSensitiveNoteWithTypeAndSlashBass()
    {
        var chord = "c#MaJ7/g#";
        var expected = new Chord(Note.CSharp, "maj7", Note.GSharp);
        CheckChordParser(chord, expected);
    }

    [Test]
    public void WrongBassWithoutType()
    {
        var chord = "C#/T";
        CheckThrowsException(chord, "note cannot be found");
    }
    
    [Test]
    public void WrongBassWithType()
    {
        var chord = "C#m7b5/T";
        CheckThrowsException(chord, "note cannot be found");
    }
}