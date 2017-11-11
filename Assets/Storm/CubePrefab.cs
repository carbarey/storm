using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CubePrefab : MonoBehaviour {

    private GameObject mySphereLeft;
    private GameObject mySphereRight;
    private GameObject mySphereHead;

    private Vector3 triangleCenter;
    private float strength;
    private float minStrength2;
    private float interiorStrength;
    private float distTriangleScale;


    void Start () {

        mySphereLeft = GameObject.Find("SphereLeft");
        mySphereRight = GameObject.Find("SphereRight");
        mySphereHead = GameObject.Find("SphereHead");
    }



    void Update () {

        distTriangleScale = GameObject.Find("Main").GetComponent<Main>().distTriangleScale;

        strength = GameObject.Find("Main").GetComponent<Main>().strength;
        minStrength2 = GameObject.Find("Main").GetComponent<Main>().minStrength2;
        interiorStrength = GameObject.Find("Main").GetComponent<Main>().interiorStrength;

        UpdateTriangleCenter();

        Vector2 thisPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 p0 = new Vector2(mySphereLeft.transform.position.x, mySphereLeft.transform.position.z);
        Vector2 p1 = new Vector2(mySphereRight.transform.position.x, mySphereRight.transform.position.z);
        Vector2 p2 = new Vector2(mySphereHead.transform.position.x, mySphereHead.transform.position.z);
                
        bool moving = ! PointInTriangle(thisPos, p0, p1, p2);
        
        if ( moving ){

            float distTriangle = Mathf.Min(minimum_distance(p0,p1,thisPos), minimum_distance(p1, p2, thisPos), minimum_distance(p2, p0, thisPos));
            float strength2 = distTriangle / distTriangleScale + minStrength2;

            // transform.position = Vector3.Lerp(transform.position, triangleCenter, 0.01f * strength);
            this.GetComponent<Rigidbody>().AddForce( (triangleCenter - transform.position).normalized * strength * strength2);
            
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f);
        
        } else {

            Vector3 randomForceVector = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f, 1f));            
            this.GetComponent<Rigidbody>().AddForce(randomForceVector * interiorStrength);
            
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,0f,1f);
        }

    }
    
    
    void UpdateTriangleCenter(){
        Vector3 hands_center = 0.5f * (mySphereLeft.transform.position + mySphereRight.transform.position);
        triangleCenter = 0.5f * (hands_center + mySphereHead.transform.position); 
    }
    

    float minimum_distance(Vector2 v, Vector2 w, Vector2 p)  {
        // Return minimum distance between line segment vw and point p
        float l2 = Vector2.Distance(v, w) * Vector2.Distance(v, w);  // i.e. |w-v|^2 -  avoid a sqrt
        if (Vector2.Distance(v, w) == 0.0f) return Vector2.Distance(p, v);   // v == w case
                                                // Consider the line extending the segment, parameterized as v + t (w - v).
                                                // We find projection of point p onto the line. 
                                                // It falls where t = [(p-v) . (w-v)] / |w-v|^2
                                                // We clamp t from [0,1] to handle points outside the segment vw.
        float t = Mathf.Max(0, Mathf.Min(1, Vector2.Dot(p - v, w - v) / l2));
        Vector2 projection = v + t * (w - v);  // Projection falls on the segment
        return Vector2.Distance(p, projection);
    }

    bool PointInTriangle(Vector2 p, Vector2 p0, Vector2 p1, Vector2 p2) {
        float A = 0.5f * (-p1.y * p2.x + p0.y * (-p1.x + p2.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y);
        float sign = A < 0f ? -1f : 1f;
        float s = (p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y) * sign;
        float t = (p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y) * sign;
        
        return s > 0f && t > 0f && (s + t) < 2f * A * sign;
    }

}
