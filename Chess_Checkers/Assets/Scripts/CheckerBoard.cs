using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckerBoard: Board{

    // Use this for initialization
	void Start () {
        width = 10;
        height = 10;

        boardPeices = new BoardPiece[20];

        boardSquares = new BoardSquare[width * height];

        for (int i = 0; i < boardSquares.Length; i++)
        {
            boardSquares[i] = new BoardSquare(new CheckerBoardSquare());
        }

        DisplayBoard();
        DisplayDefaultPieces();
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MovePieceToSquare(boardPeices[0], new Vector2( 5,5));
        }
	}

	public override void DisplayDefaultPieces()
    {
        int sideOnePieceCount = 0;
        int sideTwoPieceCount = 0;

        int squareCount = 0;

        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                if ((q == 0 && i % 2 == 0) || (q == 1 && i % 2 != 0))
                {
                    boardPeices[sideOnePieceCount] = new BoardPiece(new CheckerPiece());
                    boardPeices[sideOnePieceCount].Display(0, 0, boardSquares[squareCount], transform);
                    boardSquares[squareCount].MovePieceToSquare(boardPeices[sideOnePieceCount]);

                    sideOnePieceCount++;
                }

                if ((q == height - 1  && i % 2 != 0) || (q == height - 2 && i % 2 == 0))
                {
                    boardPeices[sideOnePieceCount] = new BoardPiece(new CheckerPiece());
                    boardPeices[sideOnePieceCount].Display(0, 1, boardSquares[squareCount], transform);
                    boardSquares[squareCount].MovePieceToSquare(boardPeices[sideOnePieceCount]);

                    sideTwoPieceCount++;
                }

                squareCount++;
            }
        }

    }

    public override bool MovePieceToSquare(BoardPiece boardPiece, Vector2 to)
    {

        BoardSquare boardSquare = boardSquares.FirstOrDefault(obj => obj.GridPos == to);

        if (boardSquare == null)
        {
            return false;
        }

        boardSquare.MovePieceToSquare(boardPiece);

        return true;

    }

}
