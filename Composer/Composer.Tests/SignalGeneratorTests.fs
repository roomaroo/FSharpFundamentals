﻿
module SignalGeneratorTests

open NUnit.Framework
open SignalGenerator

[<TestFixture>]
type ``When generating 2 seconds at 440 Hz`` ()=

    [<Test>]
    member this.``there should be 88200 samples`` ()=
        let samples = generateSamples 2000. 440.
        Assert.AreEqual(88200, Seq.length samples)

    [<Test>]
    member this.``all samples should be in range`` ()=
        let sixteenBitSampleLimit = 32767s
        let samples = generateSamples 2000. 440.
        samples |> Seq.iter (fun s -> Assert.IsTrue(s > (-1s * sixteenBitSampleLimit) & s < sixteenBitSampleLimit))
    
    [<Test>]
    member this.``the samples should all be 0`` ()=
        let samples = generateSamples 2000. 0.
        Assert.AreEqual(Seq.init 88200 (fun i -> int16 0), samples)

    [<Test>]
    member this.``for a note the samples should not all be 0`` ()=
        let samples = generateSamples 2000. 440.
        Assert.AreNotEqual(Seq.init 88200 (fun i -> int16 0), samples)

