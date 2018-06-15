﻿module SignalGenerator

let generateSamples milliseconds frequency = 
    let sampleRate = 44100.
    let sixteenBitSampleLimit = 32767.
    let volume = 0.4

    let toAmplitude x = 
            x
            |> (*)(2. * System.Math.PI * frequency / sampleRate)
            |> sin
            |> (*) sixteenBitSampleLimit
            |> (*) volume
            |> int16

    let numberOfSamples = milliseconds / 1000. * sampleRate
    let requiredSamples = seq { 1.0..numberOfSamples}

    Seq.map toAmplitude requiredSamples
    



