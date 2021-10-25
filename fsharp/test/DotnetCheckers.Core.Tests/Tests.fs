module Tests

open System.Collections.Generic
open Xunit
open DotnetCheckers.Core

let incrementIfPiece state cell =
    match cell with
    | Black _
    | White _ -> state + 1
    | _ -> state

[<Fact>]
let ``every column has 3 pieces`` () =
    let cells = (Board.initialize AI AI).Cells

    for col in 0..7 do
        let pieces =
            cells.[col..col, 0..7]
            |> fun arr ->
                Array.init 8 (fun i -> arr.[0, i])
                |> Array.fold incrementIfPiece 0

        Assert.Equal(3, pieces)

[<Fact>]
let ``every row has either 0 or 4 pieces`` () =
    let cells = (Board.initialize AI AI).Cells

    for row in 0..7 do
        let pieces =
            cells.[0..7, row..row]
            |> fun arr ->
                Array.init 8 (fun i -> arr.[i, 0])
                |> Array.fold incrementIfPiece 0
                
        Assert.Subset(HashSet([| 0; 4 |]), HashSet([| pieces |]))