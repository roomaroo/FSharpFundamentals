namespace Web.Controllers

open Microsoft.AspNetCore.Mvc
open System.Net.Mime
open Microsoft.Extensions.Primitives

type HomeController () =
    inherit Controller()

    member this.Index () =
        this.View()

    [<HttpPost>]
    member this.Produce (score:string)=
        match Assembler.assembleToPackedStream score with
        | Choice2Of2 ms -> 
            this.Response.Headers.Add("Content-Disposition", StringValues(ContentDisposition(FileName="ring.wav", Inline=false).ToString()))
            ms.Position <- 0L
            this.File(ms, "audio/x-wav")

        | Choice1Of2 err -> failwith err