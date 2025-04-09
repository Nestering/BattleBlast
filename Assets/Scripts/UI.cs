using UnityEngine;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour
{
    public Upgrade upgrade;
    public Boost boost;
    public Shoot shoot;
    public LevelController lvlController;

    public Text Star, level, cubeCount;
    public Image cubeCountBar;

    public Text[] priceDamage, damage, damageMultiplier;
    public Text[] priceCooldown, cooldown, cooldownMultiplier;
    public Text[] priceRange, range, rangeMultiplier;
    public Text[] priceIncome, income, incomeMultiplier;

    public GameObject upgradeCooldown, upgradeCooldownMax;

    public Text timerDamageMax, timerCooldown, timerIncome;
    public GameObject desriptionDamageMax, desriptionCooldown, desriptionIncome;
    public Image imageEnabledDamageMax, imageEnabledCooldown, imageEnabledIncome;

    public GameObject[] enableButtons;
    public GameObject[] disableButtons;

    void SetTexts(Text[] texts, string value)
    {
        foreach (var text in texts)
        {
            text.text = value;
        }
    }

    public void StartUpdateUI()
    {
        UpdateBase();
        UpdateDamage();
        UpdateCooldown();
        UpdateRange();
        UpdateIncome();
        UpdateTimerDamageMax();
        UpdateTimerCooldown();
        UpdateTimerIncome();
    }

    public void UpdateBase()
    {
        Star.text = lvlController.star.ToString();
        level.text = "Level " + (lvlController.level + 1);
        cubeCount.text = lvlController.cubeCount + " cubes left";
        cubeCountBar.fillAmount = (float)lvlController.cubeCount / lvlController.cubeCountMax;
    }

    #region Upgrade Setting

    public void UpdateDamage()
    {
        SetTexts(priceDamage, upgrade.priceDamage.ToString());
        SetTexts(damage, shoot.damage.ToString("0"));

        int multiplier = Mathf.Max((int)(shoot.damage * upgrade.damageMultiplier - shoot.damage), 1);
        SetTexts(damageMultiplier, "+" + multiplier.ToString("0"));
    }

    public void UpdateCooldown()
    {
        if (!upgrade.upgradeCooldownMax)
        {
            upgradeCooldown.SetActive(true);
            upgradeCooldownMax.SetActive(false);

            SetTexts(priceCooldown, upgrade.priceCooldown.ToString());
            SetTexts(cooldown, shoot.cooldown.ToString("0.00"));
            SetTexts(cooldownMultiplier, "-" + upgrade.cooldownMultiplier.ToString("0.##") + "s");
        }
        else
        {
            upgradeCooldown.SetActive(false);
            upgradeCooldownMax.SetActive(true);
        }
    }

    public void UpdateRange()
    {
        SetTexts(priceRange, upgrade.priceRange.ToString());
        SetTexts(range, shoot.range.ToString("0.0"));

        float multiplier = shoot.range * upgrade.rangeMultiplier - shoot.range;
        SetTexts(rangeMultiplier, "+" + multiplier.ToString("0.0"));
    }

    public void UpdateIncome()
    {
        SetTexts(priceIncome, upgrade.priceIncome.ToString());
        SetTexts(income, lvlController.income.ToString("0"));

        int multiplier = Mathf.Max((int)(lvlController.income * upgrade.incomeMultiplier - lvlController.income), 1);
        SetTexts(incomeMultiplier, "+" + multiplier.ToString("0"));
    }

    #endregion

    #region Boost Setting

    public void UpdateTimerDamageMax()
    {
        if (boost.damageMaxEnabled)
        {
            desriptionDamageMax.SetActive(false);
            imageEnabledDamageMax.gameObject.SetActive(true);
            timerDamageMax.text = boost.timerDamageMax.ToString();
            imageEnabledDamageMax.fillAmount = 1f - (float)boost.timerDamageMax / boost.intervalDamageMax;
        }
        else
        {
            desriptionDamageMax.SetActive(true);
            imageEnabledDamageMax.gameObject.SetActive(false);
        }
    }

    public void UpdateTimerCooldown()
    {
        if (boost.cooldownEnabled)
        {
            desriptionCooldown.SetActive(false);
            imageEnabledCooldown.gameObject.SetActive(true);
            timerCooldown.text = boost.timerCooldown.ToString();
            imageEnabledCooldown.fillAmount = 1f - (float)boost.timerCooldown / boost.intervalCooldown;
        }
        else
        {
            desriptionCooldown.SetActive(true);
            imageEnabledCooldown.gameObject.SetActive(false);
        }
    }

    public void UpdateTimerIncome()
    {
        if (boost.incomeEnabled)
        {
            desriptionIncome.SetActive(false);
            imageEnabledIncome.gameObject.SetActive(true);
            timerIncome.text = boost.timerIncome.ToString();
            imageEnabledIncome.fillAmount = 1f - (float)boost.timerIncome / boost.intervalIncome;
        }
        else
        {
            desriptionIncome.SetActive(true);
            imageEnabledIncome.gameObject.SetActive(false);
        }
    }

    #endregion

    public void SetUpgradeActive(UpgradeType type, bool active)
    {
        int index = (int)type;
        enableButtons[index].SetActive(active);
        disableButtons[index].SetActive(!active);
    }
}

public enum UpgradeType
{
    Damage,
    Cooldown,
    Range,
    Income
}
