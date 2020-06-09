using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;
    [SerializeField]
    private Transform floorDesign;
    public int width;//x
    public int height;//z

    public Transform centerPoint;

    public Vector2 GetCenterPoint()
    {
        
        return new Vector2(width / 2, height / 2);
    }


    public void MakeFloor()
    {
        GameObject floor;
        for(int h = 0; h < height ; h++)
        {
            for(int w = 0; w < width; w++)
            {
                floor = Instantiate(floorPrefab , new Vector3(w,0,h) , Quaternion.identity);
                floor.name = "Floor(" + w+","+h+")";
                floor.transform.SetParent(floorDesign);               
            }
        }
        centerPoint.position = new Vector3((float)width / 2, 0, (float)height / 2);

    }

    public void ClearFloor()
    {
        int objectCount = floorDesign.childCount;
        for(int i = 0; i < objectCount; i++)
        {
            DestroyImmediate(floorDesign.GetChild(0).gameObject);
        }
    }
}
