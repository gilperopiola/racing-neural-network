using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileType {
    VOID = 0,
    GRASS = 1,
    WATER = 2
}

public class TileMono : MonoBehaviour {
    public Tile tile;
}

public class Tile {
    public GameObject gameObject;
    public TileMap tileMap;
    public List<Individual> individuals;
    public TileType type;
    public Vector2 position;

    public float healFactor;

    public Tile(TileType _type, Vector2 _position, TileMap _tileMap) {
        tileMap = _tileMap;
        type = _type;
        position = _position;
        individuals = new List<Individual>();
    }

    public Tile GetNeighbour(Direction direction) {
        switch (direction) {
            case Direction.UP:
                return ((int)position.y > 0) ? tileMap.tiles[(int)position.x][(int)position.y - 1] : null;
            case Direction.UP_RIGHT:
                return ((int)position.y > 0 && (int)position.x < (int)tileMap.config.width - 1) ? tileMap.tiles[(int)position.x + 1][(int)position.y - 1] : null;
            case Direction.RIGHT:
                return ((int)position.x < (int)tileMap.config.width - 1) ? tileMap.tiles[(int)position.x + 1][(int)position.y] : null;
            case Direction.RIGHT_DOWN:
                return ((int)position.x < (int)tileMap.config.width - 1 && (int)position.y < (int)tileMap.config.height - 1) ? tileMap.tiles[(int)position.x + 1][(int)position.y + 1] : null;
            case Direction.DOWN:
                return ((int)position.y < (int)tileMap.config.height - 1) ? tileMap.tiles[(int)position.x][(int)position.y + 1] : null;
            case Direction.DOWN_LEFT:
                return ((int)position.y < (int)tileMap.config.height - 1 && (int)position.x > 0) ? tileMap.tiles[(int)position.x - 1][(int)position.y + 1] : null;
            case Direction.LEFT:
                return ((int)position.x > 0) ? tileMap.tiles[(int)position.x - 1][(int)position.y] : null;
            case Direction.LEFT_UP:
                return ((int)position.x > 0 && (int)position.y > 0) ? tileMap.tiles[(int)position.x - 1][(int)position.y - 1] : null;
        }
        return null;
    }

    public void AddIndividual(Individual newIndividual) {
        individuals.Add(newIndividual);
    }

    public void RemoveIndividual(Individual oldIndividual) {
        individuals.Remove(oldIndividual);
    }

    public void CreateGameObject() {
        gameObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Tiles/" + type.ToString()), new Vector3(position.x * 32, position.y * -32, 0), Quaternion.identity);
        TileMono tileMono = gameObject.AddComponent<TileMono>();
        tileMono.tile = this;
    }

    public void DestroyGameObject() {
        GameObject.Destroy(gameObject);
    }

    public override string ToString() {
        string s = "Tile[" + position.x + "][" + position.y + "] has " + individuals.Count + " individuals: ";
        for (var i = 0; i < individuals.Count; i++) {
            s += individuals[i].ToString();
        }

        return s;
    }
}
