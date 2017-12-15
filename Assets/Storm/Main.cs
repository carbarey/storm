using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour {

    public GameObject cubePrefab;
    public GameObject obstaclePrefab;
    
    public int numberOfCubes;
    public int numberOfObstacles;
    public float strength;
    public float minStrength2;
    public float interiorStrength;
    public float distTriangleScale;
    public float obstacleSpeed;

    private GameObject[] myCubes;
    private GameObject[] myObstacles;    

    private Transform mySphereLeft;
    private Transform mySphereRight;
    private Transform mySphereHead;
    private Transform riftHeadset;

    private Transform myPlane;

    private bool existAvatar;

    public float sizefactor;

    void Start()
    {

        
        existAvatar = (GameObject.Find("LocalAvatar") != null);

        mySphereLeft = GameObject.Find("SphereLeft").transform;
        mySphereRight = GameObject.Find("SphereRight").transform;
        mySphereHead = GameObject.Find("SphereHead").transform;
        if (existAvatar) riftHeadset = GameObject.Find("CenterEyeAnchor").transform;

        myPlane = GameObject.Find("Plane").transform;

        GameObject CubesContainer = new GameObject("CubesContainer");

        myCubes = new GameObject[numberOfCubes];
        myObstacles = new GameObject[numberOfObstacles];


        for (int i = 0; i < numberOfCubes; i++)
        {
            myCubes[i] = (GameObject)Instantiate(cubePrefab, CubesContainer.transform);
            myCubes[i].transform.position = new Vector3(Random.Range(0f, 1f), -0.8f, Random.Range(-1f, 0f));
        }
                
        for (int i = 0; i < numberOfObstacles; i++)
        {
            myObstacles[i] = (GameObject)Instantiate(obstaclePrefab);
            //myObstacles[i].transform.position = new Vector3(Random.Range(-10.0f, -5.0f), Random.Range(-1.0f, 3f), Random.Range(-5f, 5f));
            //myObstacles[i].transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(6f, 3f), Random.Range(-5f, 5f));
            myObstacles[i].transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(2f, -0.6f), Random.Range(1.2f, 10f));

            Physics.IgnoreCollision(myObstacles[i].GetComponent<Collider>(), myPlane.GetComponent<Collider>());
        }


       
        
    }



    void Update() 
    {
        

        if (existAvatar)
        {

            

            Vector3 rightHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 leftHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Vector3 headPosition = riftHeadset.position;

            sizefactor = Mathf.Abs(leftHandPosition.x - rightHandPosition.x); 

            mySphereLeft.transform.position = new Vector3(leftHandPosition.x, -0.8f, leftHandPosition.z + 0.2f);
            mySphereRight.transform.position = new Vector3(rightHandPosition.x, -0.8f, rightHandPosition.z + 0.2f);
            mySphereHead.transform.position = new Vector3(headPosition.x, -0.8f, headPosition.z + 0.2f);

        }

    }
        
}
