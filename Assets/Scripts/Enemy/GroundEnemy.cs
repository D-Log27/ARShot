using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ground Enemy
/// </summary>
public class GroundEnemy : MonoBehaviour, IEnemy
{
    enum State { TRACE, ATTACK }
    State state;
    
    //[HideInInspector]
    public Target target;

    UnitStatusDTO enemyStatusDTO;
    public Transform targetTransform;
    public GameObject bulletPrefab;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        //targetTransform = ObjectManager.objectDic["VIP"].transform;

        state = State.TRACE;
        target = Target.VIP;
        //target = Target.USER;
        ChangeTarget(target);
        enemyStatusDTO = new UnitStatusDTO(100, 100, 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TRACE:
                Trace();
                break;
            case State.ATTACK:
                Attack();
                break;
        }
    }

    public void ChangeTarget(Target target)
    {
        switch (target)
        {
            case Target.USER:
                targetTransform = ObjectManager.objectDic["Pistol"].transform;
                this.transform.LookAt(targetTransform);
                break;
            case Target.VIP:
                targetTransform = ObjectManager.objectDic["VIP"].transform;
                this.transform.LookAt(targetTransform);
                break;
        }
    }

    public void Trace()
    {
        //print("### trace");
        //this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.01f);
        if(targetTransform != null)
        {
            Vector3 dir = targetTransform.transform.position - this.transform.position;
            this.transform.position += dir * Time.deltaTime * 0.1f;
            RangeCheck();
        }
        
        
    }

    public void Attack()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        currentTime += Time.deltaTime;
        if(currentTime > 2)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            Vector3 attackDir = this.transform.Find("AttackPoint").transform.position - this.transform.position;
            bullet.GetComponent<BulletTest>().SetTarget(attackDir);
            bullet.transform.position = this.transform.Find("AttackPoint").transform.position;
            currentTime = 0;
        }
        //print("### attack");
        
        
    }

    public void Damaged()
    {
        print("### damaged");
        if(enemyStatusDTO.shield > 0)
        {
            enemyStatusDTO.shield -= 10;
            print($"### enemy hp : {enemyStatusDTO.shield }");
        }
        else
        {
            enemyStatusDTO.hp -= 10;

            print($"### enemy hp : {enemyStatusDTO.hp }");
            if (enemyStatusDTO.hp <= 0)
            {
                Death();
            }
        }
        
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    

    

    void RangeCheck()
    {
        
        float distance = Vector3.Distance(targetTransform.transform.position, this.transform.position);
        if (enemyStatusDTO.attackRange > distance)
        {
            state = State.ATTACK;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("### ground enemy collision check");
    }

}
