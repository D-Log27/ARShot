using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameObject �� Component ����/���� manager
/// </summary>
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
