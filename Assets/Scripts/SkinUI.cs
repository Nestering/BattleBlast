using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUI : MonoBehaviour
{
    public GameObject imageLockSkin;
    public GameObject imageUnLockSkin;
    public GameObject imageSelectSkin;
    public int unlockLevel = 10;
    public bool unlock = false;
    public void ActiveSkin(bool active)
    {
        imageSelectSkin.SetActive(active);
    }
    public void UnLockSkin()
    {
        unlock = true;
        imageLockSkin.SetActive(false);
        imageUnLockSkin.SetActive(true);
    }
    public void LockSkin()
    {
        unlock = false;
        imageLockSkin.SetActive(true);
        imageUnLockSkin.SetActive(false);
    }
}
