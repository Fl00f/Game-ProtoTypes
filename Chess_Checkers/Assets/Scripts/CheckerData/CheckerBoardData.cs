using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class CheckerBoardData : MonoBehaviour {
    //sides are either 1 or 2
    //0 is empty space
    private int width = 10;
    private int height = 10;

    private int[,] boardData = new int[10, 10];
    private Image[,] boardVisual = new Image[10, 10];

    public Sprite empty;
    public Sprite filledSideOne;
    public Sprite filledSideTwo;
    public Sprite filledSideOneKing;
    public Sprite filledSideTwoKing;

    public Transform boardParent;

    public Text DebugText;
    public InputField inputField;

	// Use this for initialization
	void Start () {
        AddDefaultPieces();
        CreateBoard();
        VisualizeCurrentBoardData();
    }


    public void FillSpotX(){
        if (inputField.text.Count() != 3)
            return;


        int x = inputField.text[0] - '0';
        int y = inputField.text[2] - '0';

        boardData[x, y] = 1;
        VisualizeCurrentBoardData();
    }

    public void FillSpotY()
    {
        if (inputField.text.Count() != 3)
            return;


        int x = inputField.text[0] - '0';
        int y = inputField.text[2] - '0';

        boardData[x, y] = 2;
        VisualizeCurrentBoardData();
    }

    private void VisualizeCurrentBoardData(){
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                boardVisual[i, q].color = Color.white;

                switch (boardData[i, q])
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

                EventTrigger mouseOver = boardSquare.AddComponent<EventTrigger>();

                EventTrigger trigger = boardSquare.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { MouseOverSquare(boardSquare.name); });
                trigger.triggers.Add(entry);

                ColorBlock colorBlock = button.colors;

                colorBlock.highlightedColor = Color.blue;
                button.colors = colorBlock;

                button.onClick.AddListener(() => OnBoardSquarePressed(boardSquare.name));
            }
        }
    }

    public void MouseOverSquare(string name)
    {
        DebugText.text = name;
    }

    private void AddDefaultPieces(){
        for (int i = 0; i < width; i++)
        {
            for (int q = 0; q < height; q++)
            {
                if ((q == 0 && i % 2 == 0) || (q == 1 && i % 2 != 0))
                {
                    boardData[i, q] = 1;
                }
                else if ((q == height - 1 && i % 2 != 0) || (q == height - 2 && i % 2 == 0))
                {
                    boardData[i, q] = 2;
                }
                else
                {
                    boardData[i, q] = 0;
                }
            }
        }
    }

    private void OnBoardSquarePressed(string squareName){
        
        int row = squareName[0] - '0';
        int colm = squareName[2] - '0';

        if (boardData[row,colm] > 0)
        {
            HighlightPossibleBoardMoves(row, colm, boardData[row, colm]);
        }

    }

    private void HighlightPossibleBoardMoves(int row, int colm, int boardPiece){

        Vector2Int currentSquare = new Vector2Int(row, colm);

        Vector2Int[] possibleDirections;
        int[] opposingPieces;

        switch (boardPiece)
        {
            case 1: // side one
                possibleDirections = new Vector2Int[] {  
                    new Vector2Int(1,1), 
                    new Vector2Int(-1,1)
                };
                opposingPieces = new int[] { 2,4};
                break;

            case 2: //side two
                possibleDirections = new Vector2Int[] {
                    new Vector2Int(1,-1),
                    new Vector2Int(-1,-1)
                };
                opposingPieces = new int[] { 1, 3 };

                break;
            case 3: // side one king
                possibleDirections = new Vector2Int[] {
                    new Vector2Int(1,1),
                    new Vector2Int(-1,1),
                    new Vector2Int(1,-1),
                    new Vector2Int(-1,-1)
                };
                opposingPieces = new int[] { 2, 4 };

                break;
            case 4: // side two king
                possibleDirections = new Vector2Int[] {
                    new Vector2Int(1,1),
                    new Vector2Int(-1,1),
                    new Vector2Int(1,-1),
                    new Vector2Int(-1,-1)
                };
                opposingPieces = new int[] { 1, 3 };

                break;
            default:
                possibleDirections = new Vector2Int[0];
                opposingPieces = new int[0];

                break;
        }




        BoardSquareNode startingBoard = GetBoardNodes(currentSquare, possibleDirections,true);
        HighlightNodes(startingBoard);
    }

    private void HighlightNodes(BoardSquareNode square){
        foreach (var node in square.connectingNodes)
        {
            if (boardData[node.boardPosition.x, node.boardPosition.y] == 0)
            {
                boardVisual[node.boardPosition.x, node.boardPosition.y].color = Color.red;
            }
            HighlightNodes(node);

        }
    }

    public BoardSquareNode GetBoardNodes(Vector2Int boardPosition, Vector2Int[] possibleDirs, bool isNodeStart = false){
        BoardSquareNode node = new BoardSquareNode(boardPosition);

        foreach (var dir in possibleDirs)
        {
            Vector2Int nextNode = new Vector2Int(boardPosition.x + dir.x, boardPosition.y + dir.y);

            //out of bounds
            if (nextNode.x < 0 || nextNode.x >= width || nextNode.y < 0 || nextNode.y >= height)
                continue;

            //both the current square and the next square are filled and not the starting node
            if (!isNodeStart && boardData[boardPosition.x, boardPosition.y] != 0 && boardData[nextNode.x, nextNode.y] != 0)
                continue;

            //both the current square and the next square are empty
            if (boardData[boardPosition.x, boardPosition.y] == 0 && boardData[nextNode.x, nextNode.y] == 0)
                continue;
            
            //same side
            if (GetSide(boardPosition) == GetSide(nextNode))
                continue;

            node.connectingNodes.Add(GetBoardNodes(nextNode, possibleDirs));

        }

        return node;
    }

    private int GetSide(Vector2Int position){
        switch (boardData[position.x, position.y])
        {
            case 1:                 //Side One
            case 3:
                return 1;
            case 2:                 //Side two
            case 4:
                return 2;
            default:
                return 0;           //empty
        }

    }

    public struct BoardSquareNode{
        public Vector2Int boardPosition;
        public List<BoardSquareNode> connectingNodes;

        public BoardSquareNode(Vector2Int boardPosition){
            this.boardPosition = boardPosition;
            connectingNodes = new List<BoardSquareNode>();
        }
    }
}
