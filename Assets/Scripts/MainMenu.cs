using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Player prefs
        if (PlayerPrefs.GetString("c.tile_default") == "")
        {
            //If no playerprefs are found, default ones are added
            PlayerPrefs.SetString("c.tile_default", "#FFFFFF");
            PlayerPrefs.SetString("c.tile_highlighted", "#EDF50C");
            PlayerPrefs.SetString("c.text_given", "#000000");
            PlayerPrefs.SetString("c.text_input", "#1A67EB");
            PlayerPrefs.SetString("error_highlighting", "NEVER");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
