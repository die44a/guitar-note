using GuitarNote.Models;
using GuitarNote.Services;

namespace GuitarNoteTests.ServiceTests;

[TestFixture]
public class ChordLineParseTests
{
    private void CheckCorrectParsing(string input, string expectedText, List<(int, Chord)> expectedChords)
    {
        var chordLine = new ChordLine(input);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(chordLine.Text, Is.EqualTo(expectedText));
            Assert.That(chordLine.Chords.Count, Is.EqualTo(expectedChords.Count));
            for (var i = 0; i < expectedChords.Count; i++)
                Assert.That(chordLine.Chords[i], Is.EqualTo(expectedChords[i]));
        }
    }

    private void CheckParsingError(string input, string? expectedMessage = null)
    {
        var ex = Assert.Throws<InvalidChordLineException>(() => new ChordLine(input));
        if (expectedMessage != null)
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void EmptyInput()
    {
        CheckCorrectParsing("", 
            "",
            []);
    }
    
    [Test]
    public void SingleChord()
    {
        CheckCorrectParsing("[Am]", 
            "",
            [(0, ChordParser.ParseChord("Am"))]);
    }
    
    [Test]
    public void SeveralChords()
    {
        CheckCorrectParsing("[Am][Bm][F#m]", 
            "",
            [(0, ChordParser.ParseChord("Am")), 
                    (0, ChordParser.ParseChord("Bm")), 
                    (0, ChordParser.ParseChord("F#m"))]);
    }

    [Test]
    public void EmptyChords()
    {
        CheckCorrectParsing("Empty Chords",
            "Empty Chords",
            []);
    }

    [Test]
    public void SimpleInputWithBeginningChord()
    {
        CheckCorrectParsing("[Am] text", 
            " text",
            [(0, ChordParser.ParseChord("Am"))]);
    }
    
    [Test]
    public void SimpleInputWithMiddleChord()
    {
        CheckCorrectParsing("t[Am]ext", 
            "text",
            [(1, ChordParser.ParseChord("Am"))]);
    }
    
    [Test]
    public void SimpleInputWithEndingChord()
    {
        CheckCorrectParsing("tex[Am]t", 
            "text",
            [(3, ChordParser.ParseChord("Am"))]);
    }
    
    [Test]
    public void SimpleInputWithSpaces()
    {
        CheckCorrectParsing("[Am] first [Bm] second", 
            " first  second",
            [(0, ChordParser.ParseChord("Am")), 
                    (7, ChordParser.ParseChord("Bm"))]);
    }
    
    [Test]
    public void SimpleInputWithMixedSpaces()
    {
        CheckCorrectParsing("[Am]first [Bm]second", 
            "first second", 
            [ (0, ChordParser.ParseChord("Am")), 
                    (6, ChordParser.ParseChord("Bm")) ]);
    }
    
    [Test]
    public void SimpleInputWithoutSpaces()
    {
        CheckCorrectParsing("[Am]first[Bm]second", 
            "firstsecond", 
            [ (0, ChordParser.ParseChord("Am")), 
                (5, ChordParser.ParseChord("Bm")) ]);
    }

    [Test]
    public void UnusualChords()
    {
        CheckCorrectParsing("[F#m] Hello [G7] World", 
            " Hello  World",
            [ (0, ChordParser.ParseChord("F#m")), 
                    (7, ChordParser.ParseChord("G7")) ]);
    }
    
    [Test]
    public void ExtraSpacesEverywhere()
    {
        CheckCorrectParsing("   [Am]  first   [Bm]   second   ",
            "     first      second   ",
            [ (3, ChordParser.ParseChord("Am")), 
                    (13, ChordParser.ParseChord("Bm")) ]);
    }
    
    [Test]
    public void SeveralChordsInOnePlace()
    {
        CheckCorrectParsing("[C][G]Text",
            "Text",
            [ (0, ChordParser.ParseChord("C")), 
                    (0, ChordParser.ParseChord("G")) ]);
    }
    
    [Test]
    public void ChordsWithLeadingSpaces()
    {
        CheckCorrectParsing("   [C]Hello", 
            "   Hello",
            [(3, ChordParser.ParseChord("C"))]);
    }

    [Test]
    public void ConsecutiveChordsWithSpaces()
    {
        CheckCorrectParsing("[D] [G] [A]Test",
            "  Test",
            [(0, ChordParser.ParseChord("D")), 
                        (1, ChordParser.ParseChord("G")), 
                        (2, ChordParser.ParseChord("A"))]);
    }

    [Test]
    public void ChordAtEndWithSpaces()
    {
        CheckCorrectParsing("Text [Em]   ",
            "Text    ",
            [(5, ChordParser.ParseChord("Em"))]);
    }

    [Test]
    public void MultipleSpacesInText()
    {
        CheckCorrectParsing("Hello   [C]  world",
            "Hello     world",
            [(8, ChordParser.ParseChord("C"))]); 
    }
    
    [Test]
    public void ChordInTheEnd()
    {
        CheckCorrectParsing("Hello[C]",
            "Hello",
            [(5, ChordParser.ParseChord("C"))]); 
    }
    
    [Test]
    public void SpaceBetweenDoubleChordsInTheBeginning()
    {
        CheckCorrectParsing("[C] [G] Text",
            "  Text",
            [ (0, ChordParser.ParseChord("C")), 
                        (1, ChordParser.ParseChord("G")) ]);
    }
    
    [Test]
    public void SpaceBetweenDoubleChordsInTheMiddle()
    {
        CheckCorrectParsing("[Am] text [C] [G] Text",
            " text   Text",
            [ (0, ChordParser.ParseChord("Am")), 
                        (6, ChordParser.ParseChord("C")), 
                        (7, ChordParser.ParseChord("G")) ]);
    }
    
    [Test]
    public void MissingClosingBracket()
    {
        CheckParsingError("[Am Hello", 
            "Missing closing bracket for chord.");
    }
    
    [Test]
    public void MissingOpeningBracket()
    {
        CheckParsingError("Am] Hello", 
            "Missing opening bracket for chord.");
    }
    
    [Test]
    public void EmptyChord()
    {
        CheckParsingError("[] Hello", "Chord cannot be empty.");
    }
}