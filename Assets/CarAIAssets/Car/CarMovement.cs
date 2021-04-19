using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float vy_rot = 0;
    public float speed = 5f;
    public float rot_rate = 20f;
    private float vz;
    private float acceleration;
    private float increase_acc = 5;
    public bool active_acceleration = true;

    // Start is called before the first frame update
    void Start()
    {
        vz = speed;
    }

    // Update is called once per frame
    void Update()
    {
            float time = Time.deltaTime;
            transform.position += transform.forward * (vz * time + 0.5f * acceleration * time * time);
            transform.Rotate(new Vector3(0, vy_rot, 0));
            GetComponent<Lasers>().RotateLasers(vy_rot);
    }

    public float getAcceleration()
    {
        return acceleration;
    }

    public void updateMovement(List<float> outputs)
    {
        if (outputs[0] * 2 > 1f)
        {
            vy_rot = (outputs[0] * 2 - 1) * rot_rate * Time.deltaTime;
        }
        else
        {
            vy_rot = -(outputs[0] * 2) * rot_rate * Time.deltaTime;
        }

        if (outputs[1]*2>1f)
        {
            acceleration = (outputs[1] * 2 - 1) * increase_acc;
        }else
        {
            acceleration = -outputs[1] * 2 * increase_acc; 
        }
    }
}
