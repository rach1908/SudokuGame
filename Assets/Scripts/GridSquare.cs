using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public GameObject number_text;
    public GameObject TileImage;
    public GameObject corner_text;
    public Sprite one_edge;
    public Sprite two_edge;
    public Sprite no_edge;
    private int number_ = 0;
    public bool given = false;
    private List<int> center_marks_ = new List<int>();
    private List<int> corner_marks_ = new List<int>();
    private string DefaultHex = "#ffffff";
    private string HighlightHex = "#0000ff";
    private string SelectedHex = "#ff0000";
    void Start()
    {
        Grid.all_squares_.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayText()
    {
        if (given == false)
        {
            if (number_ <= 0)
            {
                number_text.GetComponent<Text>().text = " ";
                corner_text.GetComponent<Text>().text = " ";
                //Sorting and displaying center marks
                if (center_marks_.Count > 0)
                {
                    number_text.GetComponent<Text>().fontSize = 30;
                    foreach (int i in center_marks_)
                    {
                        number_text.GetComponent<Text>().text += i.ToString();
                    }
                }
                //Sorting and displaying center marks
                if (corner_marks_.Count > 0)
                {
                    foreach (int i in corner_marks_)
                    {
                        corner_text.GetComponent<Text>().text += i.ToString();
                    }
                }
            }
            else
            {
                number_text.GetComponent<Text>().text = number_.ToString();
                number_text.GetComponent<Text>().fontSize = 75;
                corner_text.GetComponent<Text>().text = " ";
            }
        }
    }

    public void ColorTheme(string Default, string Selected, string Highlight)
    {
        DefaultHex = Default;
        SelectedHex = Selected;
        HighlightHex = Highlight;
    }

    public void SetNumber(int number)
    {
        if (given == false)
        {
            if (number_ == 0 && number == 0)
            {
                center_marks_ = new List<int>();
                corner_marks_ = new List<int>();
            }
            number_ = number;
            DisplayText();
        }
    }

    public void ToggleCenterMark(int number)
    {
        if (given == false)
        {
            if (center_marks_.Contains(number))
            {
                center_marks_.Remove(number);
            }
            else
            {
                center_marks_.Add(number);
                center_marks_.Sort();
            }
            DisplayText();
        }
    }
    //NOT WORKING
    public void ToggleCornerMark(int number)
    {
        if (given == false)
        {
            if (corner_marks_.Contains(number))
            {
                corner_marks_.Remove(number);
            }
            else
            {
                corner_marks_.Add(number);
                corner_marks_.Sort();
            }
            DisplayText();
        }
    }

    public void SetColor(string shade)
    {
        Color color = Color.black;
        switch (shade.ToUpper())
        {
            case "DEFAULT":
                ColorUtility.TryParseHtmlString(DefaultHex, out color);
                break;
            case "SELECTED":
                ColorUtility.TryParseHtmlString(SelectedHex, out color);
                break;
            case "HIGHLIGHTED":
                ColorUtility.TryParseHtmlString(HighlightHex, out color);
                break;
            default:
                break;
        }
        TileImage.GetComponent<Image>().color = color;

    }

    //Only called while the grid is generated, the method calling this is responsible for the logic

    public void SetImage(int edges, int rotations)
    {
        Image img = TileImage.GetComponent<Image>();
        Sprite s = no_edge;
        switch (edges)
        {
            case 1:
                s = one_edge;
                break;
            case 2:
                s = two_edge;
                break;
            case 0:
                s = no_edge;
                break;
            default:
                break;
        }
        img.sprite = s;
        img.transform.rotation = Quaternion.AngleAxis(90 * rotations, Vector3.back);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Grid.SelectSquare(this);
        }
        else
        {
            Grid.ReSelectSquare(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Grid.SelectSquare(this);
        }
    }
}
