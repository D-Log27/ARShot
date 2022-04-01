using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP : MonoBehaviour
{
    UnitStatusDTO vipStatusDTO;
    // Start is called before the first frame update
    void Start()
    {
        ObjectManager.objectDic.Add("VIP", this.gameObject);
        vipStatusDTO = new UnitStatusDTO(100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    internal void Damaged()
    {
        print("### damaged");
        if (vipStatusDTO.shield > 0)
        {
            vipStatusDTO.shield -= 10;
            print($"### VIP hp : {vipStatusDTO.shield }");
        }
        else
        {
            vipStatusDTO.hp -= 10;

            print($"### VIP hp : {vipStatusDTO.hp }");
            if (vipStatusDTO.hp <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Destroy(this.gameObject);

        print("### !!! GAME OVER !!! ###");
        Time.timeScale = 0;
    }
}
