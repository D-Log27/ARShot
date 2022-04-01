using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;


public enum OptionType { USER_NAME, AREA_SIZE }
/// <summary>
/// Global ���� ����� �Լ�
/// </summary>
public class JsonHelper : MonoBehaviour
{
    private static JsonHelper Instance;
    JsonHelper() { }
    const string OPTION_FILE = "optionInfo";
    string path;
    Option option;
    /// <summary>
    /// Singleton Instance ��ȯ
    /// </summary>
    /// <returns>JsonHelper Instance</returns>
    public static JsonHelper GetInstance()
    {
        if (Instance == null) Instance = new JsonHelper();
        return Instance;
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        option = null;
        ReadJson();
        path = new StringBuilder(Application.dataPath).Append("/Resources/").Append(OPTION_FILE).ToString();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Json ���� �б�
    /// </summary>
    public void ReadJson()
    {
        string json = Resources.Load<TextAsset>(OPTION_FILE).ToString();
        option = JsonUtility.FromJson<Option>(json);

        //print($"### first : {json}");
    }

    /// <summary>
    /// Json �� �б�
    /// </summary>
    /// <param name="optionType">Key���� �ش��ϴ� enum</param>
    /// <returns>value</returns>
    public string ReadValue(OptionType optionType)
    {
        if(option != null)
        {
            switch(optionType)
            {
                case OptionType.USER_NAME:
                    return option.userName;
                    break;
                case OptionType.AREA_SIZE:
                    return option.areaSize.ToString();
                    break;
            }

        }
        return null;
    }

    /// <summary>
    /// Json �����ϱ�
    /// </summary>
    /// <param name="optionType">Key���� �ش��ϴ� enum</param>
    /// <param name="value">value</param>
    public void UpdateJson(OptionType optionType, string value)
    {
        if (option != null)
        {
            switch (optionType)
            {
                case OptionType.USER_NAME:
                    option.userName = value;
                    break;
                case OptionType.AREA_SIZE:
                    option.areaSize = int.Parse(value);
                    break;
            }

        }
        string json = JsonUtility.ToJson(option);
        //print($"### second : {json}");
        File.WriteAllText(path + ".json", json);
    }

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    public void ResetJson()
    {
        if(option != null)
        {
            option.userName = "TestName";
            option.areaSize = 10;

            string json = JsonUtility.ToJson(option);
            File.WriteAllText(path + ".json", json);
        }
    }
}
