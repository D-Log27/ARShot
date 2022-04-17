using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ro : MonoBehaviour
{

    public float F_X;
    public float F_Y;
    public float F_Z;

	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(F_X, F_Y, F_Z),Space.Self);
	}
}
