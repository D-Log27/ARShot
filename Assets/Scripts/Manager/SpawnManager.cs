using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject airEnemy;
    public GameObject groundEnemy;
    float currentTime;
    bool enemyTypeFlag;
    Transform spawnArea_1;
    Transform spawnArea_2;
    Transform spawnArea_3;
    Transform spawnArea_4;
    int totalEnemyCnt = 3;
    int enemyCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyTypeFlag = false;
        currentTime = 0;
        spawnArea_1 = this.transform.Find("Spawn_area_1").transform;
        spawnArea_2 = this.transform.Find("Spawn_area_2").transform;
        spawnArea_3 = this.transform.Find("Spawn_area_3").transform;
        spawnArea_4 = this.transform.Find("Spawn_area_4").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(enemyCnt < totalEnemyCnt)
        {
            currentTime += Time.deltaTime;
            if(currentTime > 1)
            {
                currentTime = 0;
                GameObject enemy;
                Vector3 pos;
                if (enemyTypeFlag)
                {
                    enemy = Instantiate(airEnemy);
                    pos = GetRandomPositionInArea(Random.Range(1, 4)) + (Vector3.up * 5f);
                } else
                {
                    //enemy = Instantiate(groundEnemy);
                    //pos = GetRandomPositionInArea(Random.Range(1, 4));

                    enemy = Instantiate(airEnemy);
                    pos = GetRandomPositionInArea(Random.Range(1, 4)) + (Vector3.up * 5f);
                }
                enemyCnt++;
                enemy.transform.position = pos;
                enemy.transform.parent = this.transform;
                enemyTypeFlag = !enemyTypeFlag;
            }
        }
    }

    Vector3 GetRandomPositionInArea(int idx)
    {
        Collider areaCollider;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        
        switch (idx)
        {
            case 1:
                areaCollider = spawnArea_1.gameObject.GetComponent<Collider>();
                x = Random.Range(areaCollider.bounds.min.x, areaCollider.bounds.max.x);
                z = Random.Range(areaCollider.bounds.min.z, areaCollider.bounds.max.z);
                break;
            case 2:
                areaCollider = spawnArea_2.gameObject.GetComponent<Collider>();
                x = Random.Range(areaCollider.bounds.min.x, areaCollider.bounds.max.x);
                z = Random.Range(areaCollider.bounds.min.z, areaCollider.bounds.max.z);
                break;
            case 3:
                areaCollider = spawnArea_3.gameObject.GetComponent<Collider>();
                x = Random.Range(areaCollider.bounds.min.x, areaCollider.bounds.max.x);
                z = Random.Range(areaCollider.bounds.min.z, areaCollider.bounds.max.z);
                break;
            case 4:
                areaCollider = spawnArea_4.gameObject.GetComponent<Collider>();
                x = Random.Range(areaCollider.bounds.min.x, areaCollider.bounds.max.x);
                z = Random.Range(areaCollider.bounds.min.z, areaCollider.bounds.max.z);
                break;
        }
        return new Vector3(x, y, z);
    }
}
