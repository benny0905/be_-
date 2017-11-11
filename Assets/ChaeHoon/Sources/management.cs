using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class management : MonoBehaviour {
    RotateAnim objstart;
    RotateAnim objend;
    RotateAnim obj1;
    RotateAnim obj2;
    RotateAnim obj3;
    RotateAnim obj4;

    void Start()
    {
        GameObject methodstart = GameObject.Find("start");
        GameObject methodend = GameObject.Find("end");
        GameObject method1 = GameObject.Find("1");
        GameObject method2 = GameObject.Find("2");
        GameObject method3 = GameObject.Find("3");
        GameObject method4 = GameObject.Find("4");

        if (methodstart != null) objstart = methodstart.GetComponent<RotateAnim>();
        if (methodstart != null) objend = methodend.GetComponent<RotateAnim>();
        if (methodstart != null) obj1 = method1.GetComponent<RotateAnim>();
        if (methodstart != null) obj2 = method2.GetComponent<RotateAnim>();
        if (methodstart != null) obj3 = method3.GetComponent<RotateAnim>();
        if (methodstart != null) obj4 = method4.GetComponent<RotateAnim>();
    }


    void Update () {
		if(objstart.state == 2 * 60 && obj1.state == 5 * 60 && obj2.state == 2 * 60 && obj3.state == 5 * 60 && objend.state == 4 * 60)
        {
            
        }
	}
}
