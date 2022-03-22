using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Air Enemy
/// </summary>
public class AirEnemy : MonoBehaviour, IEnemy
{
    EnemyStatusDTO enemyStatusDTO;
    public Transform target;
    bool isValidRange;

    public void Attack()
    {
        //print("### attack");
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
        //print("### trace");
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.01f);
    }

    // Start is called before the first frame update
    void Start()
    {
        isValidRange = false;
        enemyStatusDTO = new EnemyStatusDTO(100, 100, 5);
    }

    // Update is called once per frame
    void Update()
    {
        RangeCheck();
        if (!isValidRange)
        {
            Trace();
        }
        else
        {
            Attack();
        }
    }

    void RangeCheck()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, enemyStatusDTO.attackRange);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.name.Contains("VIP")) isValidRange = true;
        }
    }
}
