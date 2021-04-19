using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finale : MonoBehaviour
{
    private GameObject[] cars;
    [SerializeField]
    private ParticleSystem fireworks;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Image bingo;

    private Color temp;

    // Start is called before the first frame update
    void Start()
    {
        bingo.enabled = false;
        fireworks.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (bingo.enabled)
        {
            temp = bingo.color;
            temp.a += 0.05f;

            if (bingo.color.a >= 0.9f)
            {
                temp.a = 0.1f;
                bingo.color = temp;
            }
            bingo.color = temp;
        }
    }

    public void stop()
    {
        fireworks.Play();
        bingo.enabled = true;
        cam.GetComponent<CameraMovement>().end = true;
        cars = GameObject.FindGameObjectsWithTag("Car");
        for(int i=0; i< cars.Length; i++)
        {
            cars[i].GetComponent<CarMovement>().enabled = false;
        }
        cam.position = new Vector3(785f, 32f, 195f);
        cam.rotation = Quaternion.Euler(22f, 0f, 0f);
    }
}
