using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    private List<float[][]> dna;
    private float mutation_prob = 0.05f;
    private float max_variation = 1f;
    private float max_mutation = 5f;

    public DNA(List<float[][]> weights)
    {
        this.dna = weights;
    }

    public List<float[][]>getDNA()
    {
        return dna;
    }

    public DNA mutate()
    {
        List<float[][]> new_dna = new List<float[][]>();

        for (int i = 0; i < dna.Count; i++)
        {
            float[][] layer_weights = dna[i];
            for (int j = 0; j < layer_weights.Length; j++)
            {
                for(int k=0;k<layer_weights[j].Length;k++)
                {
                    float rand = Random.Range(0f, 1f);
                    if(rand < mutation_prob)
                    {
                        layer_weights[j][k] = Random.Range(-max_variation, max_variation);
                    }
                }
            }
            new_dna.Add(layer_weights);
        }

        return new DNA(new_dna);
    }

    //DNA of the class (current parent) + DNA parameter (other parent)
    public DNA crossover(DNA other_parent)
    {
        
        List<float[][]> child = new List<float[][]>();

        for(int i = 0; i< dna.Count; i++)
        {
            float[][] other_parent_layer_weights = other_parent.getDNA()[i];
            float[][] curr_parent_layer_weights = dna[i];

            Debug.Log("Entered crossover line 56");

            for (int j = 0; j<curr_parent_layer_weights.Length-1; j++)
            {
                for (int k = 0; k < curr_parent_layer_weights[j].Length-1; k++)
                {
                    float rand = Random.Range(0f, 1f);
                    if (rand < 0.5f)
                    {
                        curr_parent_layer_weights[j][k] = other_parent_layer_weights[j][k];
                    }
                    else
                    {
                        curr_parent_layer_weights[j][k] = curr_parent_layer_weights[j][k];  //probably can be removed
                    }
                }
            }
            child.Add(curr_parent_layer_weights);
        }
        return new DNA(child);
       // return other_parent;
    }
}
