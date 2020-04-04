﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    public Map[] maps;
    public int mapIndex;

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Transform boxPrefab;
    public Transform mapFloor;
    public Transform navmeshFloor;
    public Transform navmeshMaskPrefab;
    public Vector2 maxMapSize;

    [Range(0, 1)]
    public float outlinePercent;

    

    public float tileSize;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Queue<Coord> shuffledOpenTileCoords;
    Transform[,] tileMap;

    Map currentMap;

    void Awake()
    {
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    void MakeRandomMap()//Coord(mapsize) , obs percent , boxcount  
    {
        Map nextMap = new Map(new Coord(20,10),0.1f,3);
        currentMap = nextMap;

    }

    void OnNewWave()
    {
        Debug.Log("on new wave...");
        
        MakeRandomMap();
        GenerateMap();
    }

    public void GenerateMap()
    {
        //currentMap = maps[mapIndex];
        tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];
        System.Random prng = new System.Random(currentMap.seed);

        // Generating coords
        allTileCoords = new List<Coord>();
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), currentMap.seed));

        // Create map holder object
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // Spawning tiles
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
                newTile.parent = mapHolder;
                tileMap[x, y] = newTile;
            }
        }

        //Side Wall
        #region

        for (int x = -1; x < currentMap.mapSize.x +1; x++)
        {
            float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
            Vector3 obstaclePosition = CoordToPosition(x, currentMap.mapSize.y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
            newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

            Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
            Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
            float colourPercent = currentMap.mapSize.y / (float)currentMap.mapSize.y;
            obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
            obstacleRenderer.sharedMaterial = obstacleMaterial;

            obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
            obstaclePosition = CoordToPosition(x, -1);
            newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;

            newObstacle.parent = mapHolder;
            newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

            obstacleRenderer = newObstacle.GetComponent<Renderer>();
            obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
            colourPercent = 0 / (float)currentMap.mapSize.y;
            obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
            obstacleRenderer.sharedMaterial = obstacleMaterial;


            
        }
        for (int y = 0; y < currentMap.mapSize.y; y++)
        {
            float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
            Vector3 obstaclePosition = CoordToPosition(currentMap.mapSize.x, y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
            newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

            Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
            Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
            float colourPercent = currentMap.mapSize.y / (float)currentMap.mapSize.y;
            obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
            obstacleRenderer.sharedMaterial = obstacleMaterial;

            obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
            obstaclePosition = CoordToPosition(-1, y);
            newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;

            newObstacle.parent = mapHolder;
            newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

            obstacleRenderer = newObstacle.GetComponent<Renderer>();
            obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
            colourPercent = 0 / (float)currentMap.mapSize.y;
            obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
            obstacleRenderer.sharedMaterial = obstacleMaterial;
        }
        #endregion


        // Spawning obstacles
        #region

        bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

        int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
        int boxCount = currentMap.boxCount;
        int currentObstacleCount = 0;
        List<Coord> allOpenCoords = new List<Coord>(allTileCoords);

        for (int i = 0; i < obstacleCount + boxCount ; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != currentMap.mapCentre && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
               

                //if obstacle is wall
                if (i < obstacleCount)
                {
                    float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, (float)prng.NextDouble());
                    Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                    Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity) as Transform;
                    newObstacle.parent = mapHolder;
                    newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, obstacleHeight, (1 - outlinePercent) * tileSize);

                    Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                    Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                    float colourPercent = randomCoord.y / (float)currentMap.mapSize.y;
                    obstacleMaterial.color = Color.Lerp(currentMap.foregroundColour, currentMap.backgroundColour, colourPercent);
                    obstacleRenderer.sharedMaterial = obstacleMaterial;
                }
                else
                {
                    Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                    Transform newObstacle = Instantiate(boxPrefab, obstaclePosition, Quaternion.identity) as Transform;
                    newObstacle.parent = mapHolder;
                    newObstacle.localScale = new Vector3((1 - outlinePercent) * tileSize, (1 - outlinePercent) * tileSize, (1 - outlinePercent) * tileSize);
                }

                //if obstacle is box


                allOpenCoords.Remove(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }
        #endregion

        shuffledOpenTileCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), currentMap.seed));

        // Creating navmesh mask
        Transform maskLeft = Instantiate(navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Transform maskRight = Instantiate(navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

        Transform maskTop = Instantiate(navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

        Transform maskBottom = Instantiate(navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

        navmeshFloor.localScale = new Vector3(maxMapSize.x, maxMapSize.y) * tileSize;
        mapFloor.localScale = new Vector3(currentMap.mapSize.x * tileSize, currentMap.mapSize.y * tileSize);
    }
    //end generate map

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.mapCentre);
        mapFlags[currentMap.mapCentre.x, currentMap.mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-currentMap.mapSize.x / 2f + 0.5f + x, 0, -currentMap.mapSize.y / 2f + 0.5f + y) * tileSize;
    }

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (currentMap.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / tileSize + (currentMap.mapSize.y - 1) / 2f);
        x = Mathf.Clamp(x, 0, tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileMap.GetLength(1) - 1);
        return tileMap[x, y];
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenTileCoords.Dequeue();
        shuffledOpenTileCoords.Enqueue(randomCoord);
        return tileMap[randomCoord.x, randomCoord.y];
    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }

    }
    

    [System.Serializable]
    public class Map
    {

        Color newColor()
        {
            Color color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            return color;
        }

        public Map(Coord size , float obs , int box)
        {
            mapSize = size;
            obstaclePercent = obs;
            boxCount = box;

            seed = Random.Range(1,255);
            minObstacleHeight = 1;
            maxObstacleHeight = 3;

            foregroundColour = newColor();
            backgroundColour = newColor();
        }

        public Coord mapSize;
        [Range(0, 1)]
        public float obstaclePercent;
        public int seed;
        public float minObstacleHeight;
        public float maxObstacleHeight;
        public Color foregroundColour;
        public Color backgroundColour;

        [Range(0,10)]
        public int boxCount;

        public Coord mapCentre
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }



    }
}