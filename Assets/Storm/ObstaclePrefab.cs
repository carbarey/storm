using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePrefab : MonoBehaviour {

    private float obstacleSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        obstacleSpeed = GameObject.Find("Main").GetComponent<Main>().obstacleSpeed;

        this.GetComponent<Rigidbody>().velocity = new Vector3(10f,0f,0f) * obstacleSpeed;

    }
}
