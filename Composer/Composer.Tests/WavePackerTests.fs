module WavePackerTests 

open NUnit.Framework
open WavePacker
open SignalGenerator
open System.IO

[<TestFixture>]
type ``when packing an audio file`` ()=

    let getFile milliseconds = 
        generateSamples milliseconds 440.
        |> Array.ofSeq
        |> pack
        |> (fun ms -> 
            ms.Seek(0L, SeekOrigin.Begin) |> ignore
            ms)

    [<Test>]
    member this.``the stream should start with 'RIFF'`` ()=
        let file = getFile 2000.
        let bucket = Array.zeroCreate 4
        file.Read(bucket, 0, 4) |> ignore
        let first4chars = System.Text.Encoding.ASCII.GetString(bucket)

        Assert.AreEqual("RIFF", first4chars)

    [<Test>]
    member this.``the file size is correct`` ()=
        let formatOverhead = 44.
        let audioLengths = [2000.; 50.; 1500.; 3000.]
        let files = List.zip audioLengths (List.map getFile audioLengths)

        let assertLength (length, file:MemoryStream) = 
            Assert.AreEqual((length/1000.) * 44100. * 2. + formatOverhead, file.Length)

        List.iter assertLength files


