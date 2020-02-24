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
    //Will need to be updated to load the playerpref once that is a thing
    private error_Highlighting current_error;
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
        Grey
    }
    // Start is called before the first frame update
    void Start()
    {
        current_error = error_Highlighting.Never;
        color_button01.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Yellow); });
        color_button02.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Blue); });
        color_button03.GetComponent<Button>().onClick.AddListener(delegate { ColorButtons(color_Themes.Grey); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleError()
    {
        List<Options.error_Highlighting> valueList = Enum.GetValues(typeof(error_Highlighting)).Cast<Options.error_Highlighting>().ToList();
        int j = valueList.IndexOf(current_error) + 1;
        current_error = valueList.Count == j ? (error_Highlighting)0 : (error_Highlighting)j;
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
        //Need to add player pref?
    }

    private void ColorButtons(color_Themes color)
    {
        switch (color)
        {
            case color_Themes.Yellow:
                PlayerPrefs.SetString("")
                break;
            case color_Themes.Blue:
                break;
            case color_Themes.Grey:
                break;
            default:
                break;
        }
    }
}
