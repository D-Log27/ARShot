using FXV;
using QFX.SFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ±ÇÃÑ Å×½ºÆ®
/// </summary>
public class Pistol : MonoBehaviour, IPlayerGun
{
    CharacterInfoDTO characterInfoDTO;
    UnitStatusDTO playerStatusDTO;
    AmmoDTO ammoDTO;
    Transform rayStartpoint;
    public GameObject[] bulletPrefab;
    bool isShottable;
    Transform weaponPos;
    Transform reactPivot;
    public Transform skillTransform;
    public GameObject skill;
    SFX_MouseControlledObjectLauncher dealerSkill;

    // FOR DEVELOP
    float vertical;
    float horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        characterInfoDTO = new CharacterInfoDTO("Healer");
        if (!ObjectManager.objectDic.ContainsKey("Pistol")) ObjectManager.objectDic.Add("Pistol", this.gameObject);
        weaponPos = this.transform.Find("Pistol");
        reactPivot = this.transform.Find("ReactPivot");
        playerStatusDTO = new UnitStatusDTO(50, 100);
        //lineRenderer = this.GetComponent<LineRenderer>();
        isShottable = true;
        vertical = 0f;
        horizontal = 0f;
        ammoDTO = new AmmoDTO(7,7);
        rayStartpoint = this.transform.Find("GunRayPoint").transform;
        //dealerSkill = skillTransform.Find("DealerSkill").GetComponentInChildren<SFX_MouseControlledObjectLauncher>();
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

        if (Input.GetMouseButtonDown(1))
        {
            //dealerSkill.isShotGun = false;
            Skill();
        }

#if UNITY_EDITOR

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 dir = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        this.transform.position += dir.normalized * Time.deltaTime * 3f;
        
#endif

    }

    public void Attack()
    {

        Vector3 point = rayStartpoint.position - weaponPos.transform.position;

        GameObject bullet = Instantiate(bulletPrefab[0]);
        //bullet.GetComponent<BulletTest>().SetTarget(point);
        bullet.transform.position = rayStartpoint.position;
        //bullet.transform.parent = this.transform;
        bullet.name = "Pistol_bullet";
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 1000);

        //this.transform.RotateAround(reactPivot.position, Vector3.left, 200 * Time.deltaTime);

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
        // TODO : Add Score;
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

        if(Physics.Raycast(ray,out hit)){
            if(hit.collider != null)
            {
                GameObject skill = skillTransform.Find("HealerSkill").gameObject;
                //GameObject skill = Instantiate(this.skill, hit.point, Quaternion.identity);
                skill.GetComponent<FXVShield>().SetShieldActive(true);
                skill.transform.position = hit.point;

            }
        }
    }

    public void Heal(int point)
    {
        if (playerStatusDTO.hp < 100) playerStatusDTO.hp += point;
    }
}
