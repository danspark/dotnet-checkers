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
    Cells: Cell[,]
}

module Board =
    let initialize playerBlack playerWhite =
        let cells: Cell[,] = Array2D.init 8 8 (fun row col -> Empty)
        
        {
            CurrentTurn = playerBlack
            NextTurn = playerWhite
            Cells = cells
        }