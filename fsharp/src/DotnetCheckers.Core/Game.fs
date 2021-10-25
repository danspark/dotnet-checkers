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
      Cells: Cell [,] }

type Position = { Row: int; Column: int }

type Movement = { To: Position; From: Position }

type CapturedPiece = { Piece: Piece; Position: Position }

type MovementResult =
    | Failed of string * Board
    | Succeeded of Board
    | Captured of CapturedPiece * Board

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
            |> array2D

        { CurrentTurn = playerWhite
          NextTurn = playerBlack
          Cells = cells }

    let getCell position board =
        board.Cells.[position.Column, position.Row]
    
    let move movement board =
        
        let swap fromCell toCell board =
            let piece = board |> getCell fromCell
            let empty = board |> getCell toCell
            
            board.Cells.[fromCell.Column, fromCell.Row] <- empty
            board.Cells.[toCell.Column, fromCell.Row] <- piece
        
        let origin = board |> getCell movement.From
        let destin = board |> getCell movement.To

        match origin, destin with
        // no piece to move
        | Empty, _ -> Failed("There's no piece to move", board)
        // already a piece on destination cell
        | _, Black _
        | _, White _ -> Failed("There's already a piece on the destination", board)
        | Black piece, _
        | White piece, _ ->
            match piece with
            | Pawn player
            | King player ->
                if player.Equals(board.CurrentTurn) |> not then
                    Failed($"It's not %A{player}'s turn", board)
                else
                    board |> swap movement.From movement.To
                    
                    Succeeded
                        { board with
                              CurrentTurn = board.NextTurn
                              NextTurn = board.CurrentTurn }

    let print board =
        let background =
            [| ConsoleColor.Black
               ConsoleColor.White |]

        let mutable index = 0

        for col in 0 .. 7 do
            for row in 0 .. 7 do
                Console.BackgroundColor <- background.[index]

                board.Cells.[col, row]
                |> Cell.print ConsoleColor.Red ConsoleColor.White

                index <- 1 - index

            printfn ""
            index <- 1 - index
