using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private Shoot shoot;
    [SerializeField] private LevelController lvlController;
    public int priceDamage, priceCooldown, priceRange, priceIncome;
    public float multPriceDamage, multPriceCooldown, multPriceRange, multPriceIncome;
    public float damageMultiplier, cooldownMultiplier, rangeMultiplier, incomeMultiplier;

    public bool upgradeCooldownMax;

    private void Start()
    {
        Load();
        ui.StartUpdateUI();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("priceDamage", priceDamage);
        PlayerPrefs.SetInt("priceCooldown", priceCooldown);
        PlayerPrefs.SetInt("priceRange", priceRange);
        PlayerPrefs.SetInt("priceIncome", priceIncome);
        shoot.Save();
        lvlController.Save();
    }
    public void Load()
    {
        priceDamage = PlayerPrefs.GetInt("priceDamage", 10);
        priceCooldown = PlayerPrefs.GetInt("priceCooldown", 20);
        priceRange = PlayerPrefs.GetInt("priceRange", 25);
        priceIncome = PlayerPrefs.GetInt("priceIncome", 15);

    }
    #region UpgradeBase
    public void UpgradeDamage()
    {
        int star = lvlController.star;
        if (star >= priceDamage)
        {
            lvlController.DelStar(priceDamage);
            shoot.damage = (int)(shoot.damage * damageMultiplier);
            priceDamage = (int)(priceDamage * multPriceDamage);
            ui.UpdateDamage();
            ui.UpdateBase();
            CheckPrice();
            Save();
        }
    }

    public void UpgradeCooldown()
    {
        int star = lvlController.star;
        if (star >= priceCooldown)
        {
            lvlController.DelStar(priceCooldown);
            shoot.cooldown -= cooldownMultiplier;
            priceCooldown = (int)(priceCooldown * multPriceCooldown);
            if (shoot.cooldown <= 0.06f)
            {
                upgradeCooldownMax = true;
            }
            else
            {
                upgradeCooldownMax = false;

            }
            ui.UpdateCooldown();
            ui.UpdateBase();
            CheckPrice();
            Save();
        }

    }

    public void UpgradeRange()
    {
        int star = lvlController.star;
        if (star >= priceRange)
        {
            lvlController.DelStar(priceRange);
            shoot.range = shoot.range * rangeMultiplier;
            priceRange = (int)(priceRange * multPriceRange);
            ui.UpdateRange();
            ui.UpdateBase();
            CheckPrice();
            Save();
        }
    }

    public void UpgradeIncome()
    {
        int star = lvlController.star;
        if (star >= priceIncome)
        {
            lvlController.DelStar(priceIncome);
            if (((int)(lvlController.income * incomeMultiplier)/ lvlController.income) <= 1)
            {
                lvlController.income++;
            }
            else
            {
                lvlController.income = (int)(lvlController.income * incomeMultiplier);
            }
            priceIncome = (int)(priceIncome * multPriceIncome);
            ui.UpdateIncome();
            ui.UpdateBase();
            CheckPrice();
            Save();
        }
    }
    #endregion

    public void CheckPrice()
    {
        int star = lvlController.star;
        ui.SetUpgradeActive(UpgradeType.Damage, star >= priceDamage);
        ui.SetUpgradeActive(UpgradeType.Cooldown, star >= priceCooldown);
        ui.SetUpgradeActive(UpgradeType.Range, star >= priceRange);
        ui.SetUpgradeActive(UpgradeType.Income, star >= priceIncome);

    }
}
