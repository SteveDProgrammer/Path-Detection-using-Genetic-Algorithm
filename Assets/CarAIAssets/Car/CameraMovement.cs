using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject first_car;
    public float speed_rot_camera = 0.2f;
    public float rot_rate = 500f;
    private float rotation = 0;
    public float time_animation = 1; //in seconds

    public GameObject follow;
    public bool end;
    public Vector3 init_position;
    public float init_time;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject>cars = GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars();
        follow = cars[Random.Range(0, cars.Count - 1)];
        init_position = transform.position;

        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(end == true) return;
        if(follow != null)
        {
            float time_passed = (Time.time - init_time);
            float proportion = time_passed / time_animation;
            Vector3 curr_position;
            if (proportion < 1)
            {
                curr_position = Vector3.Lerp(init_position, follow.transform.position, proportion);
            }
            else
            {
                curr_position = follow.transform.position;
            }
            transform.position = new Vector3(curr_position.x, curr_position.y + 11.21f, curr_position.z - 17.91f);
            transform.LookAt(curr_position);
            transform.Translate(Vector3.right * Time.deltaTime * rotation * 5);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            List<GameObject> cars = GameObject.Find("CarController").GetComponent<CarControllerAI>().getCars();
            int index = cars.IndexOf(follow);
            if (index == cars.Count - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }
            Follow(cars[index]);
        }
    }

    public void Follow(GameObject new_car)
    {
        init_position = follow.transform.position;
        init_time = Time.time;
        follow = new_car;
    }

    public void unFollow()
    {
        follow = null;
    }

    public GameObject getFollowing()
    {
        return follow;
    }
}
