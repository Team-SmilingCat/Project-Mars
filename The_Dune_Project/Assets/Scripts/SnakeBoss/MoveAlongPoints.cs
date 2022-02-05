using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPoints : MonoBehaviour
{

    [SerializeField] Transform[] points;
    [SerializeField] public float speed = 10f;
    Transform currentTarget;
    Vector3 startPos;
    Quaternion startRot;

    int index = 0;

    bool moving = false;



    // Start is called before the first frame update
    void Start()
    {
        StartMovement();
    }

    private void StartMovement()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        currentTarget = points[index];
        StartCoroutine(MoveToTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == currentTarget.position && !moving)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }

            StartMovement();
        }


    }

    private IEnumerator MoveToTarget()
    {
        moving = true;

        float t = 0f;
        float distance = Vector3.Distance(transform.position, currentTarget.position);
        float timeToTarget = distance / speed;

        while (transform.position != currentTarget.position)
        {
            t = t + Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, currentTarget.position, t / timeToTarget);
            transform.rotation = Quaternion.Lerp(startRot, currentTarget.rotation, t / timeToTarget);

            yield return null;
        }

        moving = false;
    }
}
