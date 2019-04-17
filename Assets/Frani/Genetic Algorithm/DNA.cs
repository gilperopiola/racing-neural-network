using System.Collections.Generic;

public class DNA {
    public List<float> weights;

    public DNA(int nWeights) {
        weights = new List<float>();

        for (var i = 0; i < nWeights; i++) {
            weights.Add(RandomGenerator.Float(-1, 1));
        }
    }

    public DNA(List<float> _weights) {
        weights = _weights;
    }

    public DNA Merge(DNA otherDNA) {
        List<float> newWeights = new List<float>();

        int crossPosition1 = RandomGenerator.Int(0, weights.Count - 2);
        int crossPosition2 = RandomGenerator.Int(crossPosition1, weights.Count - 1);

        for (var i = 0; i < weights.Count; i++) {
            newWeights.Add((i <= crossPosition1 || i >= crossPosition2) ? weights[i] : otherDNA.weights[i]);
        }

        return new DNA(newWeights);
    }
}