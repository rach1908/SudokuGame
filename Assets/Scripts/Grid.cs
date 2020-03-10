using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Options;

public class Grid : MonoBehaviour, IPointerDownHandler
{
    //Determines the side lenght of the sudoku. Length must be a squared number, will be set ingame, drop down menu possibly (anything above 8^2 seems to slow down the game considerably, 13^2 almost made unity crash). Must be at least 4
    public static int length = 0;
    public GameObject grid_square;
    public GameObject btn_Menu;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    private float square_scale = 1.0f;
    public float buffer = 0.85f;
    private static int root = 0;
    private static List<GameObject> grid_squares_ = new List<GameObject>();
    public static List<GridSquare> selected_squares_ = new List<GridSquare>();
    public static List<GridSquare> all_squares_ = new List<GridSquare>();
    private static int sudoku_width;

    void Start()
    {
        length = int.Parse(Math.Sqrt(double.Parse(DataPassing.sudoku_.sudoku_string.Length.ToString())).ToString());
        root = int.Parse(Math.Sqrt(double.Parse(length.ToString())).ToString());
        sudoku_width = (117 * root + 4) * root;
        SceneManager.LoadScene("MetaScene", LoadSceneMode.Additive);
        grid_squares_ = new List<GameObject>();
        all_squares_ = new List<GridSquare>();
        selected_squares_ = new List<GridSquare>();
        CreateGrid();
        if (grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("GridSquare object needs to have GridSquare script attached");
        }
        foreach (GameObject game in grid_squares_)
        {
            all_squares_.Add(game.GetComponent<GridSquare>());
        }
        //Return to menu button setup
        btn_Menu.GetComponent<RectTransform>().anchoredPosition = new Vector3(Screen.width - (btn_Menu.GetComponent<RectTransform>().rect.width / 2 + 25), Screen.height - (btn_Menu.GetComponent<RectTransform>().rect.height / 2 + 25));
        btn_Menu.GetComponent<Button>().onClick.AddListener(ReturnToMenu);
        //This will recieve data from the Sudoku.current object
        FillGrid(DataPassing.sudoku_.sudoku_string, DataPassing.sudoku_.Player_prog_numbers, DataPassing.sudoku_.Player_prog_corners, DataPassing.sudoku_.Player_prog_centers);
        foreach (GridSquare gridSquare in all_squares_)
        {
            gridSquare.ColorTheme(
                PlayerPrefs.GetString(pref_keys.c_tile_default.ToString()),
                PlayerPrefs.GetString(pref_keys.c_tile_highlighted.ToString()),
                PlayerPrefs.GetString(pref_keys.c_text_input.ToString()),
                PlayerPrefs.GetString(pref_keys.c_text_given.ToString())
                );
        }

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void CreateGrid()
    {
        //sizing the grid relative to screen size
        if (Screen.height <= Screen.width)
        {
            square_scale = (float.Parse(Screen.height.ToString()) * buffer) / sudoku_width;
        }
        else
        {
            square_scale = (float.Parse(Screen.width.ToString()) * buffer) / sudoku_width;
        }
        //Setting start position
        start_position.x = float.Parse(Screen.width.ToString()) / 2 - (float.Parse(sudoku_width.ToString()) * square_scale / 2 - 119 * square_scale / 2);
        start_position.y = float.Parse(Screen.height.ToString()) / 2 + (float.Parse(sudoku_width.ToString()) * square_scale / 2 - 119 * square_scale / 2);
        SpawnGridSquares();
        SetSquarePosition();

    }
    private void ReturnToMenu()
    {
        CompilePlayer_prog();
        SceneManager.LoadScene("MenuScene");
    }
    private void SpawnGridSquares()
    {
        for (int row = 0; row < length; row++)
        {
            for (int column = 0; column < length; column++)
            {
                grid_squares_.Add(Instantiate(grid_square) as GameObject);
                grid_squares_[grid_squares_.Count - 1].transform.SetParent(this.transform, false);
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale);
            }
        }

    }

