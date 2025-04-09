using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public string nameFigure;
    public List<Cube> listCube;
    public int currentHealthCube = 10;
    [SerializeField] private Vector3 direction;
    public void FillHealth()
    {
        foreach (Cube cube in listCube)
        {
            cube.gameObject.SetActive(true);
            cube.healthMaxCube = currentHealthCube;
            cube.SpawnThis();
        }
    }
    void Update()
    {
        transform.Rotate(direction * 10 * Time.deltaTime);
    }
}
