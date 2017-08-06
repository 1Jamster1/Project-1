using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManagement : MonoBehaviour {

    #region inputs & public variables

    public Vector2 mapSize;
    public Transform[] hexTiles;



    #endregion

    #region consts & private Variables

    private const int chunkSize = 8;

    private int numOfChunksX = 0;
    private int numOfChunksY = 0;

    private ChunkManager[,] chunkMap;

    #endregion



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Do nothing for now
        //TODO: Add updates Maybe?
	}

    /**
     * brief: Generate an empty grid constisting of only ocean tiles.
     * param: None.
     * retval: None.
     */
    void GenerateGrid()
    {

        numOfChunksX = Mathf.CeilToInt(mapSize.x / chunkSize);
        numOfChunksY = Mathf.CeilToInt(mapSize.y / chunkSize);

        // Allocate array;
        chunkMap = new ChunkManager[numOfChunksX, numOfChunksY];

        for (int i = 0; i < numOfChunksX; i++)
        {

            for(int j = 0; j < numOfChunksY; j++)
            {

                //Create new ChunkManager Object

                //Generate the chunks contents
                GenerateNewChunk(i, j);

            }


        }


    }

    /**
     * brief: Generate an new chunk constisting of only ocean tiles.
     * param: x - the X position, y - the Y position.
     * retval: None. 
     */
    void GenerateNewChunk(int x, int y)
    {

        // Chunks consist of a set amount of tiles defined by chunkSize
        // They are square in size
        int[,] chunkData = new int[chunkSize, chunkSize];
        int k = 0;
        int l = 0;

        for(int i = (0 + x * chunkSize); i < (chunkSize + x * chunkSize); i++)
        {

            for(int j = (0 + y * chunkSize); j < (chunkSize + j * chunkSize); j++)
            {

       
                float noiseSample = Mathf.PerlinNoise(i, j);

                //TODO: Improve this to divisfy the generation
                if(noiseSample < 0.5)
                {
                    // Water
                    chunkData[k,l] = 0;


                }
                else if(noiseSample >= 0.5)
                {
                    //Land
                    chunkData[k,l] = 1;
                }

                l++;

            }

            k++;

        }


    }
}
