        
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectManager.SaveObject("Weapon", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ObjectManager.LoadObject("MarkerGuide").GetComponent<MarkerGuide_KSY>().isBriefFinish) return;
        else this.gameObject.SetActive(true);
    }
}
