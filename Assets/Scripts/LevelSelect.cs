using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Options;

public class LevelSelect : MonoBehaviour, IPointerDownHandler
{
    public GameObject sudoku_level;
    public GameObject btn_next;
    public GameObject btn_prev;
    public GameObject drop_dif;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    private int entries_per_line;
    //starts at 0
    private int current_page;
    private List<GameObject> sudoku_levels_ = new List<GameObject>();
    private List<Sudoku> sudokus_ = new List<Sudoku>();
    private List<GameObject> in_use_sudokus_ = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
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
        drop_dif.GetComponent<Dropdown>().value = PlayerPrefs.GetInt(pref_keys.selected_difficulty.ToString());
        //Default value is 0, so the method must be explicitly called in case the saved value is also 0
        DropdownChanged(PlayerPrefs.GetInt(pref_keys.selected_difficulty.ToString()));
        //Loading current page from player prefs
        switch (PlayerPrefs.GetInt(pref_keys.selected_difficulty.ToString()))
        {
            default:
            case 0:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_all.ToString());
                break;
            case 1:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_easy.ToString());
                break;
            case 2:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_medium.ToString());
                break;
            case 3:
                current_page = PlayerPrefs.GetInt(pref_keys.level_page_hard.ToString());
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

    private void SpawnSudoku_Levels()
    {
        foreach (Sudoku sudoku in sudokus_)
        {
            sudoku_levels_.Add(Instantiate(sudoku_level) as GameObject);
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<Level>().sudoku_level = sudoku;
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<Level>().CreateText(sudoku_levels_.Count);
            sudoku_levels_[sudoku_levels_.Count - 1].transform.SetParent(this.transform, false);
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            sudoku_levels_[sudoku_levels_.Count - 1].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            sudoku_levels_[sudoku_levels_.Count - 1].transform.localScale = new Vector3(0, 0, 0);
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
       
        switch (PlayerPrefs.GetInt(pref_keys.selected_difficulty.ToString()))
        {
            default:
            case 0:
                PlayerPrefs.SetInt(pref_keys.level_page_all.ToString(), current_page);
                break;
            case 1:
                PlayerPrefs.SetInt(pref_keys.level_page_easy.ToString(), current_page);
                break;
            case 2:
                PlayerPrefs.SetInt(pref_keys.level_page_medium.ToString(), current_page);
                break;
            case 3:
                PlayerPrefs.SetInt(pref_keys.level_page_hard.ToString(), current_page);
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
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_all.ToString());
                    break;
                case 1:
                    //Easy
                    current = Difficulty.Easy;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_easy.ToString());
                    break;
                case 2:
                    //Medium
                    current = Difficulty.Medium;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_medium.ToString());
                    break;
                case 3:
                    //Hard
                    current = Difficulty.Hard;
                    current_page = PlayerPrefs.GetInt(pref_keys.level_page_hard.ToString());
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
            current_page = PlayerPrefs.GetInt(pref_keys.level_page_all.ToString());
        }
        PlayerPrefs.SetInt(pref_keys.selected_difficulty.ToString(), i);
        PositionSudoku_Levels();
    }

    private void SelectLevel(Level lvl)
    {
        Debug.Log(sudokus_.IndexOf(lvl.sudoku_level));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Before");
        if (eventData.selectedObject != null && eventData.selectedObject.transform.parent != null && eventData.selectedObject.GetComponentInParent<Level>() != null)
        {
            Debug.Log("after");
            SelectLevel(eventData.selectedObject.GetComponentInParent<Level>());
        }
    }
}
