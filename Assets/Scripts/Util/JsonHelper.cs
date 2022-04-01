using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;


public enum OptionType { USER_NAME, AREA_SIZE }
/// <summary>
/// Global 설정 저장용 함수
/// </summary>
public class JsonHelper : MonoBehaviour
{
    private static JsonHelper Instance;
    JsonHelper() { }
    const string OPTION_FILE = "optionInfo";
    string path;
    Option option;
    /// <summary>
    /// Singleton Instance 반환
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
    /// Json 파일 읽기
    /// </summary>
    public void ReadJson()
    {
        string json = Resources.Load<TextAsset>(OPTION_FILE).ToString();
        option = JsonUtility.FromJson<Option>(json);

        //print($"### first : {json}");
    }

    /// <summary>
    /// Json 값 읽기
    /// </summary>
    /// <param name="optionType">Key값에 해당하는 enum</param>
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
    /// Json 수정하기
    /// </summary>
    /// <param name="optionType">Key값에 해당하는 enum</param>
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
    /// 설정 초기화
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
