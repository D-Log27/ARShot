using FXV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerSkill : MonoBehaviour
{
    SkillStatusDTO skillStatusDTO;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        skillStatusDTO = new SkillStatusDTO();
        GetComponent<FXVShield>().SetShieldActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        
        //print($"### healing skill trigger check :{other.name}");
        if (other.name.Contains("prefab"))
        {
            currentTime += Time.deltaTime;
            
            if (currentTime >= 3)
            {
                other.GetComponent<IPlayer>().Heal();
                currentTime = 0;
            }
        }
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
