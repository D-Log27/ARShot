using FXV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerSkill : MonoBehaviour
{
    SkillStatusDTO skillStatusDTO;

    // Start is called before the first frame update
    void Start()
    {
        skillStatusDTO = new SkillStatusDTO();
        GetComponent<FXVShield>().SetShieldActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExtendRange()
    {

    }

    void AddAttack()
    {

    }

    void FastHealing()
    {

    }
}
