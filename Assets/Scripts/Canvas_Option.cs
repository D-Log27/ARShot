using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Option : MonoBehaviour
{
    //Canvas_Option �̱���
    public static Canvas_Option instance;

    private void Awake()
    {
        //instance�� �Ҵ���� ���� ���
        if (instance == null)
        {
            instance = this;
        }

        //instance�� �Ҵ�� Ŭ������ �ν��Ͻ��� �ٸ� ��� ���� ������ Ŭ������ �ǹ���
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //�ٸ� ������ �Ѿ���� �������� �ʰ� ������
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
