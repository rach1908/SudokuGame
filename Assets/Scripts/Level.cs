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
        //Button.GetComponent<Button>().onClick.AddListener();
    }

    public void CreateText(int i)
    {
        Text.GetComponent<Text>().text = sudoku_level.GenerateText();
        Button.GetComponentInChildren<Text>().text = sudoku_level.dif.ToString() + " " + i.ToString();
    }
}
