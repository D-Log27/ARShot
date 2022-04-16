using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriefingCanvas_KSY : MonoBehaviour
{
    
    //목표: 홀로그램이 다 뜨면 캔버스의 Text를 차례대로 켰다 끄고 싶다.
    //1)Text00을 켜고 싶다.
    //2)3초 뒤, Text00을 끄고 Text01을 켜고 싶다.
    //3)3초 뒤, Text1를 끄고 Text02을 켜고 싶다.
    //4)3초 뒤, Text2를 끄고 Text03을 켜고 싶다.
    //5)3초 뒤, Text3를 끄고 Text04을 켜고 싶다.
    //6)3초뒤, Text04를 끄고 싶다.

    public Image text00;
    public Image text01;
    public Image text02;
    public Image text03;
    public Image text04;

    void Update()
    {
        
    }
}
