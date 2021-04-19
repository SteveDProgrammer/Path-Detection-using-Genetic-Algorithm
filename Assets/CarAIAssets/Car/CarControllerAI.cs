using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControllerAI : MonoBehaviour
{
    List<GameObject> cars;
    public int population = 50;
    public int generation = 0;
    public GameObject car;    

    [HideInInspector]
    public DNA best_dna1;
    public DNA best_dna2;

    private int cars_created = 0;

    public Text genText;

    // Start is called before the first frame update
    void Start()
    {
        genNewPopulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> getCars()
    {
        return cars;
    }

    public void genNewPopulation()
    {
        cars = new List<GameObject>();
        for(int i=0; i<population; i++)
        {
            GameObject car_obj = Instantiate(car);
            car_obj.SetActive(true);
            cars.Add(car_obj);
            car_obj.GetComponent<Car>().Initialize();
        }
        generation++;
        genText.text = "Generation: " + generation.ToString();
    }

    public void genNewPopulation(bool hasGenes)
    {
        if(hasGenes)
        {
            cars = new List<GameObject>();
            for (int i = 0; i < population; i++)
            {
                DNA dna = best_dna1.crossover(best_dna2);
                DNA mutated = dna.mutate();
                GameObject car_obj = (Instantiate(car,transform.position,Quaternion.identity));
                car_obj.SetActive(true);
                cars.Add(car_obj);
                car_obj.GetComponent<Car>().Initialize();
            }

            generation++;
            genText.text = "Generation: " + generation.ToString();
            cars_created = 0;
            GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[0]);
        }
    }

    public void restartGeneration()
    {
        cars.Clear();
        genNewPopulation();
    }
}
