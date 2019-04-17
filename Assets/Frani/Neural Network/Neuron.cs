using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Neuron {
    public List<Neuron> InputNeurons { get; set; }
    public List<Neuron> OutputNeurons { get; set; }
    public List<float> Weights { get; set; }

    public float OutputValue { get; set; }

    public Neuron(List<Neuron> inputNeurons) {
        InputNeurons = new List<Neuron>();
        OutputNeurons = new List<Neuron>();
        Weights = new List<float>();

        foreach (var inputNeuron in inputNeurons) {
            InputNeurons.Add(inputNeuron);
            inputNeuron.OutputNeurons.Add(this);
            Weights.Add(RandomGenerator.Float(-1, 1));
        }

        if (inputNeurons.Count > 0) {
            Weights.Add(RandomGenerator.Float(-1, 1)); //this is the bias, only for the hidden and output layers
        }
    }

    public void Compute() {
        float sum = 0;
        for (int i = 0; i < InputNeurons.Count; i++) {
            sum += InputNeurons[i].OutputValue * Weights[i];
        }

        OutputValue = Sigmoid.Output(sum + Weights[Weights.Count - 1]);
    }

    public override string ToString() {
        string s = "NEURON => inputs = " + InputNeurons.Count + " | outputs = " + OutputNeurons.Count + " [";
        foreach (var weight in Weights) {
            s += weight.ToString() + ", ";
        }
        return s;
    }
}