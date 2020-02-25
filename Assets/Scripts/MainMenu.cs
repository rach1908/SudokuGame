using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using static Options;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Player prefs
        if (PlayerPrefs.GetString(pref_keys.c_tile_default.ToString()) == "")
        {
            //If no playerprefs are found, default ones are added
            PlayerPrefs.SetString(pref_keys.c_tile_default.ToString(), "#FFFFFF");
            PlayerPrefs.SetString(pref_keys.c_tile_highlighted.ToString(), "#EDF50C");
            PlayerPrefs.SetString(pref_keys.c_text_given.ToString(), "#000000");
            PlayerPrefs.SetString(pref_keys.c_text_input.ToString(), "#1A67EB");
            PlayerPrefs.SetInt(pref_keys.error_highlighting.ToString(), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
