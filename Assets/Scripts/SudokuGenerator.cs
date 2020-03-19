using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LevelSelect;

public class SudokuGenerator: MonoBehaviour
{
    static List<Sudoku> seeds = new List<Sudoku>();
    
    static public void GeneratorStart()
    {
        try
        {
            seeds = SaveLoad.LoadSeeds();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    static public Sudoku GenerateSudoku(Difficulty dif)
    {
        //Selecting a random seed of the given difficulty
        List<Sudoku> possibles = seeds.Where(x => x.dif == dif).ToList();
        if (possibles.Count == 0)
        {
            throw new ArgumentException("There are no seeds of the requested difficulty");
        }
        Sudoku seed = possibles[rnd.Next(0, possibles.Count)];

        //Generate a sudoku from the seed
        return seed;
    }
    //Takes a Sudoku with a valid answer as parameter
    static public Sudoku GenerateSeed(Sudoku sudoku)
    {
      
        Dictionary<int, char> placeholders = new Dictionary<int, char>();
        //This should create a list of the first n letters of the alphabet, using the sudokus side length as n
        List<char> letters = Enumerable.Range('a', int.Parse(Math.Sqrt(sudoku.sudoku_string.Length).ToString())).Select(x => (char)x).ToList();
        
        for (int i = 0; i < Math.Sqrt(sudoku.sudoku_string.Length); i++)
        {
            placeholders.Add(i + 1, letters[i]);
        }
        //Iterating through each letter of the sudoku and answer strings to convert them to the corresponding characters;
        string replacement_string = "";
        string replacement_solution = "";
        for (int i = 0; i < sudoku.sudoku_string.Length; i++)
        {             
            replacement_string += sudoku.sudoku_string[i] != '0' ? placeholders[int.Parse(sudoku.sudoku_string[i].ToString())] : '0';
            replacement_solution += placeholders.TryGetValue(int.Parse(sudoku.solution_string[i].ToString()), out char value) ? value : throw new ArgumentException("The seed cannot be made as the solution contains disallowed characters");
        }
        //Solution must be added to this ctor call once the ctor is updated!
        return new Sudoku(replacement_string, replacement_solution, sudoku.dif);
    }
}
