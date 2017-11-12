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

    private bool existAvatar;


    void Start()
    {


        existAvatar = (GameObject.Find("LocalAvatar") != null);

        mySphereLeft = GameObject.Find("SphereLeft").transform;
        mySphereRight = GameObject.Find("SphereRight").transform;
        mySphereHead = GameObject.Find("SphereHead").transform;
        if (existAvatar) riftHeadset = GameObject.Find("CenterEyeAnchor").transform;


        GameObject CubesContainer = new GameObject("CubesContainer");

        myCubes = new GameObject[numberOfCubes];
        myObstacles = new GameObject[numberOfObstacles];


        for (int i = 0; i < numberOfCubes; i++)
        {
            myCubes[i] = (GameObject)Instantiate(cubePrefab, CubesContainer.transform);
            myCubes[i].transform.position = new Vector3(Random.Range(0f, 1f), -1.0f, Random.Range(-1f, 0f));
        }
                
        for (int i = 0; i < numberOfObstacles; i++)
        {
            myObstacles[i] = (GameObject)Instantiate(obstaclePrefab);
            myObstacles[i].transform.position = new Vector3(Random.Range(-4.0f, 0f), -1.0f, Random.Range(-1f, 0f));
        }


    }



    void Update() 
    {
        

        if (existAvatar)
        {

            Vector3 rightHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 leftHandPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Vector3 headPosition = riftHeadset.position;

            print(leftHandPosition);

            mySphereLeft.transform.position = new Vector3(leftHandPosition.x, -1.0f, leftHandPosition.z);
            mySphereRight.transform.position = new Vector3(rightHandPosition.x, -1.0f, rightHandPosition.z);
            mySphereHead.transform.position = new Vector3(headPosition.x, -1.0f, headPosition.z);

        }

    }
        
}
