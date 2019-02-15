using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGenLevel : MonoBehaviour
{
    public Tilemap levelMap;
    public Tile tile;
    public int height, width;
    private int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        levelMap.ClearAllTiles();
        initializeMap();
        createMap();
        renderMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeMap(){
        map = new int[width, height];
        
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x, y] = 0;
            }
        }
    }

    void renderMap(){

        //Clear the map (ensures we dont overlap)
        levelMap.ClearAllTiles(); 
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0) ; x++) 
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++) 
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1) 
                {
                    levelMap.SetTile(new Vector3Int(x, y, 0), tile); 
                }
            }
        }
    }

    

    void createMap(){
        int newPoint;
        //Used to reduced the position of the Perlin point
        float reduction = 0.5f;
        //Create the Perlin

        float seed = Random.Range(100f, 100000f);

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * 4);
 
            //Make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2); 
            for (int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }

    }

    void createBoulder(int xLower, int xUpper, int yLower, int yUpper){

        int newPoint;
        //Used to reduced the position of the Perlin point
        float reduction = 0.5f;

        float seed = Random.Range(100f, 100000f);

        for (int x = xLower; x < xUpper; x++)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * ((yUpper - yLower)/2));
 
            //Make sure the noise starts near the halfway point of the height
            newPoint += yLower + ((yUpper - yLower)/2); 
            for (int y = newPoint; y >= yLower; y--)
            {
                map[x, y] = 1;
            }
        }

    }
}
