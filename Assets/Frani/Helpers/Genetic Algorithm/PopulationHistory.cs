using System.Collections.Generic;

public static class PopulationHistory {

    public static List<List<Individual>> History { get; set; }

    public static void AddIndividuals(List<Individual> individuals) {
        if (History == null) {
            History = new List<List<Individual>>();
        }

        History.Add(new List<Individual>(individuals));
    }

    public static List<float> LoadWeightsFromFile(string fileName) {
        List<float> weights = new List<float>();
        string[] weightsStrings = FileHandler.Read(fileName).Split(',');

        foreach (var weightsString in weightsStrings) {
            if (weightsString != "") {
                weights.Add(float.Parse(weightsString));
            }
        }

        return weights;
    }

    public static void SaveBest(string fileName) {
        List<float> weights = GetBest().Dna.weights;
        FileHandler.WriteFloatList(fileName, weights, ",");
    }

    public static Individual GetBest() {
        Individual best = History[0][0];

        foreach (var generation in History) {
            foreach (var individual in generation) {
                if (individual.Fitness > best.Fitness) {
                    best = individual;
                }
            }
        }

        return best;
    }
}