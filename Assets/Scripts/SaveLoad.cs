using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad
{
    public static List<Sudoku> sudokus_ = new List<Sudoku>();
    //Saves all changes made to Sudokus in the system
    public static void Save(Sudoku sudoku)
    {
        sudokus_[sudokus_.IndexOf(sudokus_.Find(x => x.sudoku_string == sudoku.sudoku_string))] = sudoku;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "levels.lvl", FileMode.OpenOrCreate);
        bf.Serialize(file, sudokus_);
        
        file.Close();
    }

    public static void SaveNew(List<Sudoku> list)
    {
        sudokus_.AddRange(list);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "levels.lvl", FileMode.OpenOrCreate);
        bf.Serialize(file, sudokus_);

        file.Close();
    }

    public static void ReMakeSave(List<Sudoku> list)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "levels.lvl", FileMode.OpenOrCreate);
        bf.Serialize(file, list);
        file.Close();
    }
    public static List<Sudoku> Load()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "levels.lvl"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "levels.lvl", FileMode.Open);
            sudokus_ = (List<Sudoku>)bf.Deserialize(file);
            file.Close();
            return sudokus_;
        }
        throw new ArgumentException("There are no sudokus to load!");
    }

    public static void SaveSeeds(List<Sudoku> seeds)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "seeds.lvl", FileMode.OpenOrCreate);
        bf.Serialize(file, seeds);
        file.Close();
    }

    public static List<Sudoku> LoadSeeds()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "seeds.lvl"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "seeds.lvl", FileMode.Open);
            List<Sudoku> seeds = (List<Sudoku>)bf.Deserialize(file);
            file.Close();
            return seeds;
        }
        throw new ArgumentException("There are no seeds to load");
    }

}
