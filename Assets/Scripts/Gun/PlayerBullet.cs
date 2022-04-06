using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Å×½ºÆ® ÅºÈ¯
/// </summary>
public class PlayerBullet : MonoBehaviour
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
        //if (dir.Equals(Vector3.zero)) dir = Vector3.forward;
        //this.transform.LookAt(dir);
        //this.transform.position += dir * bulletSpeed * Time.deltaTime;
    }

    public void SetTarget(Vector3 target)
    {
        this.dir = target;
    }

    /// <summary>
    /// ÅºÈ¯ ¸íÁß
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        print($"### player bullet collision name check : {collision.gameObject.name}");
        if (collision.gameObject.name.Contains("Ground"))
        {
            collision.gameObject.GetComponent<IEnemy>().Damaged();

        }
        else if(collision.gameObject.name.Contains("Air"))
        {
            collision.gameObject.GetComponent<IEnemy>().Damaged();
        }
        Destroy(this.gameObject);
    }
}
