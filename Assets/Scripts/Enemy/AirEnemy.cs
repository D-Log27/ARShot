using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Air Enemy
/// </summary>
public class AirEnemy : MonoBehaviour, IEnemy
{
    enum State { TRACE, ATTACK }
    State state;

    [HideInInspector]
    public Target target;

    EnemyStatusDTO enemyStatusDTO;
    public Transform targetTransform;

    public void ChangeTarget(Target target)
    {
        switch (target)
        {
            case Target.USER:
                break;
            case Target.VIP:
                targetTransform = ObjectManager.objectDic["VIP"].transform;
                this.transform.LookAt(targetTransform);
                break;
        }
    }

    public void Attack()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(targetTransform.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        //this.transform.LookAt(target);
        //print("### attack");
        //RangeCheck();
        //transform.position = Vector3.zero;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        

    }

    public void Damaged()
    {
        print("### damaged");
        enemyStatusDTO.hp -= 10;
        print($"### enemy hp : {enemyStatusDTO.hp }");
        if (enemyStatusDTO.hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }

    public void Trace()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(targetTransform.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        //print("### trace");
        Vector3 dir = targetTransform.transform.position - this.transform.position;
        this.transform.position += dir * Time.deltaTime * 0.1f;
        RangeCheck();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = ObjectManager. objectDic["VIP"].transform;
        state = State.TRACE;
        this.transform.LookAt(targetTransform);
        enemyStatusDTO = new EnemyStatusDTO(100, 100, 5);
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

    void RangeCheck()
    {
        float distance = Vector3.Distance(targetTransform.transform.position, this.transform.position);
        if(enemyStatusDTO.attackRange > distance)
        {
            state = State.ATTACK;
        }
    }
}
