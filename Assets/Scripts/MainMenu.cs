using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Options;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MetaScene", LoadSceneMode.Additive);
        //Player prefs
        if (PlayerPrefs.GetString(pref_keys.c_tile_default_str.ToString()) == "")
        {
            //If no playerprefs are found, default ones are added
            PlayerPrefs.SetString(pref_keys.c_tile_default_str.ToString(), "#FFFFFF");
            PlayerPrefs.SetString(pref_keys.c_tile_highlighted_str.ToString(), "#EDF50C");
            PlayerPrefs.SetString(pref_keys.c_text_given_str.ToString(), "#000000");
            PlayerPrefs.SetString(pref_keys.c_text_input_str.ToString(), "#1A67EB");
            PlayerPrefs.SetString(pref_keys.c_error_str.ToString(), "FF0000");
            //Following ints do not need to be set as they default to 0:
            //error_highlighting
            //level_page - all of them
            //selected difficulty
        }

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
