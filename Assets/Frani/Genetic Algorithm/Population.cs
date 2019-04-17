using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Population {
    public List<Individual> Individuals;

    public int nGeneration = 0;

    public Population() {
        Individuals = new List<Individual>();

        Individuals.Add(new Individual(new DNA(PopulationHistory.GetBestWeights())));
        Individuals[0].Index = 0;
        Individuals[0].CreateGameObject();

        for (var i = 1; i < ConfigManager.config.geneticAlgorithm.nIndividuals; i++) {
            Individuals.Add(new Individual(new DNA(NeuralNetworkHelper.GetWeightsNumberFromConfig())));
            Individuals[i].Index = i;
            Individuals[i].CreateGameObject();
        }
    }

    public void Advance() {
        for (var i = 0; i < Individuals.Count; i++) {
            if (!Individuals[i].Finished) {
                Individuals[i].Advance();
            }
        }

        if (HasFinished()) {
            Epoch();
        }
    }

    public void Epoch() {
        Debug.Log("Finished generation " + nGeneration + " with a total fitness of " + GetFitness());
        PopulationHistory.AddIndividuals(Individuals);
        nGeneration++;
        DestroyGameObjects();

        Individuals = Individuals.OrderByDescending(o => o.Fitness).ToList();

        List<Individual> newIndividuals = new List<Individual>();
        newIndividuals.AddRange(CloneElite(ConfigManager.config.geneticAlgorithm.nElite));
        newIndividuals.AddRange(ReproductionHelper.Reproduce(Individuals, ConfigManager.config.geneticAlgorithm.nIndividuals - ConfigManager.config.geneticAlgorithm.nElite, GetFitness()));
        MutationHelper.Mutate(newIndividuals.GetRange(ConfigManager.config.geneticAlgorithm.nElite, newIndividuals.Count - ConfigManager.config.geneticAlgorithm.nElite));

        for (int i = 0; i < newIndividuals.Count; i++) {
            newIndividuals[i].Index = i;
        }

        Individuals = new List<Individual>(newIndividuals);
        CreateGameObjects();
    }

    public List<Individual> CloneElite(int nElite) {
        List<Individual> elite = new List<Individual>();
        for (var i = 0; i < nElite; i++) {
            elite.Add(new Individual(Individuals[i].CloneDNA()));
            elite[i].Elite = true;
        }
        return elite;
    }

    public float GetFitness() {
        float sum = 0;
        for (int i = 0; i < Individuals.Count; i++) {
            sum += Individuals[i].Fitness;
        }
        return sum;
    }

    public bool HasFinished() {
        foreach (var individual in Individuals) {
            if (!individual.Finished) {
                return false;
            }
        }
        return true;
    }

    public void CreateGameObjects() {
        for (int i = 0; i < Individuals.Count; i++) {
            Individuals[i].CreateGameObject();
        }
    }

    public void DestroyGameObjects() {
        for (int i = 0; i < Individuals.Count; i++) {
            Individuals[i].DestroyGameObject();
        }
    }


    public override string ToString() {
        string s = "Population: ";
        for (int i = 0; i < Individuals.Count; i++) {
            s += Individuals[i].ToString();
        }
        return s;
    }
}