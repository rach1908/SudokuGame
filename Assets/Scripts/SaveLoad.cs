using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad
{
    public static List<Sudoku> sudokus_ = new List<Sudoku>();

    public static void Save()
    {
        sudokus_.Add(Sudoku.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("./levels.lvl");
        bf.Serialize(file, sudokus_);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists("./levels.lvl"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("./levels.lvl", FileMode.Open);
            sudokus_ = (List<Sudoku>)bf.Deserialize(file);
            file.Close();
        }
    }

    //WORKING
    //public static void TestSave(string test)
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create("./Test.lel");
    //    bf.Serialize(file, test);
    //    file.Close();
    //}

    //public static string TestLoad()
    //{
    //    string returner = "";
    //    if (File.Exists("./Test.lel"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open("./Test.lel", FileMode.Open);
    //        returner = (string)bf.Deserialize(file);
    //        file.Close();
    //    }
    //    return returner;
    //}
}
