using UnityEngine;

public static class CollisionManager {
    public static bool Squares(GameObject square1, GameObject square2, float squareSideLength) {
        bool collidesX = Mathf.Abs(square1.transform.position.x - square2.transform.position.x) < squareSideLength;
        bool collidesY = Mathf.Abs(square1.transform.position.y - square2.transform.position.y) < squareSideLength;

        return collidesX && collidesY; //only returns positive if both axis are colliding
    }
}