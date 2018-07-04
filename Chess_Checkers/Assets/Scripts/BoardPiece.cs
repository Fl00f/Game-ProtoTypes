using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece {

    private IBoardPeice boardPeice;

    public BoardPiece(IBoardPeice display)
    {
        this.boardPeice = display;
    }

    public void Display(int boardPiece, int side, BoardSquare square, Transform parent)
    {
        boardPeice.Display(boardPiece,side,square,parent);
    }

    public void SetPosition(Vector3 vector3){
        boardPeice.SetPosition(vector3);
    }
}
