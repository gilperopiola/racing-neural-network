using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TileType {
    VOID = 0,
    WALL = 1,
    FOOD = 2
}

public class TileMono : MonoBehaviour {
    public Tile tile;
}

public class Tile {
    public GameObject gameObject;
    public TileMap tileMap;
    public TileType type;
    public Vector2 position;

    public float healFactor;

    public Tile(TileType _type, Vector2 _position, TileMap _tileMap) {
        tileMap = _tileMap;
        type = _type;
        position = _position;
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

    public void CreateGameObject() {
        if (type == TileType.VOID) {
            return;
        }

        gameObject = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Prefabs/Tiles/" + type.ToString()),
                        new Vector3(
                            -ConfigManager.config.tileMap.width / 2 * ConfigManager.config.simulation.wallWidth + position.x * ConfigManager.config.simulation.wallWidth,
                            +ConfigManager.config.tileMap.height / 2 * ConfigManager.config.simulation.wallWidth + position.y * -ConfigManager.config.simulation.wallWidth,
                            0),
                        Quaternion.identity);
        gameObject.transform.SetParent(tileMap.ParentGameObject.transform);
        TileMono tileMono = gameObject.AddComponent<TileMono>();
        tileMono.tile = this;
    }

    public void DestroyGameObject() {
        GameObject.Destroy(gameObject);
    }
}
