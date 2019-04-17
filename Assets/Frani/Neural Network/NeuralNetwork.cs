using System.Collections.Generic;

public class NeuralNetwork {
    public List<Neuron> inputLayer;
    public List<List<Neuron>> hiddenLayers;
    public List<Neuron> outputLayer;

    public NeuralNetwork(List<float> weights) {
        ConfigManager.NeuralNetworkConfig config = ConfigManager.config.neuralNet;

        inputLayer = new List<Neuron>();
        hiddenLayers = new List<List<Neuron>>();
        outputLayer = new List<Neuron>();

        for (int i = 0; i < config.nInputNeurons; i++) {
            inputLayer.Add(new Neuron(new List<Neuron>()));
        }

        int weightsUsed = 0;
        for (int i = 0; i < config.nHiddenLayers; i++) {
            hiddenLayers.Add(new List<Neuron>());

            for (int j = 0; j < config.nHiddenNeurons; j++) {
                hiddenLayers[i].Add(new Neuron(i == 0 ? inputLayer : hiddenLayers[i - 1]));

                if (weights.Count > 0) {
                    hiddenLayers[i][j].Weights = weights.GetRange(weightsUsed, (i == 0) ? config.nInputNeurons + 1 : config.nHiddenNeurons + 1);
                    weightsUsed += (i == 0) ? config.nInputNeurons + 1 : config.nHiddenNeurons + 1;
                }
            }
        }

        for (int i = 0; i < config.nOutputNeurons; i++) {
            outputLayer.Add(new Neuron(hiddenLayers[hiddenLayers.Count - 1]));

            if (weights.Count > 0) {
                outputLayer[i].Weights = weights.GetRange(weightsUsed, config.nHiddenNeurons + 1);
                weightsUsed += config.nHiddenNeurons + 1;
            }
        }
    }

    public List<float> Compute(List<float> inputs) {
        for (int i = 0; i < inputLayer.Count; i++) {
            inputLayer[i].OutputValue = inputs[i];
        }

        foreach (var hiddenLayer in hiddenLayers) {
            foreach (var hiddenNeuron in hiddenLayer) {
                hiddenNeuron.Compute();
            }
        }

        foreach (var outputNeuron in outputLayer) {
            outputNeuron.Compute();
        }

        List<float> outputs = new List<float>();
        foreach (var outputNeuron in outputLayer) {
            outputs.Add(outputNeuron.OutputValue);
        }
        return outputs;
    }

    public List<float> GetWeights() {
        List<float> weights = new List<float>();

        foreach (var inputNeuron in inputLayer) {
            weights.AddRange(inputNeuron.Weights);
        }

        foreach (var hiddenLayer in hiddenLayers) {
            foreach (var hiddenNeuron in hiddenLayer) {
                weights.AddRange(hiddenNeuron.Weights);
            }
        }

        foreach (var outputNeuron in outputLayer) {
            weights.AddRange(outputNeuron.Weights);
        }

        return new List<float>(weights);
    }

    public override string ToString() {
        string s = "INPUT LAYER => " + inputLayer.Count + " NEURONS: ";
        for (int i = 0; i < inputLayer.Count; i++) {
            s += inputLayer[i].ToString() + "\n";
        }

        for (int i = 0; i < hiddenLayers.Count; i++) {
            s += "\n HIDDEN LAYER " + i + " => " + hiddenLayers[i].Count + " NEURONS: ";

            for (int j = 0; j < hiddenLayers[i].Count; j++) {
                s += hiddenLayers[i][j].ToString() + "\n";
            }
        }

        s += "\n OUTPUT LAYER => " + outputLayer.Count + " NEURONS: ";
        for (int i = 0; i < outputLayer.Count; i++) {
            s += outputLayer[i].ToString() + "\n";
        }

        return s;
    }
}