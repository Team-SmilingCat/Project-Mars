using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool canStop;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 60 * 3 && timer > 5)
        {
            gameObject.GetComponent<RectTransform>().Translate(Vector3.up * speed * Time.deltaTime);   
        }

        timer += Time.deltaTime;

    }
}
