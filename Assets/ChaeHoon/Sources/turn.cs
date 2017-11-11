using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        for (int n = 0; n < 60; n++)
        {
            transform.Rotate(0, 0, 1);
            for(int time = 0; time < 1000; time++)
            {

            }
        }
    }
}