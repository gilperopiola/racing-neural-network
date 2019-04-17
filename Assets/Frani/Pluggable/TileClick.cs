using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameObject must have a Collider2D component

public class TileClick : MonoBehaviour {
    void OnMouseDown() {
        Debug.Log(gameObject.GetComponent<TileMono>().tile.ToString());
    }
}
