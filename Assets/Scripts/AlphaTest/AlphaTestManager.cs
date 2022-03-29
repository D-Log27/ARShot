using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTestManager : MonoBehaviour
{
    Transform guns;
    // Start is called before the first frame update
    void Start()
    {
        guns = GameObject.Find("Gun").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // TODO : Pistol
            UnableWeaponObject();
            EnableWeaponObject(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // TODO : Submachine gun
            UnableWeaponObject();
            EnableWeaponObject(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // TODO : Heavymachine gun
            UnableWeaponObject();
            EnableWeaponObject(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // TODO : Shot gun
            UnableWeaponObject();
            EnableWeaponObject(3);
        }
    }

    private void UnableWeaponObject()
    {
        for(int i = 0; i < 4; i++)
        {
            guns.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void EnableWeaponObject(int i)
    {
        guns.GetChild(i).gameObject.SetActive(true);
    }
}
