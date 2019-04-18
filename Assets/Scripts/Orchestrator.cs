using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {
    public Population Population;
    public TileMap TileMap;

    void Start() {
        ConfigManager.Init("Assets/config.json");

        List<List<float>> initialWeights = AIManager.LoadWeights(ConfigManager.config.neuralNet.weightsFolder);

        TileMap = new TileMap(ConfigManager.config.tileMap);
        TileMap.CreateGameObjects();

        Population = new Population(initialWeights);

        Debug.Log(ConfigManager.config.projectName + " started | debug = " + ConfigManager.config.debugMode);
    }

    void FixedUpdate() {
        Population.Advance();

        if (Input.GetKeyDown(KeyCode.Space)) {
            PopulationHistory.SaveBest(ConfigManager.config.neuralNet.weightsFolder);
        }
    }
}
