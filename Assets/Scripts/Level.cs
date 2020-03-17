using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public GameObject Button;
    public GameObject Text;
    public Sudoku sudoku_level;
   
    void Start()
    {
    }

    public void CreateText(int i)
    {
        Text.GetComponent<Text>().text = sudoku_level.GenerateText();
        Button.GetComponentInChildren<Text>().text = sudoku_level.dif.ToString() + " " + i.ToString();
    }

    public void UpdateProgress()
    {
        Text.GetComponent<Text>().text = sudoku_level.GenerateText();
    }
}
