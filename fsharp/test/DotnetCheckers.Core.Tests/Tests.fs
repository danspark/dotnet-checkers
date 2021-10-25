module Tests

open Xunit
open DotnetCheckers.Core

[<Fact>]
let ``every column has 3 pieces`` () =
    let cells = (Board.initialize AI AI).Cells
    
    let incrementIfPiece state cell =
        match cell with
        | Black _ | White _ -> state + 1
        | _ -> state
        
    for col in 0..cells.Length - 1 do
        let pieces = cells.[col] |> Array.fold incrementIfPiece 0
        
        Assert.Equal(3, pieces)