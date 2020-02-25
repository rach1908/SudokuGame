using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelSelect;

[System.Serializable]
public class Sudoku : MonoBehaviour
{
    public static Sudoku current;
    public GameObject Button;
    public GameObject text;
    private Difficulty difficulty;
    public string sudoku_string;
    private string player_prog_string;
    public Sudoku(string Sud_string, Difficulty dif)
    {
        difficulty = dif;
        sudoku_string = Sud_string;
    }
    private enum progress
    {
        
    }
    private void GenerateText()
    {
        if (player_prog_string == null)
        {
            text.GetComponent<Text>().text = " ";
        }
    }
}
