using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInput : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject Gun;
    // Start is called before the first frame update
    void Start()
    {
        Buttons.transform.Find("Btn_Skill").GetComponent<Button>().onClick.AddListener(()=> Gun.GetComponent<IPlayerGun>().Skill());
        Buttons.transform.Find("Btn_Attack").GetComponent<Button>().onClick.AddListener(()=> Gun.GetComponent<IPlayerGun>().Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
