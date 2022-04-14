using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameObject 정보 정보 Manager
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

    public static void SaveObject(string name, GameObject obj)
    {
        if (!objectDic.ContainsKey(name))
        {
            objectDic.Add(name, obj);
            print($"### OBJECT SAVED : {name}");
        }
        else
        {
            return;
        }
    }

    public static GameObject LoadObject(string name)
    {
        if (objectDic.ContainsKey(name))
        {
            return objectDic[name];
        }
        else
        {
            return null;
        }
    }
}
