using NUnit.Framework;
using GuitarNote.Services;
using System.Collections.Generic;

namespace GuitarNoteTests.ServiceTests;

[TestFixture]
public class ChordLineParseTests
{
    private void CheckCorrectParsing(string input, string expectedText, List<(int, string)> expectedChords)
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
            [(0, "Am")]);
    }
    
    [Test]
    public void SeveralChords()
    {
        CheckCorrectParsing("[Am][Bm][F#m]", 
            "",
            [(0, "Am"), (0, "Bm"), (0, "F#m")]);
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
            [(0, "Am")]);
    }
    
    [Test]
    public void SimpleInputWithMiddleChord()
    {
        CheckCorrectParsing("t[Am]ext", 
            "text",
            [(1, "Am")]);
    }
    
    [Test]
    public void SimpleInputWithEndingChord()
    {
        CheckCorrectParsing("tex[Am]t", 
            "text",
            [(3, "Am")]);
    }
    
    [Test]
    public void SimpleInputWithSpaces()
    {
        CheckCorrectParsing("[Am] first [Bm] second", 
            " first  second",
            [(0, "Am"), (7, "Bm")]);
    }
    
    [Test]
    public void SimpleInputWithMixedSpaces()
    {
        CheckCorrectParsing("[Am]first [Bm]second", 
            "first second", 
            [ (0, "Am"), (6, "Bm") ]);
    }
    
    [Test]
    public void SimpleInputWithoutSpaces()
    {
        CheckCorrectParsing("[Am]first[Bm]second", 
            "firstsecond", 
            [ (0, "Am"), (5, "Bm") ]);
    }

    [Test]
    public void UnusualChords()
    {
        CheckCorrectParsing("[F#m] Hello [G7] World", 
            " Hello  World",
            [ (0, "F#m"), (7, "G7") ]);
    }
    
    [Test]
    public void ExtraSpacesEverywhere()
    {
        CheckCorrectParsing("   [Am]  first   [Bm]   second   ",
            "     first      second   ",
            [ (3, "Am"), (13, "Bm") ]);
    }
    
    [Test]
    public void SeveralChordsInOnePlace()
    {
        CheckCorrectParsing("[C][G]Text",
            "Text",
            [ (0, "C"), (0, "G") ]);
    }
    
    [Test]
    public void ChordsWithLeadingSpaces()
    {
        CheckCorrectParsing("   [C]Hello", 
            "   Hello",
            [(3, "C")]);
    }

    [Test]
    public void ConsecutiveChordsWithSpaces()
    {
        CheckCorrectParsing("[D] [G] [A]Test",
            "  Test",
            [(0, "D"), (1, "G"), (2, "A")]);
    }

    [Test]
    public void ChordAtEndWithSpaces()
    {
        CheckCorrectParsing("Text [Em]   ",
            "Text    ",
            [(5, "Em")]);
    }

    [Test]
    public void MultipleSpacesInText()
    {
        CheckCorrectParsing("Hello   [C]  world",
            "Hello     world",
            [(8, "C")]); 
    }
    
    [Test]
    public void ChordInTheEnd()
    {
        CheckCorrectParsing("Hello[C]",
            "Hello",
            [(5, "C")]); 
    }
    
    [Test]
    public void SpaceBetweenDoubleChordsInTheBeginning()
    {
        CheckCorrectParsing("[C] [G] Text",
            "  Text",
            [ (0, "C"), (1, "G") ]);
    }
    
    [Test]
    public void SpaceBetweenDoubleChordsInTheMiddle()
    {
        CheckCorrectParsing("[Am] text [C] [G] Text",
            " text   Text",
            [ (0, "Am"), (6, "C"), (7, "G") ]);
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