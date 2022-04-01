using FXV;
using QFX.SFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavymachineGun : MonoBehaviour, IPlayerGun, IPlayer
{
    CharacterInfoDTO characterInfoDTO;
    UnitStatusDTO playerStatusDTO;
    AmmoDTO ammoDTO;
    Transform rayStartpoint;
    public GameObject[] bulletPrefab;
    bool isShottable;
    public Transform skillTransform;
    SFX_MouseControlledObjectLauncher dealerSkill;

    // FOR DEVELOP
    float vertical;
    float horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        characterInfoDTO = new CharacterInfoDTO("Tanker", 100, 100);
        if (!ObjectManager.objectDic.ContainsKey("HeavymachineGun")) ObjectManager.objectDic.Add("HeavymachineGun", this.gameObject);
        playerStatusDTO = new UnitStatusDTO(100, 100);
        //lineRenderer = this.GetComponent<LineRenderer>();
        isShottable = true;
        vertical = 0f;
        horizontal = 0f;
        ammoDTO = new AmmoDTO(7, 7);
        rayStartpoint = this.transform.Find("GunRayPoint").transform;
        dealerSkill = skillTransform.Find("DealerSkill").GetComponentInChildren<SFX_MouseControlledObjectLauncher>();
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

        if (Input.GetMouseButtonDown(1))
        {
            dealerSkill.isShotGun = false;
            Skill();
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

    public void Skill()
    {
        RaycastHit hit;
        //Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(rayStartpoint.position);
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                GameObject skill = skillTransform.Find("TankerSkill").gameObject;
                Vector3 pos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                skill.transform.position = pos;
                skill.GetComponent<ParticleSystem>().Play();

            }
        }
    }
}
