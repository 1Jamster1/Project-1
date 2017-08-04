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
     * brief: Gets the midpoint between two vectors
     * param: point1 - the first point, point2 - the second point
     * retval: The midpoint
     */
    Vector2 midpointCalc(Vector2 point1, Vector2 point2) {

        return new Vector2((point1.x + point2.x) / 2, (point1.y + point2.y) / 2);

    }


    void midpointDisplacement(Vector2 point1, Vector2 point2, float randomRange, int currentIteration, int totalIterations) {


        Vector2 midpoint = midpointCalc(point1, point2);

        // TODO: Prevent -ve points
        Vector2 displacedPoint = midpoint + new Vector2(Random.Range(-randomRange, randomRange) * midpoint.x, Random.Range(-randomRange, randomRange) * midpoint.y);

        // Check point is within bounds. If out place it within.
        if (displacedPoint.x < 0) {

            displacedPoint.x = 0;

        } else if(displacedPoint.x > gridSizeX - 1) {

            displacedPoint.x = gridSizeX - 1;

        }

        if (displacedPoint.y < 0)
        {

            displacedPoint.y = 0;

        }
        else if (displacedPoint.y > gridSizeY - 1)
        {

            displacedPoint.y = gridSizeY - 1;

        }


        gridArray[Mathf.FloorToInt(point1.x)][Mathf.FloorToInt(point1.y)] = 1;
        gridArray[Mathf.FloorToInt(point2.x)][Mathf.FloorToInt(point2.y)] = 1;
        gridArray[Mathf.FloorToInt(displacedPoint.x)][Mathf.FloorToInt(displacedPoint.y)] = 1;

        currentIteration++;
        if(currentIteration >= totalIterations) {
            return;
        }

        randomRange = randomRange / 2;

        midpointDisplacement(point1, displacedPoint, randomRange, currentIteration, totalIterations);
        midpointDisplacement(point2, displacedPoint, randomRange, currentIteration, totalIterations);
    }

    void generateTwoPoints(Vector2 point1, int range) {

        // Choose point based off first point(Range of X tiles)
        Vector2 randomDist = new Vector2(Mathf.FloorToInt(Random.Range(-range, range)), Mathf.FloorToInt(Random.Range(-range, range)));
        Vector2 point2 = point1 + randomDist;

        // Check point is within bounds. If out place it within.
        if (point2.x < 0) {

            point2.x = 0;

        }
        else if (point2.x > gridSizeX - 1) {

            point2.x = gridSizeX - 1;

        }

        if (point2.y < 0) {

            point2.y = 0;

        } else if (point2.y > gridSizeY - 1) {

            point2.y = gridSizeY - 1;

        }

        // Choose point based off the midpoint of point 1 and 2 (Range of X tiles)
        randomDist = new Vector2(Mathf.FloorToInt(Random.Range(-range, range)), Mathf.FloorToInt(Random.Range(-range, range)));
        Vector2 point3 = midpointCalc(point1, point2) + randomDist;

        // Check point is within bounds. If out place it within.
        if (point3.x < 0) {

            point3.x = 0;

        } else if (point3.x > gridSizeX - 1) {

            point3.x = gridSizeX - 1;

        }

        if (point3.y < 0) {

            point3.y = 0;

        } else if (point3.y > gridSizeY - 1) {

            point3.y = gridSizeY - 1;

        }

        midpointDisplacement(point1, point2, 0.5f, 0, 10);
        midpointDisplacement(point1, point3, 0.5f, 0, 10);
        midpointDisplacement(point2, point3, 0.5f, 0, 10);
    }

    void randomlyGenerateLand()
    {

        Vector2 point1 = new Vector2(Mathf.FloorToInt(Random.Range(0, gridSizeX - 1)), Mathf.FloorToInt(Random.Range(0, gridSizeY - 1)));

        generateTwoPoints(point1, 5);

        point1 = new Vector2(Mathf.FloorToInt(Random.Range(0, gridSizeX - 1)), Mathf.FloorToInt(Random.Range(0, gridSizeY - 1)));

        generateTwoPoints(point1, 5);

        point1 = new Vector2(Mathf.FloorToInt(Random.Range(0, gridSizeX - 1)), Mathf.FloorToInt(Random.Range(0, gridSizeY - 1)));

        generateTwoPoints(point1, 5);
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


        // Fill world with Water
        for(i = 0; i < gridSizeX; i++) {

            gridArray.Add(new List<int>());

            for (j = 0; j < gridSizeY; j++) {

                //TODO: improve the randomgen system
                r = 0;

                gridArray[i].Add(r);

            }


        }

        // Generate Land
        randomlyGenerateLand();

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
