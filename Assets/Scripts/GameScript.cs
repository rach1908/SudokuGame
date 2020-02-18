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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            KeyboardController(e);
        }
        

    }
    private void KeyboardController(Event ev)
    {
        KeyCode kc = ev.keyCode;
        switch (kc)
        {
            case KeyCode.None:
                break;
            case KeyCode.Backspace:
                Grid.SetGridNumber(0);
                break;
            case KeyCode.Delete:
                Grid.SetGridNumber(0);
                break;
            case KeyCode.Clear:
                break;
            case KeyCode.Return:
                break;
            case KeyCode.Escape:
                break;
            case KeyCode.Space:
                break;
            case KeyCode.Keypad0:
                Grid.SetGridNumber(0);
                break;
            case KeyCode.Keypad1:
                Grid.SetGridNumber(1);
                break;
            case KeyCode.Keypad2:
                Grid.SetGridNumber(2);
                break;
            case KeyCode.Keypad3:
                Grid.SetGridNumber(3);
                break;
            case KeyCode.Keypad4:
                Grid.SetGridNumber(4);
                break;
            case KeyCode.Keypad5:
                Grid.SetGridNumber(5);
                break;
            case KeyCode.Keypad6:
                Grid.SetGridNumber(6);
                break;
            case KeyCode.Keypad7:
                Grid.SetGridNumber(7);
                break;
            case KeyCode.Keypad8:
                Grid.SetGridNumber(8);
                break;
            case KeyCode.Keypad9:
                Grid.SetGridNumber(9);
                break;
            case KeyCode.UpArrow:
            case KeyCode.W:
            case KeyCode.DownArrow:
            case KeyCode.S:
            case KeyCode.RightArrow:
            case KeyCode.D:
            case KeyCode.LeftArrow:
            case KeyCode.A:
                //Call to static Grid.cs method to add an adjacent square to the selection list
                Grid.SelectAdjacent(kc);
                break;
            case KeyCode.Alpha0:
                Grid.SetGridNumber(0);
                break;
            case KeyCode.Alpha1:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(1);
                }
                else
                {
                    Grid.SetGridNumber(1);
                }
                break;
            case KeyCode.Alpha2:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(2);
                }
                else
                {
                    Grid.SetGridNumber(2);
                }
                break;
            case KeyCode.Alpha3:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(3);
                }
                else
                {
                    Grid.SetGridNumber(3);
                }
                break;
            case KeyCode.Alpha4:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(4);
                }
                else
                {
                    Grid.SetGridNumber(4);
                }
                break;
            case KeyCode.Alpha5:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(5);
                }
                else
                {
                    Grid.SetGridNumber(5);
                }
                break;
            case KeyCode.Alpha6:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(6);
                }
                else
                {
                    Grid.SetGridNumber(6);
                }
                break;
            case KeyCode.Alpha7:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(7);
                }
                else
                {
                    Grid.SetGridNumber(7);
                }
                break;
            case KeyCode.Alpha8:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(8);
                }
                else
                {
                    Grid.SetGridNumber(8);
                }
                break;
            case KeyCode.Alpha9:
                if (ev.control || ev.shift)
                {
                    Grid.SetGridCenterMark(9);
                }
                else
                {
                    Grid.SetGridNumber(9);
                }
                break;
            case KeyCode.B:
                Debug.Log("B");
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
