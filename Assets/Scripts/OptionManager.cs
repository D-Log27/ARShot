using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickBackToTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_AL");
    }

}
