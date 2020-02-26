using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using static Options;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Player prefs
        if (PlayerPrefs.GetString(pref_keys.c_tile_default.ToString()) == "")
        {
            //If no playerprefs are found, default ones are added
            PlayerPrefs.SetString(pref_keys.c_tile_default.ToString(), "#FFFFFF");
            PlayerPrefs.SetString(pref_keys.c_tile_highlighted.ToString(), "#EDF50C");
            PlayerPrefs.SetString(pref_keys.c_text_given.ToString(), "#000000");
            PlayerPrefs.SetString(pref_keys.c_text_input.ToString(), "#1A67EB");
            PlayerPrefs.SetInt(pref_keys.error_highlighting.ToString(), 0);
        }
        //Base Levels
        List<Sudoku> suds = new List<Sudoku>() {
            new Sudoku("060501800473002005501000024810600000090000030357020601005207480946100750008900010", LevelSelect.Difficulty.Easy),
            new Sudoku("000409802570380004000002500328017060057930000900020730780100000605208007094073050", LevelSelect.Difficulty.Easy),
            new Sudoku("759300000800040360604000005300205001060974050900803006100000504098030002000002189", LevelSelect.Difficulty.Easy),
            new Sudoku("400102680100090004038064010005071920026009800800250000903000008250600107607905300", LevelSelect.Difficulty.Easy),
            new Sudoku("000900005406500800700800100002150308080037000004200061501020004049300000000000610", LevelSelect.Difficulty.Medium),
            new Sudoku("070900400065000908000180000200406150040008306000003000400500072700040001306000509", LevelSelect.Difficulty.Medium),
            new Sudoku("070030200005002900400900000004205090010390706200000005192700030047500100000103000", LevelSelect.Difficulty.Medium),
            new Sudoku("060420085000000374410308000007800096306700000000006050070900500000250000040013800", LevelSelect.Difficulty.Medium),
            new Sudoku("003600000900800207000000000040150830070004000820000000090005308500760400000000560", LevelSelect.Difficulty.Hard),
            new Sudoku("000247000000100070197003500000001003050000060000000908605000004030908000009070300", LevelSelect.Difficulty.Hard),
            new Sudoku("000700340000005071047000006056010020200000900710000003500070000400060590000530064", LevelSelect.Difficulty.Hard)
        };

        foreach (Sudoku sudoku in suds)
        {
            Sudoku.current = sudoku;
            SaveLoad.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
