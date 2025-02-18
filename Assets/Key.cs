using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Key : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    public string Keyboard_key;
    public Button btn;
    public TMP_InputField _input;
    private bool Check;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        _input = GetComponentInParent<AttachInputField>().InputField;
      //  btn.onClick.AddListener(SetKey);
     
    }
    

    public void SetKey()
    {
     /*   if(Keyboard_key=="Back")
        {
            string TempText = _input.text;
            TempText = TempText.Remove(TempText.Length-1);
            Debug.Log(TempText + " Tempt text");
            _input.text = TempText;
        }
       else if (Keyboard_key == "Shift")
        {
            string TempText = _input.text;
            TempText = TempText.Remove(TempText.Length - 1);
            Debug.Log(TempText + " Tempt text");
            _input.text = TempText;
        }
        else
        {
            _input.text += Keyboard_key;
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Check)
        {

            if (Keyboard_key == "Back")
            {
                string TempText = _input.text;
                TempText = TempText.Remove(TempText.Length - 1);
                Debug.Log(TempText + " Tempt text");
                _input.text = TempText;
            }
            else if (Keyboard_key == "Shift")
            {
                string TempText = _input.text;
                TempText = TempText.Remove(TempText.Length - 1);
                Debug.Log(TempText + " Tempt text");
                _input.text = TempText;
            }
            else
            {
                _input.text += Keyboard_key;
            }
            Check = true;
           
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Check = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the flag if the pointer exits the button area while holding down the click
        Check = false;
        
    }
}
