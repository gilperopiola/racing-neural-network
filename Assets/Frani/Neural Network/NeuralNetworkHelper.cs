public static class NeuralNetworkHelper {
    public static int GetWeightsNumberFromConfig() {
        int nWeights = ConfigManager.config.neuralNet.nInputNeurons * ConfigManager.config.neuralNet.nHiddenNeurons + ConfigManager.config.neuralNet.nHiddenNeurons; //first hidden layer

        for (int i = 1; i < ConfigManager.config.neuralNet.nHiddenLayers; i++) {
            nWeights += ConfigManager.config.neuralNet.nHiddenNeurons * ConfigManager.config.neuralNet.nHiddenNeurons + ConfigManager.config.neuralNet.nHiddenNeurons; //other hidden layers
        }

        nWeights += ConfigManager.config.neuralNet.nHiddenNeurons * ConfigManager.config.neuralNet.nOutputNeurons + ConfigManager.config.neuralNet.nOutputNeurons; //output layer

        return nWeights;
    }
}