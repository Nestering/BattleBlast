using System.Collections;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private Shoot shoot;
    [SerializeField] private Task task;
    [SerializeField] private LevelController levelController;
    [HideInInspector] public int timerDamageMax, timerCooldown, timerIncome;
    public int intervalDamageMax, intervalCooldown, intervalIncome;
    public bool damageMaxEnabled, cooldownEnabled, incomeEnabled;

    public void BuyBoostDamageMax()
    {
        StartCoroutine(BoostDamageMaxTimer());
        task.AddBuyBoosts();
    }
    public void BuyBoostCooldown()
    {
        StartCoroutine(BoostCooldownTimer());
        task.AddBuyBoosts();
    }

    public void BuyBoostIncome()
    {
        StartCoroutine(BoostIncomeTimer());
        task.AddBuyBoosts();
    }
    private IEnumerator BoostDamageMaxTimer()
    {
        damageMaxEnabled = true;
        shoot.enabledBoostDamage = true;
        timerDamageMax = intervalDamageMax;
        while (timerDamageMax > 0)
        {
            ui.UpdateTimerDamageMax();
            yield return new WaitForSeconds(1f);
            timerDamageMax -= 1;
        }
        shoot.enabledBoostDamage = false;
        damageMaxEnabled = false;
        ui.UpdateTimerDamageMax();
    }

    private IEnumerator BoostCooldownTimer()
    {
        cooldownEnabled = true;
        shoot.enabledBoostCooldown = true;
        timerCooldown = intervalCooldown;
        while (timerCooldown > 0)
        {
            ui.UpdateTimerCooldown();
            yield return new WaitForSeconds(1f);
            timerCooldown -= 1;
        }
        shoot.enabledBoostCooldown = false;
        cooldownEnabled = false;
        ui.UpdateTimerCooldown();
    }

    private IEnumerator BoostIncomeTimer()
    {
        incomeEnabled = true;
        levelController.enabledBoostIncome = true;
        timerIncome = intervalIncome;
        while (timerIncome > 0)
        {
            ui.UpdateTimerIncome();
            yield return new WaitForSeconds(1f);
            timerIncome -= 1;
        }
        levelController.enabledBoostIncome = false;
        incomeEnabled = false;
        ui.UpdateTimerIncome();
    }
    
}
