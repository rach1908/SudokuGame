using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelSelect;

[System.Serializable]
public class Sudoku : MonoBehaviour
{
    public static Sudoku current;
    public Difficulty difficulty;
    public string sudoku_string;
    public string player_prog_string;
    public Sudoku(string Sud_string, Difficulty dif)
    {
        difficulty = dif;
        sudoku_string = Sud_string;
    }
}
