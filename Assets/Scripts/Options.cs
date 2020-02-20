using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject color_button;
    public GameObject error_button;
    //Will need to be updated to load the playerpref once that is a thing
    private error_Highlighting current_error = error_Highlighting.Never;
    private enum error_Highlighting
    {
        Never,
        Duplicates,
        All
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CycleError()
    {
        //Not working atm
        var valueList = Enum.GetValues(typeof(error_Highlighting)).Cast<error_Highlighting>().ToList();
        int j = valueList.IndexOf(current_error) + 1;
        current_error = valueList.Count == j ? (error_Highlighting)0 : (error_Highlighting)j;
        error_button.GetComponentInChildren<Text>().text = current_error.ToString();
    }
}
