using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CheckerBoardData : MonoBehaviour {
    //sides are either 1 or 2
    //0 is empty space
    private int width = 10;
    private int height = 10;

    private int[,] board = new int[10, 10];
    private Image[,] boardVisual = new Image[10, 10];

    public Sprite empty;
    public Sprite filledSideOne;
    public Sprite filledSideTwo;
    public Sprite filledSideOneKing;
    public Sprite filledSideTwoKing;

    public Transform boardParent;

	// Use this for initialization
	void Start () {
        AddDefaultPieces();
        CreateBoard();
        VisualizeCurrentBoardData();
    }

    private void VisualizeCurrentBoardData(){
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                boardVisual[i, q].color = Color.white;

                switch (board[i, q])
                {
                    
                    case 0: //empty
                        if ((i + q) % 2 != 0)
                        {
                            boardVisual[i, q].color = Color.black;
                        }
                        break;
                    case 1://side one
                        boardVisual[i, q].sprite = filledSideOne;
                        break;

                    case 2: //side two
                        boardVisual[i, q].sprite = filledSideTwo;
                        break;

                    case 3://side one King
                        boardVisual[i, q].sprite = filledSideOneKing;
                        break;
                    case 4://side two king
                        boardVisual[i, q].sprite = filledSideTwoKing;
                        break;
                    default:
                        break;
                }


            }
        }
    }

    private void CreateBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                GameObject boardSquare = new GameObject();
                boardSquare.name = i+"-"+q;
                boardSquare.transform.SetParent(boardParent);
                boardVisual[i, q] = boardSquare.AddComponent<Image>();
                Button button = boardSquare.AddComponent<Button>();

                ColorBlock colorBlock = button.colors;

                colorBlock.highlightedColor = Color.blue;
                button.colors = colorBlock;

                button.onClick.AddListener(() => OnBoardSquarePressed(boardSquare.name));
            }
        }
    }

    private void AddDefaultPieces(){
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                if ((q == 0 && i % 2 == 0) || (q == 1 && i % 2 != 0))
                {
                    board[i, q] = 1;
                }
                else if ((q == height - 1 && i % 2 != 0) || (q == height - 2 && i % 2 == 0))
                {
                    board[i, q] = 2;
                }
                else
                {
                    board[i, q] = 0;
                }
            }
        }
    }

    private void OnBoardSquarePressed(string squareName){
        
        int row = squareName[0] - '0';
        int colm = squareName[2] - '0';

        if (board[row,colm] > 0)
        {
            HighlightPossibleBoardMoves(row, colm, board[row, colm]);
        }

    }

    private void HighlightPossibleBoardMoves(int row, int colm, int boardPiece){
        Vector2[] possibleDirections;

        switch (boardPiece)
        {
            case 1: // side one
                possibleDirections = new Vector2[] {  
                    new Vector2(1,1), 
                    new Vector2(-1,1)
                };
                break;

            case 2: //side two
                possibleDirections = new Vector2[] {
                    new Vector2(1,-1),
                    new Vector2(-1,-1)
                };
                break;
            case 3: // side one king
            case 4: // side two king
                possibleDirections = new Vector2[] {
                    new Vector2(1,1),
                    new Vector2(1,-1),
                    new Vector2(-1,1),
                    new Vector2(-1,-1)
                };
                break;
            default:
                possibleDirections = new Vector2[0];
                break;
        }


        foreach (var item in possibleDirections)
        {
            int newRow = row + (int)item.x;
            int newcol = colm + (int) item.y;

            boardVisual[newRow, newcol].color = Color.red;

        }

    }
}
