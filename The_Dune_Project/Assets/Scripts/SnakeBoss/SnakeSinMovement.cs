using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSinMovement : MonoBehaviour
{

    [SerializeField] float magnitude = 3f;
    [SerializeField] float speed = 0.5f;

    [SerializeField] float rotationMagnitude = 60f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = new Vector3(Mathf.Sin(Time.time * speed) * magnitude, transform.localPosition.y, transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(0f, Mathf.Sin((Time.time * speed) + Mathf.PI) * rotationMagnitude, 0f);

    }
}
