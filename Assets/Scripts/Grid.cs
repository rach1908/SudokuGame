﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    //Determines the side lenght of the sudoku. Length must be a squared number, will be set ingame, drop down menu possibly (anything above 8^2 seems to slow down the game considerably, 13^2 almost made unity crash). Must be at least 4
    public int length = 0;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;

    private static List<GameObject> grid_squares_ = new List<GameObject>();
    public static List<GridSquare> selected_squares_ = new List<GridSquare>();
    public static List<GridSquare> all_squares_ = new List<GridSquare>();

    void Start()
    {
        CreateGrid();
        if (grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("GridSquare object needs to have GridSquare script attached");
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

     
        //literally wtf
        int block_length = int.Parse(Math.Sqrt(double.Parse(length.ToString())).ToString());
        
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
                if (row % block_length == 0) 
                {
                    rotation += 1;
                }
                //Below the Square
                if (row % block_length == block_length -1)
                {
                    rotation += 100;
                }

                //Only one of these two statements can ever trigger, unless the length is too small
                //Left of the Square
                if (column % block_length == 0)
                {
                    rotation += 1000;
                }
                //Right of the Square
                if (column % block_length == block_length - 1)
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

    public static void SetGridNumber(int number)
    {
        if (selected_squares_.Count > 0)
        {
            foreach (GridSquare square in selected_squares_)
            {
                square.SetNumber(number);
            }
        }
    }

    public static void SetGridCenterMark(int number)
    {
        if (selected_squares_.Count > 0)
        {
            foreach (GridSquare square in selected_squares_)
            {
                square.ToggleCenterMark(number);
            }
        }
    }

    public static void SetGridCornerMark(int number)
    {
        if (selected_squares_.Count > 0)
        {
            foreach (GridSquare square in selected_squares_)
            {
                square.ToggleCornerMark(number);
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
    public static void RemoveSelected()
    {
        selected_squares_ = new List<GridSquare>();
    }

    public static void SelectAdjacent(KeyCode kc)
    {
        switch (kc)
        {
            case KeyCode.A:
            case KeyCode.LeftArrow:
                //move left
                if (selected_squares_.Count > 0)
                {
                                
                }
                break;
            case KeyCode.D:
            case KeyCode.RightArrow:
                //move right
                break;
            case KeyCode.W:
            case KeyCode.UpArrow:
                //move up
                break;
            case KeyCode.S:
            case KeyCode.DownArrow:
                //move down
                break;
            default:
                break;
        }
    }
}
