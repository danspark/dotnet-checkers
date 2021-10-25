namespace DotnetCheckers.Core

open System

type Player =
    | Human of string
    | AI

type Piece =
    | Pawn of Player
    | King of Player

type Cell =
    | Empty
    | White of Piece
    | Black of Piece

type Board =
    { CurrentTurn: Player
      NextTurn: Player
      Cells: Cell [] array }

[<RequireQualifiedAccess>]
module Piece =
    let asString piece =
        match piece with
        | Pawn _ -> "P"
        | King _ -> "K"

[<RequireQualifiedAccess>]
module Cell =
    let print blackForeground whiteForeground cell =
        let previousForeground = Console.ForegroundColor
        
        let text =
            match cell with
            | Empty _ -> " "
            | Black b ->
                Console.ForegroundColor <- blackForeground
                Piece.asString b
            | White p ->
                Console.ForegroundColor <- whiteForeground
                Piece.asString p

        printf $" %s{text} "
        Console.ForegroundColor <- previousForeground

[<RequireQualifiedAccess>]
module Board =
    let initialize playerBlack playerWhite =
        let e = Empty
        let b = Black(Pawn playerBlack)
        let w = White(Pawn playerWhite)

        let cells =
            [| [| b; e; b; e; e; e; w; e |]
               [| e; b; e; e; e; w; e; w |]
               [| b; e; b; e; e; e; w; e |]
               [| e; b; e; e; e; w; e; w |]
               [| b; e; b; e; e; e; w; e |]
               [| e; b; e; e; e; w; e; w |]
               [| b; e; b; e; e; e; w; e |]
               [| e; b; e; e; e; w; e; w |] |]

        { CurrentTurn = playerBlack
          NextTurn = playerWhite
          Cells = cells }

    let print board =
        let background = [| ConsoleColor.Black; ConsoleColor.White |]

        let mutable index = 0

        for col in 0 .. 7 do
            for row in 0 .. 7 do
                Console.BackgroundColor <- background.[index]

                board.Cells.[col].[row]
                |> Cell.print ConsoleColor.Red ConsoleColor.White

                index <- 1 - index

            printfn ""
            index <- 1 - index
