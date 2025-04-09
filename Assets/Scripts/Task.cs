using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public UI ui;
    public TaskUI taskUI;
    public LevelController lvlcontroller;
    public int countDestroyCube, countMaxDestroyCube;
    public int countPickUpStars, countMaxPickUpStars;
    public int countBuyBoosts, countMaxBuyBoosts;

    public float multiplierDestroyCube, multiplierPickUpStars, multiplierBuyBoosts;
    public int giftStarDestroyCube, giftPickUpStars, giftBuyBoosts;

    private void Start()
    {
        CheckComlectedTask();
        Load();
        taskUI.UpdateTextTask(TypeTask.Destroy, countDestroyCube, countMaxDestroyCube, giftStarDestroyCube);
        taskUI.UpdateTextTask(TypeTask.PickUp, countPickUpStars, countMaxPickUpStars, giftPickUpStars);
        taskUI.UpdateTextTask(TypeTask.Buy, countBuyBoosts, countMaxBuyBoosts, giftBuyBoosts);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("countDestroyCube", countDestroyCube);
        PlayerPrefs.SetInt("countPickUpStars", countPickUpStars);
        PlayerPrefs.SetInt("countBuyBoosts", countBuyBoosts);
    }
    public void Load()
    {
        countDestroyCube = PlayerPrefs.GetInt("countDestroyCube", 0);
        countPickUpStars = PlayerPrefs.GetInt("countPickUpStars", 0);
        countBuyBoosts = PlayerPrefs.GetInt("countBuyBoosts", 0);
    }
    public void AddDestroyCube()
    {
        countDestroyCube++;
        CheckComlectedTask();
        taskUI.UpdateTextTask(TypeTask.Destroy, countDestroyCube, countMaxDestroyCube, giftStarDestroyCube);
        ui.UpdateBase();
        Save();
    }
    public void AddPickUpStars(int count)
    {
        countPickUpStars += count;
        CheckComlectedTask();
        taskUI.UpdateTextTask(TypeTask.PickUp, countPickUpStars, countMaxPickUpStars, giftPickUpStars);
        ui.UpdateBase();
        Save();
    }
    public void AddBuyBoosts()
    {
        countBuyBoosts++;
        CheckComlectedTask();
        taskUI.UpdateTextTask(TypeTask.Buy, countBuyBoosts, countMaxBuyBoosts, giftBuyBoosts);
        ui.UpdateBase();
        Save();
    }
    public void CheckComlectedTask()
    {
        int lvl = lvlcontroller.level;
        if (countDestroyCube >= countMaxDestroyCube)
        {
            if (giftStarDestroyCube != 0)
            {
                taskUI.EnabledTaskWin(TypeTask.Destroy, giftStarDestroyCube);
            }
            lvlcontroller.star += giftStarDestroyCube;
            countMaxDestroyCube = MultiplierCountDestroyCube(lvl);
            countDestroyCube = 0;
        }

        if (countPickUpStars >= countMaxPickUpStars)
        {
            if (giftPickUpStars != 0)
            {
                taskUI.EnabledTaskWin(TypeTask.PickUp, giftPickUpStars);
            }    
            lvlcontroller.star += giftPickUpStars;
            countMaxPickUpStars = MultiplierCountPickUpStars(lvl);
            countPickUpStars = 0;
        }

        if (countBuyBoosts >= countMaxBuyBoosts)
        {
            if (giftBuyBoosts != 0)
            {
                taskUI.EnabledTaskWin(TypeTask.Buy, giftBuyBoosts);
            }
            lvlcontroller.star += giftBuyBoosts;
            countMaxBuyBoosts = MultiplierCountBuyBoosts(lvl);
            countBuyBoosts = 0;
        }
    }
    public int MultiplierCountDestroyCube(int lvl)
    {
        giftStarDestroyCube = (int)(giftStarDestroyCube * multiplierDestroyCube) + 1000;
        return (int)(multiplierDestroyCube * lvl * 50) + 100;
    }
    public int MultiplierCountPickUpStars(int lvl)
    {
       
        giftPickUpStars = (int)(giftPickUpStars * multiplierPickUpStars) + 1000;
        return (int)(multiplierPickUpStars * (lvl +1) * 1000) + 1000;
    }

    public int MultiplierCountBuyBoosts(int lvl)
    {
        giftBuyBoosts = (int)(giftBuyBoosts * multiplierBuyBoosts) + 1000;
        return 3;
    }

}
