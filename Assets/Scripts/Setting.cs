using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject windowSetting;
    public bool isActiveWinow;
    public void OpenWindow()
    {
        isActiveWinow = !isActiveWinow;
        windowSetting.SetActive(isActiveWinow);
    }
    public void CloseWindow()
    {
        isActiveWinow = false;
        windowSetting.SetActive(isActiveWinow);
    }
}
