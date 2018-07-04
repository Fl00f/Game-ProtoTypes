using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoardSquare : IBoardSquareDisplay
{
    public GameObject squareGO;

    public void Display(Vector2 gridPosition, Vector3 worldPos, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/BlackBoardSquare");

        if ((gridPosition.x + gridPosition.y) % 2 == 0)
        {
            prefab = Resources.Load<GameObject>("Prefabs/BlackBoardSquare");
        } else
        {
            prefab = Resources.Load<GameObject>("Prefabs/WhiteBoardSquare");
        }

        squareGO = GameObject.Instantiate(prefab,worldPos, Quaternion.identity, parent);
    }

    public Vector3 WorldPosition()
    {
        Debug.Log(squareGO);
        return squareGO.transform.position;
    }
}
