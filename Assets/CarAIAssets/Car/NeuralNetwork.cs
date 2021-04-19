using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    public int hidden_layers = 1;
    public int size_hidden_layers = 10;
    public int outputs = 2;
    public int inputs = 5;
    public float max_initial_value = 5f;

    private const float EULER_NUMBER = 2.71828f;
    private List<List<float>> neurons;
    private List<float[][]> weights;

    private int totalLayers = 0;

    public NeuralNetwork()
    {
        totalLayers = hidden_layers + 2;
        //Initialize weights and the neurons array
        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        //Fill neurons
        for (int i=0; i< totalLayers; i++)
        {
            float[][] layer_weights;
            List<float> layer = new List<float>();
            int layer_size = getLayerSize(i);
            if(i != hidden_layers+1)
            {
                layer_weights = new float[layer_size][];
                int next_layer_size = getLayerSize(i + 1);
                for(int j=0; j<layer_size; j++)//current layer
                {
                    layer_weights[j] = new float[next_layer_size];
                    for(int k=0; k<next_layer_size; k++)
                    {
                        layer_weights[j][k] = genRandomValue();
                    }
                }
                weights.Add(layer_weights);
            }
            for(int j = 0; j<layer_size; j++)
            {
                layer.Add(0);
            }
            neurons.Add(layer);
        }
    }

    public NeuralNetwork(DNA dna)
    {
        List<float[][]> DNA_weight_list = dna.getDNA(); //+++++++++
        totalLayers = hidden_layers + 2;
        //Initialize weights and the neurons array
        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        //Fill neurons
        for (int i = 0; i < totalLayers; i++)
        {
            float[][] layer_weights;
            float[][] DNA_layer_weights; //+++++++++
            List<float> layer = new List<float>();
            int layer_size = getLayerSize(i);
            if (i != hidden_layers + 1)
            {
                DNA_layer_weights = DNA_weight_list[i]; //+++++++++
                layer_weights = new float[layer_size][];
                int next_layer_size = getLayerSize(i + 1);
                for (int j = 0; j < layer_size; j++) //current layer
                {
                    layer_weights[j] = new float[next_layer_size];
                    for (int k = 0; k < next_layer_size; k++)
                    {
                        layer_weights[j][k] = DNA_layer_weights[j][k];
                    }
                }
                weights.Add(layer_weights);
            }
            for (int j = 0; j < layer_size; j++)
            {
                layer.Add(0);
            }
            neurons.Add(layer);
        }
    }

    public void feedForward(float[] inputs)
    {
        List<float> input_layer = neurons[0];
        for(int i=0; i<inputs.Length; i++)
        {
            input_layer[i] = inputs[i];
        }
        //Update neurons from the input layer to the output layer
        for (int layer = 0; layer < neurons.Count-1; layer++)
        {
            float[][] layer_weights = weights[layer];
            List<float> layer_neurons = neurons[layer];
            List<float> next_layer_neurons = neurons[layer+1]; //layer+1 is the next next neuron layer
            for (int i = 0; i < next_layer_neurons.Count; i++)
            {
                float sum = 0;
                for(int j=0; j<layer_neurons.Count; j++)
                {
                    sum += layer_weights[j][i] * layer_neurons[j]; //feed-forward multiplication
                }
                next_layer_neurons[i] = sigmoid(sum);
            }
        }
    }

    public int getLayerSize(int i)
    {
        int layer_size = 0;
        if (i == 0)
        {
            layer_size = inputs;
        }
        else if (i == hidden_layers + 1)
        {
            layer_size = inputs;
        }
        else
        {
            layer_size = size_hidden_layers;
        }

        return layer_size;
    }

    public List<float> getOutputs()
    {
        return neurons[neurons.Count - 1];
    }

    public float sigmoid(float x)
    {
        return 1 / (float)(1 + Mathf.Pow(EULER_NUMBER, -x));
    }

    public float genRandomValue()
    {
        return Random.Range(-max_initial_value, max_initial_value);
    }

    public List<List<float>> getNeurons()
    {
        return neurons;
    }
    public List<float[][]> getWeights()
    {
        return weights;
    }
}
