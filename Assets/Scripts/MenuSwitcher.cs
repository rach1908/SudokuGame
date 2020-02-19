using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public GameObject LevelSelectCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectThis()
    {
        uiCanvas.GetComponent<Canvas>().sortingLayerID = 5;
        uiCanvas.GetComponent<Canvas>().sortingOrder = 10;
    }
}
