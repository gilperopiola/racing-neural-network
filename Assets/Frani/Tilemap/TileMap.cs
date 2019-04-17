using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap {
    public List<List<Tile>> tiles;
    public ConfigManager.TileMapConfig config;

    public TileMap(ConfigManager.TileMapConfig _config) {
        config = _config;

        string mapChars = FileHandler.Read(config.fileName).Replace("\n", "").Replace("\r", "");
        tiles = new List<List<Tile>>();

        for (int y = 0; y < config.height; y++) {
            for (int x = 0; x < config.width; x++) {
                if (y <= 0) {
                    tiles.Add(new List<Tile>());
                }

                tiles[x].Add(new Tile((TileType)System.Char.GetNumericValue(mapChars[x + y * (int)config.width]), new Vector2(x, y), this));
            }
        }
    }

    public Tile GetRandomTile() {
        return tiles[RandomGenerator.Int(0, (int)config.width - 1)][RandomGenerator.Int(0, (int)config.height - 1)];
    }

    public void CreateGameObjects() {
        for (int y = 0; y < config.height; y++) {
            for (int x = 0; x < config.width; x++) {
                tiles[x][y].CreateGameObject();
            }
        }
    }

    public void DestroyGameObjects() {
        for (int y = 0; y < config.height; y++) {
            for (int x = 0; x < config.width; x++) {
                tiles[x][y].DestroyGameObject();
            }
        }
    }

    public override string ToString() {
        string str = "";

        for (int y = 0; y < config.height; y++) {
            for (int x = 0; x < config.width; x++) {
                str += (int)tiles[x][y].type;
            }
            str += "\n";
        }

        return str;
    }
}
