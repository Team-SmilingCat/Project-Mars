using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DampedTransformController : MonoBehaviour
{

    [Range(0, 1)] [SerializeField] float dampPosition = 0.5f;
    [Range(0, 1)] [SerializeField] float dampRotation = 0.5f;

    [SerializeField] bool maintainAim = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (DampedTransform dampedTransform in GetComponentsInChildren<DampedTransform>())
        {
            dampedTransform.data.dampPosition = dampPosition;
            dampedTransform.data.dampRotation = dampRotation;
            dampedTransform.data.maintainAim = maintainAim;
        }


    }
}
