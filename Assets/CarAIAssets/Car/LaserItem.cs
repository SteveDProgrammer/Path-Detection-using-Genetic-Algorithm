using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserItem : MonoBehaviour
{
    public Color laser_color = new Color(0, 1, 0, 0.5f);
    public float distance_laser = 50;
    public float final_length = 0.02f;
    public float initial_length = 0.02f;

    private Vector3 position_laser;
    private LineRenderer line_renderer;
    private float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        distance = distance_laser;
        position_laser = new Vector3(0, 0, final_length);
        line_renderer = gameObject.AddComponent<LineRenderer>();
        line_renderer.material = new Material(Shader.Find("Particles/Priority Additive"));
        line_renderer.startColor = laser_color;
        line_renderer.endColor = laser_color;
        line_renderer.startWidth = final_length;
        line_renderer.endWidth = final_length;
        line_renderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 final_point = transform.position + transform.forward * distance_laser;
        RaycastHit collision_point;
        if(Physics.Raycast(transform.position, transform.forward, out collision_point, distance_laser))
        {
            line_renderer.SetPosition(0, transform.position);
            line_renderer.SetPosition(1, collision_point.point);
            distance = collision_point.distance;
        }
        else
        {
            line_renderer.SetPosition(0, transform.position);
            line_renderer.SetPosition(1, final_point);
            distance = distance_laser;
        }
    }

    public float getDistance()
    {
        return distance/distance_laser;
    }
}
