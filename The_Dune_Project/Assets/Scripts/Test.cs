using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fight;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Player is taking damage");
            other.GetComponent<Fighter>().TakeDamage(500);
            
        }
    }
}
