using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelSelect;

[System.Serializable]
public class Sudoku
{
    private Difficulty difficulty;
    public string sudoku_string;
    public string solution_string;
    private bool finished;
    private string player_prog_numbers;
    private string player_prog_corners;
    private string player_prog_centers;

    public bool Finished
    {
        get { return finished; }
        set { finished = value; }
    }

    public Difficulty dif
    {
        get { return difficulty; }
    }

    public string Player_prog_numbers
    {
        get { return player_prog_numbers; }
        set { player_prog_numbers = value; }
    }

    public string Player_prog_corners
    {
        get { return player_prog_corners; }
        set { player_prog_corners = value; }
    }
    public string Player_prog_centers
    {
        get { return player_prog_centers; }
        set { player_prog_centers = value; }
    }


    public Sudoku(string Sud_string, string sol_string, Difficulty dif)
    {
        difficulty = dif;
        solution_string = sol_string;
        sudoku_string = Sud_string;
    }

    public string GenerateText()
    {
        string returner = "";
        if (player_prog_numbers == null && player_prog_centers == null && player_prog_corners == null)
        {
            returner = "Not Started";
        }
        else if (!finished)
        {
            returner = "In Progress";
        }
        else
        {
            returner = "Completed";
        }
        return returner;
    }

    public bool VerifyAnswer()
    {
        string res_no_givens = "";
        for (int i = 0; i < DataPassing.sudoku_.sudoku_string.Length; i++)
        {
            if (DataPassing.sudoku_.sudoku_string[i] == '0')
            {
                res_no_givens += solution_string[i];
            }
        }
        if (res_no_givens == player_prog_numbers)
        {
            finished = true;
            return true;
        }

        return false;
    }
}
