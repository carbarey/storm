using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour {

    public GameObject cubePrefab;

    public int numberOfCubes;
    public float strength;
    public float minStrength2;
    public float interiorStrength;
    public float distTriangleScale;

    private GameObject[] myCubes;

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

        for (int i = 0; i < numberOfCubes; i++)
        {
            myCubes[i] = (GameObject)Instantiate(cubePrefab, CubesContainer.transform);
            myCubes[i].transform.position = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
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

            mySphereLeft.position = leftHandPosition;
            mySphereRight.position = rightHandPosition;
            mySphereHead.position = riftHeadset.position - new Vector3(0, 0.5f, 0);
        }

    }
        
}
