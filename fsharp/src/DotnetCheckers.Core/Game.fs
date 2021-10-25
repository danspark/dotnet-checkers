namespace DotnetCheckers.Core

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
    
type Board = {
    CurrentTurn: Player
    NextTurn: Player
    Cells: Cell[] array
}

module Board =
    let initialize playerBlack playerWhite =
        let e = Empty
        let b = Black (Pawn playerBlack)
        let w = White (Pawn playerWhite)
        
        let cells = [|
            [| b; e; b; e; e; e; w; e |]
            [| e; b; e; e; e; w; e; w |]
            [| b; e; b; e; e; e; w; e |]
            [| e; b; e; e; e; w; e; w |]
            [| b; e; b; e; e; e; w; e |]
            [| e; b; e; e; e; w; e; w |]
            [| b; e; b; e; e; e; w; e |]
            [| e; b; e; e; e; w; e; w |]
        |]
        
        {
            CurrentTurn = playerBlack
            NextTurn = playerWhite
            Cells = cells
        }