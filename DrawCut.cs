using Parabox.Stl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawCut : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;
    
    Camera cam;
    Scene scene;
    public GameObject obj;
    
    void Start() {
        Physics.autoSimulation = false;
    }

    float numberOfCuts = 5;

    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            /*
            string path = Path.Combine(Application.persistentDataPath, "data");
            Debug.Log(path);
            float objectX = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x * gameObject.transform.localScale.x;
            float xIndex = (objectX / 2) * -1;
            float xInterval = objectX / (numberOfCuts + 1);
            Debug.Log("\nObject X value is: " + objectX);
            Debug.Log("\nObject X index value is: " + xIndex);
            Debug.Log("\nObject X interval value is: " + xInterval);
            float objectZ = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.z * gameObject.transform.localScale.z;
            float zIndex = (objectZ / 2);
            float zInterval = objectZ / (numberOfCuts + 1);
            Debug.Log("\nObject Z value is: " + objectZ);
            Debug.Log("\nObject Z index value is: " + zIndex);
            Debug.Log("\nObject Z interval value is: " + zInterval);
            */
            
            float objectX = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x * gameObject.transform.localScale.x;
            float xIndex = (objectX / 2) * -1;
            float xInterval = objectX / (numberOfCuts+1);
            for(int i=0; i<numberOfCuts; i++)
            {
                xIndex += xInterval;
                pointA = new Vector3(xIndex,0,1);
                pointB = new Vector3(xIndex,1,0);
                pointC = new Vector3(xIndex,0,0);
                CreateSlicePlane(gameObject);
                if (i == numberOfCuts - 1)
                {
                    List<GameObject> rootObjects = new List<GameObject>();
                    scene = SceneManager.GetActiveScene();
                    scene.GetRootGameObjects(rootObjects);

                    for (int j = 0; j < rootObjects.Count; j++)
                    {
                        GameObject newObject = rootObjects[j];
                        if (newObject.name.Equals("New Game Object"))
                        {
                            float newObjectZ = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.z * gameObject.transform.localScale.z;
                            float newZIndex = (newObjectZ / 2);
                            float newZInterval = newObjectZ / (numberOfCuts+1);
                            for (int k = 0; k < numberOfCuts; k++)
                            {
                                newZIndex -= newZInterval;
                                pointA = new Vector3(1, 0, newZIndex);
                                pointB = new Vector3(0, 1, newZIndex);
                                pointC = new Vector3(0, 0, newZIndex);
                                CreateSlicePlane(newObject);
                            }
                        }
                    }
                }
            }

            float objectZ = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.z * gameObject.transform.localScale.z;
            float zIndex = (objectZ / 2);
            float zInterval = objectZ / (numberOfCuts + 1);
            for (int i = 0; i < numberOfCuts; i++)
            {
                zIndex -= zInterval;
                pointA = new Vector3(1, 0, zIndex);
                pointB = new Vector3(0, 1, zIndex);
                pointC = new Vector3(0, 0, zIndex);
                CreateSlicePlane(gameObject);
            }
            gameObject.name = "New Game Object";

            List<GameObject> objects = new List<GameObject>();
            scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(objects);
            int index = 1;
            for(int i = 0; i < objects.Count; i++)
            {
                string path = Path.Combine(Application.persistentDataPath, "data");
                path = Path.Combine(path, "tile" + index + ".stl");

                GameObject objMeshToExport = objects[i];
                if(objMeshToExport.name.Equals("New Game Object"))
                {
                    Mesh mesh = objMeshToExport.GetComponent<MeshFilter>().mesh;

                    //Create Directory if it doesn't exist
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }

                    Exporter.WriteFile(path, mesh, FileType.Ascii);
                    index++;
                }
            }
        }
    }

    void CreateSlicePlane(GameObject obj) {
        Vector3 centre = (pointA+pointB)/2;
        Vector3 up = Vector3.Cross((pointB-pointA),(pointC-pointA)).normalized;
        
        Cutter.Cut(obj, centre, up,null,true,true);
    }
}
