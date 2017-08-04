using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public float moveSensistivity = 2f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //TODO: Make movement feel better (Currently very sharp)

        //TODO: Move camera with keyboard buttons

        float x = 0f;
        float y = 0f;

        if (Input.GetMouseButton(0))
        {

            x = moveSensistivity * Input.GetAxis("Mouse X");
            y = moveSensistivity * Input.GetAxis("Mouse Y");

            //TODO: Improve logging
            Debug.Log(x);
            Debug.Log(y);

            this.transform.Translate(x, y, 0);

        }
	}
}
