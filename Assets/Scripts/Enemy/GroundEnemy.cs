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
    
    [HideInInspector]
    public Target target;

    UnitStatusDTO enemyStatusDTO;
    public Transform targetTransform;
    public GameObject bulletPrefab;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = ObjectManager.LoadObject("VIP").transform;

        state = State.TRACE;
        target = Target.VIP;
        //target = Target.USER;
        //ChangeTarget(target);
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
        //targetTransform = ObjectManager.LoadObject("VIP").transform;
        switch (target)
        {
            case Target.USER:
                break;
            case Target.VIP:
                targetTransform = ObjectManager.LoadObject("VIP").transform;
                this.transform.LookAt(targetTransform);
                break;
        }
    }

    public void Trace()
    {
        targetTransform = ObjectManager.LoadObject("VIP").transform;
        //print("### trace");
        //this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.01f);
        if (targetTransform != null)
        {
            Vector3 dir = targetTransform.transform.position - this.transform.position;
            this.transform.position += dir * Time.deltaTime * 0.1f;
            RangeCheck();
        }
        
        
    }

    public void Attack()
    {
        targetTransform = ObjectManager.LoadObject("VIP").transform;
        if (targetTransform != null)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(targetTransform.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        }

        //this.transform.LookAt(target);
        //print("### attack");
        //RangeCheck();
        //transform.position = Vector3.zero;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;


    }

    public void Damaged(int damage)
    {
        print("### damaged");

        if(enemyStatusDTO.shield > 0)
        {
            enemyStatusDTO.shield -= 10;
            print($"### ground enemy shield : {enemyStatusDTO.shield }");
        }
        else
        {
            enemyStatusDTO.hp -= 10;

            print($"### ground enemy hp : {enemyStatusDTO.hp }");
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
        targetTransform = ObjectManager.LoadObject("VIP").transform;
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
