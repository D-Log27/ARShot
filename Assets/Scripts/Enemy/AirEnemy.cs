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

    //[HideInInspector]
    public Target target;

    UnitStatusDTO enemyStatusDTO;
    public Transform targetTransform;
    public GameObject[] bulletPrefab;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = ObjectManager.LoadObject("VIP").transform;

        state = State.TRACE;
        this.transform.LookAt(targetTransform);
        enemyStatusDTO = new UnitStatusDTO(100, 100, 10);
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
                targetTransform = ObjectManager.LoadObject("Pistol").transform;
                this.transform.LookAt(targetTransform);
                break;
            case Target.VIP:
                targetTransform = ObjectManager.LoadObject("VIP").transform;
                this.transform.LookAt(targetTransform);
                break;
        }
    }

    public void Trace()
    {
        if (targetTransform != null)
        {

            Quaternion lookOnLook = Quaternion.LookRotation(targetTransform.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
            //print("### trace");
            Vector3 dir = targetTransform.transform.position - this.transform.position;
            this.transform.position += dir * Time.deltaTime * 0.1f;
            RangeCheck();
        }
    }

    public void Attack()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        currentTime += Time.deltaTime;
        if (currentTime > 3)
        {
            GameObject bullet = Instantiate(bulletPrefab[0]);
            Vector3 attackDir = targetTransform.position - this.transform.Find("AttackPoint").transform.position;
            //bullet.GetComponent<BulletTest>().SetTarget(attackDir);
            bullet.transform.position = this.transform.Find("AttackPoint").transform.position;
            bullet.name = "Pistol_bullet";
            bullet.GetComponent<Rigidbody>().AddForce(attackDir * 100);
            currentTime = 0;
        }
        //print("### attack");

    }

    public void Damaged(int damage)
    {
        if(enemyStatusDTO == null) print("enemyStatusDTO is null");
        print($"### damaged, original enemy info :{enemyStatusDTO}");
        if (enemyStatusDTO.shield > 0)
        {
            enemyStatusDTO.shield -= 10;
            print($"### air enemy shield : {enemyStatusDTO.shield }");
        }
        else
        {
            enemyStatusDTO.hp -= 10;

            print($"### air enemy hp : {enemyStatusDTO.hp }");
            if (enemyStatusDTO.hp <= 0)
            {
                Death();
            }
        }
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

    void RangeCheck()
    {
        float distance = Vector3.Distance(targetTransform.transform.position, this.transform.position);
        if(enemyStatusDTO.attackRange > distance)
        {
            state = State.ATTACK;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("### air enemy collision check");
    }
}
