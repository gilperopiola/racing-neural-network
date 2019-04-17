using System.Collections.Generic;

public static class PopulationHistory {

    public static List<List<Individual>> History { get; set; }

    public static void AddIndividuals(List<Individual> individuals) {
        if (History == null) {
            History = new List<List<Individual>>();
        }

        History.Add(new List<Individual>(individuals));
    }

    public static List<float> GetBestWeights() {
        List<float> bestWeights = new List<float>();
        string[] bestWeightsStrings = FileHandler.Read(@"best.txt").Split(',');

        foreach (var bestWeightsString in bestWeightsStrings) {
            if (bestWeightsString == "") {
                continue;
            }
            bestWeights.Add(float.Parse(bestWeightsString));
        }

        return bestWeights;
    }

    public static void SaveBest(string fileName) {
        List<float> bestWeights = GetBest().Dna.weights;
        FileHandler.WriteFloatList(fileName, bestWeights, ",");
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