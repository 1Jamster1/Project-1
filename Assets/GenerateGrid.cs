using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    public int gridSizeX;
    public int gridSizeY;
    public float hexWidth;
    public float hexHeight;
    public Transform greenTile;
    public Transform blueTile;

    float startX = 0;
    float startY = 0;

    // X, Y
    List<List<int>> gridArray = new List<List<int>>();


    /**
     * brief: Starts on object initalisation
     * param: None
     * retval: None
     */
	void Start () {

        generateGrid();
        placeGrid();

    }

    /**
     * brief: Generates a grid of randomily selected Hex tiles
     * param: None
     * retval: None
     */
    void generateGrid() {
        int i;
        int j;

        int r;

        for(i = 0; i < gridSizeX; i++) {

            gridArray.Add(new List<int>());

            for (j = 0; j < gridSizeY; j++) {

                //TODO: improve the randomgen system
                r = Mathf.FloorToInt(Random.Range(0f, 1.5f));

                gridArray[i].Add(r);

            }


        }

        string line;

        for (i = 0; i < gridSizeX; i++) {

            line = "";

            for(j = 0; j < gridSizeY; j++) {

                line = line + gridArray[i][j].ToString() + ",";

            }

            Debug.Log(line);
        }

    }

    /**
     * brief: Places the grid onto the scene
     * param: None
     * retval: None
     */
    void placeGrid() {

        int i;
        int j;

        for(i = 0; i < gridSizeX; i++) {
            
            for(j = 0; j < gridSizeY; j++) {
            
                if(gridArray[i][j] == 0) {

                    // Place blue hex
                    placeHex(blueTile, i, j);

                } else {

                    // Place green hex
                    placeHex(greenTile, i, j);

                }

            }

        }
    }

    /**
     * brief: Places a hex onto the scene
     * param: hex - The hex object to place, i - the X pos, j - the Y pos
     * retval: None
     */
    void placeHex(Transform hex, int i, int j) {

        float x, z, offset;
        
        offset = 0f;

        // Every second row of hexes need to be offset by 1/2 a hex
        if(j % 2 != 0) {

            offset = hexWidth / 2;

        }

        x = hexWidth * i + offset;
        z = hexHeight * j * 0.75f;

        Transform newHex = Instantiate(hex) as Transform;
        newHex.position = new Vector3(x, 0, z);
        newHex.name = "Hex (" + i.ToString() + "," + j.ToString() + ")";
        newHex.parent = this.transform;

    }

	
}
