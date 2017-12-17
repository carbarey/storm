using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExtraCubePrefab : MonoBehaviour {

    public GameObject cubePrefab;

    private float obstacleSpeed;

    private GameObject myNewCube;
    private Vector3 myNewCubePosition;
    

    void Start () {
       
        
    }



    void Update () {

        obstacleSpeed = GameObject.Find("Main").GetComponent<Main>().obstacleSpeed;
                
        this.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5.0f, 5f), Random.Range(-5.0f, 5f), -20f * (2.0f)) * obstacleSpeed;

        if (transform.position.z < -2.5f)
        {
            transform.position = new Vector3(Random.Range(-1f, 1f), -0.6f, Random.Range(10f, 20f));
        }

                
    }
    
    

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "obstacle")
        {
           // Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "cube")
        {
            myNewCubePosition = gameObject.transform.position;
            Destroy(gameObject);

            myNewCube = (GameObject)Instantiate(cubePrefab);
            myNewCube.transform.position = myNewCubePosition;
                       

        }


    }


}
