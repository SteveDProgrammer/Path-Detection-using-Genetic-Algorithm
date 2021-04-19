using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private DNA dna;
    private NeuralNetwork network;
    private Vector3 init_pos;
    private float distance;

    private bool initialized = false;

    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize()
    {
        network = new NeuralNetwork();
        dna = new DNA(network.getWeights());
        init_pos = transform.position;
        initialized = true;
    }

    public void Initialize(DNA dna)
    {
        network = new NeuralNetwork();
        this.dna = dna;
        init_pos = transform.position;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            //Get the distances of lasers as inputs
            float[] inputs = GetComponent<Lasers>().getDistances();

            //Execute feed-forward
            network.feedForward(inputs);

            List<float> outputs = network.getOutputs();
            GetComponent<CarMovement>().updateMovement(outputs);
            distance = Vector3.Distance(transform.position, init_pos);
        }
    }

    public DNA getDNA()
    {
        return dna;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Gate")
        {
            gate.GetComponent<finale>().stop();
        }
        else if (col.tag != "Car")
        {
            changeCamera();

            for (int i = 0; i < GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars().Count; i++)
            {
                if ((GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars())[i] == gameObject)
                {
                    GetComponent<Lasers>().removeLasers();
                    GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars().Remove(gameObject);  //Removing car from the list
                    Destroy(gameObject);    //Removing car from the scence
                    break;
                }
            }
        }
    }

    public void changeCamera()
    {
        CarControllerAI controller = GameObject.Find("CarController").GetComponent<CarControllerAI>();
        List<GameObject> cars = controller.getCars();
        if(cars.Count == 2)
        {
            controller.best_dna1 = cars[0].GetComponent<Car>().getDNA();
            controller.best_dna2 = cars[1].GetComponent<Car>().getDNA();
        }
        if(cars.Count == 1)
        {
            if (!controller.best_dna1.Equals(cars[0].GetComponent<Car>().getDNA()))
            {
                DNA inter = controller.best_dna2;
                controller.best_dna2 = controller.best_dna1;
                controller.best_dna1 = inter;
            }

            controller.genNewPopulation(true);

            GetComponent<Lasers>().removeLasers();
            Destroy(gameObject);
        }
        else
        {
            int rand = Random.Range(0, (int)cars.Count);
            if (cars[rand] == gameObject)   //Don't forget gameObject refers to the current object
            {
                changeCamera();
            }
            else
            {
                if (gameObject == GameObject.Find("Camera").GetComponent<CameraMovement>().getFollowing())
                {
                    GameObject.Find("Camera").GetComponent<CameraMovement>().Follow(cars[rand]);

                    //GetComponent<Lasers>().removeLasers();
                    //cars.Remove(gameObject);
                    //Destroy(gameObject);
                }
            }
        }
        
        //Debug.Log("Car Count: "+cars.Count.ToString());
    }
}
