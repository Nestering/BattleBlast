using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Text countDestroyCube, countDestroyCubePrice;
    public Text countPickUpStars, countPickUpStarsPrice;
    public Text countBuyBoosts, countBuyBoostsPrice;

    public GameObject[] TaskWin;
    public Text[] CurrentStar;
    public int currrentTaskWin;

    public GameObject windowTask;
    public bool isActiveWinow;
    public void OpenWindow()
    {
        isActiveWinow = !isActiveWinow;
        windowTask.SetActive(isActiveWinow);
    }
    public void CloseWindow()
    {
        isActiveWinow = false ;
        windowTask.SetActive(isActiveWinow);
    }
    public void EnabledTaskWin(TypeTask type, int star)
    {
        if (type == TypeTask.Destroy)
        {
            TaskWin[0].SetActive(true);
            CurrentStar[0].text = star.ToString();
            currrentTaskWin = 0;
        }
        else if (type == TypeTask.PickUp)
        {
            TaskWin[1].SetActive(true);
            CurrentStar[1].text = star.ToString();
            currrentTaskWin = 1;
        }
        else if (type == TypeTask.Buy)
        {
            TaskWin[2].SetActive(true);
            CurrentStar[2].text = star.ToString();
            currrentTaskWin = 2;
        }
        StartCoroutine(DisableObject(currrentTaskWin));
    }
    public IEnumerator DisableObject(int currrentTask)
    {
        yield return new WaitForSeconds(2);
        TaskWin[currrentTask].SetActive(false);
    }
    public void UpdateTextTask(TypeTask type, int count, int countMax, int money)
    {
        if(type == TypeTask.Destroy)
        {
            countDestroyCube.text = count + " / " + countMax;
            countDestroyCubePrice.text = money.ToString();
        }
        else if (type == TypeTask.PickUp)
        {
            countPickUpStars.text = count + " / " + countMax;
            countPickUpStarsPrice.text = money.ToString();
        }
        else if(type == TypeTask.Buy)
        {
            countBuyBoosts.text = count + " / " + countMax;
            countBuyBoostsPrice.text = money.ToString();
        }

    }
}
public enum TypeTask
{
    Destroy,
    PickUp,
    Buy
}
