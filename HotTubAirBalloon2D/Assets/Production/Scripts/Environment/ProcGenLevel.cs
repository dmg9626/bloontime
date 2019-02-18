using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGenLevel : MonoBehaviour
{
    public Tilemap levelMap;
    public Tile tile;
    public int height, width, tunnelHeight, buffer, maxSlope, maxDisturb;
    private List<List<int>> map;
    private List<int> floor, ceiling;
    public List<int> levelShape;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        levelMap.ClearAllTiles();
        initializeMap();

        
        if(Random.Range(0,1) == 0){
            randomLevelShape();
        }else{
            disturbLevelShape();
        }
        

        //disturbLevelShape();

        buildFromLevelShape();
        addFloorCeilingToMap();
        renderMap();
        player.GetComponent<BalloonController>().moveBalloon(new Vector2(buffer+1, levelShape[0]+buffer+Mathf.FloorToInt(tunnelHeight/2)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeMap(){
        //map = new int[width+(buffer*2), height+(buffer*2)];
        map = new List<List<int>>();
        floor = new List<int>();
        ceiling = new List<int>();

        for (int x = 0; x < width+(buffer*2); x++)
        {
            map.Add(new List<int>());
            for (int y = 0; y < height+(buffer*2); y++)
            {
                if((x<buffer || x>width+buffer || y<buffer || y>height+buffer)){
                    map[x].Add(1);
                }else{
                    map[x].Add(0);
                }
            }
        }

    }

    void renderMap(){

        //Clear the map (ensures we dont overlap)
        levelMap.ClearAllTiles(); 

        //Loop through the width of the map
        for (int x = 0; x < width+(buffer*2); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < height+(buffer*2); y++)
            {
                // 1 = tile, 0 = no tile
                if (map[x][y] == 1) 
                {
                    levelMap.SetTile(new Vector3Int(x, y, 0), tile); 
                }
            }
        }
    }

    
    /*

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
    */


    void buildFromLevelShape(){

        int fillSpace = Mathf.FloorToInt(width/(levelShape.Count - 1));
        float heightGrowth = 0, curHeight = levelShape[0];
        int heightIndex = 0, curFill = 0;

        for(int x = buffer; x < width+buffer+1; x++){
            
            if(curFill >= fillSpace){
                curFill = 0;
                heightIndex += 1;
                if(heightIndex == levelShape.Count){
                    heightGrowth = 0;
                }else{
                    heightGrowth = (levelShape[heightIndex]-curHeight)/fillSpace;
                }
            }
            curHeight+=heightGrowth;
            floor.Add(Mathf.FloorToInt(curHeight));
            ceiling.Add(Mathf.FloorToInt(curHeight) + Mathf.FloorToInt(tunnelHeight + (tunnelHeight*Mathf.Abs(heightGrowth)/2)));
            curFill++;
        }
    }


    void addFloorCeilingToMap(){

        int x = buffer;
        foreach (int ceilingHeight in ceiling)
        {
            for(int y = ceilingHeight+buffer; y<=height+buffer; y++){
                map[x][y] = 1;
            }
            x++;
        }

        x = buffer;
        foreach (int floorHeight in floor)
        {
            for(int y = buffer; y<=floorHeight+buffer; y++){
                map[x][y] = 1;
            }
            x++;
        }
        
    }

    void disturbLevelShape(){

        float reduction = 0f;
        //Create the Perlin
        
        for(int x = 0; x < levelShape.Count; x++){
            float xseed = Random.Range(-1f, 1f);
            float yseed = Random.Range(0f, 1f);
            //levelShape[x] = (levelShape[x] + Mathf.FloorToInt((Mathf.PerlinNoise(xseed, yseed) - reduction) * maxDisturb));
            levelShape[x] = (levelShape[x] + Mathf.FloorToInt(xseed * maxDisturb));
        }

    }

    void randomLevelShape(){

        float reduction = 0.3f;
        //Create the Perlin
        float seed = Random.Range(10f, 100000f);
        int lastHeight = Mathf.FloorToInt(height/2);

        levelShape = new List<int>();
        
        for(int x = 0; x < Random.Range(2,10); x++){
            float ran = Random.Range(-1f, 1f);
            Debug.Log(ran);
            levelShape.Add(lastHeight + Mathf.FloorToInt(ran * maxSlope));
            //levelShape.Add(lastHeight + Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction)) * maxSlope);
        }
    }

}
