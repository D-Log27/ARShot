using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System;

public class JsonHelper : MonoBehaviour
{
    public enum OptionType { USER_NAME, AREA_SIZE }
    private static JsonHelper Instance;
    JsonHelper() { }
    const string OPTION_FILE = "optionInfo";
    Option option;
    public static JsonHelper GetInstance()
    {
        return Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        option = null;
        ReadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadJson()
    {
        string json = Resources.Load<TextAsset>(OPTION_FILE).ToString();
        option = JsonUtility.FromJson<Option>(json);
        

    }

    public void ReadValue()
    {
        if(option != null)
        {
            Type type = option.userName.GetType();


        }
        
    }

    public void UpdateJson()
    {

    }

    public void ResetJson()
    {

    }
}
