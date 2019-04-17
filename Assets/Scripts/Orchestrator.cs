using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {
    public Population population;

    void Start() {
        ConfigManager.Init("Assets/config.json");

        population = new Population();

        Debug.Log(ConfigManager.config.projectName + " started | debug = " + ConfigManager.config.debugMode);
    }

    void FixedUpdate() {
        population.Advance();

        if (Input.GetKeyDown(KeyCode.Space)) {
            PopulationHistory.SaveBest(@"best.txt");
        }
    }
}
