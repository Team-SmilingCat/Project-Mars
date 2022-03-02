using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //public variables
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    float cubesPivotDistance;
    Vector3 cubesPivot;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    //new material
    [SerializeField] Material newMaterial;
    

    void Start() 
    {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }

    void Update() 
    {
        
    }

    public void explode() 
    {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int i = 0; i < cubesInRow; i++) 
        {
            for (int j = 0; j < cubesInRow; j++) 
            {
                for (int k = 0; k < cubesInRow; k++) 
                {
                    createPiece(i, j, k);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders) 
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

    }

    void createPiece(int x, int y, int z) 
    {
        //generate singletons
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.GetComponent<Renderer>().material = newMaterial;
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        //delay();
        //destroy object to free up space
        Destroy(piece, 5);


    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(10);
        
    }

}
