using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelController : MonoBehaviour
{
    #region Instance
    public static LevelController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public Upgrade upgrade;
    public Skins skins;
    public Effects effects;
    public UI ui;
    public Task task;
    [SerializeField] private List<Figure> figureList;
    [HideInInspector] public Figure currentFigure;
    public int cubeCount, cubeCountMax;

    [SerializeField] private float healthCubemultiplier;
    public int startHealthCube;

    public int level, levelMax;
    public int star, income;

    public bool enabledBoostIncome;
    public bool enabledRandomSpawnFigure;
    public int currentNumberFigure;

    private void Start()
    {
        Load();
        currentFigure = figureList[currentNumberFigure];
        currentFigure.gameObject.SetActive(true);
        FillFigure();
        ui.StartUpdateUI();
        upgrade.CheckPrice();
        skins.CheckUnlock();
        effects.CheckUnlock();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("star", star);
        PlayerPrefs.SetInt("income", income);
        PlayerPrefs.SetInt("currentNumberFigure", currentNumberFigure);
    }
    public void Load()
    {
        level = PlayerPrefs.GetInt("level", 0);
        star = PlayerPrefs.GetInt("star", 0);
        income = PlayerPrefs.GetInt("income", 1);
        currentNumberFigure = PlayerPrefs.GetInt("currentNumberFigure", 0);
    }
    public IEnumerator NextFigure()
    {
        yield return new WaitForSeconds(2f);
        currentFigure.gameObject.SetActive(false);
        level ++;
        if (level>= levelMax)
        {
            currentNumberFigure = Random.Range(0, figureList.Count);
        }
        else
        {
            currentNumberFigure = level;
        }
        currentFigure = figureList[currentNumberFigure];
        currentFigure.gameObject.SetActive(true);
        FillFigure();
        skins.CheckUnlock();
        effects.CheckUnlock();
        ui.UpdateBase();
        Save();
    }

    public void FillFigure()
    {
        startHealthCube = (int)(10 + 10 * healthCubemultiplier * level);
        cubeCountMax = currentFigure.listCube.Count;
        currentFigure.currentHealthCube = startHealthCube;
        currentFigure.FillHealth();
        cubeCount = cubeCountMax;
        ui.UpdateBase();
    }
    public void CubeDied()
    {
        cubeCount = Mathf.Max(0, cubeCount - 1);
        AddStar();
        if (cubeCount == 0)
        {
           StartCoroutine( NextFigure());
        }
        ui.UpdateBase();
        upgrade.CheckPrice();
        task.AddDestroyCube();
        Save();
    }
    public void AddStar()
    {
        if (enabledBoostIncome) 
        {
            star += income * 5;
            task.AddPickUpStars(income * 5);
        }
        else
        {
            star += income;
            task.AddPickUpStars(income);
        }

    }
    public void DelStar(int count)
    {
        star -= count;
    }
}
