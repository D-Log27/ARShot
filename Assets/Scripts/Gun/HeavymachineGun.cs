using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavymachineGun : MonoBehaviour, IPlayerGun, IPlayerAttack, IPlayer
{
    UnitStatusDTO playerStatusDTO;
    AmmoDTO ammoDTO;
    Transform rayStartpoint;
    public GameObject[] bulletPrefab;
    //LineRenderer lineRenderer;
    bool isShottable;
    float vertical;
    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        if (!ObjectManager.objectDic.ContainsKey("HeavymachineGun")) ObjectManager.objectDic.Add("HeavymachineGun", this.gameObject);
        playerStatusDTO = new UnitStatusDTO(100, 100);
        //lineRenderer = this.GetComponent<LineRenderer>();
        isShottable = true;
        vertical = 0f;
        horizontal = 0f;
        ammoDTO = new AmmoDTO(7, 7);
        rayStartpoint = this.transform.Find("GunRayPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.enabled = false;
        if (Input.GetMouseButtonDown(0) && isShottable)
        {
            //lineRenderer.enabled = true;
            //lineRenderer.positionCount = 2;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
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

        Vector3 point = rayStartpoint.position - this.transform.position;

        GameObject bullet = Instantiate(bulletPrefab[0]);
        //bullet.GetComponent<BulletTest>().SetTarget(point);
        bullet.transform.position = rayStartpoint.position;
        bullet.name = "HeavymachineGun_bullet";
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000);
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

    public void UnderAttack()
    {
        print("### damaged");
        if (playerStatusDTO.shield > 0)
        {
            playerStatusDTO.shield -= 10;
            print($"### enemy hp : {playerStatusDTO.shield }");
        }
        else
        {
            playerStatusDTO.hp -= 10;

            print($"### enemy hp : {playerStatusDTO.hp }");
            if (playerStatusDTO.hp <= 0)
            {
                Death();
            }
        }
    }

    public void Kill()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        Destroy(this.gameObject);

        print("### !!! GAME OVER !!! ###");
        Time.timeScale = 0;
    }
}
