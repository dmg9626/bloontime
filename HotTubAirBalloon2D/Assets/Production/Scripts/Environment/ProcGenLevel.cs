using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGenLevel : MonoBehaviour
{
    public Tilemap levelMap;
    public TileBase tile;

    public int height, width, tunnelHeight, buffer, maxSlope, maxDisturb, minShapes, maxShapes, maxBoulderHeight, maxBoulderWidth, minBoulderNum, maxBoulderNum, minEnemyNum, maxEnemyNum;

    public List<int> levelShape;

    public GameObject goalPost;
    public BalloonController player;
    public CameraController camera;

    public enum EnemyPositionType { ANY, TOP, BOTTOM, CENTER };
    public List<GameObject> enemyList;
    public List<EnemyPositionType> enemyPositions;

    private List<List<int>> map;
    private List<int> floor, ceiling;

    // Start is called before the first frame update
    void Awake()
    {
        levelMap.ClearAllTiles();
        initializeMap();
        
        if(Random.Range(0,2) == 0){
            randomLevelShape();
        }else{
            disturbLevelShape();
            Debug.Log("this was a defined shape");
        }

        buildFromLevelShape();
        createBoulders();
        addFloorCeilingToMap();
        renderMap();
        addEnemies();
        player.moveBalloon(new Vector2(buffer+1, levelShape[0]+buffer+Mathf.FloorToInt(tunnelHeight/2)));
        
        GameObject goalInstance = Instantiate(goalPost, new Vector2(width+buffer, levelShape[levelShape.Count-1] + buffer + Mathf.FloorToInt(tunnelHeight/2)), goalPost.transform.rotation);
        goalInstance.transform.localScale = new Vector3(10f,15f,1f);
    }

    public void NextLevel(){
        initializeMap();
        
        if(Random.Range(0,2) == 0){
            randomLevelShape();
        }else{
            disturbLevelShape();
            Debug.Log("this was a defined shape");
        }

        buildFromLevelShape();
        createBoulders();
        addFloorCeilingToMap();
        renderMap();
        addEnemies();
        player.moveBalloon(new Vector2(buffer+1, levelShape[0]+buffer+Mathf.FloorToInt(tunnelHeight/2)));
        camera.resetCameraToPlayer();
        GameObject goalInstance = Instantiate(goalPost, new Vector2(width+buffer, levelShape[levelShape.Count-1] + buffer + Mathf.FloorToInt(tunnelHeight/2)), goalPost.transform.rotation);
        goalInstance.transform.localScale = new Vector3(10f,15f,1f);
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

    void disturbLevelShape(){

        float reduction = 0f;
        //Create the Perlin
        
        for(int x = 0; x < levelShape.Count; x++){
            float xseed = Random.Range(-1f, 1f);

            int newHeight = levelShape[x] + Mathf.FloorToInt(xseed * maxDisturb);
            if(newHeight>(height-tunnelHeight)){
                newHeight = height-tunnelHeight;
            }else if(newHeight<0){
                newHeight = 0;
            }

            levelShape[x] = (newHeight);
            
            //float yseed = Random.Range(0f, 1f);
            //levelShape[x] = (levelShape[x] + Mathf.FloorToInt((Mathf.PerlinNoise(xseed, yseed) - reduction) * maxDisturb));
        }
    }

    void randomLevelShape(){

        float reduction = 0.3f;
        //Create the Perlin
        float seed = Random.Range(10f, 100000f);
        int lastHeight = Mathf.FloorToInt(height/2);

        levelShape = new List<int>();
        int shapeNum = Random.Range(minShapes, maxShapes);
        
        for(int x = 0; x < shapeNum; x++){
            float ran = Random.Range(-1f, 1f);

            lastHeight = lastHeight + Mathf.FloorToInt(ran * maxSlope);
            if(lastHeight>(height-tunnelHeight)){
                lastHeight = height-tunnelHeight;
            }else if(lastHeight<0){
                lastHeight = 0;
            }

            levelShape.Add(lastHeight);
            //levelShape.Add(lastHeight + Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction)) * maxSlope);
        }
    }


    void createBoulders(){

        int boulderNum = Random.Range(minBoulderNum, maxBoulderNum);
        List<int> boulderPlaces = new List<int>();
        int lastPlace = buffer;

        for(int x = 0; x < boulderNum; x++){
            lastPlace += Mathf.FloorToInt(Random.Range(maxBoulderWidth, (float)(width/(boulderNum/2))));
            if(lastPlace<width-5){
                boulderPlaces.Add(lastPlace);
            }
        }

        for(int x = 0; x < boulderPlaces.Count; x++){
            
            int bHeight = Random.Range(3, maxBoulderHeight);
            int bWidth = Random.Range(3, maxBoulderWidth);
            int onFloor = Random.Range(0, 2);
            //int onFloor = 1;
            float heightFalloff = (float)bHeight/bWidth;

            for(int i = 0; i < bWidth; i++){

                if(onFloor == 1){
                    floor[boulderPlaces[x]+i] += bHeight;
                    if(i>0){
                        floor[boulderPlaces[x]-i] += bHeight;
                    }
                }else{
                    ceiling[boulderPlaces[x]+i] -= bHeight;
                    if(i>0){
                        ceiling[boulderPlaces[x]-i] -= bHeight;
                    }
                }
                bHeight = Mathf.FloorToInt(bHeight-heightFalloff);
            }

        }
    }

    void buildFromLevelShape(){

        int fillSpace = Mathf.FloorToInt((float)width/(levelShape.Count));
        float heightGrowth = 0, curHeight = levelShape[0];
        int heightIndex = 0, curFill = 0;

        for(int x = buffer; x < width+buffer+1; x++){
            
            if(curFill >= fillSpace){
                curFill = 0;
                heightIndex += 1;
                if(heightIndex == levelShape.Count){
                    heightGrowth = 0;
                }else{
                    heightGrowth = (float)(levelShape[heightIndex]-curHeight)/fillSpace;
                }
            }
            curHeight+=heightGrowth;
            floor.Add(Mathf.FloorToInt(curHeight - (tunnelHeight*Mathf.Abs(heightGrowth)/4)));
            ceiling.Add(Mathf.FloorToInt(curHeight + tunnelHeight + (tunnelHeight*Mathf.Abs(heightGrowth)/4)));
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


    void addEnemies(){

        int enemyNum = Random.Range(minEnemyNum, maxEnemyNum); //replace with some kind of density variable
        List<float> enemyPlaces = new List<float>();
        float lastPlace = buffer+10;   //replace with some enemy buffer so enemy isn't too close to player at start

        for(int x = 0; x < enemyNum; x++){  //this randomly picks a number of enemies to put in a level and gives them a unique x position
            lastPlace += Random.Range(3f, (float)((width-10)/(enemyNum/2)));
            if(lastPlace<width-5){
                enemyPlaces.Add(lastPlace);
            }
        }

        for(int x = 0; x < enemyPlaces.Count; x++){
            int intPlace = Mathf.RoundToInt(enemyPlaces[x]);   //this is to find the x position in the map
            int enemyType = Random.Range(0, enemyList.Count);  //pick a random enemy

            float enemyHeight = -1;
            if(enemyPositions[enemyType].Equals(EnemyPositionType.TOP)){  //find enemy's y position based on how its preferences were defined, ie Top, Bottom, etc.
                enemyHeight = ceiling[intPlace] + buffer - 1f;
            }else if(enemyPositions[enemyType].Equals(EnemyPositionType.BOTTOM)){
                enemyHeight = floor[intPlace] + buffer + 1f;
            }else if(enemyPositions[enemyType].Equals(EnemyPositionType.CENTER)){
                enemyHeight = floor[intPlace] + buffer + (tunnelHeight/2);
            }else{
                enemyHeight = Random.Range(floor[intPlace] + buffer + 1f, ceiling[intPlace] + buffer - 1f);
            }
            
            Instantiate(enemyList[enemyType], new Vector2(enemyPlaces[x]+buffer, enemyHeight), enemyList[enemyType].transform.rotation); //create enemy
        }

    }

}
