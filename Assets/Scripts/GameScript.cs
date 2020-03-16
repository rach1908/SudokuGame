using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Grid.selected_squares_.Count > 0)
            {
                Grid.selected_squares_[Grid.selected_squares_.Count - 1].SetColor("HIGHLIGHTED");
            }
        }
        else
        {
            if (Grid.selected_squares_.Count > 0)
            {
                Grid.selected_squares_[Grid.selected_squares_.Count - 1].SetColor("SELECTED");
            }
        }
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            KeyboardController(e);
        }
        

    }
    private void KeyboardController(Event ev)
    {
        Grid.NumberPos np = Grid.NumberPos.Standard;
        if (ev.shift)
        {
            np = Grid.NumberPos.Corner;
        }
        if (ev.control)
        {
            np = Grid.NumberPos.Center;
        }
        KeyCode kc = ev.keyCode;
        switch (kc)
        {
            case KeyCode.None:
                break;
                //All 'delete' cases
            case KeyCode.Backspace:
            case KeyCode.Delete:
            case KeyCode.Keypad0:
            case KeyCode.Alpha0:
                Grid.SetGridNumber(0, Grid.NumberPos.Standard);
                break;
            case KeyCode.Clear:
                break;
            case KeyCode.Return:
                break;
            case KeyCode.Escape:
                break;
            case KeyCode.Space:
                break;
                //All directional cases
            case KeyCode.UpArrow:
            case KeyCode.W:
            case KeyCode.DownArrow:
            case KeyCode.S:
            case KeyCode.RightArrow:
            case KeyCode.D:
            case KeyCode.LeftArrow:
            case KeyCode.A:
                //Call to static Grid.cs method to add an adjacent square to the selection list
                Grid.SelectAdjacent(ev);
                break;
                //All cases from 1-9
            case KeyCode.Keypad1:
            case KeyCode.Alpha1:
            case KeyCode.Keypad2:
            case KeyCode.Alpha2:
            case KeyCode.Keypad3:
            case KeyCode.Alpha3:
            case KeyCode.Keypad4:
            case KeyCode.Alpha4:
            case KeyCode.Keypad5:
            case KeyCode.Alpha5:
            case KeyCode.Keypad6:
            case KeyCode.Alpha6:
            case KeyCode.Keypad7:
            case KeyCode.Alpha7:
            case KeyCode.Keypad8:
            case KeyCode.Alpha8:
            case KeyCode.Keypad9:
            case KeyCode.Alpha9:
                //Don't look
                Grid.SetGridNumber(int.Parse(kc.ToString()[kc.ToString().Length -1].ToString()), np);
                break;
            case KeyCode.B:
                break;
            case KeyCode.C:
                break;
            case KeyCode.E:
                break;
            case KeyCode.F:
                break;
            case KeyCode.G:
                break;
            case KeyCode.H:
                break;
            case KeyCode.I:
                break;
            case KeyCode.J:
                break;
            case KeyCode.K:
                break;
            case KeyCode.L:
                break;
            case KeyCode.M:
                break;
            case KeyCode.N:
                break;
            case KeyCode.O:
                break;
            case KeyCode.P:
                break;
            case KeyCode.Q:
                break;
            case KeyCode.R:
                break;
            case KeyCode.T:
                break;
            case KeyCode.U:
                break;
            case KeyCode.V:
                break;
            case KeyCode.X:
                break;
            case KeyCode.Y:
                break;
            case KeyCode.Z:
                break;
            case KeyCode.RightShift:
                break;
            case KeyCode.LeftShift:
                break;
            case KeyCode.RightControl:
                break;
            case KeyCode.LeftControl:
                break;
            case KeyCode.RightAlt:
                break;
            case KeyCode.LeftAlt:
                break;
            case KeyCode.AltGr:
                break;
            //Do stuff
            case KeyCode.Mouse0:
                //clicky
                Event e = Event.current;

                break;
            case KeyCode.Mouse1:
                break;
            case KeyCode.Mouse2:
                break;
            case KeyCode.Mouse3:
                break;
            case KeyCode.Mouse4:
                break;
            case KeyCode.Mouse5:
                break;
            case KeyCode.Mouse6:
                break;
            default:
                break;
        }
    }

}
