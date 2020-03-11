using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Options;

public class LevelSelect : MonoBehaviour
{
    public GameObject sudoku_level;
    public GameObject btn_next;
    public GameObject btn_prev;
    public GameObject drop_dif;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public static System.Random rnd = new System.Random();
    private int entries_per_line;
    //starts at 0
    private int current_page;
    private List<GameObject> sudoku_levels_ = new List<GameObject>();
    private List<Sudoku> sudokus_ = new List<Sudoku>();
    private List<GameObject> in_use_sudokus_ = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        //RecreateSudokuFile();
        drop_dif.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { DropdownChanged(drop_dif.GetComponent<Dropdown>().value); });
        //Adding text to the dropdown
        drop_dif.GetComponent<Dropdown>().options.Clear();
        drop_dif.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "All Difficulties" });
        foreach (Difficulty difficulty in (Difficulty[]) Enum.GetValues(typeof(Difficulty)))
        {
            drop_dif.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = difficulty.ToString() });
        }
        btn_next.GetComponent<Button>().onClick.AddListener(delegate { ChangePage(true); });
        btn_prev.GetComponent<Button>().onClick.AddListener(delegate { ChangePage(false); });
        if (DataPassing.sudoku_ != null)
        {
            SaveLoad.Save(DataPassing.sudoku_);
        }
        try
        {
            sudokus_ = SaveLoad.Load();
        }
        catch (System.ArgumentException e)
        {
            Debug.Log(e.Message);
        }
        SpawnSudoku_Levels();
        //Loading difficulty selection from player prefs
        drop_dif.GetComponent<Dropdown>().value = PlayerPrefs.GetInt(pref_keys.selected_difficulty_int.ToString());
        //Default value is 0, so the method must be explicitly called in case the saved value is also 0
        DropdownChanged(PlayerPrefs.GetInt(pref_keys.selected_difficulty_int.ToString()));
        //Loading current page from player prefs
        switch (PlayerPrefs.GetInt(pref_keys.selected_difficulty_int.ToString()))
        {
            default:
            case 0:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_all_int.ToString());
                break;
            case 1:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_easy_int.ToString());
                break;
            case 2:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_medium_int.ToString());
                break;
            case 3:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_hard_int.ToString());
                break;
            case 4:
                Debug.Log("PlayerPrefs has a difficulty selection saved that is not coded");
                break;
        }
        entries_per_line = 3;
        if (current_page == 0 )
        {
            btn_prev.GetComponent<Button>().interactable = false;
        }
        if ((current_page + 1) * entries_per_line * entries_per_line >= in_use_sudokus_.Count)
        {
            btn_next.GetComponent<Button>().interactable = false;
        }
        //Creating and positioning the appropriate levels
        PositionSudoku_Levels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public enum Difficulty{
        Easy,
        Medium,
        Hard
    }

    private void RecreateSudokuFile()
    {
        ////Base Levels
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

        SaveLoad.ReMakeSave(suds);
    }

    private void AddSudoku(Sudoku sud)
    {

    }

    private void SpawnSudoku_Levels()
    {
        foreach (Sudoku sudoku in sudokus_)
        {
            sudoku_levels_.Add(Instantiate(sudoku_level) as GameObject);
            GameObject sudlvl = sudoku_levels_[sudoku_levels_.Count - 1];
            sudlvl.GetComponent<Level>().sudoku_level = sudoku;
            sudlvl.GetComponent<Level>().CreateText(sudoku_levels_.Count);
            sudlvl.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectLevel(sudlvl.GetComponent<Level>()); });
            sudlvl.transform.SetParent(this.transform, false);
            sudlvl.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            sudlvl.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            sudlvl.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void PositionSudoku_Levels()
    {
        btn_prev.GetComponent<Button>().interactable = current_page > 0 ? true : false;
        btn_next.GetComponent<Button>().interactable = (current_page + 1) * entries_per_line * entries_per_line < in_use_sudokus_.Count ? true : false;
        //Generating list of Level objects to be placed on the current page
        List<GameObject> current_sudokus_ = new List<GameObject>();
        if ((current_page + 1) * entries_per_line * entries_per_line <= in_use_sudokus_.Count)
        {
            //Full page
            current_sudokus_ = in_use_sudokus_.GetRange(entries_per_line * entries_per_line * current_page, entries_per_line * entries_per_line);

        }
        else if (current_page * entries_per_line * entries_per_line < in_use_sudokus_.Count)
        {
            //Partial page
            current_sudokus_ = in_use_sudokus_.GetRange(entries_per_line * entries_per_line * current_page, in_use_sudokus_.Count - (entries_per_line * entries_per_line * current_page));
        }
        else
        {
            Debug.Log("The page of levelselect that was requested is out of range of the index");
            Debug.Log(sudokus_.Count);
        }
        //Removing any on-screen levels by decreasing size to 0:
        foreach (GameObject obj in sudoku_levels_)
        {
            obj.transform.localScale = new Vector3(0, 0, 0);

        }
        //Placement
        start_position.y = Screen.height;
        Vector2 offset = new Vector2();
        offset.x = Screen.width / (entries_per_line + 1);
        offset.y = Screen.height / (entries_per_line + 2);
        for (int i = 0; i < current_sudokus_.Count; i++)
        {
            current_sudokus_[i].transform.localScale = new Vector3(1, 1, 1);
            current_sudokus_[i].GetComponent<RectTransform>().position = 
                new Vector3(start_position.x + offset.x * ((i % entries_per_line) + 1) , start_position.y  - offset.y * ((i / entries_per_line) + 1));
        }
    }

    private void ChangePage(bool next)
    {
        if (next)
        {
            if ((current_page + 1) * entries_per_line * entries_per_line < in_use_sudokus_.Count)
            {
                current_page += 1;
            }
        }
        else
        {
            if (current_page > 0)
            {
                current_page -= 1;
            }
        }
       
        switch (PlayerPrefs.GetInt(pref_keys.selected_difficulty_int.ToString()))
        {
            default:
            case 0:
                PlayerPrefs.SetInt(pref_keys.level_page_all_int.ToString(), current_page);
                break;
            case 1:
                PlayerPrefs.SetInt(pref_keys.level_page_easy_int.ToString(), current_page);
                break;
            case 2:
                PlayerPrefs.SetInt(pref_keys.level_page_medium_int.ToString(), current_page);
                break;
            case 3:
                PlayerPrefs.SetInt(pref_keys.level_page_hard_int.ToString(), current_page);
                break;
            case 4:
                Debug.Log("PlayerPrefs has a difficulty selection saved that is not coded");
                break;
        }
        PositionSudoku_Levels();
    }

    private void DropdownChanged(int i)
    {
        if (i != 0)
        {
            Difficulty current = Difficulty.Easy;
            switch (i)
            {
                default:
                case 0:
                    //Should be unreachable
                    //All difficulties
                    in_use_sudokus_ = sudoku_levels_;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_all_int.ToString());
                    break;
                case 1:
                    //Easy
                    current = Difficulty.Easy;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_easy_int.ToString());
                    break;
                case 2:
                    //Medium
                    current = Difficulty.Medium;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_medium_int.ToString());
                    break;
                case 3:
                    //Hard
                    current = Difficulty.Hard;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_hard_int.ToString());
                    break;
                case 4:
                    //Not implemented
                    Debug.Log("The dropdown selector in the Level Select menu has an option that has no code");
                    break;

            }
            in_use_sudokus_ = sudoku_levels_.Where(x => x.GetComponent<Level>().sudoku_level.dif == current).ToList();
        }
        else
        {
            in_use_sudokus_ = sudoku_levels_;
            current_page = PlayerPrefs.GetInt(pref_keys.level_page_all_int.ToString());
        }
        PlayerPrefs.SetInt(pref_keys.selected_difficulty_int.ToString(), i);
        PositionSudoku_Levels();
    }

    private void SelectLevel(Level lvl)
    {
        DataPassing.sudoku_ = lvl.sudoku_level;
        SceneManager.LoadScene("GameScene");
    }

}
