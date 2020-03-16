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


    public Sudoku(string Sud_string, Difficulty dif)
    {
        difficulty = dif;
        //not until after the answers are done
        //solution_string = sol_string;
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
            returner = "Finished";
        }
        return returner;
    }

    public bool VerifyAnswer(string answer)
    {
        //Checks that all the gives are present in the solution
        for (int i = 0; i < sudoku_string.Length; i++)
        {
            if (sudoku_string[i] != '0')
            {
                if (sudoku_string[i] != answer[i])
                {
                    Debug.Log("The solution and the sudoku string gives conflict");
                    return false;
                }
            }
        }
        //The given answer is valid for this sudoku
        int sudoku_side = int.Parse(Math.Sqrt(answer.Length).ToString());
        int square_side = int.Parse(Math.Sqrt(sudoku_side).ToString());
        //The answer is checked to see if it is valid in itself
        for (int i = 0; i < sudoku_side; i++)
        {
            //row check
            HashSet<char> vs = new HashSet<char>();
            for (int j = 0; j < sudoku_side; j++)
            {
                if (!vs.Add(answer[(i * sudoku_side) + j]))
                {
                    Debug.Log("The row flags false");
                    return false;
                }
            }
            //Column check
            vs = new HashSet<char>();
            for (int j = 0; j < sudoku_side; j++)
            {
                if (!vs.Add(answer[i + j * sudoku_side]))
                {
                    Debug.Log("The column flags false");
                    return false;
                }
            }
            //Square check
            vs = new HashSet<char>();
            for (int j = 0; j < sudoku_side; j++)
            {
                if (!vs.Add(answer[(i % square_side) * square_side + ((i / square_side) * square_side * sudoku_side) + j % square_side + sudoku_side * (j / square_side)]))
                {
                    Debug.Log("The square flags false");
                    return false;
                }
            }
        }
        //The answer is valid
        return true;

    }
}
