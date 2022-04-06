using FXV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerSkill : MonoBehaviour
{
    SkillStatusDTO skillStatusDTO;

    // Start is called before the first frame update
    void Start()
    {
        skillStatusDTO = new SkillStatusDTO();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print($"collision name  : {collision.collider.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShieldBash()
    {

    }

    void AddReflect()
    {

    }

    void BrakeMove()
    {

    }
}
