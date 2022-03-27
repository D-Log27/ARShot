using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// loading circle rotate
/// </summary>
public class LoadingProgress : MonoBehaviour
{
    Vector3 rotationEuler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationEuler += Vector3.forward * 180 * Time.deltaTime; //increment 30 degrees every second
        transform.rotation = Quaternion.Euler(rotationEuler);

        //To convert Quaternion -> Euler, use eulerAngles
        //print(transform.rotation.eulerAngles);
    }
}
