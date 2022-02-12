using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool platformMoved = false;
    public Vector3 moveDistance = new Vector3(0,0,0);
    public float moveSpeed = 0.1f;

    Vector3 goalPos;

    void Start()
    {
        goalPos = transform.localPosition + moveDistance;
    }

    void Update()
    {
        if (platformMoved) StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        if (transform.localPosition != goalPos){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPos, moveSpeed);
            yield return new WaitForSeconds(1);
            StartCoroutine(MovePlatform());
        }
        else Destroy(this); // TODO: Change this if platforms need to move more than once.
    }
}
