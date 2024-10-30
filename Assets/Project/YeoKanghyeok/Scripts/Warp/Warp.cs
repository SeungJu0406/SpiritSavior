using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Queue<ButtonController> buttons = new Queue<ButtonController>();
    public bool uiActive;
    public void OnUI()
    {
        ButtonController[] arrButtons = new ButtonController[buttons.Count]; // Queue에 있는 버튼 오브젝트 꺼내기

        for (int i = 0; i < buttons.Count; i++)
        {
            if (arrButtons[i].ActiveButton == true) // 활성화된 버튼 찾기
            {
                arrButtons[i].OnActive(); // 오브젝트 활성화
            }
        }
    }
    public void OffUI()
    {
        ButtonController[] arrButtons = new ButtonController[buttons.Count];

        for(int i = 0;i < buttons.Count; i++)
        {
            arrButtons[i].OffActive();
        }
    }
}
