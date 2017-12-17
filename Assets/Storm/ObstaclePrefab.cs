using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefab : MonoBehaviour {

    private float obstacleSpeed;
    private float sizefactor;

    private float pullRadius;
    private float pullForce;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        obstacleSpeed = GameObject.Find("Main").GetComponent<Main>().obstacleSpeed;
        sizefactor = GameObject.Find("Main").GetComponent<Main>().sizefactor;


        pullRadius = GameObject.Find("Main").GetComponent<Main>().pullRadius;
        pullForce = GameObject.Find("Main").GetComponent<Main>().pullForce;

        this.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5.0f, 5f), -20f, -20f * (4.0f - sizefactor * 2f)) * obstacleSpeed ;

        if (transform.position.y < -2.5f)
        {
            transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(3f, -0.6f), Random.Range(3f, 10f));
        }


        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)){
            // calculate direction from target to me
            
            if (collider.gameObject.tag == "obstacle")
            {

                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce);

            }

        }


    }
}
