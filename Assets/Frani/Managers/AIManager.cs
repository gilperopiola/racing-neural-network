using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class AIManager {
    public static List<List<float>> LoadWeights(string directory) {
        List<List<float>> weights = new List<List<float>>();
        string[] fileNames = Directory.GetFiles(ConfigManager.config.neuralNet.weightsFolder + "/", "*.nn");

        foreach (var fileName in fileNames) {
            weights.Add(VarHandler.StringToFloatList(FileHandler.Read(fileName), ','));
        }

        return weights;
    }
}