using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare  {
    private IBoardSquareDisplay display;

    private Vector2 gridPos;
    public Vector2 GridPos => gridPos;

    public BoardSquare(IBoardSquareDisplay display)
    {
        this.display = display;
    }

    public void Display(Vector2 gridPosition, Vector3 vector3, Transform parent){
        display.Display(gridPosition, vector3,parent);
        gridPos = gridPosition;
    }

    public void MovePieceToSquare(BoardPiece boardPiece){
        boardPiece.SetPosition(display.WorldPosition() + Vector3.up * .25f);
    }
}
