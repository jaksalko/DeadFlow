using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject towerOutsideCubePrefab; // for tower wall design
    [SerializeField]
    private GameObject towerInnerCubePrefab; // for tower inner design
    [SerializeField]
    private GameObject towerFloorPerfab;
    [SerializeField]
    private FloorGenerator floorGenerator;

    [SerializeField]
    private Transform towerDesign;

    public int height;
    public int size;
    
    
    public int holeAreaFrequency;

    public void Centralize()
    {
        Vector2 floorCenterPoint = floorGenerator.GetCenterPoint();
        floorCenterPoint = floorCenterPoint - new Vector2(size / 2, size / 2);
        towerDesign.position = new Vector3(floorCenterPoint.x, 0, floorCenterPoint.y);

    }

    public void MakeTower()
    {
        //Build inside
        towerDesign.transform.position = default;

        GameObject cube;
        for (int h = 0; h < height; h++)
        {
            GameObject towerfloor = Instantiate(towerFloorPerfab, new Vector3(0, h, 0), Quaternion.identity);
            towerfloor.name = "Floor" + h;
            towerfloor.transform.SetParent(towerDesign);
            for(int x = 1; x <size-1; x++)
            {
                
                for(int y = 1; y < size-1; y++)
                {
                    
                    cube = Instantiate(towerInnerCubePrefab, new Vector3(x, h, y), Quaternion.identity);
                    cube.name = "Cube(" + x + "," + h +","+y+ ")";
                    cube.transform.SetParent(towerfloor.transform);
                }
            }

            for(int x = 0; x < size; x++)
            {
                if (x == 0 || x == size-1)
                {
                    for(int y = 0; y < size; y++)
                    {
                        cube = Instantiate(towerOutsideCubePrefab, new Vector3(x, h, y), Quaternion.identity);
                        cube.name = "Cube(" + x + "," + h + "," + y + ")";
                        cube.transform.SetParent(towerfloor.transform);
                    }
                    
                }
                else
                {
                    cube = Instantiate(towerOutsideCubePrefab, new Vector3(x, h, 0), Quaternion.identity);
                    cube.name = "Cube(" + x + "," + h + ",0)";
                    cube.transform.SetParent(towerfloor.transform);

                    cube = Instantiate(towerOutsideCubePrefab, new Vector3(x, h, size-1), Quaternion.identity);
                    cube.name = "Cube(" + x + "," + h + ","+(size-1)+")";
                    cube.transform.SetParent(towerfloor.transform);
                }
              
            }
        }
    }

    public void ClearTower()
    {
        towerDesign.transform.position = default;

        int objectCount = towerDesign.childCount;
        for (int i = 0; i < objectCount; i++)
        {
            DestroyImmediate(towerDesign.GetChild(0).gameObject);
        }
    }

    public void MoveRight()
    {
        towerDesign.position += Vector3.right;
    }
    public void MoveLeft()
    {
        towerDesign.position += Vector3.left;
    }
    public void MoveUp()
    {
        towerDesign.position += Vector3.forward;
    }
    public void MoveDown()
    {
        towerDesign.position += Vector3.back;
    }

}
