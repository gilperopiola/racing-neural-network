public static class ConfigManager {
    public static Config config;

    public static void Init(string fileName) {
        config = UnityEngine.JsonUtility.FromJson<Config>(FileHandler.Read(fileName));
    }

    [System.Serializable]
    public class Config {
        public string projectName;
        public bool debugMode;
        public SimulationConfig simulation;
        public GeneticAlgorithmConfig geneticAlgorithm;
        public NeuralNetworkConfig neuralNet;
    }

    [System.Serializable]
    public class SimulationConfig {
        public float individualWidth;
        public float individualSpeedModifier;
        public float individualRotationSpeedModifier;
        public float individualRaycastStartingRotation;
        public float individualRaycastRotationStep;
        public float individualStartingX;
        public float individualStartingY;
        public float individualMinSpeed;
        public float wallWidth;
        public float foodFitness;
    }

    [System.Serializable]
    public class TileMapConfig {
        public string fileName;
        public int width;
        public int height;
    }

    [System.Serializable]
    public class GeneticAlgorithmConfig {
        public int nIndividuals;
        public int nElite;
        public float mutationRate;
    }

    [System.Serializable]
    public class NeuralNetworkConfig {
        public int nInputNeurons;
        public int nHiddenNeurons;
        public int nOutputNeurons;
        public int nHiddenLayers;

        public float inputEase;
    }
}
