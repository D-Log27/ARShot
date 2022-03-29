using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �׽�Ʈ źȯ
/// </summary>
public class BulletTest : MonoBehaviour
{
    Vector3 dir = Vector3.zero;
    float bulletSpeed = 10f;
    string attcker;

    // Start is called before the first frame update
    void Start()
    {
        attcker = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dir.Equals(Vector3.zero)) dir = this.transform.up;
        this.transform.LookAt(dir);
        this.transform.position += dir * bulletSpeed * Time.deltaTime;
    }

    public void SetTarget(Vector3 target)
    {
        this.dir = target;
    }

    /// <summary>
    /// źȯ ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        //print($"### collision name check : {collision.gameObject.name}");
        if (collision.gameObject.name.Contains("Ground"))
        {
            collision.gameObject.GetComponent<GroundEnemy>().Damaged();

        }
        else if(collision.gameObject.name.Contains("Air"))
        {
            collision.gameObject.GetComponent<AirEnemy>().Damaged();
        }
        if (collision.gameObject.CompareTag("VIP"))
        {
            collision.gameObject.GetComponent<VIP>().Damaged();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IPlayer>().UnderAttack();
        }
        Destroy(this.gameObject);
    }
}
