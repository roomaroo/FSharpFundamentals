module ParsingTests

open NUnit.Framework
open Parsing

[<TestFixture>]
type ``when parsing a score`` ()=

    [<Test>]
    member this.``it should parse a simple score`` ()=
        let score = "32.#d3 16-"
        let result = parse score
        let assertFirstToken token =
            Assert.AreEqual({fraction = ThirtySecondth; extended = true}, token.length)
            Assert.AreEqual(Tone (DSharp,Three), token.sound)
        let assertSecondToken token =
            Assert.AreEqual({fraction = Sixteenth; extended = false}, token.length)
            Assert.AreEqual(Rest, token.sound)

        match result with
            | Choice1Of2 errorMsg -> Assert.Fail(errorMsg)
            | Choice2Of2 tokens ->
                Assert.AreEqual(2, List.length tokens)
                printfn "%A" tokens
                List.head tokens |> assertFirstToken
                List.nth tokens 1 |> assertSecondToken
                ()
   
[<TestFixture>]
type ``When calculating the semitones between notes`` ()=
    [<Test>]
    member this.``A1->A2 should be 12`` ()=
        Assert.AreEqual(12, semitonesBetween (A,One) (A,Two))
        
    [<Test>]
    member this.``A1->A3 should be 24`` ()=
        Assert.AreEqual(24, semitonesBetween (A,One) (A,Three))
    
    [<Test>]
    member this.``A2->CSharp4 should be 16`` ()=
        Assert.AreEqual(16, semitonesBetween (A,Two) (CSharp,Three))

[<TestFixture>]
type ``When calculating the duration of a note`` ()=
    [<Test>]
    member this.``a quarter note should last 500ms`` ()=
        Assert.AreEqual(
          500., 
          durationFromToken { 
           length={fraction = Quarter; extended = false}; 
           sound=Tone (DSharp,Three)})

    [<Test>]
    member this.``an extended quarter note should last 750ms`` ()=
        Assert.AreEqual(
          750., 
          durationFromToken { 
           length={fraction = Quarter; extended = true}; 
           sound=Tone (DSharp,Three)})

    [<Test>]
    member this.``a 32nd note should last 62.5ms`` ()=
        Assert.AreEqual(
          62.5, 
          durationFromToken { 
           length={fraction = ThirtySecondth; extended = false}; 
           sound=Tone (DSharp,Three)})

    [<Test>]
    member this.``an extended 32th note should last 62.5ms`` ()=
        Assert.AreEqual(
          93.75, 
          durationFromToken { 
           length={fraction = ThirtySecondth; extended = true}; 
           sound=Tone (DSharp,Three)})


[<TestFixture>]
type ``when calculating the pitch of a note`` ()=
    
    [<Test>]
    member this.``A1 should be 220 hz`` ()=
        let sound = {length={fraction = Full; extended = false}; sound = Tone(A, One)}
        let pitch = frequency sound
        Assert.AreEqual(220., pitch, 0.0001)

    [<Test>]
    member this.``A2 should be 440 hz`` ()=
        let sound = {length={fraction = Full; extended = false}; sound = Tone(A, Two)}
        let pitch = frequency sound
        Assert.AreEqual(440., pitch, 0.0001)
