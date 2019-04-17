using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {
    public Population Population;
    public TileMap TileMap;

    void Start() {
        ConfigManager.Init("Assets/config.json");

        TileMap = new TileMap(ConfigManager.config.tileMap);
        TileMap.CreateGameObjects();
        Population = new Population();

        Debug.Log(ConfigManager.config.projectName + " started | debug = " + ConfigManager.config.debugMode);
    }

    void FixedUpdate() {
        Population.Advance();

        if (Input.GetKeyDown(KeyCode.Space)) {
            PopulationHistory.SaveBest(ConfigManager.config.neuralNet.weightsFile);
        }
    }
}
