using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Options;

public class Grid : MonoBehaviour
{
    //Determines the side lenght of the sudoku. Length must be a squared number, will be set ingame, drop down menu possibly (anything above 8^2 seems to slow down the game considerably, 13^2 almost made unity crash). Must be at least 4
    public int length = 0;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;

    private static int root = 0;
    private static List<GameObject> grid_squares_ = new List<GameObject>();
    public static List<GridSquare> selected_squares_ = new List<GridSquare>();
    public static List<GridSquare> all_squares_ = new List<GridSquare>();

    void Start()
    {
        root = int.Parse(Math.Sqrt(double.Parse(length.ToString())).ToString());
        CreateGrid();
        if (grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("GridSquare object needs to have GridSquare script attached");
        }
        foreach (GameObject game in grid_squares_)
        {
            all_squares_.Add(game.GetComponent<GridSquare>());
        }
        //This will recieve data from the Sudoku.current object
        FillGrid(Sudoku.current.sudoku_string, Sudoku.current.Player_prog_numbers, Sudoku.current.Player_prog_corners, Sudoku.current.Player_prog_centers);
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
        SpawnGridSquares();
        SetSquarePosition();

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
                empties[i].SetNumber(int.Parse(player_prog_numbers[i].ToString()));
                if (centers[i].Length > 1)
                {
                    List<int> center_temp_ = new List<int>();
                    foreach (char c in centers[i])
                    {
                        center_temp_.Add(int.Parse(c.ToString()));
                    }
                    empties[i].Center_marks_ = center_temp_;
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
                player_prog_centers += ",";
            }
            else
            {
                player_prog_corners += "0,";
            }
        }

        Sudoku.current.Player_prog_numbers = player_prog_numbers;
        Sudoku.current.Player_prog_corners = player_prog_corners;
        Sudoku.current.Player_prog_centers = player_prog_centers;

    }
}
