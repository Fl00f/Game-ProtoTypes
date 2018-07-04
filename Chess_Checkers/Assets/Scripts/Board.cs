using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board: MonoBehaviour {
    protected int width;
    protected int height;

    protected BoardSquare[] boardSquares;
    protected BoardPiece[] boardPeices;

    public virtual void DisplayBoard(){
        int boardCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                boardSquares[boardCount].Display(new Vector2(i,q), new Vector3(i,0,q), transform);
                boardCount++;
            }
        }
    }

    public abstract void DisplayDefaultPieces();

    public abstract bool MovePieceToSquare(BoardPiece boardPiece, Vector2 to);
}
