using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public Bullet bullet;

    public List<SkinUI> listEffectUI;
    public SkinUI currentEffectUI;

    public LevelController lvlController;

    private void Start()
    {
        currentEffectUI = listEffectUI[0];
    }
    public void SelectSkin(int number)
    {
        currentEffectUI.ActiveSkin(false);
        listEffectUI[number].ActiveSkin(true);
        currentEffectUI = listEffectUI[number];
        EnabledSkinGame(number);
    }
    public void CheckUnlock()
    {
        foreach (SkinUI checkSkin in listEffectUI)
        {
            if (lvlController.level >= checkSkin.unlockLevel - 1)
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
        foreach(GameObject effect in bullet.effects)
        {
            effect.SetActive(false);
        }
        bullet.effects[number].SetActive(true);
    }
}
