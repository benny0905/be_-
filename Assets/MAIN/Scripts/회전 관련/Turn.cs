using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    
	/*void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(0.0f, 0.0f, 60.0f);
        }
		
	}*/

    private void OnMouseDown()
    {
        transform.Rotate(0, 0, 60);
    }
}