    private void SetSquarePosition()
    {
        var square_rect = grid_squares_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        //Offsetting Square positions based on their position in the grid
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y;

        //Alternative, more scalable placement
        for (int row = 0; row < length; row++)
        {
            for (int column = 0; column < length; column++)
            {
                GameObject obj = grid_squares_[(row * length) + column];
                //Figuring out which Image is needed, and how to rotate it
                int rotation = 0;
                int edges = 0;
                //If the column/row is right before or after a block or the edge of the grid it will be caught in these if statements

                //Only one of these two statements can ever trigger, unless the length is too small
                //Above the square
                if (row % root == 0)
                {
                    rotation += 1;
                }
                //Below the Square
                if (row % root == root - 1)
                {
                    rotation += 100;
                }

                //Only one of these two statements can ever trigger, unless the length is too small
                //Left of the Square
                if (column % root == 0)
                {
                    rotation += 1000;
                }
                //Right of the Square
                if (column % root == root - 1)
                {
                    rotation += 10;
                }

                //A switch statement to Parse which possible combination of edges has come true. God will smite this like Sodom
                switch (rotation)
                {
                    case 1:
                        rotation = 1;
                        edges = 1;
                        break;
                    case 10:
                        rotation = 2;
                        edges = 1;
                        break;
                    case 100:
                        rotation = 3;
                        edges = 1;
                        break;
                    case 1000:
                        rotation = 0;
                        edges = 1;
                        break;
                    case 11:
                        rotation = 1;
                        edges = 2;
                        break;
                    case 110:
                        rotation = 2;
                        edges = 2;
                        break;
                    case 1100:
                        rotation = 3;
                        edges = 2;
                        break;
                    case 1001:
                        rotation = 0;
                        edges = 2;
                        break;
                    default:
                        rotation = 0;
                        break;
                }
                //Change the source image to create the block line?
                //This method sets an image, and rotates it as necessary (i hope)
                obj.GetComponent<GridSquare>().SetImage(edges, rotation);

                //setting the actual position             
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + (offset.x * column), start_position.y - (offset.y * row));
            }
        }
    }

    private void FillGrid(string sudokuString, string player_prog_numbers, string player_prog_corners, string player_prog_centers)
    {
        if (sudokuString.Length != length * length)
        {
            throw new ArgumentException("The string is not appropriate length");
        }

        //Givens
        for (int i = 0; i < length * length; i++)
        {
            if (int.Parse(sudokuString[i].ToString()) != 0)
            {
                grid_squares_[i].GetComponent<GridSquare>().SetNumber(int.Parse(sudokuString[i].ToString()));
                grid_squares_[i].GetComponent<GridSquare>().given = true;
            }
        }
        //Player inputs
        if (player_prog_numbers != null)
        {
            List<GridSquare> empties = all_squares_.Where(x => x.given == false).ToList();
            string[] corners = player_prog_corners.Split(',');
            string[] centers = player_prog_centers.Split(',');


            for (int i = 0; i < empties.Count; i++)
            {
                empties[i].Number_ = int.Parse(player_prog_numbers[i].ToString());
                if (centers[i].Length > 1)
                {
                    List<int> center_temp_ = new List<int>();
                    foreach (char c in centers[i])
                    {
                        center_temp_.Add(int.Parse(c.ToString()));
                    }
                    empties[i].Center_marks_ = center_temp_;
                }
                else if (int.Parse(centers[i][0].ToString()) != 0)
                {
                    empties[i].Center_marks_ = new List<int>() { int.Parse(centers[i][0].ToString()) };
                }
                if (corners[i].Length > 1)
                {
                    List<int> corner_temp_ = new List<int>();
                    foreach (char c in corners[i])
                    {
                        corner_temp_.Add(int.Parse(c.ToString()));
                    }
                    empties[i].Corner_marks_ = corner_temp_;
                }
                else if (int.Parse(corners[i][0].ToString()) != 0)
                {
                    empties[i].Corner_marks_ = new List<int>() { int.Parse(corners[i][0].ToString()) };
                }
                empties[i].DisplayText();
            }
        }
    }
    public enum NumberPos
    {
        Standard,
        Center,
        Corner
    }

    public static void SetGridNumber(int number, NumberPos np)
    {
        if (selected_squares_.Count > 0)
        {
            if (np == NumberPos.Standard)
            {
                foreach (GridSquare square in selected_squares_)
                {
                    square.SetNumber(number);
                }
                NumberChanged(selected_squares_[selected_squares_.Count - 1]);
            }
            else if (np == NumberPos.Center)
            {
                foreach (GridSquare square in selected_squares_)
                {
                    square.ToggleCenterMark(number);
                }
            }
            else if (np == NumberPos.Corner)
            {
                foreach (GridSquare square in selected_squares_)
                {
                    square.ToggleCornerMark(number);
                }
            }
        }
    }

    //Called when a selection is added to current list of selections
    public static void SelectSquare(GridSquare g)
    {
        selected_squares_[selected_squares_.Count - 1].SetColor("SELECTED");
        if (selected_squares_.Contains(g))
        {
            selected_squares_.Remove(g);
            selected_squares_.Add(g);
        }
        else
        {
            selected_squares_.Add(g);
        }

        g.SetColor("SELECTED");
    }

    //Called when a selection overwrites the current list
    public static void ReSelectSquare(GridSquare g)
    {
        foreach (GridSquare gs in selected_squares_)
        {
            gs.SetColor("DEFAULT");
        }
        selected_squares_ = new List<GridSquare>();
        selected_squares_.Add(g);
        g.SetColor("SELECTED");
    }

    //Called when a click is registrered outside of the grid
    //Not in use as of now
    public static void ClearSelected()
    {
        selected_squares_ = new List<GridSquare>();
    }

    public static void SelectAdjacent(Event ev)
    {
        if (selected_squares_.Count > 0)
        {
            //Finding the index of the most recently selected square
            int index = all_squares_.IndexOf(selected_squares_[selected_squares_.Count - 1]);
            //target is the gridsquare that will be added to the selection.
            //Currently it refers to the most recently selected, so a failure in the method will not result in a random square being selected
            GridSquare target = all_squares_[index];
            switch (ev.keyCode)
            {
                case KeyCode.A:
                case KeyCode.LeftArrow:
                    //move left
                    if (index % (root * root) == 0)
                    {
                        target = all_squares_[index + 8];
                    }
                    else
                    {
                        target = all_squares_[index - 1];
                    }
                    break;
                case KeyCode.D:
                case KeyCode.RightArrow:
                    //move right
                    if (index % (root * root) == (root * root) - 1)
                    {
                        target = all_squares_[index - 8];
                    }
                    else
                    {
                        target = all_squares_[index + 1];
                    }
                    break;
                case KeyCode.W:
                case KeyCode.UpArrow:
                    //move up
                    if (index / (root * root) == 0)
                    {
                        target = all_squares_[index + (8 * root * root)];
                    }
                    else
                    {
                        target = all_squares_[index - (root * root)];
                    }
                    break;
                case KeyCode.S:
                case KeyCode.DownArrow:
                    //move down
                    if (index / (root * root) == (root * root) - 1)
                    {
                        target = all_squares_[index - (8 * root * root)];
                    }
                    else
                    {
                        target = all_squares_[index + (root * root)];
                    }
                    break;
                default:
                    break;
            }
            if (ev.shift || ev.control)
            {
                SelectSquare(target);
            }
            else
            {
                ReSelectSquare(target);
            }
        }
    }

    //Called by gridSquare whenever a number is changed
    public static void NumberChanged(GridSquare gs)
    {
        int index = all_squares_.IndexOf(gs);
        switch (PlayerPrefs.GetInt(pref_keys.error_highlighting.ToString()))
        {
            default:
            case 0:
                //No error highlighting
                break;
            case 1:
                //Highlight obvious logical errors
                int row = index / length;
                int col = index % length;
                //PICK UP HERE - Formula to get all gridsquares in the same nonnet
                int squarestack = (index / root) % root;
                int squareband = index / (length * root);
                List<GridSquare> same_row_ = new List<GridSquare>();
                List<GridSquare> same_col_ = new List<GridSquare>();
                List<GridSquare> same_square_ = new List<GridSquare>();
                List<GridSquare> error_row_ = new List<GridSquare>();
                List<GridSquare> error_col_ = new List<GridSquare>();
                List<GridSquare> error_square_ = new List<GridSquare>();
                for (int i = 0; i < length; i++)
                {
                    //HashSets to check for duplicates
                    var row_keys = new HashSet<int>();
                    var col_keys = new HashSet<int>();
                    var square_keys = new HashSet<int>();
                    //Get all in same row
                    same_row_.Add(all_squares_[i + row * length]);
                    //Get all in same col
                    same_col_.Add(all_squares_[i * length + col]);
                    //Get all in same nonnet
                    same_square_.Add(all_squares_[(squarestack * root + squareband * root * length) + (i % root) + (length * (i / root))]);
                    //Checking for duplicates in row, column and square
                    //Row check
                    if (!row_keys.Add(same_row_[same_row_.Count - 1].Number_))
                    {
                        if (same_row_[same_row_.Count - 1].Number_ > 0)
                        {
                            foreach (GridSquare gridSquare in same_row_.Where(x => x.Number_ == same_row_[same_row_.Count - 1].Number_))
                            {
                                error_row_.Add(gridSquare);
                                same_row_.Remove(gridSquare);
                            }
                        }
                    }
                    //Column check
                    if (!col_keys.Add(same_col_[same_col_.Count - 1].Number_))
                    {
                        if (same_col_[same_col_.Count - 1].Number_ > 0)
                        {
                            foreach (GridSquare gridSquare in same_col_.Where(x => x.Number_ == same_col_[same_col_.Count - 1].Number_))
                            {
                                error_col_.Add(gridSquare);
                                same_col_.Remove(gridSquare);
                            }
                        }
                    }
                    //Square Check
                    if (!square_keys.Add(same_square_[same_square_.Count - 1].Number_))
                    {
                        if (same_square_[same_square_.Count - 1].Number_ > 0)
                        {
                            foreach (GridSquare gridSquare in same_square_.Where(x => x.Number_ == same_square_[same_square_.Count - 1].Number_))
                            {
                                error_square_.Add(gridSquare);
                                same_square_.Remove(gridSquare);
                            }
                        }
                    }
                }
                Debug.Log($"There are {error_row_.Count} duplicates in the row, {error_col_.Count} duplicates in the column, and {error_square_.Count} duplicates in the square");

                break;
            case 2:
                //Highlight ALL error (will need solved sudoku for this)
                break;
        }
    }

    public static void CompilePlayer_prog()
    {
        string player_prog_numbers = null;
        string player_prog_centers = null;
        string player_prog_corners = null;
        foreach (GridSquare gs in all_squares_.Where(x => x.given == false).ToList())
        {
            player_prog_numbers += gs.Number_.ToString();
            if (gs.Center_marks_.Count != 0)
            {
                foreach (int i in gs.Center_marks_)
                {
                    player_prog_centers += i.ToString();
                }
                player_prog_centers += ",";
            }
            else
            {
                player_prog_centers += "0,";
            }
            if (gs.Corner_marks_.Count != 0)
            {
                foreach (int i in gs.Corner_marks_)
                {
                    player_prog_corners += i.ToString();
                }
                player_prog_corners += ",";
            }
            else
            {
                player_prog_corners += "0,";
            }
        }
        char[] trimChars = { ',' };
        DataPassing.sudoku_.Player_prog_numbers = player_prog_numbers;
        DataPassing.sudoku_.Player_prog_corners = player_prog_corners.TrimEnd(trimChars);
        DataPassing.sudoku_.Player_prog_centers = player_prog_centers.TrimEnd(trimChars);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("I just clicked" + eventData.selectedObject.name);
    }
}
