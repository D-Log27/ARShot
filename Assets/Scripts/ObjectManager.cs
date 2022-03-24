using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    ObjectManager() {
        objectDic = new Dictionary<string, GameObject>();
    }
    public static ObjectManager GetInstance()
    {
        if (instance == null) instance = new ObjectManager();
        return instance;
    }
    [HideInInspector]
    public static Dictionary<string, GameObject> objectDic;

}
