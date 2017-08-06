using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

    private HexTile[,] hexMap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void createHex()
    {

    }

    void createHexArray(int chunkSize)
    {
        hexMap = new HexTile[chunkSize, chunkSize];

        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {



            }
        }
    }
}
