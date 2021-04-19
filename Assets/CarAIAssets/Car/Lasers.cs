using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public Color color_beam = new Color(0, 255, 0, 0.5f);
    public int distance_laser = 20;
    static int laser_count = 5;
    public static int view = 120; //In degrees
    public float height = 0;

    private int count;
    private GameObject[] laser_objects;

    // Start is called before the first frame update
    void Start()
    {
        count = view / (laser_count - 1);
        laser_objects = new GameObject[laser_count];

        for (int i = 0; i < laser_count; i++)
        {
            float current_degree = count * i - view/2;
            GameObject obj = new GameObject();
            LaserItem laser = obj.AddComponent<LaserItem>();

            laser.GetComponent<LaserItem>().final_length = 0.02f;
            laser.laser_color = color_beam;
            laser.distance_laser = distance_laser;
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);

            laser_objects[i] = obj;
            laser_objects[i].transform.Rotate(new Vector3(0, current_degree, 0));
        }
    }

    public float[] getDistances()
    {
        float[] lasers = new float[laser_objects.Length];
        for (int i = 0; i < laser_objects.Length; i++)
        {
            if(laser_objects[i])    //Compromise
            lasers[i] = laser_objects[i].GetComponent<LaserItem>().getDistance();
        }

        return lasers;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in laser_objects)
        {
            if(obj)     //Compromise
            obj.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        }
    }

    public void RotateLasers(float rot)
    {
        foreach (GameObject obj in laser_objects)
        {
            if(obj)     //Compromise
            obj.transform.Rotate(new Vector3(0, rot, 0));
        }
    }

    public void removeLasers()
    {
        for (int i = 0; i < laser_count; i++)
        {
            Destroy(laser_objects[i]);
        }
        
    }
}
