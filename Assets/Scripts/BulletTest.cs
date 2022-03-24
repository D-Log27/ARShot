using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Å×½ºÆ® ÅºÈ¯
/// </summary>
public class BulletTest : MonoBehaviour
{
    Vector3 dir = Vector3.zero;
    float bulletSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    /// ÅºÈ¯ ¸íÁß
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
        Destroy(this.gameObject);
    }
}
