using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Option : MonoBehaviour
{
    //Canvas_Option 싱글톤
    public static Canvas_Option instance;

    private void Awake()
    {
        //instance가 할당되지 않은 경우
        if (instance == null)
        {
            instance = this;
        }

        //instance에 할당된 클래스의 인스턴스가 다를 경우 새로 생성된 클래스를 의미함
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //다른 씬으로 넘어가더라도 삭제하지 않고 유지함
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
