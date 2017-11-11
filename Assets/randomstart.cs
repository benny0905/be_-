using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomstart : MonoBehaviour {
    private int r;
	// Use this for initialization
	void Start () {
        r = Random.Range(0,6);
	}

    // Update is called once per frame
    void Update() {
        Debug.Log(r);

    }
}
