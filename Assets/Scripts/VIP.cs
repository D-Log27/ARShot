using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectManager.objectDic.Add("VIP", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print($"Something collision : {collision.gameObject.name}");
    }
}
