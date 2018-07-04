using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardSquareDisplay {
    void Display(Vector2 gridPosition, Vector3 worldPosition, Transform parent);
    Vector3 WorldPosition();
}
