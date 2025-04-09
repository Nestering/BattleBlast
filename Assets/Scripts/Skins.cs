using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    public GameObject windowSkins;
    public bool isActiveWinow;

    public List<Material> listSkins;
    public GameObject bullet;

    public List<SkinUI> listSkinsUI;
    public SkinUI currentSkinUI;

    public LevelController lvlController;
    private void Start()
    {
        CheckUnlock();
        SelectSkin(0);
    }
    public void OpenWindow()
    {
        isActiveWinow = !isActiveWinow;
        windowSkins.SetActive(isActiveWinow);
    }
    public void CloseWindow()
    {
        isActiveWinow = false;
        windowSkins.SetActive(isActiveWinow);
    }
    public void SelectSkin(int number)
    {
        currentSkinUI.ActiveSkin(false);
        listSkinsUI[number].ActiveSkin(true);
        currentSkinUI = listSkinsUI[number];
        EnabledSkinGame(number);
    }
    public void CheckUnlock()
    {
        foreach(SkinUI checkSkin in listSkinsUI)
        {
            if (lvlController.level >= checkSkin.unlockLevel-1)
            {
                checkSkin.UnLockSkin();
            }
            else
            {
                checkSkin.LockSkin();
            }
        }
    }

    public void EnabledSkinGame(int number)
    {
        bullet.GetComponent<Renderer>().material = listSkins[number];
    }
}
