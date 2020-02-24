using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject color_button01;
    public GameObject color_button02;
    public GameObject color_button03;
    public GameObject error_button;
    private error_Highlighting current_error;
    private color_Themes current_theme;
    private enum error_Highlighting
    {
        Never,
        Duplicates,
        All
    }
    private enum color_Themes
    {
        Yellow,
        Blue,
        Green
    }
    //All playerPrefs keys - be sure to call playerprefs using this enum to avoid spelling errors
    public enum pref_keys
    {
        c_tile_default,
        c_tile_highlighted,
        c_text_given,
        c_text_input,
        error_highlighting
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetString(pref_keys.c_tile_highlighted.ToString()))
        {
            case "":
                break;

            default:
                break;
        }
        current_error = (error_Highlighting)PlayerPrefs.GetInt(pref_keys.error_highlighting.ToString());
        color_button01.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Yellow); });
        color_button02.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Blue); });
        color_button03.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Green); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleError()
    {
        List<Options.error_Highlighting> valueList = Enum.GetValues(typeof(error_Highlighting)).Cast<Options.error_Highlighting>().ToList();
        int j = valueList.IndexOf(current_error) + 1;
        j = valueList.Count == j ? 0 : j;
        current_error = (error_Highlighting)j;
        PlayerPrefs.SetInt(pref_keys.error_highlighting.ToString(), j);
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
    }

    private void ColorButtons(color_Themes color)
    {
        string col = "";
        switch (color)
        {
            default:
            case color_Themes.Yellow:
                col = "#EDF50C";
                color_button01.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Blue:
                col = "#17A4EB";
                color_button02.GetComponent<Button>().interactable = false;
                break;
            case color_Themes.Green:
                col= "#4BC96E";
                color_button03.GetComponent<Button>().interactable = false;
                break;

        }
        PlayerPrefs.SetString(pref_keys.c_tile_default.ToString(), col);
    }
}
