using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static LevelSelect;

public static class SudokuGenerator 
{
    static List<Sudoku> seeds = new List<Sudoku>();
    // Start is called before the first frame update
    
    
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
            char insert = letters[rnd.Next(0, letters.Count)];
            letters.Remove(insert);
            placeholders.Add(i + 1, insert);
        }
        //Iterating through each letter of the sudoku and answer strings to convert them to the corresponding characters;
        string replacement_string = "";
        string replacement_solution = "";
        for (int i = 0; i < sudoku.sudoku_string.Length; i++)
        {
            replacement_string += sudoku.sudoku_string[i] != '0' ? char.Parse(placeholders.TryGetValue(sudoku.sudoku_string[i], out char character).ToString()) : '0';
            replacement_solution += placeholders.TryGetValue(sudoku.sudoku_string[i], out char value);
        }
        //Solution must be added to this ctor call once the ctor is updated!
        Sudoku returner = new Sudoku(replacement_string, replacement_solution, sudoku.dif);
        return returner;
    }
}
