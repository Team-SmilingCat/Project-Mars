using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSegmentMovement : MonoBehaviour
{

    [SerializeField] Transform parent;

    // Record parent position and rotation at t1
    // At t2 ... tn, move to parent position
    bool moving = false;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponentInParent<MoveAlongPoints>().speed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, parent.position);
        float t = distance / speed;

        transform.position = Vector3.Lerp(transform.position, parent.position, Time.deltaTime / t);
        transform.rotation = Quaternion.Lerp(transform.rotation, parent.rotation, Time.deltaTime / t);

    }

}


