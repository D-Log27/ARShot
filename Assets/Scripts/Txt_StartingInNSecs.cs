using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Txt_StartingInNSecs : MonoBehaviour
{
    public TMP_Text loadingGame;
    float sec;
    // Start is called before the first frame update
    void Start()
    {
        loadingGame.text = "Starting in 5 Seconds";
        sec = 5;
    }

    // Update is called once per frame
    void Update()
    {
        sec -= Time.deltaTime;
        loadingGame.text = "Starting in " + sec + " Seconds";
    }
}
