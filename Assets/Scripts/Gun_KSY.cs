using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_KSY : MonoBehaviour
{
    // Start is called before the first frame update
   
   
        void Update()
        {
            SetPosition(Camera.main.transform);
        }

        void SetPosition(Transform anchor)
        {
            Vector3 Offset = anchor.forward * 0.5f + anchor.up;
            transform.position = anchor.position + Offset;
        }
    }



