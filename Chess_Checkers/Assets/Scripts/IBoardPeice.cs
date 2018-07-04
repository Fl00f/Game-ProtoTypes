using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardPeice  {
    Vector2[] GetPotentialMovementSquares();
    void Display(int boardPiece, int side, BoardSquare square, Transform parent);
    void SetPosition(Vector3 vector3);
}
