using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCut : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;
    
    Camera cam;
    public GameObject obj;

    void Start() {
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
			    obj = hit.collider.gameObject; 
			}
        }
        
        if (Input.GetKeyDown("space")) 
        {
            //float xIndex = obj.GetComponent<MeshFilter>().mesh.bounds.size.x * obj.transform.localScale.x;
            //float zIndex = obj.GetComponent<MeshFilter>().mesh.bounds.size.z * obj.transform.localScale.z;
           
            float xIndex = -5;
            for(int i=0; i<3; i++)
            {
                pointA = new Vector3(xIndex,0,1);
                pointB = new Vector3(xIndex,1,0);
                pointC = new Vector3(xIndex,0,0);
                CreateSlicePlane();
                xIndex += 5;
            }
        }
        if (Input.GetKeyDown("z"))
        {
            float zIndex = 5;
            for(int i=0; i<3; i++)
            {
                pointA = new Vector3(1,0,zIndex);
                pointB = new Vector3(0,1,zIndex);
                pointC = new Vector3(0,0,zIndex);
                CreateSlicePlane();
                zIndex -= 5;
            }
        }
    }

    void CreateSlicePlane() {
        Vector3 centre = (pointA+pointB)/2;
        Vector3 up = Vector3.Cross((pointB-pointA),(pointC-pointA)).normalized;
        
        
        Cutter.Cut(obj, centre, up,null,true,true);
    }
}
