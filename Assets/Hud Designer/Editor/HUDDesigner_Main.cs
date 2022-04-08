#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class HUDDesigner_Main : EditorWindow
{
    public GUISkin daSkin;

    Texture2D loaderTexture;
    Texture2D tempyTexture;
    Texture2D tempyTextureTwo;
    Texture2D[] circleTexture;
    Texture2D[] squareTexture;
    Sprite[] spriteArr = new Sprite[1];
    Sprite daSprite;

    int mainPage     = 0;
    int mainPageTemp = 0;
    int tempIntOne   = 0;
    int cp           = 0;

    int[,] tempInt2DArr = new int [30, 30];
    int[] daSettingInt  = new int [10];
    int[] tempIntArr    = new int [10];

    float layoutSize;
    float buttonPos;
    float widthFloat;
    float[] tempFloatArr     = new float[10];
    float[,] colorSchemeInfo = new float[50, 20];

    string folderLoc = "";
    string[] settingsSelection   = new string[] {"SpriteRenderer", "Unity GUI"};
    string[] menuString          = new string[20];
    List<string> tempStringArr   = new List<string> ();
    List<string> lineList        = new List<string> ();
    List<string> settingsInfoArr = new List<string> ();

    Vector2 scrollPosition      = Vector2.zero;
    Vector2 scrollPosition2     = Vector2.zero;
    Vector2[,] vector2Arr       = new Vector2[10, 100];

    GameObject hudMainObj;
    GameObject prevHudmain;
    GameObject currSelection;
    GameObject currSelectedLayer;
    GameObject daCanvas;
    GameObject tempyObject;
    GameObject[] animatedPrefabs;
    GameObject[] shapesPrefabs;

    List<GameObject> circleObjects   = new List<GameObject> ();
    List<GameObject> squareObjects   = new List<GameObject> ();
    List<GameObject> animatedObjects = new List<GameObject> ();
    List<GameObject> shapesObjects   = new List<GameObject> ();
    List<GameObject> designList      = new List<GameObject> ();

    bool directoryFind  = true;
    bool previewOn      = false;
    bool tempBoolOne    = false;
    bool isolateToggle  = false;
    bool deleteConfirm  = false;
    bool uniCheckerOne  = true;
    bool[] menuBool     = new bool[20];
    bool isolateToggleChecker   = false;
    bool playModeOn     = false;

    StreamReader genInfo;
    StreamReader colorInfo;

    Color tempColor;
    Color[] autoGenColor = new Color[5];

    Object daAsset;

    Material shapesMaterial = null;


    [MenuItem ("Window/HUD Designer")]
    static void OpenWindow ()
    {
        Debug.Log("Thank you for purchasing HUD Designer, please visit http://hud.naga.ninja for User manual and tutorials");
        HUDDesigner_Main window = (HUDDesigner_Main) GetWindow (typeof (HUDDesigner_Main));
        string daFolder = "";
        string[] daDirResult = AssetDatabase.FindAssets ("HUDDesigner_Main");
        foreach (string guid in daDirResult)
        {
            if (AssetDatabase.GUIDToAssetPath (guid).Length > 27 &&
                    AssetDatabase.GUIDToAssetPath (guid).Substring (AssetDatabase.GUIDToAssetPath (guid).Length - 19, 16) == "HUDDesigner_Main")
            {
                daFolder = AssetDatabase.GUIDToAssetPath (guid).Substring (0, AssetDatabase.GUIDToAssetPath (guid).Length - 27);
            }
        }

        window.minSize = new Vector2 (400, 300);
        window.titleContent.text = "HUD Designer";
        if (daFolder != "")
        {
            window.titleContent.image = (Texture2D) AssetDatabase.LoadAssetAtPath (daFolder + "/Hud_Icon.jpg", typeof (Texture2D));
        }
        window.Show ();
    }

    void OnEnable ()
    {
        directoryFind = true;
    }

    void OnGUI ()
    {
        if(EditorApplication.isPlaying)
        {
            playModeOn = true;
        }
        else
        {
            if(playModeOn)
            {
                playModeOn = false;
                readSettingsInfo();

                while(hudMainObj && hudMainObj.transform.childCount > 0)
                {
                    DestroyImmediate(hudMainObj.transform.GetChild(0).gameObject);
                }
                if(hudMainObj)
                    prevHudmain = hudMainObj;
                hudMainObj = Instantiate( (GameObject)AssetDatabase.LoadAssetAtPath( folderLoc +
                                          "/Prefabs/Current_HUD.prefab", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);

                if(daSettingInt[0] == 1)
                {
                    hudMainObj.name = "HUD Designer Canvas";
                    hudMainObj.transform.SetParent(daCanvas.transform);
                }
                else
                {
                    hudMainObj.name = "HUD Designer";
                }
                if(prevHudmain)
                    DestroyImmediate(prevHudmain);
                for(int i = 0; i < hudMainObj.transform.childCount; i ++)
                {
                    GameObject daObj = hudMainObj.transform.GetChild(i).gameObject;
                    if(daObj.GetComponent<ShapesScript>())
                    {
                        daObj.GetComponent<ShapesScript>().playAnimation = true;
                        if(daObj.GetComponent<LineRenderer>())
                        {
                            daObj.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
                        }
                    }
                }
            }
        }

        if(mainPage != mainPageTemp && folderLoc != "")
        {
            mainPageTemp = mainPage;
            saveHUDprefab("Current_HUD", false);
        }

        if (daSettingInt[0] == 0 && (hudMainObj == null || (hudMainObj.transform.parent != null && hudMainObj.transform.parent.GetComponent<Canvas>())))
        {
            if (GameObject.Find ("HUD Designer") == null)
            {
                hudMainObj = new GameObject ("HUD Designer");
                CheckParents("ALL");
            }
            else
            {
                hudMainObj = GameObject.Find ("HUD Designer");
                CheckParents("ALL");
            }
            if(GameObject.Find("HUD Designer Canvas") && GameObject.Find("HUD Designer Canvas").transform.childCount == 0)
            {
                DestroyImmediate(GameObject.Find("HUD Designer Canvas"));
                if(GameObject.Find("Canvas") && GameObject.Find("Canvas").transform.childCount == 0)
                {
                    DestroyImmediate(GameObject.Find("Canvas"));
                }
            }
        }
        else if (daSettingInt[0] == 1 && (hudMainObj == null || hudMainObj.transform.parent == null
                                          || (hudMainObj.transform.parent != null && hudMainObj.transform.parent.GetComponent<Canvas>() == null)))
        {
            if(GameObject.Find("HUD Designer") && GameObject.Find("HUD Designer").transform.childCount == 0)
            {
                DestroyImmediate(GameObject.Find("HUD Designer"));
            }
            if(GameObject.Find("Canvas") == null )
            {
                daCanvas = new GameObject("Canvas");
                daCanvas.AddComponent<RectTransform>();
                daCanvas.AddComponent<Canvas>();
                daCanvas.AddComponent<CanvasScaler>();
                daCanvas.AddComponent<GraphicRaycaster>();
            }
            else
            {
                daCanvas = GameObject.Find("Canvas");
            }
            if (GameObject.Find ("HUD Designer Canvas") == null)
            {
                hudMainObj = new GameObject ("HUD Designer Canvas");
                hudMainObj.AddComponent<RectTransform>();

                hudMainObj.transform.SetParent(daCanvas.transform);
                hudMainObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                hudMainObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
                CheckParents("ALL");
            }
            else
            {
                hudMainObj = GameObject.Find ("HUD Designer Canvas");
                CheckParents("ALL");
            }
        }
        if (directoryFind)
        {
            folderLoc = "";
            string[] daDirResult = AssetDatabase.FindAssets ("HUDDesigner_Main");

            foreach (string guid in daDirResult)
            {
                if (AssetDatabase.GUIDToAssetPath (guid).Length > 27 &&
                        AssetDatabase.GUIDToAssetPath (guid).Substring (AssetDatabase.GUIDToAssetPath (guid).Length - 19, 16) == "HUDDesigner_Main")
                {
                    folderLoc = AssetDatabase.GUIDToAssetPath (guid).Substring (0, AssetDatabase.GUIDToAssetPath (guid).Length - 27);
                    directoryFind = false;
                }
            }

            if (directoryFind)
            {
                Debug.Log ("ERROR 101: Cannot find HUD Designer Directory, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
            }
            else
            {
                FindTheTextures();
            }

            readColorInfo(false);
            readSettingsInfo();
        }

        if(shapesMaterial == null && folderLoc != "")
        {
            shapesMaterial = (Material)AssetDatabase.LoadAssetAtPath(folderLoc + "/HudData/ShapesMaterial.mat", typeof (Material));
        }

        if(daSkin == null && folderLoc != "")
        {
            daSkin = (GUISkin)AssetDatabase.LoadAssetAtPath(folderLoc + "/HudData/HUD_Skin.guiskin", typeof (GUISkin));
        }

        widthFloat = position.width;
        layoutSize = position.width - (widthFloat / 4 ) - 20;

        if(!uniCheckerOne && mainPage != 0)
        {
            uniCheckerOne = true;
        }

        switch (mainPage)
        {

        case 0:

            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));
            buttonPos = 0;

            if(uniCheckerOne)
            {
                uniCheckerOne   = false;
                deleteConfirm   = false;
                CheckParents("ALL");
                menuBool        = new bool[20];
                tempInt2DArr    = new int [30, 30];
                tempIntArr      = new int [10];
                tempIntOne      = 0;
                tempBoolOne     = false;

                menuString[0]  = "0";
                menuString[1]  = "10";
                menuString[2]  = "0";
                menuString[3]  = "20";
                menuString[4]  = "0";
                menuString[5]  = "360";
                menuString[6]  = "0";
                menuString[7]  = "10";
                menuString[8]  = "0";
                menuString[9]  = "5";
                menuString[10] = "0";
                menuString[11] = "1";
                menuString[12] = "0";
                menuString[13] = "2";
                menuString[14] = "0";
                menuString[15] = "5";
            }

            currSelection       = null;
            currSelectedLayer   = null;

            if (GUI.Button (new Rect (0, 0, position.width, 65), "GENERATE HUD", daSkin.GetStyle ("HUD_ButtonRed")))
            {
                mainPage = 1;
            }

            buttonPos = buttonPos + 70;

            if (GUI.Button (new Rect (0, buttonPos, widthFloat / 4, widthFloat / 3.2f), "", daSkin.GetStyle ("HUD_ButtonCircle")))
            {
                CheckParents ("Circle");
                if (circleObjects.Count > 0)
                {
                    currSelection = currSelectedLayer = circleObjects[0];
                }
                mainPage = 2;
            }

            if (GUI.Button (new Rect (widthFloat / 4, buttonPos, widthFloat / 4, widthFloat / 3.2f), "", daSkin.GetStyle ("HUD_ButtonSquare")))
            {
                mainPage = 3;
            }

            if (GUI.Button (new Rect (widthFloat / 2, buttonPos, widthFloat / 4, widthFloat / 3.2f), "", daSkin.GetStyle ("HUD_ButtonShapes")))
            {
                if(shapesObjects.Count > 0)
                {
                    for(int i = 0; i < shapesObjects.Count; i++)
                    {
                        if(shapesObjects[i] != null)
                        {
                            menuString[0] = shapesObjects[i].GetComponent<ShapesScript>().speed.ToString();
                            menuString[1] = shapesObjects[i].GetComponent<ShapesScript>().distance.ToString();
                            menuString[5] = shapesObjects[i].GetComponent<LineRenderer>().startWidth.ToString();
                            break;
                        }
                    }
                }
                else
                {
                    menuString[0] = "1";
                    menuString[1] = "1";
                    menuString[5] = "0.05";
                }
                menuString[2] = "5";
                menuString[3] = "5";
                menuString[4] = "10";

                menuString[6] = "100";
                tempIntArr    = new int [10];
                menuBool[15]  = true;
                menuBool[16] = false;
                mainPage      = 4;
            }

            if (GUI.Button (new Rect (widthFloat - widthFloat / 4, buttonPos, widthFloat / 4, widthFloat / 3.2f), "", daSkin.GetStyle ("HUD_ButtonAnimated")))
            {
                menuString[0] = "0";
                menuString[1] = "0";
                mainPage      = 5;
            }

            buttonPos = buttonPos + widthFloat / 3.2f + 50;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "SAVE / LOAD", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                saveHUDprefab("", true);
                mainPage = 6;
            }

            buttonPos = buttonPos + 70;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "MANAGE COLOR SCHEMES", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                mainPage = 8;
            }

            buttonPos = buttonPos + 70;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "SETTINGS", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                mainPage = 7;
            }
            buttonPos = buttonPos + 80;
            GUI.EndScrollView ();
            break;

        case 1:
            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));
            buttonPos = 0;
            if(!menuBool[8])
            {
                GUI.Box (new Rect (0, buttonPos, position.width, 505), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }
            else
            {
                GUI.Box (new Rect (0, buttonPos, position.width, 50), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }

            if (GUI.Button (new Rect (0, buttonPos + 20, position.width, 10), "SET COLORS:", daSkin.GetStyle ("HUD_TextCenter")))
            {
                menuBool[8] = (menuBool[8]) ? false : true;
            }
            buttonPos = buttonPos + 50;

            if(!menuBool[8])
            {
                ColorSchemeButtons(position.width);
                buttonPos = buttonPos + 20;

                if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "REFRESH COLORS", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    RefreshColors(hudMainObj);
                    mainPageTemp = 99;
                }
                buttonPos = buttonPos + 100;
            }
            else
            {
                buttonPos = buttonPos + 30;
            }

            if(!menuBool[6])
            {
                GUI.Box (new Rect (0, buttonPos - 15, position.width, 190), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }
            else
            {
                GUI.Box (new Rect (0, buttonPos - 15, position.width, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }

            if (GUI.Button (new Rect (0, buttonPos, position.width, 10), "LOCK DESIGNS:", daSkin.GetStyle ("HUD_TextCenter")))
            {
                menuBool[6] = (menuBool[6]) ? false : true;
            }

            buttonPos = buttonPos + 30;

            if(!menuBool[6])
            {
                if(menuBool[0])
                {
                    if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "CIRCLE Locked", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[0] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "CIRCLE", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[0] = true;
                    }
                }
                if(menuBool[1])
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "SQUARE Locked", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[1] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "SQUARE", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[1] = true;
                    }
                }
                buttonPos = buttonPos + 70;

                if(daSettingInt[0] == 0)
                {

                    if(menuBool[2])
                    {
                        if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "SHAPES Locked", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            menuBool[2] = false;
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "SHAPES", daSkin.GetStyle ("HUD_ButtonBlue")))
                        {
                            menuBool[2] = true;
                        }
                    }
                }
                else
                {
                    GUI.Label (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "Shapes are only available in SpriteRenderer", daSkin.GetStyle ("HUD_TextCenterSmall"));
                    menuBool[2] = true;
                }

                if(menuBool[3])
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "ANIMATED Locked", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[3] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "ANIMATED", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[3] = true;
                    }
                }
                buttonPos = buttonPos + 70;
            }

            buttonPos = buttonPos + 30;
            if(!menuBool[7])
            {
                GUI.Box (new Rect (0, buttonPos - 15, position.width, 190), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }
            else
            {
                GUI.Box (new Rect (0, buttonPos - 15, position.width, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }

            if (GUI.Button (new Rect (0, buttonPos, position.width, 10), "DON'T GENERATE:", daSkin.GetStyle ("HUD_TextCenter")))
            {
                menuBool[7] = (menuBool[7]) ? false : true;
            }
            buttonPos = buttonPos + 30;

            if(!menuBool[7])
            {
                if(menuBool[10])
                {
                    if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "CIRCLE", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[10] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "CIRCLE", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[10] = true;
                    }
                }
                if(menuBool[11])
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "SQUARE", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[11] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "SQUARE", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[11] = true;
                    }
                }
                buttonPos = buttonPos + 70;

                if(daSettingInt[0] == 0)
                {
                    if(menuBool[12])
                    {
                        if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "SHAPES", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            menuBool[12] = false;
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "SHAPES", daSkin.GetStyle ("HUD_ButtonBlue")))
                        {
                            menuBool[12] = true;
                        }
                    }
                }
                else
                {
                    GUI.Label (new Rect (10, buttonPos, (position.width / 2) - 20, 65), "Shapes are only available in SpriteRenderer", daSkin.GetStyle ("HUD_TextCenterSmall"));
                    menuBool[12] = true;
                }
                if(menuBool[13])
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "ANIMATED", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        menuBool[13] = false;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect ((position.width / 2) - 5, buttonPos, (position.width / 2) - 20, 65), "ANIMATED", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        menuBool[13] = true;
                    }
                }
                buttonPos = buttonPos + 90;
            }

            buttonPos = buttonPos + 10;

            if(!menuBool[10] || !menuBool[11] || !menuBool[12] || !menuBool[13])
            {
                GUI.Box (new Rect (0, buttonPos - 7, position.width, 170), "", daSkin.GetStyle ("HUD_BoxBlack"));

                GUI.Label (new Rect (10, buttonPos, position.width - 20, 20), "Set Minimum and Maximum Values", daSkin.GetStyle ("HUD_TextCenter"));
                buttonPos = buttonPos + 40;
            }

            if(!menuBool[10])
            {
                GUI.Label (new Rect (20, buttonPos, position.width - 20, 20), "Min and Max number of Circles", daSkin.GetStyle ("HUD_TextLeft"));
                menuString[8] = GUI.TextField(new Rect(position.width - 160, buttonPos, 50, 20), menuString[8], 25);
                menuString[9] = GUI.TextField(new Rect(position.width - 80, buttonPos, 50, 20), menuString[9], 25);
                buttonPos = buttonPos + 30;
            }
            if(!menuBool[11])
            {
                GUI.Label (new Rect (20, buttonPos, position.width - 20, 20), "Min and Max number of Squares", daSkin.GetStyle ("HUD_TextLeft"));
                menuString[10] = GUI.TextField(new Rect(position.width - 160, buttonPos, 50, 20), menuString[10], 25);
                menuString[11] = GUI.TextField(new Rect(position.width - 80, buttonPos, 50, 20), menuString[11], 25);
                buttonPos = buttonPos + 30;
            }
            if(!menuBool[12])
            {
                GUI.Label (new Rect (20, buttonPos, position.width - 20, 20), "Min and Max number of Shapes", daSkin.GetStyle ("HUD_TextLeft"));
                menuString[12] = GUI.TextField(new Rect(position.width - 160, buttonPos, 50, 20), menuString[12], 25);
                menuString[13] = GUI.TextField(new Rect(position.width - 80, buttonPos, 50, 20), menuString[13], 25);
                buttonPos = buttonPos + 30;
            }
            if(!menuBool[13])
            {
                GUI.Label (new Rect (20, buttonPos, position.width - 20, 20), "Min and Max number of Animated", daSkin.GetStyle ("HUD_TextLeft"));
                menuString[14] = GUI.TextField(new Rect(position.width - 160, buttonPos, 50, 20), menuString[14], 25);
                menuString[15] = GUI.TextField(new Rect(position.width - 80, buttonPos, 50, 20), menuString[15], 25);
                buttonPos = buttonPos + 50;
            }

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "GENERATE HUD", daSkin.GetStyle ("HUD_ButtonGreen")))
            {
                float minCircles    = 0f;
                float maxCircles    = 5f;
                float minSquares    = 0f;
                float maxSquares    = 5f;
                float minShapes     = 0f;
                float maxShapes     = 5f;
                float minAnimated   = 0f;
                float maxAnimated   = 5f;
                float.TryParse(menuString[8],  out minCircles);
                float.TryParse(menuString[9],  out maxCircles);
                float.TryParse(menuString[10], out minSquares);
                float.TryParse(menuString[11], out maxSquares);
                float.TryParse(menuString[12], out minShapes );
                float.TryParse(menuString[13], out maxShapes );
                float.TryParse(menuString[14], out minAnimated);
                float.TryParse(menuString[15], out maxAnimated);

                GenHUD( Mathf.FloorToInt( Random.Range(minCircles,  maxCircles )),
                        Mathf.FloorToInt( Random.Range(minSquares,  maxSquares )),
                        Mathf.FloorToInt( Random.Range(minShapes,   maxShapes  )),
                        Mathf.FloorToInt( Random.Range(minAnimated, maxAnimated)));
                mainPageTemp = 99;
            }
            buttonPos = buttonPos + 90;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "SAVE AS PREFAB", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                string[] daPath = new string[1];
                daPath[0] = folderLoc + "/Prefabs";
                string[] guids = AssetDatabase.FindAssets("t:prefab", daPath);
                int tempyInt = 1;
                string daName = "HUD Design 1";
                for(int i = 0; i < guids.Length; i++)
                {
                    if(AssetDatabase.GUIDToAssetPath(guids[i]) == (folderLoc + "/Prefabs/" + daName + ".prefab"))
                    {
                        tempyInt++;
                        i = 0;
                        daName =  "HUD Design " + tempyInt.ToString();
                    }
                }
                saveHUDprefab(daName, false);
            }
            buttonPos = buttonPos + 70;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                mainPage = 0;
            }
            buttonPos = buttonPos + 80;
            GUI.EndScrollView ();

            break;

        case 2:

            daSideScrollPanel(circleTexture);
            scrollPosition2 = GUI.BeginScrollView (new Rect ((widthFloat / 4) + 20, 0, layoutSize + 20, position.height),
                                                   scrollPosition2, new Rect (0, 0, position.width, buttonPos));
            if (menuBool[9])
            {
                addCustomImage(layoutSize);
            }
            else if(previewOn)
            {
                if(tempyTexture)
                {
                    previewWindow(tempyTexture, null);
                }
                else
                {
                    previewOn = false;
                }
            }
            else
            {
                buttonPos = 0;
                if (currSelection == null)
                {
                    nextPrev (true, circleObjects);
                }
                else
                {
                    if (circleObjects.Count > 0)
                    {
                        GUI.Label (new Rect (0, buttonPos, layoutSize, layoutSize / 5), currSelection.name, daSkin.GetStyle ("HUD_TextCenter"));
                    }

                    if (circleObjects.Count > 1)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                        {
                            nextPrev (false, circleObjects);
                        }
                        if (GUI.Button (new Rect (layoutSize - (layoutSize / 5), buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonRight")))
                        {
                            nextPrev (true, circleObjects);
                        }
                    }

                    buttonPos = layoutSize / 5 + 10;
                    if(currSelection != null && currSelection.transform.childCount > 0)
                    {
                        GUI.BeginGroup(new Rect(0, buttonPos, layoutSize, layoutSize));
                        for(int i = 0; i < currSelection.transform.childCount; i++)
                        {
                            if(i < 10 && currSelection.transform.GetChild(i).GetComponent<CircleScript>())
                            {
                                if( daSettingInt[0] == 0 && currSelection.transform.GetChild(i).GetComponent<SpriteRenderer>()
                                        && currSelection.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
                                {
                                    float currSize = ((layoutSize / currSelection.transform.childCount) *
                                                      currSelection.transform.GetChild(i).localScale.x) * currSelection.transform.childCount;
                                    if(currSelectedLayer == currSelection || currSelection.transform.GetChild(i).gameObject == currSelectedLayer)
                                    {
                                        GUI.Box (new Rect ((layoutSize - currSize) / 2, ( (layoutSize - currSize) / 2),
                                                           currSize, currSize),
                                                 currSelection.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.texture);
                                    }
                                }
                                else if ( daSettingInt[0] == 1 && currSelection.transform.GetChild(i).GetComponent<Image>()
                                          && currSelection.transform.GetChild(i).GetComponent<Image>().sprite != null)
                                {
                                    float currSize = ((layoutSize / currSelection.transform.childCount) *
                                                      currSelection.transform.GetChild(i).localScale.x) * currSelection.transform.childCount;
                                    if(currSelectedLayer == currSelection || currSelection.transform.GetChild(i).gameObject == currSelectedLayer)
                                    {
                                        GUI.Box (new Rect ((layoutSize - currSize) / 2, ( (layoutSize - currSize) / 2),
                                                           currSize, currSize),
                                                 currSelection.transform.GetChild(i).GetComponent<Image>().sprite.texture);
                                    }
                                }
                            }
                        }
                        GUI.EndGroup();
                        buttonPos = buttonPos + layoutSize + 20;
                        if(currSelectedLayer)
                        {
                            GUI.Label (new Rect (0, buttonPos, layoutSize, layoutSize / 5), currSelectedLayer.name, daSkin.GetStyle ("HUD_TextCenter"));

                            if (GUI.Button (new Rect (0, buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                            {
                                nextPrevLayer(false, currSelection);
                            }
                            if (GUI.Button (new Rect (layoutSize - (layoutSize / 5), buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonRight")))
                            {
                                nextPrevLayer(true, currSelection);
                            }
                            buttonPos = buttonPos + (layoutSize / 5) + 10;
                        }
                    }
                    else if (currSelection != null && daSettingInt[0] == 0 && currSelection.GetComponent<SpriteRenderer>())
                    {
                        currSelectedLayer = null;
                        GUI.Box (new Rect (0, buttonPos, layoutSize, layoutSize),
                                 currSelection.GetComponent<SpriteRenderer>().sprite.texture);
                        buttonPos = buttonPos + layoutSize + 20;
                    }
                    else if (currSelection != null && daSettingInt[0] == 1 && currSelection.GetComponent<Image>())
                    {
                        currSelectedLayer = null;
                        GUI.Box (new Rect (0, buttonPos, layoutSize, layoutSize),
                                 currSelection.GetComponent<Image>().sprite.texture);
                        buttonPos = buttonPos + layoutSize + 20;
                    }

                    isolateToggle = GUI.Toggle(new Rect(10, buttonPos, 100, 30), isolateToggle, "Isolate Object");

                    if(isolateToggleChecker != isolateToggle)
                    {
                        isolateToggleVoid();
                    }

                    buttonPos = buttonPos + 30;

                    if(currSelectedLayer != null && currSelectedLayer != currSelection)
                    {
                        tempColor = EditorGUI.ColorField(new Rect(0, buttonPos, layoutSize - 10, 20), "Color", tempColor);
                        if ( daSettingInt[0] == 0 && tempColor != currSelectedLayer.GetComponent<SpriteRenderer>().color)
                        {

                            currSelectedLayer.GetComponent<SpriteRenderer>().color = tempColor;
                        }
                        buttonPos = buttonPos + 40;

                        if(menuBool[5])
                        {
                            if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "USING RANDOM VALUES", daSkin.GetStyle ("HUD_ButtonBlue")))
                            {
                                if(daSettingInt[0] == 0 && currSelectedLayer.GetComponent<SpriteRenderer>())
                                {
                                    tempColor = currSelectedLayer.GetComponent<SpriteRenderer>().color;
                                }
                                else if(daSettingInt[0] == 1 && currSelectedLayer.GetComponent<Image>())
                                {
                                    tempColor = currSelectedLayer.GetComponent<Image>().color;
                                }
                                menuString[0] = menuString[1] = currSelectedLayer.GetComponent<CircleScript>().minRotationSpeed.ToString();
                                menuString[2] = menuString[3] = currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed.ToString();
                                menuString[4] = menuString[5] = currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle.ToString();
                                menuString[6] = menuString[7] = currSelectedLayer.GetComponent<CircleScript>().maxStopDuration.ToString();
                                menuBool[5]   = currSelectedLayer.GetComponent<CircleScript>().randomizeValues = false;
                            }
                            buttonPos = buttonPos + 70;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Minimum Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[0] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[0], 25);
                            buttonPos     = buttonPos + 30;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Maximum Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[2] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[2], 25);
                            buttonPos     = buttonPos + 30;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Maximum Rotation Angle", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[4] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[4], 25);
                            buttonPos     = buttonPos + 30;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Maximum Stop Duration", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[6] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[6], 25);
                            buttonPos     = buttonPos + 30;

                            if(menuString[0] != menuString[1] && menuString[0] != "" && menuString[0].Substring(menuString[0].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[0], out currSelectedLayer.GetComponent<CircleScript>().minRotationSpeed);
                                menuString[1] = menuString[0] = currSelectedLayer.GetComponent<CircleScript>().minRotationSpeed.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                            else if(menuString[2] != menuString[3] && menuString[2] != "" && menuString[2].Substring(menuString[2].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[2], out currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed);
                                menuString[3] = menuString[2] = currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                            else if(menuString[4] != menuString[5] && menuString[4] != "" && menuString[4].Substring(menuString[4].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[4], out currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle);
                                menuString[5] = menuString[4] = currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                            else if(menuString[6] != menuString[7] && menuString[6] != "" && menuString[6].Substring(menuString[6].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[6], out currSelectedLayer.GetComponent<CircleScript>().maxStopDuration);
                                menuString[7] = menuString[6] = currSelectedLayer.GetComponent<CircleScript>().maxStopDuration.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                        }
                        else
                        {
                            if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "USING CUSTOM VALUES", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                            {
                                menuBool[5] = currSelectedLayer.GetComponent<CircleScript>().randomizeValues = true;
                            }
                            buttonPos = buttonPos + 75;

                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[2] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[2], 25);
                            buttonPos     = buttonPos + 30;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Rotation Angle", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[4] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[4], 25);
                            buttonPos     = buttonPos + 30;
                            GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Stop Duration", daSkin.GetStyle ("HUD_TextLeft"));
                            menuString[6] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[6], 25);
                            buttonPos     = buttonPos + 30;

                            if(menuString[2] != menuString[3] && menuString[2] != "" && menuString[2].Substring(menuString[2].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[2], out currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed);
                                menuString[3] = menuString[2] = currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                            else if(menuString[4] != menuString[5] && menuString[4] != "" && menuString[4].Substring(menuString[4].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[4], out currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle);
                                menuString[5] = menuString[4] = currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                            else if(menuString[6] != menuString[7] && menuString[6] != "" && menuString[6].Substring(menuString[6].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[6], out currSelectedLayer.GetComponent<CircleScript>().maxStopDuration);
                                menuString[7] = menuString[6] = currSelectedLayer.GetComponent<CircleScript>().maxStopDuration.ToString();
                                if(EditorApplication.isPlaying)
                                {
                                    currSelectedLayer.GetComponent<CircleScript>().moveIt = false;
                                    currSelectedLayer.transform.eulerAngles = currSelectedLayer.GetComponent<CircleScript>().startAngle;
                                    currSelectedLayer.GetComponent<CircleScript>().Init();
                                }
                            }
                        }
                    }
                    else
                    {
                        buttonPos = buttonPos + 10;

                        if(!menuBool[6])
                        {
                            GUI.Box (new Rect (0, buttonPos - 15, layoutSize, 470), "", daSkin.GetStyle ("HUD_BoxBlack"));
                        }
                        else
                        {
                            GUI.Box (new Rect (0, buttonPos - 15, layoutSize, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
                        }

                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 10), "COLOR SCHEMES", daSkin.GetStyle ("HUD_TextCenter")))
                        {
                            menuBool[6] = (menuBool[6]) ? false : true;
                        }

                        if (!menuBool[6])
                        {
                            buttonPos = buttonPos + 30;
                            ColorSchemeButtons( layoutSize);
                            if (GUI.Button (new Rect (10, buttonPos, layoutSize - 20, 65), "REFRESH COLORS", daSkin.GetStyle ("HUD_ButtonBlue")))
                            {
                                if(currSelection != null)
                                {
                                    RefreshColors(currSelection);
                                }
                                else if (currSelectedLayer != null)
                                {
                                    RefreshColors(currSelectedLayer);
                                }
                                mainPageTemp = 99;
                            }
                            buttonPos = buttonPos + 50;
                        }

                        buttonPos = buttonPos + 25;

                        posMenu(layoutSize);

                        GUI.Box (new Rect (0, buttonPos - 5, layoutSize, 220), "", daSkin.GetStyle ("HUD_BoxBlack"));

                        GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Set Random Values", daSkin.GetStyle ("HUD_TextCenter"));
                        buttonPos = buttonPos + 30;

                        GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Minimum Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[0] = GUI.TextField(new Rect(layoutSize - 120, buttonPos, 50, 20), menuString[0], 25);
                        menuString[1] = GUI.TextField(new Rect(layoutSize - 60, buttonPos, 50, 20), menuString[1], 25);
                        buttonPos = buttonPos + 30;

                        GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Maximum Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[2] = GUI.TextField(new Rect(layoutSize - 120, buttonPos, 50, 20), menuString[2], 25);
                        menuString[3] = GUI.TextField(new Rect(layoutSize - 60, buttonPos, 50, 20), menuString[3], 25);
                        buttonPos = buttonPos + 30;

                        GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Maximum Rotation Angle", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[4] = GUI.TextField(new Rect(layoutSize - 120, buttonPos, 50, 20), menuString[4], 25);
                        menuString[5] = GUI.TextField(new Rect(layoutSize - 60, buttonPos, 50, 20), menuString[5], 25);
                        buttonPos = buttonPos + 30;

                        GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Maximum Stop Duration", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[6] = GUI.TextField(new Rect(layoutSize - 120, buttonPos, 50, 20), menuString[6], 25);
                        menuString[7] = GUI.TextField(new Rect(layoutSize - 60, buttonPos, 50, 20), menuString[7], 25);
                        buttonPos = buttonPos + 30;

                        if (GUI.Button (new Rect (20, buttonPos, layoutSize - 40, 50), "APPLY VALUES", daSkin.GetStyle ("HUD_ButtonBlue")))
                        {
                            float minRotateSpeed    = 0;
                            float minRotateSpeed2   = 0;
                            float maxRotateSpeed    = 0;
                            float maxRotateSpeed2   = 0;
                            float maxRotateAngle    = 0;
                            float maxRotateAngle2   = 0;
                            float maxStopDuration   = 0;
                            float maxStopDuration2  = 0;
                            float.TryParse(menuString[0], out minRotateSpeed);
                            float.TryParse(menuString[1], out minRotateSpeed2);
                            float.TryParse(menuString[2], out maxRotateSpeed);
                            float.TryParse(menuString[3], out maxRotateSpeed2);
                            float.TryParse(menuString[4], out maxRotateAngle);
                            float.TryParse(menuString[5], out maxRotateAngle2);
                            float.TryParse(menuString[6], out maxStopDuration);
                            float.TryParse(menuString[7], out maxStopDuration2);
                            if(currSelection != null && currSelection.transform.childCount > 0)
                            {
                                for(int i = 0; i < currSelection.transform.childCount; i++)
                                {
                                    GameObject daObj = currSelection.transform.GetChild(i).gameObject;
                                    if(daObj.GetComponent<CircleScript>())
                                    {
                                        daObj.GetComponent<CircleScript>().minRotationSpeed = Random.Range(minRotateSpeed, minRotateSpeed2);
                                        daObj.GetComponent<CircleScript>().maxRotationSpeed = Random.Range(maxRotateSpeed, maxRotateSpeed2);
                                        daObj.GetComponent<CircleScript>().maxRotateAngle   = Random.Range(maxRotateAngle, maxRotateAngle2);
                                        daObj.GetComponent<CircleScript>().maxStopDuration  = Random.Range(maxStopDuration, maxStopDuration2);
                                        daObj.GetComponent<CircleScript>().Init();
                                    }
                                }
                            }
                            mainPageTemp = 99;
                        }
                        buttonPos = buttonPos + 80;
                    }

                    if(deleteConfirm)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonGreen")))
                        {
                            deleteConfirm = false;
                        }
                        buttonPos = buttonPos + 65;
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CONFIRM", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            deleteConfirm = false;
                            DestroyImmediate (currSelectedLayer);
                            mainPageTemp = 99;
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            deleteConfirm = true;
                        }
                    }
                }
                buttonPos = buttonPos + 65;
                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                {
                    previewOn = false;
                    if(currSelectedLayer != null && currSelection != null && currSelectedLayer != currSelection)
                    {
                        currSelectedLayer = currSelection;
                    }
                    else
                    {
                        mainPage = 0;
                    }
                }
                buttonPos = buttonPos + 80;
            }

            GUI.EndScrollView ();

            break;

        case 3:
            daSideScrollPanel(squareTexture);
            scrollPosition2 = GUI.BeginScrollView (new Rect ((widthFloat / 4) + 20, 0, layoutSize + 20, position.height),
                                                   scrollPosition2, new Rect (0, 0, position.width, buttonPos));

            buttonPos = 0;

            if (menuBool[9])
            {
                addCustomImage(layoutSize);
            }
            else if(previewOn)
            {
                if(tempyTexture)
                {
                    previewWindow(tempyTexture, null);
                }
                else
                {
                    previewOn = false;
                }
            }
            else
            {
                if (currSelection == null)
                {
                    nextPrev (true, squareObjects);
                }
                else
                {
                    menuBool[4] = false;
                    if(currSelection == null && squareObjects.Count > 0)
                    {
                        CheckParents("Square");
                        if(squareObjects[0])
                        {
                            currSelection = squareObjects[0];
                            isolateToggleVoid();
                        }
                    }
                    if (squareObjects.Count > 0)
                    {
                        GUI.Label (new Rect (0, buttonPos, layoutSize, layoutSize / 5), currSelection.name, daSkin.GetStyle ("HUD_TextCenter"));
                    }

                    if (squareObjects.Count > 1)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                        {
                            nextPrev (false, squareObjects);
                        }
                        if (GUI.Button (new Rect (layoutSize - (layoutSize / 5), buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonRight")))
                        {
                            nextPrev (true, squareObjects);
                        }
                    }

                    buttonPos = layoutSize / 5 + 10;
                    if(daSettingInt[0] == 0 && currSelection != null && currSelection.GetComponent<SpriteRenderer>())
                    {
                        GUI.Box (new Rect (0, buttonPos, layoutSize, layoutSize),
                                 currSelection.GetComponent<SpriteRenderer>().sprite.texture);
                        buttonPos = buttonPos + layoutSize + 20;
                    }
                    else if(daSettingInt[0] == 1 && currSelection != null && currSelection.GetComponent<Image>())
                    {
                        GUI.Box (new Rect (0, buttonPos, layoutSize, layoutSize),
                                 currSelection.GetComponent<Image>().sprite.texture);
                        buttonPos = buttonPos + layoutSize + 20;
                    }

                    isolateToggle = GUI.Toggle(new Rect(10, buttonPos, 100, 30), isolateToggle, "Isolate Object");

                    if(isolateToggleChecker != isolateToggle)
                    {
                        isolateToggleVoid();
                    }

                    buttonPos = buttonPos + 30;

                    tempColor = EditorGUI.ColorField(new Rect(0, buttonPos, layoutSize - 10, 20), "Color", tempColor);
                    if ( daSettingInt[0] == 0 && currSelection.GetComponent<SpriteRenderer>() && tempColor != currSelection.GetComponent<SpriteRenderer>().color)
                    {
                        currSelection.GetComponent<SpriteRenderer>().color = tempColor;
                    }
                    buttonPos = buttonPos + 40;

                    posMenu(layoutSize);

                    if(deleteConfirm)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonGreen")))
                        {
                            deleteConfirm = false;
                        }
                        buttonPos = buttonPos + 65;
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CONFIRM", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            deleteConfirm = false;
                            DestroyImmediate (currSelectedLayer);
                            mainPageTemp = 99;
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            deleteConfirm = true;
                        }
                    }
                }
                buttonPos = buttonPos + 65;
                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                {
                    mainPage = 0;
                }
                buttonPos = buttonPos + 80;
            }
            GUI.EndScrollView ();
            break;

        case 4:
            if(daSettingInt[0] == 0)
            {
                daSideScrollPanelShapes(shapesPrefabs);
                scrollPosition2 = GUI.BeginScrollView (new Rect ((widthFloat / 4) + 20, 0, layoutSize + 20, position.height),
                                                       scrollPosition2, new Rect (0, 0, position.width, buttonPos));
                buttonPos = 0;

                if(previewOn)
                {
                    if(tempyObject)
                    {
                        previewWindow(null, tempyObject);
                    }
                    else
                    {
                        previewOn = false;
                    }
                    menuBool[16] = false;
                }
                else
                {
                    tempyObject = null;
                    if (currSelection == null)
                    {
                        if(shapesObjects.Count > 0)
                            buttonPos = 1481;
                        nextPrev (true, shapesObjects);
                    }
                    else
                    {
                        if(currSelection == null && shapesObjects.Count > 0)
                        {
                            CheckParents("Shapes");
                            if(shapesObjects[0])
                            {
                                currSelection = shapesObjects[0];
                                isolateToggleVoid();
                            }
                        }

                        if (shapesObjects.Count > 0)
                        {
                            GUI.Label (new Rect (0, buttonPos, layoutSize, layoutSize / 7), currSelection.name, daSkin.GetStyle ("HUD_TextCenter"));
                        }

                        if (shapesObjects.Count > 1)
                        {
                            if (GUI.Button (new Rect (0, buttonPos, layoutSize / 7, layoutSize / 7), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                            {
                                nextPrev (false, shapesObjects);
                            }

                            if (GUI.Button (new Rect (layoutSize - (layoutSize / 7), buttonPos, layoutSize / 7, layoutSize / 7), "", daSkin.GetStyle ("HUD_ButtonRight")))
                            {
                                nextPrev (true, shapesObjects);
                            }
                        }

                        buttonPos = layoutSize / 7 + 10;

                        if(currSelection != null && currSelection.GetComponent<ShapesScript>())
                        {
                            if(!menuBool[6])
                            {
                                GUI.Box (new Rect (0, buttonPos - 15, layoutSize, 500), "", daSkin.GetStyle ("HUD_BoxBlack"));
                            }
                            else
                            {
                                GUI.Box (new Rect (0, buttonPos - 15, layoutSize, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
                            }

                            if (GUI.Button (new Rect (0, buttonPos, layoutSize, 10), "COLOR SCHEMES", daSkin.GetStyle ("HUD_TextCenter")))
                            {
                                menuBool[6] = (menuBool[6]) ? false : true;
                            }
                            buttonPos = buttonPos + 40;

                            if (!menuBool[6])
                            {
                                ColorSchemeButtons(layoutSize);
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), new GUIContent("APPLY COLORS",
                                                "Apply the Colors Selected Above"), daSkin.GetStyle ("HUD_ButtonBlue")))
                                {
                                    SetShapeColor(currSelection);
                                    mainPageTemp = 99;
                                }
                                buttonPos = buttonPos + 80;
                            }


                            if(!EditorApplication.isPlaying && currSelection.GetComponent<LineRenderer>())
                            {
                                posMenu(layoutSize);
                            }

                            if(menuBool[5])
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "USE RANDOM VALUES", daSkin.GetStyle ("HUD_ButtonBlue")))
                                {
                                    menuBool[5] = false;
                                }
                                buttonPos = buttonPos + 80;

                                if(currSelection != null && currSelection.GetComponent<LineRenderer>() && currSelection.GetComponent<ShapesScript>())
                                {
                                    menuString[0] = currSelection.GetComponent<ShapesScript>().speed.ToString();
                                    menuString[1] = currSelection.GetComponent<ShapesScript>().distance.ToString();
                                    menuString[2] = currSelection.GetComponent<ShapesScript>().maxDistance.x.ToString();
                                    menuString[3] = currSelection.GetComponent<ShapesScript>().maxDistance.y.ToString();
                                    menuString[4] = currSelection.GetComponent<LineRenderer>().positionCount.ToString();
                                    menuString[5] = currSelection.GetComponent<LineRenderer>().startWidth.ToString();
                                }
                            }
                            else
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "USE CUSTOM VALUES", daSkin.GetStyle ("HUD_ButtonRed")))
                                {
                                    menuBool[5] = true;
                                }
                                buttonPos = buttonPos + 80;

                                GUI.Box (new Rect (0, buttonPos - 5, layoutSize, 170), "", daSkin.GetStyle ("HUD_BoxBlack"));

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Animation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[0] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[0], 25);
                                buttonPos = buttonPos + 30;

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Line Length", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[1] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[1], 25);
                                buttonPos = buttonPos + 30;

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Line Width", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[5] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[5], 25);
                                buttonPos = buttonPos + 30;

                                if (GUI.Button (new Rect (10, buttonPos, layoutSize - 20, 65), "APPLY VALUES", daSkin.GetStyle ("HUD_ButtonBlue")))
                                {
                                    float daSpeed = 0.1f;
                                    float lineLength = 1f;
                                    float.TryParse(menuString[0], out daSpeed);
                                    float.TryParse(menuString[1], out lineLength);
                                    if(currSelection.GetComponent<ShapesScript>())
                                    {
                                        currSelection.GetComponent<ShapesScript>().speed = daSpeed;
                                        currSelection.GetComponent<ShapesScript>().distance = lineLength;
                                    }
                                    if(currSelection.GetComponent<LineRenderer>())
                                    {
                                        float lineWidth = currSelection.GetComponent<LineRenderer>().startWidth;
                                        float.TryParse(menuString[5], out lineWidth);
                                        currSelection.GetComponent<LineRenderer>().startWidth = lineWidth;
                                        currSelection.GetComponent<LineRenderer>().endWidth = lineWidth;
                                    }
                                    mainPageTemp = 99;
                                }
                                buttonPos = buttonPos + 90;

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Maximum Width", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[2] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[2], 25);
                                buttonPos = buttonPos + 30;

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Maximum Height", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[3] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[3], 25);
                                buttonPos = buttonPos + 30;

                                GUI.Label (new Rect (10, buttonPos, layoutSize - 20, 20), "Number of Points", daSkin.GetStyle ("HUD_TextLeft"));
                                menuString[4] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[4], 25);
                                buttonPos = buttonPos + 30;

                            }

                            if(menuBool[16])
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize / 2 - 15, 65), "REPLACE", daSkin.GetStyle ("HUD_ButtonBlue")))
                                {
                                    menuBool[16] = false;
                                    CreateNewShape(true);
                                    CheckParents("Shapes");
                                    mainPageTemp = 99;
                                }
                                if (GUI.Button (new Rect (layoutSize / 2 - 15, buttonPos, layoutSize / 2 - 15, 65), "NEW", daSkin.GetStyle ("HUD_ButtonGreen")))
                                {
                                    menuBool[16] = false;
                                    CreateNewShape(false);
                                    CheckParents("Shapes");
                                    mainPageTemp = 99;
                                }
                                if (GUI.Button (new Rect (layoutSize - 30, buttonPos, 30, 65), "X", daSkin.GetStyle ("HUD_ButtonRed")))
                                {
                                    menuBool[16] = false;
                                }
                            }
                            else
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "RANDOMIZE SHAPE", daSkin.GetStyle ("HUD_ButtonBlue")))
                                {
                                    menuBool[16] = true;
                                }
                            }
                            buttonPos = buttonPos + 70;
                            if(deleteConfirm)
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonGreen")))
                                {
                                    deleteConfirm = false;
                                }
                                buttonPos = buttonPos + 65;
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CONFIRM", daSkin.GetStyle ("HUD_ButtonRed")))
                                {
                                    deleteConfirm = false;
                                    DestroyImmediate (currSelection);
                                    mainPageTemp = 99;
                                }
                            }
                            else
                            {
                                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                                {
                                    deleteConfirm = true;
                                }
                            }
                            buttonPos = buttonPos + 70;
                        }
                    }

                    buttonPos = buttonPos + 10;
                    if (GUI.Button (new Rect (0, buttonPos + 5, layoutSize, 65), "CREATE CUSTOM SHAPE", daSkin.GetStyle ("HUD_ButtonGreen")))
                    {
                        menuBool[15] = false;
                        tempIntArr = new int [10];
                        mainPage = 9;
                    }
                    buttonPos = buttonPos + 70;

                    if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                    {
                        mainPage = 0;
                    }
                    buttonPos = buttonPos + 90;
                }

                GUI.EndScrollView ();
            }
            else
            {
                buttonPos = 10;
                if(daSettingInt[0] == 1)
                {
                    GUI.Label (new Rect (10, buttonPos, position.width - 20, 100), "Shapes cannot be created using Unity GUI," +
                               "please change it to SpriteRenderer in the settings menu", daSkin.GetStyle ("HUD_TextCenter"));
                }
                else if(daSettingInt[0] == 2)
                {
                    GUI.Label (new Rect (10, buttonPos, position.width - 20, 100), "Shapes cannot be created using 3D Plane," +
                               "please change it to SpriteRenderer in the settings menu", daSkin.GetStyle ("HUD_TextCenter"));
                }
                buttonPos = buttonPos + 120;
                if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                {
                    mainPage = 0;
                }
            }

            break;

        case 5:

            daSideScrollPanelPrefab(animatedPrefabs);
            scrollPosition2 = GUI.BeginScrollView (new Rect ((widthFloat / 4) + 20, 0, layoutSize + 20, position.height),
                                                   scrollPosition2, new Rect (0, 0, position.width, buttonPos));

            buttonPos = 0;

            if (menuBool[9])
            {
                addCustomPrefab(layoutSize);
            }
            else if(previewOn)
            {
                menuBool[17] = false;
                if(tempyObject)
                {
                    previewWindow(tempyTexture, tempyObject);
                }
                else
                {
                    previewOn = false;
                }

            }
            else
            {
                menuBool[17] = false;
                if (currSelection == null)
                {
                    nextPrev (true, animatedObjects);
                }
                else
                {
                    if(currSelection == null && animatedObjects.Count > 0)
                    {
                        CheckParents("Animated");
                        if(animatedObjects[0])
                        {
                            currSelection = animatedObjects[0];
                            isolateToggleVoid();
                        }
                    }

                    if (animatedObjects.Count > 0)
                    {
                        GUI.Label (new Rect (0, buttonPos, layoutSize, layoutSize / 5), currSelection.name, daSkin.GetStyle ("HUD_TextCenter"));
                    }

                    if (animatedObjects.Count > 1)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                        {
                            nextPrev (false, animatedObjects);
                        }
                        if (GUI.Button (new Rect (layoutSize - (layoutSize / 5), buttonPos, layoutSize / 5, layoutSize / 5), "", daSkin.GetStyle ("HUD_ButtonRight")))
                        {
                            nextPrev (true, animatedObjects);
                        }
                    }

                    buttonPos = layoutSize / 5 + 10;

                    if(tempyTexture != null)
                    {
                        if(tempyTexture.width > layoutSize)
                        {
                            GUI.Box (new Rect (0, buttonPos, layoutSize, layoutSize), tempyTexture);
                            buttonPos = buttonPos + layoutSize + 20;
                        }
                        else
                        {
                            GUI.Box (new Rect ((layoutSize / 2) - (tempyTexture.width / 2), buttonPos, tempyTexture.width, tempyTexture.height), tempyTexture);
                            buttonPos = buttonPos + tempyTexture.height + 20;
                        }
                    }

                    isolateToggle = GUI.Toggle(new Rect(10, buttonPos, 100, 30), isolateToggle, "Isolate Object");

                    if(isolateToggleChecker != isolateToggle)
                    {
                        isolateToggleVoid();
                    }

                    buttonPos = buttonPos + 30;

                    tempColor = EditorGUI.ColorField(new Rect(0, buttonPos, layoutSize - 10, 20), "Color", tempColor);
                    if ( daSettingInt[0] == 0 && currSelection.GetComponent<SpriteRenderer>() &&
                            tempColor != currSelection.GetComponent<SpriteRenderer>().color)
                    {
                        currSelection.GetComponent<SpriteRenderer>().color = tempColor;
                    }
                    buttonPos = buttonPos + 40;

                    if( currSelection != null && currSelection.GetComponent<AnimatedScript>())
                    {
                        GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Animation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[0] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[0], 25);

                        buttonPos = buttonPos + 40;

                        GUI.Label (new Rect (0, buttonPos, layoutSize, 20), "Rotation Speed", daSkin.GetStyle ("HUD_TextLeft"));
                        menuString[2] = GUI.TextField(new Rect(layoutSize - 100, buttonPos, 50, 20), menuString[2], 25);

                        buttonPos = buttonPos + 40;

                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "APPLY VALUES", daSkin.GetStyle ("HUD_ButtonBlue")))
                        {
                            if(menuString[0] != menuString[1] && menuString[0] != "" && menuString[0].Substring(menuString[0].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[0], out currSelection.GetComponent<AnimatedScript>().animSpeed);
                                menuString[1] = menuString[0] = currSelection.GetComponent<AnimatedScript>().animSpeed.ToString();
                            }
                            if(menuString[2] != menuString[3] && menuString[2] != "" && menuString[2].Substring(menuString[2].Length - 1, 1) != ".")
                            {
                                float.TryParse(menuString[2], out currSelection.GetComponent<AnimatedScript>().rotationSpeed);
                                menuString[3] = menuString[2] = currSelection.GetComponent<AnimatedScript>().rotationSpeed.ToString();
                            }
                            mainPageTemp = 99;
                        }

                        buttonPos = buttonPos + 80;
                    }

                    posMenu(layoutSize);

                    if(deleteConfirm)
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonGreen")))
                        {
                            deleteConfirm = false;
                        }
                        buttonPos = buttonPos + 65;
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CONFIRM", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            deleteConfirm = false;
                            DestroyImmediate (currSelection);
                            mainPageTemp = 99;
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                        {
                            // Debug.Log("YO");
                            deleteConfirm = true;
                        }
                    }
                }
                buttonPos = buttonPos + 65;
                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                {
                    mainPage = 0;
                }
                buttonPos = buttonPos + 80;
            }
            GUI.EndScrollView ();
            break;

        case 6:
            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));

            buttonPos = 0;

            if(!tempBoolOne)
            {
                tempBoolOne = true;
                if(AssetDatabase.IsValidFolder(folderLoc + "/Prefabs"))
                {
                    string[] daFolder = {folderLoc + "/Prefabs"};
                    designList.Clear();
                    string[] daDesignPaths = AssetDatabase.FindAssets ("t:prefab", daFolder);
                    foreach (string guid in daDesignPaths)
                    {
                        designList.Add( (GameObject) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (GameObject)) );
                    }
                }
                tempIntArr = new int[designList.Count];
                menuBool[8]  = false;
            }
            else
            {
                if(designList.Count > 0)
                {
                    for( int i = 0; i < designList.Count; i++ )
                    {
                        if(designList[i] != null)
                        {
                            string styleName = "HUD_BoxBlack";
                            if(designList[i].name == "Current_HUD")
                            {
                                styleName = "HUD_ButtonGreen";
                            }

                            if(i != 0 && i == tempIntOne)
                            {
                                styleName = "HUD_ButtonBlue";
                            }

                            if (GUI.Button (new Rect (0, buttonPos, widthFloat / 3, widthFloat / 3), AssetPreview.GetAssetPreview(designList[i]), daSkin.GetStyle (styleName)))
                            {
                                tempIntOne = i;
                                GameObject prevHudmain  = hudMainObj;
                                saveHUDprefab("", true);
                                hudMainObj              = Instantiate( designList[i], new Vector3(0, 0, 0), Quaternion.identity);

                                if(hudMainObj.transform.GetChild(0).childCount > 0)
                                {
                                    if (hudMainObj.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>())
                                    {
                                        daSettingInt[0] = 1;

                                    }
                                    else if(hudMainObj.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>())
                                    {
                                        daSettingInt[0] = 0;
                                    }
                                }
                                else
                                {
                                    if (hudMainObj.transform.GetChild(0).GetComponent<RectTransform>())
                                    {
                                        daSettingInt[0] = 1;

                                    }
                                    else if(hudMainObj.transform.GetChild(0).GetComponent<SpriteRenderer>())
                                    {
                                        daSettingInt[0] = 0;
                                    }
                                }

                                if(daSettingInt[0] == 1)
                                {
                                    hudMainObj.name = "HUD Designer Canvas";
                                    if(daCanvas == null )
                                    {
                                        daCanvas = new GameObject("Canvas");
                                        daCanvas.AddComponent<RectTransform>();
                                        daCanvas.AddComponent<Canvas>();
                                        daCanvas.AddComponent<CanvasScaler>();
                                        daCanvas.AddComponent<GraphicRaycaster>();
                                    }
                                    hudMainObj.transform.SetParent(daCanvas.transform);
                                }
                                else
                                {
                                    hudMainObj.name = "HUD Designer";
                                }
                                DestroyImmediate(prevHudmain);
                            }


                            GUI.Label (new Rect (widthFloat / 3 + 10, buttonPos + 10, position.width - (widthFloat / 3) - 20, 10),
                                       "File Name: ", daSkin.GetStyle ("HUD_TextLeft"));
                            GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 30, position.width - (widthFloat / 3) - 20, 10),
                                       designList[i].name, daSkin.GetStyle ("HUD_TextLeft"));
                            GUI.Label (new Rect (widthFloat / 3 + 10, buttonPos + 50, position.width - (widthFloat / 3) - 20, 10),
                                       "Number of Elements: ", daSkin.GetStyle ("HUD_TextLeft"));
                            GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 65, position.width - (widthFloat / 3) - 20, 10),
                                       designList[i].transform.childCount.ToString(), daSkin.GetStyle ("HUD_TextLeft"));
                            if(designList[i].transform.childCount > 0)
                            {
                                GUI.Label (new Rect (widthFloat / 3 + 10, buttonPos + 85, position.width - (widthFloat / 3) - 20, 10),
                                           "Type of HUD: ", daSkin.GetStyle ("HUD_TextLeft"));
                                if(designList[i].transform.GetChild(0).childCount > 0)
                                {
                                    if (designList[i].transform.GetChild(0).GetChild(0).GetComponent<RectTransform>())
                                    {
                                        GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 100, position.width - (widthFloat / 3) - 20, 10),
                                                   "Unity GUI", daSkin.GetStyle ("HUD_TextLeft"));

                                    }
                                    else if(designList[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>())
                                    {
                                        GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 100, position.width - (widthFloat / 3) - 20, 10),
                                                   "Sprite Renderer", daSkin.GetStyle ("HUD_TextLeft"));
                                    }
                                }
                                else
                                {
                                    if (designList[i].transform.GetChild(0).GetComponent<RectTransform>())
                                    {
                                        GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 100, position.width - (widthFloat / 3) - 20, 10),
                                                   "Unity GUI", daSkin.GetStyle ("HUD_TextLeft"));

                                    }
                                    else if(designList[i].transform.GetChild(0).GetComponent<SpriteRenderer>())
                                    {
                                        GUI.Label (new Rect (widthFloat / 3 + 20, buttonPos + 100, position.width - (widthFloat / 3) - 20, 10),
                                                   "Sprite Renderer", daSkin.GetStyle ("HUD_TextLeft"));
                                    }
                                }
                            }

                            if(designList[i].name != "Current_HUD")
                            {
                                if(tempIntArr[i] == 5)
                                {
                                    if (GUI.Button (new Rect ((widthFloat / 3) + 10, buttonPos + 120,
                                                              (position.width - (widthFloat / 3) - 30) / 2, 30), "CANCEL", daSkin.GetStyle ("HUD_ButtonBlue")))
                                    {
                                        tempIntArr[i] = 0;
                                    }
                                    if (GUI.Button (new Rect ((widthFloat / 3) + ((position.width - (widthFloat / 3) - 30) / 2),
                                                              buttonPos + 120, (position.width - (widthFloat / 3) - 30) / 2, 30), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                                    {
                                        tempIntArr[i] = 0;
                                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(designList[i]));
                                        tempBoolOne = false;
                                        GameObject prevHudmain = hudMainObj;
                                        hudMainObj = Instantiate( (GameObject)AssetDatabase.LoadAssetAtPath( folderLoc + "/HudData/Temp/TempHUD.prefab", typeof(GameObject)),
                                                                  new Vector3(0, 0, 0), Quaternion.identity);
                                        if(daSettingInt[0] == 1)
                                        {
                                            hudMainObj.name = "HUD Designer Canvas";
                                            hudMainObj.transform.SetParent(daCanvas.transform);
                                        }
                                        else
                                        {
                                            hudMainObj.name = "HUD Designer";
                                        }
                                        DestroyImmediate(prevHudmain);
                                    }
                                }
                                else
                                {
                                    if (GUI.Button (new Rect ((widthFloat / 3) + 10, buttonPos + 120,
                                                              (position.width - (widthFloat / 3) - 30) / 2, 30), "DELETE", daSkin.GetStyle ("HUD_ButtonRed")))
                                    {
                                        tempIntArr[i] = 5;
                                    }
                                }
                            }
                            buttonPos = buttonPos + (widthFloat / 3) + 10;
                        }
                        else
                        {
                            tempBoolOne = false;
                        }
                    }
                }
            }
            if(menuBool[8])
            {
                GUI.Label (new Rect (10, buttonPos, 30, 40), "File Name: ", daSkin.GetStyle ("HUD_TextLeft"));

                menuString[0] = GUI.TextField(new Rect (80, buttonPos + 10, position.width - 50, 20), menuString[0], 25);

                buttonPos = buttonPos + 50;

                if (GUI.Button (new Rect (position.width / 2, buttonPos, position.width / 2, 65), "SAVE", daSkin.GetStyle ("HUD_ButtonGreen")))
                {
                    saveHUDprefab(menuString[0], false);
                    tempBoolOne = false;
                }
                if (GUI.Button (new Rect (0, buttonPos, position.width / 2, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    menuBool[8] = false;
                }
            }
            else
            {
                if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "SAVE CURRENT DESIGN", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    menuString[0] = "HUD Design " + designList.Count.ToString();
                    menuBool[8] = true;
                }
            }
            buttonPos = buttonPos + 70;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "APPLY", daSkin.GetStyle ("HUD_ButtonGreen")))
            {
                StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_SettingInfo.dat", false);
                writer.WriteLine( daSettingInt[0].ToString() );
                for(int i = 0; i < autoGenColor.Length; i++)
                {
                    writer.WriteLine( autoGenColor[i].r.ToString() );
                    writer.WriteLine( autoGenColor[i].g.ToString() );
                    writer.WriteLine( autoGenColor[i].b.ToString() );
                    writer.WriteLine( autoGenColor[i].a.ToString() );
                }
                writer.Close();

                FindTheTextures();
                CheckParents("ALL");

                mainPage = 0;
            }
            buttonPos = buttonPos + 70;
            if((GameObject)AssetDatabase.LoadAssetAtPath( folderLoc + "/HudData/Temp/TempHUD.prefab", typeof(GameObject)) != null)
            {
                if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonLightBlue")))
                {
                    GameObject prevHudmain = hudMainObj;
                    hudMainObj = Instantiate( (GameObject)AssetDatabase.LoadAssetAtPath( folderLoc + "/Prefabs/Current_HUD.prefab", typeof(GameObject)),
                                              new Vector3(0, 0, 0), Quaternion.identity);
                    if(hudMainObj.transform.childCount > 0)
                    {
                        if(hudMainObj.transform.GetChild(0).childCount > 0)
                        {
                            if (hudMainObj.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>())
                            {
                                daSettingInt[0] = 1;

                            }
                            else if(hudMainObj.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>())
                            {
                                daSettingInt[0] = 0;
                            }
                        }
                        else
                        {
                            if (hudMainObj.transform.GetChild(0).GetComponent<RectTransform>())
                            {
                                daSettingInt[0] = 1;

                            }
                            else if(hudMainObj.transform.GetChild(0).GetComponent<SpriteRenderer>())
                            {
                                daSettingInt[0] = 0;
                            }
                        }
                    }

                    if(daSettingInt[0] == 1)
                    {
                        hudMainObj.name = "HUD Designer Canvas";
                        if(daCanvas == null )
                        {
                            daCanvas = new GameObject("Canvas");
                            daCanvas.AddComponent<RectTransform>();
                            daCanvas.AddComponent<Canvas>();
                            daCanvas.AddComponent<CanvasScaler>();
                            daCanvas.AddComponent<GraphicRaycaster>();
                        }
                        hudMainObj.transform.SetParent(daCanvas.transform);
                    }
                    else
                    {
                        hudMainObj.name = "HUD Designer";
                    }
                    DestroyImmediate(prevHudmain);
                    mainPage = 0;
                }
                buttonPos = buttonPos + 80;
            }

            GUI.EndScrollView ();

            break;

        case 7:

            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));

            buttonPos = 10;

            GUI.Label (new Rect (10, buttonPos + 10, position.width - 20, 10), "HUD Generation Style:", daSkin.GetStyle ("HUD_TextLeft"));

            buttonPos = buttonPos + 30;

            daSettingInt[0] = GUI.SelectionGrid(new Rect(10, buttonPos, position.width - 20, 120),
                                                daSettingInt[0], settingsSelection, 1, daSkin.GetStyle("HUD_ButtonBlueSelection"));

            buttonPos = buttonPos + 140;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "BACK", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_SettingInfo.dat", false);
                writer.WriteLine( daSettingInt[0].ToString() );
                for(int i = 0; i < autoGenColor.Length; i++)
                {
                    writer.WriteLine( autoGenColor[i].r.ToString() );
                    writer.WriteLine( autoGenColor[i].g.ToString() );
                    writer.WriteLine( autoGenColor[i].b.ToString() );
                    writer.WriteLine( autoGenColor[i].a.ToString() );
                }
                writer.Close();

                FindTheTextures();
                CheckParents("ALL");
                mainPage = 0;
            }
            buttonPos = buttonPos + 80;
            GUI.EndScrollView ();

            break;

        case 8:
            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));
            buttonPos = 10;

            for(int i = 1; i <= (lineList.Count / 21); i++ )
            {
                int m = 0;
                for(int k = 0; k < 5; k++)
                {
                    GUI.backgroundColor = new Color(colorSchemeInfo[i, m], colorSchemeInfo[i, m + 1], colorSchemeInfo[i, m + 2], colorSchemeInfo[i, m + 3]);
                    GUI.Box (new Rect (10 + ((position.width / 8) * k), buttonPos, (position.width / 7), 30), "", daSkin.GetStyle ("HUD_ButtonWhite"));
                    m = m + 4;
                }

                GUI.backgroundColor = Color.white;

                if (GUI.Button (new Rect (30 + ((position.width / 8) * 5), buttonPos, (position.width / 5), 30), "X", daSkin.GetStyle ("HUD_ButtonRed")))
                {
                    List<string> lineListTemp = new List<string> ();
                    for(int k = 0; k < (lineList.Count / 21); k++)
                    {
                        if(i != (k + 1))
                        {
                            for(int y = 0; y < 21; y++)
                            {
                                lineListTemp.Add(lineList[(k * 21) + y]);
                            }
                        }
                    }
                    lineList.Clear();
                    lineList = lineListTemp;
                    readColorInfo(true);
                }
                buttonPos = buttonPos + 40;
            }

            buttonPos = buttonPos + 20;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "RELOAD PRESET COLORS", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                StreamReader sr         = new StreamReader(folderLoc + "/HudData/HUDDesigner_DefaultColorInfo.dat");
                List<string> tempList   = new List<string> ();

                while(!sr.EndOfStream)
                {
                    tempList.Add(sr.ReadLine());
                }
                sr.Close( );

                bool addStarted = false;

                for(int i = 0; i < lineList.Count; i++)
                {
                    if(lineList[i] == "HUD_COLOR_")
                    {
                        addStarted = true;
                    }
                    if(addStarted)
                    {
                        tempList.Add(lineList[i]);
                    }
                }

                lineList = tempList;

                StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_ColorInfo.dat", false);
                for(int i = 0; i < lineList.Count; i++)
                {
                    writer.WriteLine( lineList[i] );
                }
                writer.Close();
                readColorInfo(false);
            }
            buttonPos = buttonPos + 80;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "APPLY", daSkin.GetStyle ("HUD_ButtonGreen")))
            {
                StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_ColorInfo.dat", false);
                for(int i = 0; i < lineList.Count; i++)
                {
                    writer.WriteLine( lineList[i] );
                }
                writer.Close();
                readColorInfo(false);
                mainPage = 0;
            }
            buttonPos = buttonPos + 80;

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                readColorInfo(true);
                mainPage = 0;
            }
            buttonPos = buttonPos + 80;
            GUI.EndScrollView ();
            break;

        case 9:
            scrollPosition2 = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPosition2, new Rect (0, 0, position.width, buttonPos));
            buttonPos = 10;
            GUI.Label (new Rect (10, buttonPos, position.width - 20, 30), "CREATE NEW SHAPE", daSkin.GetStyle ("HUD_TextCenter"));
            buttonPos = buttonPos + 40;

            daAsset = (Object)EditorGUI.ObjectField(new Rect(10, buttonPos, position.width - 20, 20), "", daAsset, typeof(Object), false);
            if(daAsset != null && AssetDatabase.GetAssetPath(daAsset).Length > 4
                    && AssetDatabase.GetAssetPath(daAsset).Substring(AssetDatabase.GetAssetPath(daAsset).Length - 4, 4) != ".svg")
            {
                menuBool[15] = false;
                tempIntArr = new int [10];
                daAsset = null;
            }
            buttonPos = buttonPos + 20;

            GUI.Label (new Rect (10, buttonPos, position.width - 20, 30), "Please Load an SVG file above. For more information," +
                       " refer to the manual page", daSkin.GetStyle ("HUD_TextLeft"));
            buttonPos = buttonPos + 40;

            if(daAsset)
            {
                GUI.Label (new Rect (10, buttonPos, position.width / 2, 20), "Scale Divider", daSkin.GetStyle ("HUD_TextLeft"));
                menuString[6] = GUI.TextField(new Rect(position.width / 2, buttonPos, position.width / 2 - 20, 20), menuString[6], 25);
                buttonPos = buttonPos + 30;

                if (GUI.Button (new Rect (10, buttonPos, position.width - 20, 65), "FIND SHAPES", daSkin.GetStyle ("HUD_ButtonGreen")))
                {
                    menuBool[15] = false;
                    tempIntArr = new int [10];
                    tempIntOne = 1000;
                    ShapesReader(daAsset, menuString[6]);
                }
                buttonPos = buttonPos + 80;

                for(int i = 0; i < tempIntArr.Length; i++)
                {
                    if(tempIntArr[i] > 1)
                    {
                        string daStyle = "HUD_ButtonBlue";
                        if(tempIntOne == i)
                        {
                            daStyle = "HUD_ButtonGreen";
                        }
                        if (GUI.Button (new Rect (10, buttonPos, position.width - 20, 30), "Generate Shape " + (i + 1).ToString(), daSkin.GetStyle (daStyle)))
                        {
                            if(menuBool[15])
                            {
                                DestroyImmediate(currSelection);
                            }
                            ShapesCreator(i);
                            menuBool[15] = true;
                            scrollPosition2 = new Vector2(scrollPosition2.x, buttonPos);
                            tempIntOne = i;
                        }
                        buttonPos = buttonPos + 40;
                    }
                }

                if(menuBool[15] && currSelection != null)
                {
                    if(!EditorApplication.isPlaying && currSelection.GetComponent<LineRenderer>())
                    {
                        posMenu(position.width);
                    }
                    GUI.Label (new Rect (10, buttonPos, position.width - 20, 20), "Adjust the shape's position and bring it" +
                               "to the center of the screen for best results before saving", daSkin.GetStyle ("HUD_TextLeft"));
                    buttonPos = buttonPos + 40;

                    GUI.Label (new Rect (10, buttonPos, position.width / 2, 40), "File Name: ", daSkin.GetStyle ("HUD_TextLeft"));
                    menuString[0] = GUI.TextField(new Rect (position.width / 2, buttonPos + 10, position.width / 2, 20), menuString[0], 25);
                    buttonPos = buttonPos + 50;

                    if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "SAVE AS PREFAB", daSkin.GetStyle ("HUD_ButtonGreen")))
                    {
                        if(menuString[0] == "" || menuString[0] == " ")
                        {
                            menuString[0] = "0";
                        }
                        if(!AssetDatabase.IsValidFolder(folderLoc + "/Shapes"))
                        {
                            AssetDatabase.CreateFolder(folderLoc, "Shapes");
                        }
                        int tempyInt = 1;
                        for(int i = 0; i < shapesPrefabs.Length; i++)
                        {
                            if(shapesPrefabs[i] != null && shapesPrefabs[i].name == "HUDDESIGNER_SHAPES_" + menuString[0])
                            {
                                menuString[0] = menuString[0] + "_" + tempyInt.ToString();
                                tempyInt++;
                                i = 0;
                            }
                        }
                        PrefabUtility.SaveAsPrefabAsset(currSelection, folderLoc + "/Shapes/" + "HUDDESIGNER_SHAPES_" + menuString[0] + ".prefab");
                        CheckParents("Shapes");
                        FindTheTextures();
                    }
                    buttonPos = buttonPos + 80;
                }
            }

            if (GUI.Button (new Rect (0, buttonPos, position.width, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonLightBlue")))
            {
                while(GameObject.Find("SHAPES_TEMP") != null)
                {
                    DestroyImmediate(GameObject.Find("SHAPES_TEMP"));
                }
                CheckParents("Shapes");
                FindTheTextures();
                mainPage = 4;
            }
            buttonPos = buttonPos + 80;
            GUI.EndScrollView ();
            break;
        }
    }

    void RefreshColors(GameObject daObj)
    {
        if(daSettingInt[0] == 0 && daObj.GetComponent<SpriteRenderer>())
        {
            daObj.GetComponent<SpriteRenderer>().color = findMeAColor();
        }
        else if (daSettingInt[0] == 1 && daObj.GetComponent<Image>())
        {
            daObj.GetComponent<Image>().color = findMeAColor();
        }
        if(daObj.transform.childCount > 0)
        {
            for(int i = 0; i < daObj.transform.childCount; i ++)
            {
                GameObject daObjTwo = daObj.transform.GetChild(i).gameObject;
                if(daSettingInt[0] == 0 && daObjTwo.GetComponent<SpriteRenderer>())
                {
                    daObjTwo.GetComponent<SpriteRenderer>().color = findMeAColor();
                }
                else if (daSettingInt[0] == 1 && daObjTwo.GetComponent<Image>())
                {
                    daObjTwo.GetComponent<Image>().color = findMeAColor();
                }
                else if (daObjTwo.GetComponent<LineRenderer>())
                {
                    SetShapeColor(daObjTwo);
                }
                if(daObjTwo.transform.childCount > 0)
                {
                    for(int k = 0; k < daObjTwo.transform.childCount; k++)
                    {
                        GameObject daObjThree = daObjTwo.transform.GetChild(k).gameObject;
                        if(daSettingInt[0] == 0 && daObjThree.GetComponent<SpriteRenderer>())
                        {
                            daObjThree.GetComponent<SpriteRenderer>().color = findMeAColor();
                        }
                        else if (daSettingInt[0] == 1 && daObjThree.GetComponent<Image>())
                        {
                            daObjThree.GetComponent<Image>().color = findMeAColor();
                        }
                        if(daObjThree.transform.childCount > 0)
                        {
                            for(int m = 0; m < daObjThree.transform.childCount; m++)
                            {
                                GameObject daObjFour = daObjThree.transform.GetChild(m).gameObject;
                                if(daSettingInt[0] == 0 && daObjFour.GetComponent<SpriteRenderer>())
                                {
                                    daObjFour.GetComponent<SpriteRenderer>().color = findMeAColor();
                                }
                                else if (daSettingInt[0] == 1 && daObjFour.GetComponent<Image>())
                                {
                                    daObjFour.GetComponent<Image>().color = findMeAColor();
                                }
                            }
                        }
                    }
                }
            }

        }
    }

    Color findMeAColor()
    {
        float daRandomizer = Random.value;
        if(daRandomizer < 0.2f)
        {
            return autoGenColor[0];
        }
        else if(daRandomizer < 0.4f)
        {
            return autoGenColor[1];
        }
        else if(daRandomizer < 0.6f)
        {
            return autoGenColor[2];
        }
        else if(daRandomizer < 0.8f)
        {
            return autoGenColor[3];
        }
        else
        {
            return autoGenColor[4];
        }
    }

    void daSideScrollPanel(Texture2D[] ObjType)
    {
        scrollPosition = GUI.BeginScrollView (new Rect (0, 0, widthFloat / 4 + 15, position.height), scrollPosition, new Rect (0, 0, position.width, tempIntOne));
        tempIntOne = 0;
        if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 4f), "", daSkin.GetStyle ("HUD_ButtonCustomImage")))
        {
            menuBool[9] = (menuBool[9]) ? false : true;
            deleteConfirm = false;
            previewOn = false;
        }
        tempIntOne = tempIntOne + Mathf.FloorToInt (widthFloat / 4f);
        foreach (Texture2D daTexture in ObjType)
        {
            if(daTexture != null)
            {
                if( (daSettingInt[0] == 0 && ( (currSelectedLayer != currSelection && currSelectedLayer != null && currSelectedLayer.GetComponent<SpriteRenderer>()
                                                && currSelectedLayer.GetComponent<SpriteRenderer>().sprite != null
                                                && currSelectedLayer.GetComponent<SpriteRenderer>().sprite.texture == daTexture)
                                               || (currSelection != null && mainPage == 3 &&  currSelection.GetComponent<SpriteRenderer>()
                                                   && currSelection.GetComponent<SpriteRenderer>().sprite.texture == daTexture)))
                        || (daSettingInt[0] == 1
                            && ( (currSelectedLayer != currSelection && currSelectedLayer != null && currSelectedLayer.GetComponent<Image>()
                                  && currSelectedLayer.GetComponent<Image>().sprite != null
                                  && currSelectedLayer.GetComponent<Image>().sprite.texture == daTexture) ||
                                 (currSelection != null && mainPage == 3 &&  currSelection.GetComponent<Image>()
                                  && currSelection.GetComponent<Image>().sprite.texture == daTexture))))
                {
                    if(mainPage == 3)
                    {
                        if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daTexture, daSkin.GetStyle ("HUD_ButtonLightBlue")))
                        {
                            tempyTexture = daTexture;
                            if(currSelectedLayer == currSelection || currSelection.GetComponent<SpriteRenderer>() || currSelection.GetComponent<Image>())
                            {
                                if(menuBool[4] && mainPage == 3)
                                {
                                    tempInt2DArr = new int [30, 30];
                                    readGenInfo(true);
                                }
                                previewOn = true;
                            }
                            else if(daSettingInt[0] == 0 && currSelectedLayer != null && currSelectedLayer.GetComponent<SpriteRenderer>())
                            {
                                currSelectedLayer.GetComponent<SpriteRenderer>().sprite =
                                    (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daTexture), typeof(Sprite));
                            }
                            else if (daSettingInt[0] == 1 && currSelectedLayer != null && currSelectedLayer.GetComponent<Image>())
                            {
                                currSelectedLayer.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daTexture), typeof(Sprite));
                            }
                            deleteConfirm = false;
                        }
                    }
                    else
                    {
                        GUI.Box (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daTexture, daSkin.GetStyle ("HUD_ButtonLightBlue"));
                    }
                    if(!tempBoolOne)
                    {
                        scrollPosition = new Vector2(0, tempIntOne);
                        tempBoolOne = true;
                    }
                }
                else
                {
                    if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daTexture, daSkin.GetStyle ("HUD_ButtonBlack")))
                    {
                        if(menuBool[9])
                        {
                            menuBool[9] = false;
                        }
                        else
                        {
                            tempyTexture = daTexture;
                            if(currSelectedLayer == currSelection || currSelection.GetComponent<SpriteRenderer>() || currSelection.GetComponent<Image>())
                            {
                                if(menuBool[4] && mainPage == 3)
                                {
                                    tempInt2DArr = new int [30, 30];
                                    readGenInfo(true);
                                }
                                previewOn = true;
                            }
                            else if(daSettingInt[0] == 0 && currSelectedLayer != null && currSelectedLayer.GetComponent<SpriteRenderer>())
                            {
                                currSelectedLayer.GetComponent<SpriteRenderer>().sprite =
                                    (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daTexture), typeof(Sprite));
                            }

                            else if (daSettingInt[0] == 1 && currSelectedLayer != null && currSelectedLayer.GetComponent<Image>())
                            {
                                currSelectedLayer.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daTexture), typeof(Sprite));
                            }
                        }
                        deleteConfirm = false;
                    }
                }
                tempIntOne = tempIntOne + Mathf.FloorToInt (widthFloat / 3.2f);
            }
        }
        GUI.EndScrollView();
    }

    void daSideScrollPanelPrefab(GameObject[] ObjType)
    {
        scrollPosition = GUI.BeginScrollView (new Rect (0, 0, widthFloat / 4 + 15, position.height), scrollPosition, new Rect (0, 0, position.width, tempIntOne));
        tempIntOne = 0;
        if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 4f), "", daSkin.GetStyle ("HUD_ButtonCustomImage")))
        {
            menuBool[9] = (menuBool[9]) ? false : true;
            spriteArr[0] = null;
            deleteConfirm = false;
            previewOn = false;
        }
        tempIntOne = tempIntOne + Mathf.FloorToInt (widthFloat / 4f);
        foreach (GameObject daObject in ObjType)
        {
            if(daObject != null)
            {
                string daStyle = "HUD_ButtonBlack";
                Texture2D tempTex = AssetPreview.GetAssetPreview(daObject);
                if (daSettingInt[0] == 1 && daObject.GetComponent<Image>())
                {
                    tempTex = daObject.GetComponent<Image>().sprite.texture;
                }


                if(currSelection != null && daObject.GetComponent<Animator>() && currSelection.GetComponent<Animator>()
                        &&  daObject.GetComponent<Animator>().runtimeAnimatorController ==  currSelection.GetComponent<Animator>().runtimeAnimatorController)
                {
                    if(!previewOn)
                        tempyTexture = AssetPreview.GetAssetPreview(daObject);

                    daStyle = "HUD_ButtonGreen";

                    if(!tempBoolOne)
                    {
                        scrollPosition = new Vector2(0, tempIntOne);
                        tempBoolOne = true;
                    }
                }
                if(tempyObject == daObject)
                {
                    daStyle = "HUD_ButtonLightBlue";
                }
                if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), tempTex, daSkin.GetStyle (daStyle)))
                {
                    tempyObject = daObject;
                    if (daSettingInt[0] == 1 && daObject.GetComponent<Image>())
                    {
                        tempyTexture = daObject.GetComponent<Image>().sprite.texture;
                    }
                    else
                    {
                        tempyTexture = AssetPreview.GetAssetPreview(daObject);
                    }
                    previewOn = true;
                    menuBool[9] = false;
                }

                tempIntOne = tempIntOne + Mathf.FloorToInt (widthFloat / 3.2f);
            }
        }
        GUI.EndScrollView();
    }

    void daSideScrollPanelShapes(GameObject[] ObjType)
    {
        scrollPosition = GUI.BeginScrollView (new Rect (0, 0, widthFloat / 4 + 15, position.height), scrollPosition, new Rect (0, 0, position.width, tempIntOne));
        tempIntOne = 0;
        tempyTexture = null;
        foreach (GameObject daObject in ObjType)
        {
            if(daObject != null && daObject.name.Length > 19 && daObject.name.Substring(0, 19) == "HUDDESIGNER_SHAPES_")
            {
                string daName = daObject.name.Substring(19, (daObject.name.Length - 19));
                if(currSelection != null && daObject.GetComponent<LineRenderer>() && currSelection.GetComponent<LineRenderer>()
                        &&  daObject.GetComponent<LineRenderer>() ==  currSelection.GetComponent<LineRenderer>())
                {
                    GUI.Box (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daName, daSkin.GetStyle ("HUD_ButtonGreen"));
                    if(!tempBoolOne)
                    {
                        scrollPosition = new Vector2(0, tempIntOne);
                        tempBoolOne = true;
                    }
                }
                else
                {
                    if(tempyObject == daObject)
                    {
                        GUI.Box (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daName, daSkin.GetStyle ("HUD_ButtonLightBlue"));
                    }
                    else
                    {
                        if (GUI.Button (new Rect (0, tempIntOne, widthFloat / 4, widthFloat / 3.2f), daName, daSkin.GetStyle ("HUD_ButtonBlack")))
                        {
                            tempyObject = daObject;
                            previewOn = true;
                        }
                    }
                }
                tempIntOne = tempIntOne + Mathf.FloorToInt (widthFloat / 3.2f);
            }
        }

        GUI.EndScrollView();
    }

    void posMenu(float daWidth)
    {
        buttonPos = buttonPos + 10;

        if(!menuBool[7])
        {
            GUI.Box (new Rect (0, buttonPos - 5, daWidth, ((daWidth / 5) * 3) + 70), "", daSkin.GetStyle ("HUD_BoxBlack"));
        }
        else
        {
            GUI.Box (new Rect (0, buttonPos - 5, daWidth, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
        }

        if (GUI.Button (new Rect (0, buttonPos + 10, daWidth, 10), "POSITION", daSkin.GetStyle ("HUD_TextCenter")))
        {
            menuBool[7] = (menuBool[7]) ? false : true;
        }

        buttonPos = buttonPos + 40;

        if(!menuBool[7])
        {
            if(mainPage == 4 || mainPage == 9)
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the shape UP by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowU")))
                {
                    for(int i = 0; i < currSelection.GetComponent<LineRenderer>().positionCount; i++)
                    {
                        Vector3 daPos = new Vector3(currSelection.GetComponent<LineRenderer>().GetPosition(i).x,
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).y + tempFloatArr[0],
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).z );
                        currSelection.GetComponent<LineRenderer>().SetPosition(i, daPos);
                    }
                    mainPageTemp = 99;
                }

                if (GUI.Button (new Rect (daWidth / 5 - 10, buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the shape Left by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowL")))
                {
                    for(int i = 0; i < currSelection.GetComponent<LineRenderer>().positionCount; i++)
                    {
                        Vector3 daPos = new Vector3(currSelection.GetComponent<LineRenderer>().GetPosition(i).x - tempFloatArr[0],
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).y,
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).z );
                        currSelection.GetComponent<LineRenderer>().SetPosition(i, daPos);
                    }
                    mainPageTemp = 99;
                }
            }
            else
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object UP by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowU")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempPos = currSelection.transform.position;
                        currSelection.transform.position = new Vector3(tempPos.x, tempPos.y + tempFloatArr[0], tempPos.z);
                    }
                    else
                    {
                        Vector3 tempPos = currSelectedLayer.transform.position;
                        currSelectedLayer.transform.position = new Vector3(tempPos.x, tempPos.y + tempFloatArr[0], tempPos.z);
                    }
                    mainPageTemp = 99;
                }

                if (GUI.Button (new Rect (daWidth / 5 - 10, buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object Left by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowL")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempPos = currSelection.transform.position;
                        currSelection.transform.position = new Vector3(tempPos.x - tempFloatArr[0], tempPos.y, tempPos.z);
                    }
                    else
                    {
                        Vector3 tempPos = currSelectedLayer.transform.position;
                        currSelectedLayer.transform.position = new Vector3(tempPos.x - tempFloatArr[0], tempPos.y, tempPos.z);
                    }
                    mainPageTemp = 99;
                }
            }
            if(tempFloatArr[0] == 1f)
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5), "1X", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    tempFloatArr[0] = 0.5f;
                }
            }
            else if (tempFloatArr[0] == 0.5f)
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5), "0.5X", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    tempFloatArr[0] = 0.1f;
                }
            }
            else if (tempFloatArr[0] == 0.1f)
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5), "0.1X", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    tempFloatArr[0] = 0.05f;
                }
            }
            else if (tempFloatArr[0] == 0.05f)
            {
                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5), ".05X", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    tempFloatArr[0] = 1f;
                }
            }
            else
            {
                tempFloatArr[0] = 1f;
            }

            if(mainPage == 4 || mainPage == 9)
            {
                if (GUI.Button (new Rect ((daWidth / 5) * 3 + 10, buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object RIGHT by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowR")))
                {
                    for(int i = 0; i < currSelection.GetComponent<LineRenderer>().positionCount; i++)
                    {
                        Vector3 daPos = new Vector3(currSelection.GetComponent<LineRenderer>().GetPosition(i).x + tempFloatArr[0],
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).y,
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).z );
                        currSelection.GetComponent<LineRenderer>().SetPosition(i, daPos);
                    }
                    mainPageTemp = 99;
                }

                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + ((daWidth / 5) * 2) + 20, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object DOWN by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowD")))
                {
                    for(int i = 0; i < currSelection.GetComponent<LineRenderer>().positionCount; i++)
                    {
                        Vector3 daPos = new Vector3(currSelection.GetComponent<LineRenderer>().GetPosition(i).x,
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).y - tempFloatArr[0],
                                                    currSelection.GetComponent<LineRenderer>().GetPosition(i).z );
                        currSelection.GetComponent<LineRenderer>().SetPosition(i, daPos);
                    }
                    mainPageTemp = 99;
                }
            }
            else

            {
                if (GUI.Button (new Rect ((daWidth / 5) * 3 + 10, buttonPos + (daWidth / 5) + 10, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object RIGHT by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowR")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempPos = currSelection.transform.position;
                        currSelection.transform.position = new Vector3(tempPos.x + tempFloatArr[0], tempPos.y, tempPos.z);
                    }
                    else
                    {
                        Vector3 tempPos = currSelectedLayer.transform.position;
                        currSelectedLayer.transform.position = new Vector3(tempPos.x + tempFloatArr[0], tempPos.y, tempPos.z);
                    }
                    mainPageTemp = 99;
                }

                if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos + ((daWidth / 5) * 2) + 20, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Move the object DOWN by " + tempFloatArr[0].ToString() + " times"), daSkin.GetStyle ("HUD_ButtonArrowD")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempPos = currSelection.transform.position;
                        currSelection.transform.position = new Vector3(tempPos.x, tempPos.y - tempFloatArr[0], tempPos.z);
                    }
                    else
                    {
                        Vector3 tempPos = currSelectedLayer.transform.position;
                        currSelectedLayer.transform.position = new Vector3(tempPos.x, tempPos.y - tempFloatArr[0], tempPos.z);
                    }
                    mainPageTemp = 99;
                }
            }
            buttonPos = buttonPos + ((daWidth / 5) * 3) + 30;

        }
        if(mainPage != 4 && mainPage != 9)
        {
            if(!menuBool[8])
            {
                GUI.Box (new Rect (0, buttonPos, daWidth, (daWidth / 5) + 50), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }
            else
            {
                GUI.Box (new Rect (0, buttonPos, daWidth, 40), "", daSkin.GetStyle ("HUD_BoxBlack"));
            }

            if (GUI.Button (new Rect (0, buttonPos + 15, daWidth, 10), "SCALE", daSkin.GetStyle ("HUD_TextCenter")))
            {
                menuBool[8] = (menuBool[8]) ? false : true;
            }

            buttonPos = buttonPos + 40;

            if(!menuBool[8])
            {
                if (GUI.Button (new Rect (daWidth / 5 - 10, buttonPos, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Decrease the size by " + (tempFloatArr[1] * 10f).ToString() + " times"), daSkin.GetStyle ("HUD_ButtonMinus")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempScale = currSelection.transform.localScale;
                        currSelection.transform.localScale = new Vector3(tempScale.x - tempFloatArr[1], tempScale.y - tempFloatArr[1], tempScale.z);
                    }
                    else
                    {
                        Vector3 tempScale = currSelectedLayer.transform.localScale;
                        currSelectedLayer.transform.localScale = new Vector3(tempScale.x - tempFloatArr[1], tempScale.y - tempFloatArr[1], tempScale.z);
                    }
                    mainPageTemp = 99;
                }
                if (GUI.Button (new Rect ((daWidth / 5) * 3 + 10, buttonPos, daWidth / 5, daWidth / 5),
                                new GUIContent("", "Increase the size by " + (tempFloatArr[1] * 10f).ToString() + " times"), daSkin.GetStyle ("HUD_ButtonPlus")))
                {
                    if(currSelectedLayer == null)
                    {
                        Vector3 tempScale = currSelection.transform.localScale;
                        currSelection.transform.localScale = new Vector3(tempScale.x + tempFloatArr[1], tempScale.y + tempFloatArr[1], tempScale.z);
                    }
                    else
                    {
                        Vector3 tempScale = currSelectedLayer.transform.localScale;
                        currSelectedLayer.transform.localScale = new Vector3(tempScale.x + tempFloatArr[1], tempScale.y + tempFloatArr[1], tempScale.z);
                    }
                    mainPageTemp = 99;
                }

                if(tempFloatArr[1] == 0.1f)
                {
                    if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5), "1X", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        tempFloatArr[1] = 0.05f;
                    }
                }
                else if (tempFloatArr[1] == 0.05f)
                {
                    if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5), ".5X", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        tempFloatArr[1] = 0.005f;
                    }
                }
                else if (tempFloatArr[1] == 0.005f)
                {
                    if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5), ".05X", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        tempFloatArr[1] = 0.001f;
                    }
                }
                else if (tempFloatArr[1] == 0.001f)
                {
                    if (GUI.Button (new Rect (daWidth / 2 - (daWidth / 10), buttonPos, daWidth / 5, daWidth / 5), ".01X", daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        tempFloatArr[1] = 0.1f;
                    }
                }
                else
                {
                    tempFloatArr[1] = 0.1f;
                }

                buttonPos = buttonPos + (daWidth / 5) + 10;
            }
        }

        buttonPos = buttonPos + 10;
    }

    void isolateToggleVoid()
    {
        if(isolateToggle)
        {
            for(int i = 0; i < hudMainObj.transform.childCount; i++)
            {
                if(hudMainObj.transform.GetChild(i).gameObject != currSelection)
                {
                    hudMainObj.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    hudMainObj.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for(int i = 0; i < hudMainObj.transform.childCount; i++)
            {
                hudMainObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        isolateToggleChecker = isolateToggle;
    }

    void readGenInfo(bool justRead)
    {
        genInfo = new StreamReader(folderLoc + "/HudData/HUDDesigner_GenInfo.dat");
        bool tempStartBool = false;
        tempStringArr.Clear();
        if(justRead)
        {
            int k = 0;
            while(!genInfo.EndOfStream)
            {
                string inp_ln = genInfo.ReadLine();
                if(!tempStartBool && inp_ln == tempyTexture.name)
                {
                    tempStartBool = true;
                }
                else if (tempStartBool && inp_ln.Length > 12 && inp_ln.Substring(0, 12) == "HUDDESIGNER_" && inp_ln != tempyTexture.name)
                {
                    break;
                }
                else if (tempStartBool)
                {
                    for(int i = 0; i < inp_ln.Length; i++)
                    {
                        if (inp_ln.Substring(i, 1) != null)
                        {
                            if(int.Parse(inp_ln.Substring(i, 1)) >= 0)
                            {
                                tempInt2DArr[i, k] = int.Parse(inp_ln.Substring(i, 1));
                            }
                        }
                    }
                    k++;
                }
            }
            genInfo.Close( );
        }
        else
        {
            while(!genInfo.EndOfStream)
            {
                string inp_ln = genInfo.ReadLine();
                if(!tempStartBool && inp_ln == tempyTexture.name)
                {
                    tempStartBool = true;

                }
                else if (tempStartBool && inp_ln.Length > 12 && inp_ln.Substring(0, 12) == "HUDDESIGNER_" && inp_ln != tempyTexture.name)
                {
                    tempStringArr.Add(inp_ln);
                    tempStartBool = false;
                }
                else if (!tempStartBool)
                {
                    tempStringArr.Add(inp_ln);
                }
            }

            genInfo.Close( );
            writeGenInfo(true, tempyTexture.name, 0);
        }
    }

    void theAuthor(string theFilePath, string[] theLines, bool isSingleLine, string daSpacer)
    {
        StreamWriter writer = new StreamWriter(theFilePath, false);

        if(isSingleLine)
        {
            string tempStr = "";
            for(int i = 0; i < theLines.Length; i++)
            {
                tempStr = tempStr + theLines[i] + daSpacer;
            }
            writer.WriteLine(tempStr);
        }
        else
        {
            foreach(string daStr in theLines)
            {
                writer.WriteLine(daStr);
            }
        }
        writer.Close();
    }

    string[] theReader(string theFilePath)
    {
        List<string> tempyList   = new List<string> ();
        StreamReader theReadFile = new StreamReader(theFilePath);
        while(!theReadFile.EndOfStream)
        {
            tempyList.Add(genInfo.ReadLine());
        }
        theReadFile.Close();
        if(tempyList.Count > 0)
        {
            string[] tempyArr = new string[tempyList.Count];
            for(int i = 0; i < tempyList.Count; i++)
            {
                tempyArr[i] = tempyList[i];
            }
            return tempyArr;
        }
        else
        {
            return null;
        }
    }

    void writeGenInfo(bool newLine, string daText, int lineNumber)
    {
        StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_GenInfo.dat", false);
        foreach(string daStr in tempStringArr)
        {
            writer.WriteLine(daStr);
        }
        if(newLine)
        {
            writer.WriteLine(daText);
            for(int i = 0; i < 20; i ++)
            {
                string anotherTempystring = "";
                string previousTempyString = "";
                for(int k = 0; k < 20; k++)
                {
                    previousTempyString = anotherTempystring + tempInt2DArr[k, i].ToString();
                    anotherTempystring = previousTempyString;
                }
                writer.WriteLine(anotherTempystring);
            }
            writer.Close();
        }
    }

    void previewWindow (Texture2D daImgPreview, GameObject daObject)
    {
        if(daImgPreview == null && daObject != null && mainPage == 4)
        {
            if(mainPage == 4 && daObject != null && daObject.name.Length > 19 && daObject.name.Substring(0, 19) == "HUDDESIGNER_SHAPES_")
            {
                GUI.Label (new Rect (0, 0, layoutSize, layoutSize), daObject.name.Substring(19, (daObject.name.Length - 19)), daSkin.GetStyle ("HUD_TextCenter"));
            }
            else if(mainPage == 5 && daObject != null && daObject.name.Length > 12 && daObject.name.Substring(0, 12) == "HUDDESIGNER_")
            {
                GUI.Label (new Rect (0, 0, layoutSize, layoutSize), daObject.name.Substring(12, (daObject.name.Length - 12)), daSkin.GetStyle ("HUD_TextCenter"));
            }
        }
        else
        {
            GUI.Box (new Rect (0, 0, layoutSize, layoutSize), daImgPreview, daSkin.GetStyle ("HUD_ButtonBlack"));
        }
        if(menuBool[4] && mainPage == 3)
        {
            for(int i = 0; i < 20; i ++)
            {
                for(int k = 0; k < 20; k++)
                {
                    if(tempInt2DArr[i, k] == 1)
                    {
                        if (GUI.Button (new Rect (i * (layoutSize / 20), k * (layoutSize / 20), (layoutSize / 20),
                                                  (layoutSize / 20)), "", daSkin.GetStyle ("HUD_ButtonRedFadeON")))
                        {
                            tempInt2DArr[i, k] = 0;
                            readGenInfo(false);
                        }
                    }
                    else
                    {
                        if (GUI.Button (new Rect (i * (layoutSize / 20), k * (layoutSize / 20), (layoutSize / 20),
                                                  (layoutSize / 20)), "", daSkin.GetStyle ("HUD_ButtonRedFade")))
                        {
                            tempInt2DArr[i, k] = 1;
                            readGenInfo(false);
                        }
                    }
                }
            }
        }

        buttonPos = layoutSize;

        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CREATE", daSkin.GetStyle ("HUD_ButtonGreen")))
        {
            previewOn = false;
            int daCounterTwo = 0;
            bool wasIsolated = false;
            if(isolateToggle)
            {
                isolateToggle = false;
                wasIsolated = true;
                isolateToggleVoid();
            }
            switch(mainPage)
            {
            case 2:
                while (GameObject.Find ("Circle_" + daCounterTwo) != null)
                {
                    daCounterTwo++;
                    if(daCounterTwo > 100)
                        break;
                }
                GenStuff (daImgPreview, 1, 1, 0, 0, hudMainObj, "Circle_" + daCounterTwo, Color.white, "Circle", true);
                CheckParents("Circle");
                break;
            case 3:
                GenSquare(daImgPreview, 1, 1, 0, 0, hudMainObj, "Square_");
                CheckParents("Square");
                break;
            case 4:
                string daName = "Shape_";
                if(daObject.name.Length > 19 && daObject.name.Substring(0, 19) == "HUDDESIGNER_SHAPES_")
                {
                    daName = daObject.name.Substring(19, (daObject.name.Length - 19)) + "_";
                }
                GenPrefab( daObject, daName, true, Vector3.zero, Vector3.one );
                CheckParents("Shapes");
                break;
            case 5:
                if(currSelection == null)
                {
                    GenPrefab( daObject, "Animated_", true, Vector3.zero, Vector3.one );
                }
                else
                {
                    GenPrefab( daObject, "Animated_", true, currSelection.transform.position, Vector3.one );
                }
                CheckParents("Animated");
                break;
            }

            if(wasIsolated)
            {
                isolateToggle = true;
                isolateToggleVoid();
            }
            mainPageTemp = 99;

        }
        buttonPos = buttonPos + 65;
        if(mainPage == 2 || mainPage == 3)
        {
            if( daSettingInt[0] == 0 &&
                    ((currSelection != null && currSelection.GetComponent<SpriteRenderer>()) || (currSelectedLayer != null && currSelectedLayer.GetComponent<SpriteRenderer>())))
            {
                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "REPLACE", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    if (currSelectedLayer != null && currSelectedLayer.GetComponent<SpriteRenderer>() != null)
                    {
                        currSelectedLayer.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daImgPreview), typeof(Sprite));
                    }
                    else if(currSelection != null && currSelection.GetComponent<SpriteRenderer>() != null)
                    {
                        currSelection.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daImgPreview), typeof(Sprite));
                    }
                    previewOn = false;
                }
                buttonPos = buttonPos + 65;
            }
            else if( daSettingInt[0] == 1 &&
                     ((currSelection != null && currSelection.GetComponent<Image>()) || (currSelectedLayer != null && currSelectedLayer.GetComponent<Image>())))
            {
                if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "REPLACE", daSkin.GetStyle ("HUD_ButtonBlue")))
                {
                    if (currSelectedLayer != null && currSelectedLayer.GetComponent<Image>() != null)
                    {
                        currSelectedLayer.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daImgPreview), typeof(Sprite));
                    }
                    else if(currSelection != null && currSelection.GetComponent<Image>() != null)
                    {
                        currSelection.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(daImgPreview), typeof(Sprite));
                    }
                    previewOn = false;
                }
                buttonPos = buttonPos + 65;
            }
        }
        else if (mainPage == 5)
        {
            if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "REPLACE", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                GameObject currSelectionTemp = Instantiate(daObject, currSelection.transform.position, Quaternion.identity);
                currSelectionTemp.transform.SetParent(hudMainObj.transform);
                if(currSelectionTemp.GetComponent<SpriteRenderer>() && currSelection.GetComponent<SpriteRenderer>())
                    currSelectionTemp.GetComponent<SpriteRenderer>().color = currSelection.GetComponent<SpriteRenderer>().color;
                if(currSelectionTemp.GetComponent<AnimatedScript>() && currSelection.GetComponent<AnimatedScript>())
                {
                    currSelectionTemp.GetComponent<AnimatedScript>().animSpeed = currSelection.GetComponent<AnimatedScript>().animSpeed;
                    currSelectionTemp.GetComponent<AnimatedScript>().rotationSpeed = currSelection.GetComponent<AnimatedScript>().rotationSpeed;
                }
                currSelectionTemp.transform.localScale = currSelection.transform.localScale;
                currSelectionTemp.name = currSelection.name;
                DestroyImmediate(currSelection);
                currSelection = currSelectionTemp;
                previewOn = false;
            }
            buttonPos = buttonPos + 65;
        }

        if(mainPage == 3)
        {
            buttonPos = buttonPos + 10;
            menuBool[4] = GUI.Toggle(new Rect(10, buttonPos, layoutSize, 30), menuBool[4], "Auto Generate Areas");
            buttonPos = buttonPos + 30;
        }

        if(mainPage == 2 || mainPage == 3 || mainPage == 5)
        {
            if( ((mainPage == 2 || mainPage == 3 ) && ((daImgPreview.name.Length > 22 && daImgPreview.name.Substring(0, 22) == "HUDDESIGNER_CIRCLE_USR") ||
                    (daImgPreview.name.Length > 23 && daImgPreview.name.Substring(0, 23) == "HUDDESIGNER_Square_USR_"))) ||
                    (mainPage == 5 && daObject.name.Length > 25 && daObject.name.Substring(0, 25) == "HUDDESIGNER_ANIMATED_USR_"))
            {
                if(deleteConfirm)
                {
                    if (GUI.Button (new Rect (0, buttonPos, layoutSize / 2, 60), "CANCEL", daSkin.GetStyle ("HUD_ButtonGreen")))
                    {
                        deleteConfirm = false;
                    }
                    if (GUI.Button (new Rect (layoutSize / 2, buttonPos, layoutSize / 2, 60), "CONFIRM", daSkin.GetStyle ("HUD_ButtonRed")))
                    {
                        deleteConfirm = false;
                        if(mainPage == 2 || mainPage == 3 )
                        {
                            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(daImgPreview));
                        }
                        else if (mainPage == 5)
                        {
                            Motion daAnim = (Motion)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/SpriteData/" + daObject.name + ".anim", typeof (Motion));

                            if(daAnim != null)
                            {
                                AssetDatabase.DeleteAsset(folderLoc + "/Animated/SpriteData/" + daObject.name + ".anim");
                                daAnim = null;
                            }

                            daAnim = (Motion)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/UIdata/" + daObject.name + "_UI.anim", typeof (Motion));

                            if(daAnim != null)
                                AssetDatabase.DeleteAsset(folderLoc + "/Animated/UIdata/" + daObject.name + "_UI.anim");

                            RuntimeAnimatorController tempControl = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath(folderLoc +
                                                                    "/Animated/SpriteData/" + daObject.name + ".controller", typeof (RuntimeAnimatorController));

                            if(tempControl != null)
                            {
                                AssetDatabase.DeleteAsset(folderLoc + "/Animated/SpriteData/" + daObject.name + ".controller");
                                tempControl = null;
                            }

                            tempControl = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath(folderLoc +
                                          "/Animated/UIdata/" + daObject.name + "_UI.controller", typeof (RuntimeAnimatorController));

                            if(tempControl != null)
                                AssetDatabase.DeleteAsset(folderLoc + "/Animated/UIdata/" + daObject.name + "_UI.controller");

                            Object ob = (Object)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/SpriteAnim/" + daObject.name + ".prefab", typeof (Object));

                            if(ob != null)
                            {
                                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(ob));
                                ob = null;
                            }

                            ob = (Object)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/UIAnim/" + daObject.name + ".prefab", typeof (Object));

                            if(ob != null)
                                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(ob));
                        }
                        FindTheTextures();
                        previewOn = false;
                    }
                }
                else
                {
                    string daMsg = "DELETE IMAGE";
                    if(mainPage == 5)
                    {
                        daMsg = "DELETE ANIMATION";
                    }

                    if (GUI.Button (new Rect (0, buttonPos, layoutSize, 60), daMsg, daSkin.GetStyle ("HUD_ButtonBlue")))
                    {
                        deleteConfirm = true;
                    }
                }
                buttonPos = buttonPos + 65;
            }
        }

        if (GUI.Button (new Rect (0, buttonPos, layoutSize, 65), "CANCEL", daSkin.GetStyle ("HUD_ButtonRed")))
        {
            previewOn = false;
        }
        buttonPos = buttonPos + 70;
    }

    void CheckParents (string checkType)
    {
        if(checkType == "Circle" || checkType == "ALL")
        {
            circleObjects.Clear ();
            if (hudMainObj != null && hudMainObj.transform.childCount > 0)
            {
                for (int i = 0; i < hudMainObj.transform.childCount; i++)
                {
                    GameObject daHudChild = hudMainObj.transform.GetChild (i).gameObject;
                    if ((daHudChild.transform.childCount > 0 && daHudChild.transform.GetChild (0).GetComponent<CircleScript> ()) || daHudChild.GetComponent<CircleScript>())
                    {
                        circleObjects.Add (daHudChild);
                    }
                }
            }
        }
        if(checkType == "Square" || checkType == "ALL")
        {
            squareObjects.Clear ();
            if (hudMainObj != null && hudMainObj.transform.childCount > 0)
            {
                for (int i = 0; i < hudMainObj.transform.childCount; i++)
                {
                    GameObject daHudChild = hudMainObj.transform.GetChild (i).gameObject;
                    if ((daHudChild.transform.childCount > 0 && daHudChild.transform.GetChild (0).GetComponent<SquareScript> ()) || daHudChild.GetComponent<SquareScript>())
                    {
                        squareObjects.Add (daHudChild);
                    }
                }
            }
        }
        if(checkType == "Shapes" || checkType == "ALL")
        {
            shapesObjects.Clear ();
            if (hudMainObj != null && hudMainObj.transform.childCount > 0)
            {
                for (int i = 0; i < hudMainObj.transform.childCount; i++)
                {
                    GameObject daHudChild = hudMainObj.transform.GetChild (i).gameObject;
                    if (daHudChild.GetComponent<ShapesScript>() || (daHudChild.transform.childCount > 0 && daHudChild.transform.GetChild (0).GetComponent<ShapesScript> ()))
                    {
                        daHudChild.GetComponent<LineRenderer>().material = shapesMaterial;
                        shapesObjects.Add (daHudChild);
                    }
                }
            }
        }
        if(checkType == "Animated" || checkType == "ALL")
        {
            animatedObjects.Clear ();
            if (hudMainObj != null && hudMainObj.transform.childCount > 0)
            {
                for (int i = 0; i < hudMainObj.transform.childCount; i++)
                {
                    GameObject daHudChild = hudMainObj.transform.GetChild (i).gameObject;
                    if (daHudChild.GetComponent<AnimatedScript>() || (daHudChild.transform.childCount > 0 && daHudChild.transform.GetChild (0).GetComponent<AnimatedScript> ()))
                    {
                        animatedObjects.Add (daHudChild);
                    }
                }
            }
        }
    }

    void FindTheTextures()
    {
        string[] circlePaths = AssetDatabase.FindAssets ("HUDDESIGNER_CIRCLE_");
        if (circlePaths.Length > 0)
        {
            Texture2D[] circleTextureTemp = new Texture2D[circlePaths.Length];
            int daCounter = 0;
            foreach (string guid in circlePaths)
            {
                circleTextureTemp[daCounter] = (Texture2D) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (Texture2D));

                if(daCounter == 0)
                {
                    daCounter++;
                }
                else if(circleTextureTemp[daCounter] != null && circleTextureTemp[daCounter - 1] != null
                        && circleTextureTemp[daCounter] != circleTextureTemp[daCounter - 1])
                {
                    circleTextureTemp[daCounter - 1] = circleTextureTemp[daCounter];
                    circleTextureTemp[daCounter] = null;
                }
                else
                {
                    daCounter++;
                }
            }
            for(int i = daCounter; i < circleTextureTemp.Length; i++)
            {
                circleTextureTemp[i] = null;
            }
            circleTexture = new Texture2D[daCounter - 1];
            int tempCount = 0;
            for(int i = 0; i < daCounter - 1; i++)
            {
                if(circleTextureTemp[i] != null && circleTextureTemp[i].name.Length > 22 && circleTextureTemp[i].name.Substring(0, 22) == "HUDDESIGNER_CIRCLE_USR")
                {
                    circleTexture[tempCount] = circleTextureTemp[i];
                    tempCount++;
                }
            }
            for(int i = 0; i < daCounter - 1; i++)
            {
                if(circleTextureTemp[i] != null && (circleTextureTemp[i].name.Length <= 22 ||
                                                    (circleTextureTemp[i].name.Length > 22 && circleTextureTemp[i].name.Substring(0, 22) != "HUDDESIGNER_CIRCLE_USR")))
                {
                    circleTexture[tempCount] = circleTextureTemp[i];
                    tempCount++;
                }
            }
        }
        else
        {
            Debug.Log ("ERROR 102: Cannot find Circles Directory, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
        }
        string[] squarePaths = AssetDatabase.FindAssets ("HUDDESIGNER_Square_");
        if (squarePaths.Length > 0)
        {
            Texture2D[] squareTextureTemp = new Texture2D[squarePaths.Length];
            int daCounter = 0;
            foreach (string guid in squarePaths)
            {
                squareTextureTemp[daCounter] = (Texture2D) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (Texture2D));

                if(daCounter == 0)
                {
                    daCounter++;
                }
                else if(squareTextureTemp[daCounter] != null && squareTextureTemp[daCounter - 1] != null
                        && squareTextureTemp[daCounter] != squareTextureTemp[daCounter - 1])
                {
                    squareTextureTemp[daCounter - 1] = squareTextureTemp[daCounter];
                    squareTextureTemp[daCounter] = null;
                }
                else
                {
                    daCounter++;
                }
            }
            for(int i = daCounter; i < squareTextureTemp.Length; i++)
            {
                squareTextureTemp[i] = null;
            }
            squareTexture = new Texture2D[daCounter - 1];
            int tempCount = 0;
            for(int i = 0; i < daCounter - 1; i++)
            {
                if(squareTextureTemp[i] != null && squareTextureTemp[i].name.Length > 23 && squareTextureTemp[i].name.Substring(0, 23) == "HUDDESIGNER_Square_USR_")
                {
                    squareTexture[tempCount] = squareTextureTemp[i];
                    tempCount++;
                }
            }
            for(int i = 0; i < daCounter - 1; i++)
            {
                if(squareTextureTemp[i] != null && (squareTextureTemp[i].name.Length <= 23 ||
                                                    (squareTextureTemp[i].name.Length > 23 && squareTextureTemp[i].name.Substring(0, 23) != "HUDDESIGNER_Square_USR_")))
                {
                    squareTexture[tempCount] = squareTextureTemp[i];
                    tempCount++;
                }
            }
        }
        else
        {
            Debug.Log ("ERROR 103: Cannot find Squares Directory, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
        }

        string[] daFolderArr = {folderLoc + "/Animated/SpriteAnim"};

        if(daSettingInt[0] == 1)
        {
            daFolderArr[0] = folderLoc + "/Animated/UIAnim";
        }
        string[] animatedPaths = AssetDatabase.FindAssets("HUDDESIGNER_ANIMATED_", daFolderArr);
        if (animatedPaths.Length > 0)
        {
            GameObject[] animatedPrefabTemp = new GameObject[animatedPaths.Length];
            int daCounter = 0;
            foreach (string guid in animatedPaths)
            {
                animatedPrefabTemp[daCounter] = (GameObject) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (GameObject));
                daCounter++;
            }

            animatedPrefabs = new GameObject[daCounter];

            int daCounterTwo = 0;
            for(int i = 0; i < daCounter; i++)
            {
                if(animatedPrefabTemp[i] != null && animatedPrefabTemp[i].name.Length > 25 && animatedPrefabTemp[i].name.Substring(0, 25) == "HUDDESIGNER_ANIMATED_USR_")
                {
                    animatedPrefabs[daCounterTwo] = animatedPrefabTemp[i];
                    daCounterTwo++;
                }
            }

            for(int i = 0; i < daCounter; i++)
            {
                if(animatedPrefabTemp[i] != null && (animatedPrefabTemp[i].name.Length < 25 ||
                                                     (animatedPrefabTemp[i].name.Length > 25 && animatedPrefabTemp[i].name.Substring(0, 25) != "HUDDESIGNER_ANIMATED_USR_")))
                {
                    animatedPrefabs[daCounterTwo] = animatedPrefabTemp[i];
                    daCounterTwo++;
                }
            }
        }
        else
        {
            Debug.Log ("ERROR 104: Cannot find Animated Directory, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
        }

        string[] shapesPaths = AssetDatabase.FindAssets ("HUDDESIGNER_SHAPES_");
        if (shapesPaths.Length > 0)
        {
            GameObject[] shapesPrefabTemp = new GameObject[shapesPaths.Length];
            int daCounter = 0;
            foreach (string guid in shapesPaths)
            {
                shapesPrefabTemp[daCounter] = (GameObject) AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath (guid), typeof (GameObject));

                if(daCounter == 0)
                {
                    daCounter++;
                }
                else
                {
                    daCounter++;
                }
            }

            for(int i = daCounter; i < shapesPrefabTemp.Length; i++)
            {
                shapesPrefabTemp[i] = null;
            }
            shapesPrefabs = new GameObject[daCounter];
            for(int i = 0; i < daCounter; i++)
            {
                shapesPrefabs[i] = shapesPrefabTemp[i];
            }
        }
        else
        {
            Debug.Log ("ERROR 105: Cannot find Shapes Directory, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
        }
    }

    void nextPrev (bool selNext, List<GameObject> ObjType)
    {
        if(mainPage == 2)
        {
            menuString[0] = "0";
            menuString[1] = "10";
            menuString[2] = "0";
            menuString[3] = "20";
            menuString[4] = "0";
            menuString[5] = "360";
            menuString[6] = "0";
            menuString[7] = "10";
        }

        deleteConfirm = false;

        if (selNext)
        {
            for (int i = 0; i < ObjType.Count; i++)
            {
                if (ObjType[i] == currSelection)
                {
                    if (i + 1 < ObjType.Count)
                    {
                        if (ObjType[i + 1] == null)
                        {
                            CheckParents ("ALL");
                            i = 0;
                        }
                        else
                        {
                            currSelection = ObjType[i + 1];
                            break;
                        }
                    }
                    else
                    {
                        if (ObjType[ObjType.Count - 1] == null)
                        {
                            CheckParents ("ALL");
                            i = 0;
                        }
                        else
                        {
                            currSelection = ObjType[0];
                            break;
                        }
                    }
                }
            }
            if(currSelection == null && ObjType.Count > 0)
            {
                currSelection = ObjType[0];
            }
        }
        else
        {
            for (int i = ObjType.Count - 1; i >= 0; i--)
            {
                if (ObjType[i] == currSelection)
                {
                    if (i - 1 >= 0)
                    {
                        if (ObjType[i - 1] == null)
                        {
                            CheckParents ("ALL");
                            i = ObjType.Count - 1;
                        }
                        else
                        {
                            currSelection = ObjType[i - 1];
                            break;
                        }
                    }
                    else
                    {
                        if (ObjType[0] == null)
                        {
                            CheckParents ("ALL");
                            i = ObjType.Count - 1;
                        }
                        else
                        {
                            currSelection = ObjType[ObjType.Count - 1];
                            break;
                        }
                    }
                }
            }
        }
        if((mainPage == 3 || mainPage == 5) && currSelection != null)
        {
            if(currSelection.GetComponent<AnimatedScript>())
            {
                menuString[1] = menuString[0] = currSelection.GetComponent<AnimatedScript>().animSpeed.ToString();
                menuString[2] = menuString[3] = currSelection.GetComponent<AnimatedScript>().rotationSpeed.ToString();
            }

            if(currSelection.GetComponent<SpriteRenderer>())
            {
                tempColor = currSelection.GetComponent<SpriteRenderer>().color;
            }
        }

        if(isolateToggle)
        {
            for(int i = 0; i < hudMainObj.transform.childCount; i++)
            {
                if(hudMainObj.transform.GetChild(i).gameObject != currSelection)
                {
                    hudMainObj.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    hudMainObj.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        currSelectedLayer = currSelection;

        tempBoolOne = false;
    }

    void nextPrevLayer (bool selNext, GameObject daParentObj)
    {
        deleteConfirm = false;
        if (selNext)
        {
            if(daParentObj == currSelectedLayer)
            {
                currSelectedLayer = daParentObj.transform.GetChild(0).gameObject;
            }
            else
            {
                for (int i = 0; i < daParentObj.transform.childCount; i++)
                {
                    if(daParentObj.transform.GetChild(i).gameObject == currSelectedLayer)
                    {
                        if(i + 1 < daParentObj.transform.childCount)
                        {
                            currSelectedLayer = daParentObj.transform.GetChild(i + 1).gameObject;
                            break;
                        }
                        else
                        {
                            currSelectedLayer = daParentObj;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            if(daParentObj == currSelectedLayer)
            {
                currSelectedLayer = daParentObj.transform.GetChild(daParentObj.transform.childCount - 1).gameObject;
            }
            else
            {
                for (int i = daParentObj.transform.childCount - 1; i >= 0; i--)
                {
                    if(daParentObj.transform.GetChild(i).gameObject == currSelectedLayer)
                    {
                        if(i - 1 >= 0)
                        {
                            currSelectedLayer = daParentObj.transform.GetChild(i - 1).gameObject;
                            break;
                        }
                        else
                        {
                            currSelectedLayer = daParentObj;
                            break;
                        }
                    }
                }
            }
        }
        if(currSelectedLayer == daParentObj)
        {
            scrollPosition = new Vector2 (0, 0);
        }
        if(currSelectedLayer != null && currSelectedLayer.GetComponent<CircleScript>())
        {
            if(daSettingInt[0] == 0 && currSelectedLayer.GetComponent<SpriteRenderer>())
            {
                tempColor = currSelectedLayer.GetComponent<SpriteRenderer>().color;
            }
            else if(daSettingInt[0] == 1 && currSelectedLayer.GetComponent<Image>())
            {
                tempColor = currSelectedLayer.GetComponent<Image>().color;
            }
            menuBool[5] = currSelectedLayer.GetComponent<CircleScript>().randomizeValues;
            menuString[0] = menuString[1] = currSelectedLayer.GetComponent<CircleScript>().minRotationSpeed.ToString();
            menuString[2] = menuString[3]  = currSelectedLayer.GetComponent<CircleScript>().maxRotationSpeed.ToString();
            menuString[4] = menuString[5]  = currSelectedLayer.GetComponent<CircleScript>().maxRotateAngle.ToString();
            menuString[6] = menuString[7]  = currSelectedLayer.GetComponent<CircleScript>().maxStopDuration.ToString();
        }
        tempBoolOne = false;
    }


    void GenHUD (int maxCircles, int maxSquares, int maxShapes, int maxAnim)
    {
        if(!menuBool[1])
        {
            for(int i = hudMainObj.transform.childCount - 1; i >= 0; i--)
            {
                if(hudMainObj.transform.GetChild(i).GetComponent<SquareScript>())
                {
                    DestroyImmediate(hudMainObj.transform.GetChild(i).gameObject);
                }
            }
        }
        if(!menuBool[0])
        {
            for(int i = hudMainObj.transform.childCount - 1; i >= 0; i--)
            {
                if(hudMainObj.transform.GetChild(i).transform.childCount > 0
                        && hudMainObj.transform.GetChild(i).GetChild(0).GetComponent<CircleScript>())
                {
                    DestroyImmediate(hudMainObj.transform.GetChild(i).gameObject);
                }
            }
        }
        if(!menuBool[2])
        {
            for(int i = hudMainObj.transform.childCount - 1; i >= 0; i--)
            {
                if(hudMainObj.transform.GetChild(i).GetComponent<ShapesScript>())
                {
                    DestroyImmediate(hudMainObj.transform.GetChild(i).gameObject);
                }
            }
        }
        if(!menuBool[3])
        {
            for(int i = hudMainObj.transform.childCount - 1; i >= 0; i--)
            {
                if(hudMainObj.transform.GetChild(i).GetComponent<AnimatedScript>())
                {
                    DestroyImmediate(hudMainObj.transform.GetChild(i).gameObject);
                }
            }
        }

        tempyTexture = null;

        if(!menuBool[11])
        {
            if(!menuBool[1] && squareTexture.Length > 0)
            {
                tempyTexture = squareTexture[Mathf.FloorToInt(Random.Range(0, squareTexture.Length))];
                if(daSettingInt[0] == 0)
                {
                    GenSquare(tempyTexture, 1, 1, 0, 0, hudMainObj, "Square_");
                }
                else if(daSettingInt[0] == 1)
                {
                    GenSquare(tempyTexture, 20, 10, 0, 0, hudMainObj, "Square_");
                }
            }
            else if (menuBool[1] && squareTexture.Length > 0)
            {
                for(int i = 0; i < hudMainObj.transform.childCount; i++)
                {
                    if (hudMainObj.transform.GetChild(i).GetComponent<SquareScript>())
                    {
                        if(daSettingInt[0] == 0 && hudMainObj.transform.GetChild(i).GetComponent<SpriteRenderer>())
                        {
                            tempyTexture = hudMainObj.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.texture;
                            break;
                        }
                        else if(daSettingInt[0] == 1 && hudMainObj.transform.GetChild(i).GetComponent<Image>())
                        {
                            tempyTexture = hudMainObj.transform.GetChild(i).GetComponent<Image>().sprite.texture;
                            break;
                        }
                    }
                }
            }

            for(int i = 0; i < maxSquares; i++)
            {
                Texture2D tempyTexture2 = squareTexture[Mathf.FloorToInt(Random.Range(0, squareTexture.Length))];
                float randSize = Random.Range (0.05f, 0.3f);
                if(daSettingInt[0] == 1)
                    randSize = Random.Range (1.00f, 5.00f);

                int daCounterTwo = 0;
                while (GameObject.Find ("Square_" + daCounterTwo.ToString()) != null)
                {
                    daCounterTwo++;
                    if(daCounterTwo > 100)
                        break;
                }
                string daSqName = "Square_" + daCounterTwo.ToString();

                if(tempyTexture != null)
                {
                    readGenInfo(true);
                    bool foundSpot = false;
                    int loopCount = 0;
                    while(!foundSpot && loopCount < 30)
                    {
                        int locX = Mathf.FloorToInt(Random.Range(0, 20));
                        int locY = Mathf.FloorToInt(Random.Range(0, 20));
                        if(tempInt2DArr[locX, locY] == 1)
                        {
                            float daPosX = locX - 10f;
                            float daPosY = 10f - locY;
                            GenSquare(tempyTexture2, randSize, randSize, daPosX, daPosY, hudMainObj, daSqName);
                            break;
                        }
                        loopCount++;
                        if(loopCount >= 30)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    float randPosX = 0;
                    float randPosY = 0;
                    float daRandomizer = Random.value;
                    if (daRandomizer < 0.2)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(1.5f, 2.5f);
                    }
                    else if (daRandomizer < 0.4)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(-2.5f, -1.5f);
                    }
                    else if (daRandomizer < 0.6)
                    {
                        randPosX = Random.Range(-4f, -7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else if (daRandomizer < 8)
                    {
                        randPosX = Random.Range(4f, 7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else
                    {
                        randPosX = randPosY = 0;
                    }
                    GenSquare(tempyTexture2, randSize, randSize, randPosX, randPosY, hudMainObj, daSqName);
                }
            }
        }

        if(!menuBool[0] && !menuBool[10])
        {
            for(int i = 0; i < maxCircles; i++)
            {
                int randLayerCount = Mathf.FloorToInt(Random.Range(1, 8));

                float randSize = Random.Range (0.1f, 0.3f);
                if(daSettingInt[0] == 1)
                    randSize = 1;

                if(tempyTexture != null)
                {
                    readGenInfo(true);
                    bool foundSpot = false;
                    int loopCount = 0;
                    while(!foundSpot && loopCount < 30)
                    {
                        int locX = Mathf.FloorToInt(Random.Range(0, 20));
                        int locY = Mathf.FloorToInt(Random.Range(0, 20));
                        if(tempInt2DArr[locX, locY] == 1)
                        {
                            float daPosX = locX - 10f;
                            float daPosY = 10f - locY;
                            GenCircle (randLayerCount, randSize, randSize, daPosX, daPosY, hudMainObj, "Circle_");
                            break;
                        }
                        loopCount++;
                        if(loopCount >= 30)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    float randPosX = 0;
                    float randPosY = 0;
                    float daRandomizer = Random.value;
                    if (daRandomizer < 0.2)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(1.5f, 2.5f);
                    }
                    else if (daRandomizer < 0.4)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(-2.5f, -1.5f);
                    }
                    else if (daRandomizer < 0.6)
                    {
                        randPosX = Random.Range(-4f, -7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else if (daRandomizer < 8)
                    {
                        randPosX = Random.Range(4f, 7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else
                    {
                        randPosX = randPosY = 0;
                    }
                    GenCircle (randLayerCount, randSize, randSize, randPosX, randPosY, hudMainObj, "Circle_");
                }
            }
        }

        if(!menuBool[2] && !menuBool[12])
        {
            for(int i = 0; i < maxShapes; i++)
            {
                if(tempyTexture != null)
                {
                    readGenInfo(true);
                    bool foundSpot = false;
                    int loopCount = 0;
                    while(!foundSpot && loopCount < 30)
                    {
                        int locX = Mathf.FloorToInt(Random.Range(0, 20));
                        int locY = Mathf.FloorToInt(Random.Range(0, 20));
                        if(tempInt2DArr[locX, locY] == 1)
                        {
                            float daPosX = locX - 10f;
                            float daPosY = 10f - locY;
                            int daRan = Random.Range(0, shapesPrefabs.Length);
                            string daName = "Shape_";
                            if(shapesPrefabs[daRan].name.Length > 19 && shapesPrefabs[daRan].name.Substring(0, 19) == "HUDDESIGNER_SHAPES_")
                            {
                                daName = shapesPrefabs[daRan].name.Substring(19, (shapesPrefabs[daRan].name.Length - 19)) + "_";
                            }
                            GenPrefab( shapesPrefabs[daRan], daName, true, new Vector3(daPosX, daPosY, 0), Vector3.one );
                            break;
                        }
                        loopCount++;
                        if(loopCount >= 30)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    float randPosX = 0;
                    float randPosY = 0;
                    float daRandomizer = Random.value;
                    if (daRandomizer < 0.2)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(1.5f, 2.5f);
                    }
                    else if (daRandomizer < 0.4)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(-2.5f, -1.5f);
                    }
                    else if (daRandomizer < 0.6)
                    {
                        randPosX = Random.Range(-4f, -7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else if (daRandomizer < 8)
                    {
                        randPosX = Random.Range(4f, 7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else
                    {
                        randPosX = randPosY = 0;
                    }
                    int daRan = Random.Range(0, shapesPrefabs.Length);
                    string daName = "Shape_";
                    if(shapesPrefabs[daRan].name.Length > 19 && shapesPrefabs[daRan].name.Substring(0, 19) == "HUDDESIGNER_SHAPES_")
                    {
                        daName = shapesPrefabs[daRan].name.Substring(19, (shapesPrefabs[daRan].name.Length - 19)) + "_";
                    }
                    GenPrefab( shapesPrefabs[daRan], daName, true, new Vector3(randPosX, randPosY, 0), Vector3.one );
                }
                if(currSelection != null && currSelection.GetComponent<ShapesScript>() && currSelection.GetComponent<LineRenderer>())
                {
                    currSelection.GetComponent<ShapesScript>().speed = Random.Range(0.10f, 3.00f);
                    currSelection.GetComponent<ShapesScript>().distance = Random.Range(1.00f, 5.00f);
                    currSelection.GetComponent<ShapesScript>().maxDistance = new Vector3(1, 1, 0);
                    currSelection.GetComponent<LineRenderer>().startWidth = currSelection.GetComponent<LineRenderer>().endWidth = Random.Range(0.01f, 0.1f);
                }
            }
        }

        if(!menuBool[3] && !menuBool[13])
        {
            for(int i = 0; i < maxAnim; i++)
            {
                if(tempyTexture != null)
                {
                    readGenInfo(true);
                    bool foundSpot = false;
                    int loopCount = 0;
                    while(!foundSpot && loopCount < 30)
                    {
                        int locX = Mathf.FloorToInt(Random.Range(0, 20));
                        int locY = Mathf.FloorToInt(Random.Range(0, 20));
                        if(tempInt2DArr[locX, locY] == 1)
                        {
                            float daPosX = locX - 10f;
                            float daPosY = 10f - locY;
                            int daRan = Random.Range(0, animatedPrefabs.Length);
                            float randSize = Random.Range(0.20f, 1.00f);
                            if(daSettingInt[0] == 1)
                                randSize = Random.Range(0.20f, 0.40f);

                            GenPrefab( animatedPrefabs[daRan], "Animated_", false, new Vector3(daPosX, daPosY, 0), new Vector3(randSize, randSize, randSize) );
                            break;
                        }
                        loopCount++;
                        if(loopCount >= 30)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    float randPosX = 0;
                    float randPosY = 0;
                    float daRandomizer = Random.value;
                    if (daRandomizer < 0.2)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(1.5f, 2.5f);
                    }
                    else if (daRandomizer < 0.4)
                    {
                        randPosX = Random.Range(-7f, 7f);
                        randPosY = Random.Range(-2.5f, -1.5f);
                    }
                    else if (daRandomizer < 0.6)
                    {
                        randPosX = Random.Range(-4f, -7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else if (daRandomizer < 8)
                    {
                        randPosX = Random.Range(4f, 7f);
                        randPosY = Random.Range(-2.5f, -2.5f);
                    }
                    else
                    {
                        randPosX = randPosY = 0;
                    }
                    int daRan = Random.Range(0, animatedPrefabs.Length);
                    float randSize = Random.Range(0.20f, 1.00f);
                    GenPrefab( animatedPrefabs[daRan], "Animated_", false, new Vector3(randPosX, randPosY, 0), new Vector3(randSize, randSize, randSize) );
                }
            }
        }
    }

    void GenSquare(Texture2D daImg, float daWidth, float daHeight, float xPos, float yPos, GameObject daParent, string daObjName)
    {
        int daCounterTwo = 0;
        while (GameObject.Find (daObjName + daCounterTwo) != null)
        {
            daCounterTwo++;
            if(daCounterTwo > 100)
                break;
        }
        GenStuff (daImg, daWidth, daHeight, xPos, yPos, hudMainObj, daObjName + daCounterTwo, findMeAColor(), "Square", true);

        CheckParents("Square");
    }

    void GenCircle (int numOfLayers, float daWidth, float daHeight, float xPos, float yPos, GameObject daParent, string daObjName)
    {
        numOfLayers++;
        int tempyInt = 0;
        while (GameObject.Find (daObjName + tempyInt) != null)
        {
            tempyInt++;
        }

        GameObject daCircleParent = new GameObject (daObjName + tempyInt);
        daCircleParent.transform.position = new Vector3 (xPos, yPos, 0);
        daCircleParent.transform.SetParent (hudMainObj.transform);
        float maxSize = 1f;

        if(daSettingInt[0] == 1)
            maxSize = Random.Range(6.00f, 2.00f);

        float subWidth = maxSize / numOfLayers;
        float subHeight = maxSize / numOfLayers;
        for (float i = 1; i < numOfLayers; i++)
        {
            Texture2D daTex = circleTexture[Mathf.RoundToInt (Random.Range (0, circleTexture.Length))];
            GenStuff (daTex, maxSize - subWidth, maxSize - subHeight, 0, 0, daCircleParent, daObjName + "Layer_" + i, findMeAColor(), "Circle", false);

            subWidth = subWidth + (maxSize / numOfLayers);
            subHeight = subHeight + (maxSize / numOfLayers);
        }
        daCircleParent.transform.localScale = new Vector3(daWidth, daHeight, 1);
    }

    void GenStuff (Texture2D tex, float daWidth, float daHeight, float xPos, float yPos,
                   GameObject daParent, string daObjName, Color daColor, string daScriptName, bool makeCurrSel)
    {
        GameObject daObj = new GameObject (daObjName);
        daObj.transform.SetParent (daParent.transform);

        if(daSettingInt[0] == 0)
        {
            daObj.transform.localScale = new Vector3 (daWidth, daHeight, 1);
            daObj.transform.localPosition = new Vector3 (xPos, yPos, 0);
            daObj.AddComponent (typeof (SpriteRenderer));
            daObj.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(tex), typeof(Sprite));
            daObj.GetComponent<SpriteRenderer>().color = daColor;
        }
        else if(daSettingInt[0] == 1)
        {
            daObj.AddComponent<RectTransform>();
            daObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            daObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (xPos, yPos, 0);
            daObj.GetComponent<RectTransform>().sizeDelta =  new Vector3 (daWidth, daHeight, 1);
            daObj.AddComponent (typeof (Image));
            daObj.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(tex), typeof(Sprite));
            daObj.GetComponent<Image>().color = daColor;
        }
        switch (daScriptName)
        {
        case "Circle":
            daObj.AddComponent (typeof (CircleScript));
            daObj.GetComponent<CircleScript> ().maxRotateAngle = Random.Range (30, 360);
            daObj.GetComponent<CircleScript> ().minRotationSpeed = Random.Range (1, 30);
            daObj.GetComponent<CircleScript> ().maxRotationSpeed = Random.Range (daObj.GetComponent<CircleScript> ().minRotationSpeed, 200);
            daObj.GetComponent<CircleScript> ().maxStopDuration = Random.Range (0, 10);
            break;
        case "Square":
            daObj.AddComponent (typeof (SquareScript));
            break;
        }
        if(makeCurrSel)
        {
            currSelection = daObj;
        }
    }

    void GenPrefab( GameObject daObj, string daName, bool setCurrSel, Vector3 daPos, Vector3 daScale )
    {
        daObj = Instantiate(daObj, daPos, Quaternion.identity);
        daObj.transform.SetParent(hudMainObj.transform);
        int daCounter = 0;
        while (GameObject.Find (daName + daCounter) != null)
        {
            daCounter++;
            if(daCounter > 100)
                break;
        }
        daObj.name = daName + daCounter.ToString();
        daObj.transform.localScale = daScale;
        if(daObj.GetComponent<SpriteRenderer>())
        {
            daObj.GetComponent<SpriteRenderer>().color = findMeAColor();
        }
        else if (daObj.GetComponent<Image>())
        {
            daObj.GetComponent<Image>().color = findMeAColor();
        }
        if(daObj.GetComponent<AnimatedScript>())
        {
            if(Random.value > 0.8f)
            {
                daObj.GetComponent<AnimatedScript>().rotationSpeed = Random.Range(0.0f, 5.0f);
            }
            else
            {
                daObj.GetComponent<AnimatedScript>().rotationSpeed = 0;
            }
            daObj.GetComponent<AnimatedScript>().animSpeed = Random.Range(0.1f, 3.0f);
        }
        if(daObj.GetComponent<LineRenderer>())
        {
            for(int i = 0; i < daObj.GetComponent<LineRenderer>().positionCount; i++)
            {
                Vector3 daPosy = new Vector3(daObj.GetComponent<LineRenderer>().GetPosition(i).x + daPos.x,
                                             daObj.GetComponent<LineRenderer>().GetPosition(i).y + daPos.y,
                                             daObj.GetComponent<LineRenderer>().GetPosition(i).z );
                daObj.GetComponent<LineRenderer>().SetPosition(i, daPosy);
            }
            daObj.GetComponent<LineRenderer>().material = shapesMaterial;
            SetShapeColor(daObj);
        }

        if(setCurrSel)
        {
            currSelection = daObj;
        }

        tempColor = Color.white;
    }

    void ColorSchemeButtons( float maxWidth )
    {
        autoGenColor[0] = EditorGUI.ColorField(new Rect(10, buttonPos, maxWidth - 30, 20), "Color 1", autoGenColor[0]);
        buttonPos = buttonPos + 30;
        autoGenColor[1] = EditorGUI.ColorField(new Rect(10, buttonPos, maxWidth - 30, 20), "Color 2", autoGenColor[1]);
        buttonPos = buttonPos + 30;
        autoGenColor[2] = EditorGUI.ColorField(new Rect(10, buttonPos, maxWidth - 30, 20), "Color 3", autoGenColor[2]);
        buttonPos = buttonPos + 30;
        autoGenColor[3] = EditorGUI.ColorField(new Rect(10, buttonPos, maxWidth - 30, 20), "Color 4", autoGenColor[3]);
        buttonPos = buttonPos + 30;
        autoGenColor[4] = EditorGUI.ColorField(new Rect(10, buttonPos, maxWidth - 30, 20), "Color 5", autoGenColor[4]);
        buttonPos = buttonPos + 40;

        if (GUI.Button (new Rect (0, buttonPos, maxWidth, 50), "SAVE COLOR AS PRESET", daSkin.GetStyle ("HUD_ButtonBlue")))
        {
            StreamWriter writer = new StreamWriter(folderLoc + "/HudData/HUDDesigner_ColorInfo.dat", false);
            for(int i = 0; i < lineList.Count; i++)
            {
                writer.WriteLine( lineList[i] );
            }
            writer.WriteLine("HUD_COLOR_");
            for(int i = 0; i < 5; i++)
            {
                writer.WriteLine(autoGenColor[i].r.ToString());
                writer.WriteLine(autoGenColor[i].g.ToString());
                writer.WriteLine(autoGenColor[i].b.ToString());
                writer.WriteLine(autoGenColor[i].a.ToString());
            }
            writer.Close();
            readColorInfo(false);
            cp = ((lineList.Count / 21) - (((lineList.Count / 21) % 5)));
        }

        buttonPos = buttonPos + 70;

        if (GUI.Button (new Rect (0, buttonPos + 60, maxWidth / 7, maxWidth / 7), "", daSkin.GetStyle ("HUD_ButtonLeft")))
        {
            if(cp <= 0)
            {
                cp = ((lineList.Count / 21) - (((lineList.Count / 21) % 5)));
            }
            else
            {
                cp = cp - 5;
            }
            readColorInfo(true);
        }
        if (GUI.Button (new Rect (maxWidth - (maxWidth / 6), buttonPos + 60, maxWidth / 7, maxWidth / 7), "", daSkin.GetStyle ("HUD_ButtonRight")))
        {
            if(cp >= ((lineList.Count / 21) - 5))
            {
                cp = 0;
            }
            else
            {
                cp = cp + 5;
            }
            readColorInfo(true);
        }


        if(menuBool[14])
        {
            for(int i = 1; i <= 5; i++ )
            {
                int daVal = i + cp;
                int m = 0;
                if(lineList.Count >= (daVal * 21))
                {
                    for(int k = 0; k < 5; k++)
                    {
                        GUI.backgroundColor = new Color(colorSchemeInfo[daVal, m], colorSchemeInfo[daVal, m + 1], colorSchemeInfo[daVal, m + 2], colorSchemeInfo[daVal, m + 3]);
                        GUI.Box (new Rect ((maxWidth / 5.5f) + ((maxWidth / 8) * k), buttonPos, (maxWidth / 8), 30), "", daSkin.GetStyle ("HUD_ButtonWhite"));
                        m = m + 4;
                    }
                    if (GUI.Button (new Rect ((maxWidth / 5) + 20, buttonPos, maxWidth - ((maxWidth / 5) * 2) - 40, 30), "", daSkin.GetStyle ("HUD_TextCenter")))
                    {
                        for(int k = 0; k < 5; k++)
                        {
                            autoGenColor[k] = new Color (colorSchemeInfo[daVal, k], colorSchemeInfo[daVal, k + 1], colorSchemeInfo[daVal, k + 2], colorSchemeInfo[daVal, k + 3]);
                            k = k + 3;
                        }

                        autoGenColor[0] = new Color (colorSchemeInfo[daVal, 0], colorSchemeInfo[daVal, 1], colorSchemeInfo[daVal, 2], colorSchemeInfo[daVal, 3]);
                        autoGenColor[1] = new Color (colorSchemeInfo[daVal, 4], colorSchemeInfo[daVal, 5], colorSchemeInfo[daVal, 6], colorSchemeInfo[daVal, 7]);
                        autoGenColor[2] = new Color (colorSchemeInfo[daVal, 8], colorSchemeInfo[daVal, 9], colorSchemeInfo[daVal, 10], colorSchemeInfo[daVal, 11]);
                        autoGenColor[3] = new Color (colorSchemeInfo[daVal, 12], colorSchemeInfo[daVal, 13], colorSchemeInfo[daVal, 14], colorSchemeInfo[daVal, 15]);
                        autoGenColor[4] = new Color (colorSchemeInfo[daVal, 16], colorSchemeInfo[daVal, 17], colorSchemeInfo[daVal, 18], colorSchemeInfo[daVal, 19]);
                    }
                }
                buttonPos = buttonPos + 40;


            }
        }
        else
        {
            menuBool[14] = true;
            readColorInfo(true);
        }
        GUI.backgroundColor = Color.white;
    }

    void GenShape( string daObjName, Vector3 startPos, int numOfPoints, float maxX, float maxY, float daSpeed, float lineLength, float lineWidth, int shapeType )
    {
        int daCounterTwo = 0;
        while (GameObject.Find (daObjName + daCounterTwo) != null)
        {
            daCounterTwo++;
            if(daCounterTwo > 100)
                break;
        }

        currSelection = new GameObject(daObjName + daCounterTwo.ToString());
        currSelection.transform.SetParent(hudMainObj.transform);
        currSelection.transform.position = startPos;
        currSelection.AddComponent<ShapesScript>();
        currSelection.GetComponent<ShapesScript>().speed = daSpeed;
        currSelection.GetComponent<ShapesScript>().distance = lineLength;
        currSelection.AddComponent<LineRenderer>();
        LineRenderer lr = currSelection.GetComponent<LineRenderer>();
        lr.material = shapesMaterial;

        lr.startWidth       = lineWidth;
        lr.endWidth         = lineWidth;
        lr.positionCount    = numOfPoints;
        lr.SetPosition(0, startPos);
        float randEndX      = Random.Range(startPos.x, maxX);
        float randEndY      = Random.Range(startPos.y, maxY);

        int daDirection = 0;
        Vector3 movePos  = startPos;
        for(int i = 0; i < numOfPoints; i++)
        {
            lr.SetPosition(i, movePos);
            switch(shapeType)
            {
            case 0:
                if(Random.value > 0.5)
                {
                    randEndX = Random.Range(startPos.x, startPos.x + maxX);
                    randEndY = Random.Range(startPos.y, startPos.x + maxY);
                }
                else
                {
                    randEndX = Random.Range(startPos.x, startPos.x - maxX);
                    randEndY = Random.Range(startPos.y, startPos.x - maxY);
                }
                movePos = new Vector3(randEndX, randEndY, 0);
                if(i == numOfPoints - 1)
                {
                    lr.SetPosition(i, startPos);
                }
                break;

            case 1:
                if(i == 1)
                {
                    randEndX = startPos.x;
                    randEndY = startPos.y;
                }
                if(Random.value > 0.5 && daDirection == 0)
                {
                    randEndX = Random.Range(startPos.x, startPos.x + maxX);
                    daDirection = 1;
                }
                else if(Random.value > 0.5 && daDirection == 1)
                {
                    randEndY = Random.Range(startPos.y, startPos.x + maxY);
                    daDirection = 0;
                }
                else if(Random.value < 0.5 && daDirection == 0)
                {
                    randEndX = Random.Range(startPos.x, startPos.x - maxX);
                    daDirection = 1;
                }
                else
                {
                    randEndY = Random.Range(startPos.y, startPos.x - maxY);
                    daDirection = 0;
                }
                movePos = new Vector3(randEndX, randEndY, 0);
                break;
            case 2:
                if(i == 0)
                {
                    numOfPoints = 5;
                    lr.positionCount = numOfPoints;
                    randEndX = Random.Range(startPos.x, startPos.x + maxX);
                    randEndY = startPos.y;
                }
                else if(i == 1)
                {
                    randEndY = Random.Range(startPos.y, startPos.y - maxX);
                }
                else if (i == 2)
                {
                    randEndX = startPos.x;
                }
                else if (i == 3)
                {
                    randEndY = startPos.y;
                }
                movePos = new Vector3(randEndX, randEndY, 0);

                break;
            }
        }

        SetShapeColor(currSelection);
        currSelection.GetComponent<ShapesScript>().maxDistance = new Vector3(maxX, maxY, 0);

        currSelection.GetComponent<ShapesScript>().checkDaPos = true;
    }

    void SetShapeColor( GameObject daObj )
    {
        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[]
        {
            new GradientColorKey(autoGenColor[0], 0.0f),
            new GradientColorKey(autoGenColor[1], 0.25f),
            new GradientColorKey(autoGenColor[2], 0.5f),
            new GradientColorKey(autoGenColor[3], 0.75f),
            new GradientColorKey(autoGenColor[4], 1.0f)
        },
        new GradientAlphaKey[]
        {
            new GradientAlphaKey(autoGenColor[0].a, 0.0f),
            new GradientAlphaKey(autoGenColor[1].a, 0.25f),
            new GradientAlphaKey(autoGenColor[2].a, 0.5f),
            new GradientAlphaKey(autoGenColor[3].a, 0.75f),
            new GradientAlphaKey(autoGenColor[4].a, 1.0f)
        }
        );
        if(daObj.GetComponent<LineRenderer>())
        {
            daObj.GetComponent<LineRenderer>().material = shapesMaterial;

            daObj.GetComponent<LineRenderer>().colorGradient = gradient;
        }
    }

    void CreateNewShape(bool deleteCurr)
    {

        if(deleteCurr)
        {
            DestroyImmediate(currSelection);
        }

        float daSpeed    = Random.Range(0.10f, 10.00f);
        float lineLength = Random.Range(1.00f, 5.00f);
        float maxWidth   = Random.Range(1.00f, 5.00f);
        float maxHeight  = Random.Range(1.00f, 5.00f);
        float lineWidth  = Random.Range(0.01f, 0.1f);
        int numOfPoints  = Random.Range(5, 10);

        if(!menuBool[5])
        {
            float numOfPointsTemp = 3;
            float.TryParse(menuString[0], out daSpeed);
            float.TryParse(menuString[1], out lineLength);
            float.TryParse(menuString[2], out maxWidth);
            float.TryParse(menuString[3], out maxHeight);
            float.TryParse(menuString[4], out numOfPointsTemp);
            float.TryParse(menuString[5], out lineWidth);
            numOfPoints = Mathf.FloorToInt(numOfPointsTemp);
        }

        if(numOfPoints < 2)
        {
            numOfPoints = Random.Range(5, 10);
        }
        if(maxWidth == 0)
            maxWidth = Random.Range(1.00f, 5.00f);

        if(maxHeight == 0)
            maxHeight = Random.Range(1.00f, 5.00f);

        int lineType = Random.Range(0, 3);

        GenShape( "Shape_", Vector3.zero, numOfPoints, maxWidth, maxHeight, daSpeed, lineLength, lineWidth, lineType );
    }

    void saveHUDprefab(string prefabName, bool isTemp)
    {
        if(!AssetDatabase.IsValidFolder(folderLoc + "/Prefabs"))
        {
            AssetDatabase.CreateFolder(folderLoc, "Prefabs");
        }

        if(EditorApplication.isPlaying)
        {
            for(int i = 0; i < hudMainObj.transform.childCount; i ++)
            {
                GameObject daObj = hudMainObj.transform.GetChild(i).gameObject;
                if(daObj.GetComponent<ShapesScript>() && daObj.GetComponent<LineRenderer>())
                {
                    daObj.GetComponent<ShapesScript>().playAnimation = false;
                    daObj.GetComponent<LineRenderer>().positionCount = daObj.GetComponent<ShapesScript>().linePositions.Length;
                    for(int k = 0; k < daObj.GetComponent<ShapesScript>().linePositions.Length; k++)
                    {
                        daObj.GetComponent<LineRenderer>().SetPosition(k, daObj.GetComponent<ShapesScript>().linePositions[k]);
                    }
                }
                else if(daObj.GetComponent<Animator>() && daObj.GetComponent<SpriteRenderer>())
                {
                    daObj.GetComponent<Animator>().StopPlayback();
                }
            }
        }
        if(isTemp)
        {
            PrefabUtility.SaveAsPrefabAsset(hudMainObj, folderLoc + "/HudData/Temp/TempHUD.prefab");
        }
        else
        {
            PrefabUtility.SaveAsPrefabAsset(hudMainObj, folderLoc + "/Prefabs/" + prefabName + ".prefab");
        }

        if(EditorApplication.isPlaying)
        {
            for(int i = 0; i < hudMainObj.transform.childCount; i ++)
            {
                GameObject daObj = hudMainObj.transform.GetChild(i).gameObject;
                if(daObj.GetComponent<ShapesScript>())
                {
                    daObj.GetComponent<ShapesScript>().checkDaPos = true;
                    daObj.GetComponent<ShapesScript>().playAnimation = true;
                }
                else if(daObj.GetComponent<Animator>())
                {
                    daObj.GetComponent<Animator>().StartPlayback();
                }
            }
        }
    }

    void readColorInfo(bool justRead)
    {
        int k = 0;
        int m = 0;

        if(!justRead)
        {
            lineList.Clear();

            colorInfo = new StreamReader(folderLoc + "/HudData/HUDDesigner_ColorInfo.dat");

            while(!colorInfo.EndOfStream)
            {
                lineList.Add(colorInfo.ReadLine());
            }
            colorInfo.Close( );
        }

        for(int i = 0; i < lineList.Count; i++)
        {
            if(lineList[i].Length >= 10 && lineList[i].Substring(0, 10) == "HUD_COLOR_")
            {
                k++;
                m = 0;
            }
            else
            {
                float.TryParse(lineList[i], out colorSchemeInfo[k, m]);
                m++;
            }
        }
    }

    void addCustomImage(float maxWidth)
    {
        buttonPos = 10;
        GUI.Label (new Rect (0, buttonPos, maxWidth, 20), "Load Texture :", daSkin.GetStyle ("HUD_TextCenter"));
        buttonPos = buttonPos + 30;
        loaderTexture = (Texture2D)EditorGUI.ObjectField(new Rect(maxWidth / 2 - 45, buttonPos, 90, 90), "", loaderTexture, typeof(Texture2D), false);
        buttonPos = buttonPos + 100;

        if(loaderTexture)
        {
            if(loaderTexture.width > (maxWidth - 20) && loaderTexture.height > (maxWidth - 20))
            {
                GUI.Box (new Rect ( 10, buttonPos, maxWidth - 20, maxWidth - 20), loaderTexture);
                buttonPos = buttonPos + maxWidth;
            }
            else if(loaderTexture.width > (maxWidth - 20) && loaderTexture.height < (maxWidth - 20))
            {
                GUI.Box (new Rect ( 10, buttonPos, maxWidth - 20, loaderTexture.height), loaderTexture);
                buttonPos = buttonPos + loaderTexture.height + 20;
            }
            else if(loaderTexture.width < (maxWidth - 20) && loaderTexture.height > (maxWidth - 20))
            {
                GUI.Box (new Rect ( (maxWidth / 2) - (loaderTexture.width / 2), buttonPos, loaderTexture.width, maxWidth - 20), loaderTexture);
                buttonPos = buttonPos + maxWidth;
            }
            else
            {
                GUI.Box (new Rect ( (maxWidth / 2) - (loaderTexture.width / 2), buttonPos, loaderTexture.width, loaderTexture.height), loaderTexture);
                buttonPos = buttonPos + loaderTexture.height + 20;
            }

            GUI.Label (new Rect (0, buttonPos, maxWidth / 2, 20), "Image Size:", daSkin.GetStyle ("HUD_TextLeft"));
            GUI.Label (new Rect ((maxWidth / 2), buttonPos, maxWidth / 2, 20), (loaderTexture.width + " x " + loaderTexture.height), daSkin.GetStyle ("HUD_TextLeft"));
            buttonPos = buttonPos + 30;

            if(tempyTextureTwo != loaderTexture)
            {
                tempyTextureTwo = loaderTexture;
            }

            if (GUI.Button (new Rect (0, buttonPos, maxWidth, 60), "SAVE TEXTURE", daSkin.GetStyle ("HUD_ButtonGreen")))
            {
                string daOrgPath = AssetDatabase.GetAssetPath( loaderTexture );
                string daExtension = ".png";
                for(int i = daOrgPath.Length - 2; i > 0; i --)
                {
                    if (daOrgPath.Substring(i, 1) == ".")
                    {
                        daExtension = daOrgPath.Substring(i, (daOrgPath.Length - i));
                    }
                }
                string assetPath = folderLoc + "/HudData/Temp" + loaderTexture.name + daExtension;
                AssetDatabase.CopyAsset(daOrgPath, assetPath );

                TextureImporter tImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
                if ( tImporter != null )
                {
                    tImporter.textureType = TextureImporterType.Default;

                    tImporter.isReadable = true;

                    AssetDatabase.ImportAsset( assetPath );
                    AssetDatabase.Refresh();
                }
                Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));

                if(t != null && (mainPage == 2 || mainPage == 3))
                {
                    Texture2D destTex = new Texture2D(loaderTexture.width, loaderTexture.height);
                    Color[] pix = t.GetPixels(0, 0, loaderTexture.width, loaderTexture.height);
                    destTex.SetPixels(pix);
                    destTex.Apply();


                    byte[] bytes = destTex.EncodeToPNG();
                    int fnCount  = 1;
                    string assetPathTwo = folderLoc + "/HudData/Temp" + loaderTexture.name + daExtension;
                    switch(mainPage)
                    {
                    case 2:
                        for(int i = 0; i < circleTexture.Length; i++)
                        {
                            if(circleTexture[i].name == ("HUDDESIGNER_CIRCLE_USR_" + fnCount.ToString()))
                            {
                                fnCount++;
                                i = 0;
                            }
                        }
                        assetPathTwo = folderLoc + "/Circles/HUDDESIGNER_CIRCLE_USR_" + fnCount.ToString() + ".png";
                        break;
                    case 3:
                        for(int i = 0; i < squareTexture.Length; i++)
                        {
                            if(squareTexture[i].name == ("HUDDESIGNER_Square_USR_" + fnCount.ToString()))
                            {
                                fnCount++;
                                i = 0;
                            }
                        }
                        assetPathTwo = folderLoc + "/Squares/HUDDESIGNER_Square_USR_" + fnCount.ToString() + ".png";
                        break;
                    }
                    File.WriteAllBytes(assetPathTwo, bytes);
                    AssetDatabase.Refresh();
                    tImporter = AssetImporter.GetAtPath( assetPathTwo ) as TextureImporter;
                    if ( tImporter != null )
                    {
                        tImporter.textureType = TextureImporterType.Sprite;

                        tImporter.isReadable = true;

                        AssetDatabase.ImportAsset( assetPathTwo );
                        AssetDatabase.Refresh();
                    }
                    FindTheTextures();
                }
                else
                {
                    Debug.Log("ERROR 106: Cannot load texture, please reinstall HUD Designer or visit http://hud.naga.ninja for troubleshooting guide");
                }
                AssetDatabase.DeleteAsset(assetPath);
            }
            buttonPos = buttonPos + 80;

            if (GUI.Button (new Rect (0, buttonPos, maxWidth, 60), "CANCEL", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                menuBool[9] = false;
            }
            buttonPos = buttonPos + 80;
        }
        else
        {
            menuString[1] = menuString[0];
            menuString[3] = menuString[2];
        }
    }

    void addCustomPrefab(float maxWidth)
    {
        buttonPos = 10;
        GUI.Label (new Rect (0, buttonPos, maxWidth, 20), "Load the sprite image in the sequence:", daSkin.GetStyle ("HUD_TextCenterSmall"));
        buttonPos = buttonPos + 30;
        daSprite = (Sprite)EditorGUI.ObjectField(new Rect(maxWidth / 2 - 45, buttonPos, 90, 90), "", daSprite, typeof(Sprite), false);
        buttonPos = buttonPos + 100;

        if(daSprite)
        {
            if(spriteArr[0] != daSprite)
            {
                Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(daSprite));
                if(data.Length > 1)
                {
                    spriteArr = new Sprite[data.Length];
                    tempIntArr = new int[data.Length];
                    spriteArr[0] = daSprite;
                    int tempyInt = 1;
                    foreach (Object o in data)
                    {
                        if (o.GetType() == typeof(UnityEngine.Sprite))
                        {
                            spriteArr[tempyInt] = o as Sprite;
                            tempyInt++;
                        }
                    }
                    menuBool[17] = true;
                }
            }

            if(menuBool[17] && spriteArr.Length > 1)
            {
                for(int i = 1; i < spriteArr.Length; i++)
                {
                    if(spriteArr[i])
                    {
                        if(tempIntArr[i] == 5)
                        {
                            GUI.Label (new Rect (60, buttonPos, maxWidth - 90, 15), spriteArr[i].name, daSkin.GetStyle ("HUD_ButtonRedSmall"));
                        }
                        else
                        {
                            GUI.Label (new Rect (60, buttonPos, maxWidth - 90, 15), spriteArr[i].name, daSkin.GetStyle ("HUD_ButtonBlueSmall"));
                        }
                        if(tempIntArr[i] == 5)
                        {
                            if (GUI.Button (new Rect (maxWidth - 30, buttonPos, 30, 15), ">", daSkin.GetStyle ("HUD_ButtonBlue")))
                            {
                                tempIntArr[i] = 0;
                            }
                        }
                        else
                        {
                            if(i != 1)
                                if (GUI.Button (new Rect (0, buttonPos, 20, 15), "", daSkin.GetStyle ("HUD_ButtonLeft")))
                                {
                                    Sprite tempSprite = spriteArr[i];
                                    spriteArr[i] = spriteArr[i - 1];
                                    spriteArr[i - 1] = tempSprite;
                                }
                            if(i != (spriteArr.Length - 1))
                                if (GUI.Button (new Rect (30, buttonPos, 20, 15), "", daSkin.GetStyle ("HUD_ButtonRight")))
                                {
                                    Sprite tempSprite = spriteArr[i];
                                    spriteArr[i] = spriteArr[i + 1];
                                    spriteArr[i + 1] = tempSprite;
                                }
                            if (GUI.Button (new Rect (maxWidth - 30, buttonPos, 30, 15), "x", daSkin.GetStyle ("HUD_ButtonRed")))
                            {
                                tempIntArr[i] = 5;
                            }
                        }
                    }
                    buttonPos = buttonPos + 20;
                }
                buttonPos = buttonPos + 10;
                if (GUI.Button (new Rect (0, buttonPos, maxWidth, 60), "CREATE", daSkin.GetStyle ("HUD_ButtonGreen")))
                {
                    string theAnimName = "HUDDESIGNER_ANIMATED_USR_";
                    string[] theFilePath = {folderLoc + "/Animated/SpriteData"};

                    string[] results;

                    results = AssetDatabase.FindAssets(theAnimName, theFilePath);

                    int daCounter = 0;

                    for(int i = 0; i < results.Length; i++)
                    {
                        string daGUID = AssetDatabase.GUIDToAssetPath(results[i]);
                        if(daGUID.Substring(theFilePath[0].Length + 1, daGUID.Length - (theFilePath[0].Length + 6)) == theAnimName + daCounter.ToString() )
                        {
                            i = 0;
                            daCounter++;
                        }
                    }

                    theAnimName = theAnimName + daCounter.ToString();

                    AnimationClip clip = new AnimationClip();

                    clip.frameRate = 12;

                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite";
                    ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[spriteArr.Length - 1];

                    for( int i = 0 ; i < spriteArr.Length - 1; i++ )
                    {
                        spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                        spriteKeyFrames[i].time = i;
                        spriteKeyFrames[i].value = spriteArr[i + 1];
                    }

                    AnimationClipSettings animClipSett = new AnimationClipSettings();
                    animClipSett.loopTime = true;

                    AnimationUtility.SetAnimationClipSettings(clip, animClipSett);

                    AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
                    AssetDatabase.CreateAsset(clip, folderLoc + "/Animated/SpriteData/" + theAnimName + ".anim");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject daObj = new GameObject(theAnimName);

                    daObj.AddComponent<SpriteRenderer>();
                    daObj.GetComponent<SpriteRenderer>().sprite = spriteArr[1];
                    daObj.GetComponent<SpriteRenderer>().color = Color.white;
                    daObj.AddComponent<Animator>();
                    var daControl = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(folderLoc + "/Animated/SpriteData/" + theAnimName + ".controller");
                    daObj.AddComponent<AnimatedScript>();
                    daObj.GetComponent<AnimatedScript>().animSpeed = 1;

                    var rootStateMachine = daControl.layers[0].stateMachine;

                    var stateA1 = rootStateMachine.AddState("stateA1");
                    stateA1.motion = (Motion)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/SpriteData/" + theAnimName + ".anim", typeof (Motion));
                    stateA1.speed = 20;

                    daObj.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath(folderLoc +
                            "/Animated/SpriteData/" + theAnimName + ".controller", typeof (RuntimeAnimatorController));

                    PrefabUtility.SaveAsPrefabAsset(daObj, folderLoc + "/Animated/SpriteAnim/" + theAnimName + ".prefab");
                    DestroyImmediate(daObj);

                    clip = new AnimationClip();

                    clip.frameRate = 12;

                    EditorCurveBinding uiBinding = new EditorCurveBinding();
                    uiBinding.type = typeof(Image);
                    uiBinding.path = "";
                    uiBinding.propertyName = "m_Sprite";
                    ObjectReferenceKeyframe[] uiKeyFrames = new ObjectReferenceKeyframe[spriteArr.Length - 1];

                    for( int i = 0 ; i < spriteArr.Length - 1; i++ )
                    {
                        uiKeyFrames[i] = new ObjectReferenceKeyframe();
                        uiKeyFrames[i].time = i;
                        uiKeyFrames[i].value = spriteArr[i + 1];
                    }

                    animClipSett = new AnimationClipSettings();
                    animClipSett.loopTime = true;

                    AnimationUtility.SetAnimationClipSettings(clip, animClipSett);

                    AnimationUtility.SetObjectReferenceCurve(clip, uiBinding, uiKeyFrames);
                    AssetDatabase.CreateAsset(clip, folderLoc + "/Animated/UIdata/" + theAnimName + "_UI.anim");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    daObj = new GameObject(theAnimName);

                    daObj.AddComponent<RectTransform>();
                    daObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    daObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);
                    daObj.GetComponent<RectTransform>().sizeDelta =  new Vector3 (10, 10, 1);
                    daObj.AddComponent<Image>();
                    daObj.GetComponent<Image>().sprite = spriteArr[0];
                    daObj.GetComponent<Image>().color = Color.white;

                    daObj.AddComponent<Animator>();
                    daControl = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(folderLoc + "/Animated/UIdata/" + theAnimName + "_UI.controller");
                    daObj.AddComponent<AnimatedScript>();
                    daObj.GetComponent<AnimatedScript>().animSpeed = 1;

                    rootStateMachine = daControl.layers[0].stateMachine;

                    stateA1 = rootStateMachine.AddState("stateA1");
                    stateA1.motion = (Motion)AssetDatabase.LoadAssetAtPath(folderLoc + "/Animated/UIdata/" + theAnimName + "_UI.anim", typeof (Motion));
                    stateA1.speed = 20;

                    daObj.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath(folderLoc +
                            "/Animated/UIdata/" + theAnimName + "_UI.controller", typeof (RuntimeAnimatorController));

                    PrefabUtility.SaveAsPrefabAsset(daObj, folderLoc + "/Animated/UIAnim/" + theAnimName + ".prefab");
                    FindTheTextures();
                    for(int i = 0; i < animatedPrefabs.Length; i++)
                    {
                        if(animatedPrefabs[i].name == theAnimName)
                        {
                            tempyObject = animatedPrefabs[i];
                            previewOn = true;
                        }
                    }

                    DestroyImmediate(daObj);

                    tempColor = Color.white;

                    menuBool[9] = false;
                }
                buttonPos = buttonPos + 70;
            }
            if (GUI.Button (new Rect (0, buttonPos, maxWidth, 60), "CANCEL", daSkin.GetStyle ("HUD_ButtonBlue")))
            {
                menuBool[9] = false;
            }
            buttonPos = buttonPos + 80;
        }
    }

    void ShapesReader(Object daObj, string daDivideString)
    {
        float daDivider = 1;
        if(daDivideString == "0")
            daDivideString = "1";
        float.TryParse(daDivideString, out daDivider);

        StreamReader textInfoStream = new StreamReader(AssetDatabase.GetAssetPath(daObj));
        string daInfo = "";

        while(!textInfoStream.EndOfStream)
        {
            daInfo = textInfoStream.ReadLine();
        }
        textInfoStream.Close();

        int markerOne    = 0;
        int markerTwo    = 0;
        int daCounter    = 0;
        int daCounterTwo = 0;
        bool markerBool  = false;
        float xPos       = 0f;
        float yPos       = 0f;

        vector2Arr = new Vector2[10, 500];

        for(int i = 0; i < daInfo.Length; i++)
        {
            if (daInfo.Substring(i, 1) == "<")
            {
                markerOne = i;
                markerBool = true;
            }
            else if (markerBool && daInfo.Substring(i, 1) == " ")
            {
                string daSub = daInfo.Substring(markerOne, (i - markerOne));
                markerBool = false;
                if(daSub == "<polygon" || daSub == "<polyline")
                {
                    for(int k = i; k < daInfo.Length - 8; k++)
                    {
                        if (!markerBool && daInfo.Substring(k, 7) == "points=")
                        {
                            markerOne = markerTwo = k + 8;
                            markerBool = true;
                            k = markerOne;
                        }
                        else if (markerBool && markerOne == markerTwo && daInfo.Substring(k, 1) == " ")
                        {
                            markerTwo = k + 1;
                            float.TryParse(daInfo.Substring(markerOne, k - markerOne), out xPos);
                            xPos = xPos / daDivider;
                        }
                        else if (markerBool && markerOne != markerTwo && (daInfo.Substring(k, 1) == " " || daInfo.Substring(k, 1) == "\""))
                        {
                            float.TryParse(daInfo.Substring(markerTwo, k - markerTwo), out yPos);
                            yPos = yPos / daDivider;
                            vector2Arr[daCounter, daCounterTwo] = new Vector2(xPos, yPos);
                            daCounterTwo++;
                            markerOne = markerTwo = k + 1;
                            if((daInfo.Substring(k, 1) == "\"") || daCounter >= 10 ||  daCounterTwo >= 500)
                            {
                                markerBool = false;
                                tempIntArr[daCounter] = daCounterTwo;
                                daCounter++;
                                daCounterTwo = 0;
                                break;
                            }
                        }
                    }
                    if(daCounter >= 10)
                        break;
                }
            }
        }
    }

    void ShapesCreator(int shapeNum)
    {
        CreateNewShape(false);
        CheckParents("Shapes");
        currSelection = shapesObjects[shapesObjects.Count - 1];

        if(currSelection.GetComponent<LineRenderer>())
        {
            currSelection.GetComponent<LineRenderer>().positionCount = tempIntArr[shapeNum];

            for(int i = 0; i < 500; i++)
            {
                if(i < tempIntArr[shapeNum])
                {
                    currSelection.GetComponent<LineRenderer>().SetPosition(i, new Vector3( vector2Arr[shapeNum, i].x, vector2Arr[shapeNum, i].y, 0));
                }
                else
                {
                    break;
                }
            }
            currSelection.name = "SHAPES_TEMP";
        }
        mainPageTemp = 99;
    }

    void readSettingsInfo()
    {
        StreamReader settingsInfo = new StreamReader(folderLoc + "/HudData/HUDDesigner_SettingInfo.dat");

        while(!settingsInfo.EndOfStream)
        {
            settingsInfoArr.Add(settingsInfo.ReadLine());
        }
        settingsInfo.Close( );
        daSettingInt[0] = int.Parse(settingsInfoArr[0]);

        float[] tCol = new float[20];
        for(int i = 0; i < 20; i++)
        {
            float.TryParse(settingsInfoArr[i + 1],  out tCol[i]);
        }
        autoGenColor[0] = new Color (tCol[0], tCol[1], tCol[2], tCol[3]);
        autoGenColor[1] = new Color (tCol[4], tCol[5], tCol[6], tCol[7]);
        autoGenColor[2] = new Color (tCol[8], tCol[9], tCol[10], tCol[11]);
        autoGenColor[3] = new Color (tCol[12], tCol[13], tCol[14], tCol[15]);
        autoGenColor[4] = new Color (tCol[16], tCol[17], tCol[18], tCol[19]);
    }
}
#endif