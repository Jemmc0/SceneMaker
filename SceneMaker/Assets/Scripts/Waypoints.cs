using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoints : MonoBehaviour
{
    public int GridX;
    public int GridY;
    public float x, y, z;
    public float sizeX, sizeY, sizeZ, posY;
    public int countx, countz;
    public GameObject[,] testArray;
    public int i, j;
    RaycastHit hit;

    void Update()
    {
                
    }
    public void CreateTarget(int countX, int countY)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            DestroyImmediate(gameObject.transform.GetChild(i).gameObject,false);


        sizeX = GetComponent<Renderer>().bounds.size.x;
        sizeY= GetComponent<Renderer>().bounds.size.y;
        sizeZ = GetComponent<Renderer>().bounds.size.z;
        x = sizeX / countX;
        z = sizeZ / countY;
        
        testArray =new GameObject[countX+1,countY+1];
        for (i = 0; i < countX+1; i++)
        {
            testArray[i , j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            testArray[i, j].transform.position = transform.position + new Vector3((-sizeX / 2) + (i * x), (sizeY / 2), (-sizeZ / 2) + (j * z));
            testArray[i, j].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            testArray[i, j].transform.SetParent(gameObject.transform);
            testArray[i, j].name = "Waypont "+ i + "," + j;
            //testArray[i, j].GetComponent<MeshRenderer>().enabled = false;
            

            for (j = 1; j < countY+1; j++)
            {
                testArray[i, j] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                testArray[i, j].transform.position = transform.position + new Vector3((-sizeX / 2) + (i * x), (sizeY / 2), (-sizeZ / 2) + (j * z));
                testArray[i, j].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                testArray[i, j].transform.SetParent(gameObject.transform);
                testArray[i, j].name = "Waypont "+ i + "," + j;
                //testArray[i, j].GetComponent<MeshRenderer>().enabled = false;



            }
            j =0 ;
        }
        

        /*for (int i = 0; i <countx ; i++)
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube);

            for (int j = 0; j < countz; j++)
            {
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
        }*/

        /*
        if (wp != null)
            Destroy(wp);
        GameObject waypoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
        waypoint.transform.position = new Vector3(Random.Range(7.0f, -7.0f), 0.03f,
        Random.Range(7.0f, -7.0f));
        waypoint.name = "Waypoint " + id;
        id++;
        waypoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //waypoint.GetComponent<MeshRenderer>().enabled = false;
        wp = waypoint;*/
    }
}
