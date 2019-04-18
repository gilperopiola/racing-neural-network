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
        return VarHandler.StringToFloatList(FileHandler.Read(fileName), ',');
    }

    public static void SaveBest(string directory) {
        Individual best = GetBest();

        int filesInDirectory = FileHandler.FilesInDirectory(directory);
        string fileName = filesInDirectory < 10 ? "0" + filesInDirectory + ".nn" : filesInDirectory + ".nn";

        FileHandler.WriteFloatList(@directory + "/" + fileName, best.Dna.weights, ",");
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