using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectManager.SaveObject("InGameObj", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ObjectManager.LoadObject("MarkerGuide").GetComponent<MarkerGuide_KSY>().isBriefFinish) return;
        else this.gameObject.SetActive(true);
    }
}
