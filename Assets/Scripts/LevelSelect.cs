using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject sudoku_level;
    public GameObject btn_next;
    public GameObject btn_prev;
    public int entries_per_page;
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
        catch (System.Exception e)
        {
            Debug.Log(e.Message);   
        }
        //Should be loaded from playerprefs or somesuch
        current_page = 0;
        btn_prev.GetComponent<Button>().interactable = false;
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
            sudoku_levels_[sudoku_levels_.Count - 1].transform.SetParent(this.transform, false);
        }
    }

    private void PositionSudoku_Levels()
    {
        //Generating list of sudoku objects to be placed on the current page
        List<GameObject> current_sudokus_;
        if ((current_page + 1) * entries_per_page <= sudokus_.Count)
        {
            //Full page
            current_sudokus_ = sudoku_levels_.GetRange(entries_per_page * current_page, entries_per_page);

        }
        else if (current_page * entries_per_page < sudokus_.Count)
        {
            //Partial page
            current_sudokus_ = sudoku_levels_.GetRange(entries_per_page * current_page, sudoku_levels_.Count - (entries_per_page * current_page));
        }
        else
        {
            Debug.Log("The page of levelselect that was requested is out of range of the index");
        }
    }

    private void ChangePage(bool next)
    {
        if (next)
        {
            if ((current_page + 1) * entries_per_page < sudokus_.Count)
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
        btn_next.GetComponent<Button>().interactable = (current_page + 1) * entries_per_page < sudokus_.Count ? true : false;
        PositionSudoku_Levels();
    }
}
