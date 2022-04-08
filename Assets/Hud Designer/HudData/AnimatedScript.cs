using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedScript : MonoBehaviour
{

    public float animSpeed = 1;
    public float rotationSpeed = 0;

    void Update ()
    {
        if(rotationSpeed == 0 && transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
        if(GetComponent<Animator>())
            GetComponent<Animator>().speed = animSpeed;

        if(GetComponent<RectTransform>())
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, GetComponent<RectTransform>().eulerAngles.z + (rotationSpeed / 10));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + (rotationSpeed / 10));
        }
    }
}
