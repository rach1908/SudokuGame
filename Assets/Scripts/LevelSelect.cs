using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject sudoku_level;
    public GameObject btn_next;
    public GameObject btn_prev;
    private int entries_per_line;
    //starts at 0
    private int current_page;
    private List<GameObject> sudoku_levels_ = new List<GameObject>();
    private List<Sudoku> sudokus_ = new List<Sudoku>();
    // Start is called before the first frame update
    void Start()
    {
        btn_next.GetComponent<Button>().onClick.AddListener(delegate { ChangePage(true); });
        btn_prev.GetComponent<Button>().onClick.AddListener(delegate { ChangePage(false); });
        try
        {
            sudokus_ = SaveLoad.Load();
        }
        catch (System.ArgumentException e)
        {
            Debug.Log(e.Message);
        }
        //Should be loaded from playerprefs or somesuch
        current_page = 0;
        entries_per_line = 3;
        btn_prev.GetComponent<Button>().interactable = false;
        SpawnSudoku_Levels();
        PositionSudoku_Levels();
        Debug.Log(sudokus_.Count);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
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

    private void SpawnSudoku_Levels()
    {
        foreach (Sudoku sudoku in sudokus_)
        {
            sudoku_levels_.Add(Instantiate(sudoku_level) as GameObject);
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<Level>().sudoku_level = sudoku;
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<Level>().CreateText();
            sudoku_levels_[sudoku_levels_.Count - 1].transform.SetParent(this.transform, false);
            sudoku_levels_[sudoku_levels_.Count - 1].transform.localScale = new Vector3(0, 0, 0);
        }
    }

    private void PositionSudoku_Levels()
    {
        //Generating list of Level objects to be placed on the current page
        List<GameObject> current_sudokus_ = new List<GameObject>();
        if ((current_page + 1) * entries_per_line * entries_per_line <= sudokus_.Count)
        {
            //Full page
            current_sudokus_ = sudoku_levels_.GetRange(entries_per_line * entries_per_line * current_page, entries_per_line * entries_per_line);

        }
        else if (current_page * entries_per_line * entries_per_line < sudokus_.Count)
        {
            //Partial page
            current_sudokus_ = sudoku_levels_.GetRange(entries_per_line * entries_per_line * current_page, sudoku_levels_.Count - (entries_per_line * entries_per_line * current_page));
        }
        else
        {
            Debug.Log("The page of levelselect that was requested is out of range of the index");
            Debug.Log(sudokus_.Count);
        }
        //Placement
        Vector2 offset = new Vector2();
        offset.x = Screen.width / (entries_per_line + 1);
        offset.y = Screen.height / (entries_per_line + 1);
        for (int i = 0; i < current_sudokus_.Count; i++)
        {
            current_sudokus_[i].transform.localScale = new Vector3(1, 1, 1);
            current_sudokus_[i].GetComponent<RectTransform>().anchoredPosition = 
                new Vector3(offset.x * ((i % entries_per_line) + 1) , offset.y * ((i / entries_per_line) + 1));
        }
    }

    private void ChangePage(bool next)
    {
        if (next)
        {
            if ((current_page + 1) * entries_per_line * entries_per_line < sudokus_.Count)
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
        btn_prev.GetComponent<Button>().interactable = current_page > 0 ? true : false;
        btn_next.GetComponent<Button>().interactable = (current_page + 1) * entries_per_line * entries_per_line < sudokus_.Count ? true : false;
        PositionSudoku_Levels();
    }
}
