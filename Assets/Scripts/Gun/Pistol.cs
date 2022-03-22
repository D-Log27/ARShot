using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ±ÇÃÑ Å×½ºÆ®
/// </summary>
public class Pistol : MonoBehaviour, IPlayerGun, IPlayerAttack
{
    AmmoDTO ammoDTO;
    Transform rayStartpoint;
    public GameObject bulletPrefab;
    bool isShottable;
    float vertical;
    float horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        isShottable = true;
        vertical = 0f;
        horizontal = 0f;
        ammoDTO = new AmmoDTO(7,7);
        rayStartpoint = this.transform.Find("GunRayPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isShottable)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            Reload();
        }
#if UNITY_EDITOR

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 dir = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        this.transform.position += dir.normalized * Time.deltaTime * 1f;
        


        

#endif

    }

    public void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = rayStartpoint.position;
        ammoDTO.currentAmmoCnt--;
        print($"ammo : {ammoDTO.currentAmmoCnt}");
        if (ammoDTO.currentAmmoCnt == 0) Reload();
    }

    public void Reload()
    {
        ammoDTO.currentAmmoCnt = 7;
        StartCoroutine(ReloadDelay());
    }

    IEnumerator ReloadDelay()
    {
        isShottable = false;
        print($"### reloading {isShottable}");
        yield return new WaitForSeconds(3f);
        isShottable = true;
        print($"### reload complete {isShottable}");
    }
}
