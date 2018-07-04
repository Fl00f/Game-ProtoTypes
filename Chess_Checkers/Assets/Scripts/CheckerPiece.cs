using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : IBoardPeice {

    GameObject boardPieceGO;

    public void Display(int boardPiece, int side, BoardSquare boardSquare, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/BlackBoardSquare");

        if (side == 0)
        {
            prefab = Resources.Load<GameObject>("Prefabs/CheckerBoardPiece/CheckerWhite");
        } else
        {
            prefab = Resources.Load<GameObject>("Prefabs/CheckerBoardPiece/CheckerBlack");
        }

        boardPieceGO = GameObject.Instantiate(prefab, parent);
    }

    public Vector2[] GetPotentialMovementSquares()
    {
        return new Vector2[] { 
            new Vector2(1,1),       //UR
            new Vector2(-1, -1),    //BL
            new Vector2(1, -1),     //BR
            new Vector2(-1, 1)};    //UL
    }

    public void SetPosition(Vector3 vector3)
    {
        boardPieceGO.transform.position = vector3;
    }
}
