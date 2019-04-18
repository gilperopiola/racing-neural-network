using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Population {
    public List<Individual> Individuals;
    public GameObject ParentGameObject;

    public int nGeneration = 0;

    public Population(List<List<float>> initialWeights = null) {
        Individuals = new List<Individual>();
        ParentGameObject = new GameObject();
        ParentGameObject.name = "Individuals";

        if (initialWeights != null) {
            for (int i = 0; i < initialWeights.Count; i++) {
                Individuals.Add(new Individual(new DNA(initialWeights[i])));
                Individuals[i].Index = i;
                Individuals[i].CreateGameObject();
                Individuals[i].GameObject.transform.SetParent(ParentGameObject.transform);
            }
        } else {
            initialWeights = new List<List<float>>(); //initializes it so it will be 0 on the for statement below
        }

        for (var i = initialWeights.Count; i < ConfigManager.config.geneticAlgorithm.nIndividuals; i++) {
            Individuals.Add(new Individual(new DNA(NeuralNetworkHelper.GetWeightsNumberFromConfig())));
            Individuals[i].Index = i;
            Individuals[i].CreateGameObject();
            Individuals[i].GameObject.transform.SetParent(ParentGameObject.transform);
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
            if (!individual.Finished && individual.Fitness < 1000) {
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

        GameObject.Destroy(ParentGameObject);
    }


    public override string ToString() {
        string s = "Population: ";
        for (int i = 0; i < Individuals.Count; i++) {
            s += Individuals[i].ToString();
        }
        return s;
    }
}