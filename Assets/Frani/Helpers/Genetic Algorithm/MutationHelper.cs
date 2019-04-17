using System.Collections.Generic;
using UnityEngine;

public static class MutationHelper {
    public static void Mutate(List<Individual> individuals) {
        for (var i = 0; i < individuals.Count; i++) {
            Mutate(individuals[i]);
        }
    }

    public static void Mutate(Individual individual) {
        for (var i = 0; i < individual.Dna.weights.Count; i++) {
            float n = RandomGenerator.Float(0, 100);
            if (n < ConfigManager.config.geneticAlgorithm.mutationRate) {
                Mutate(individual.Dna.weights[i]);
            } else if (n < ConfigManager.config.geneticAlgorithm.mutationRate * 2) {
                MutateSoft(individual.Dna.weights[i]);
            }
        }
    }

    public static void Mutate(float weight) {
        weight *= -1;
    }

    public static void MutateSoft(float weight) {
        weight *= RandomGenerator.Float(0.8f, 1.2f);
    }
}