using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardReferenceAssign : MonoBehaviour
{

    public AttachInputField _attachImputField;
    public TMP_Text error;
    public void AttachKeyboard(GameObject keyboardInput)
    {
        _attachImputField.InputField = keyboardInput.GetComponent<TMP_InputField>();
        foreach(var _key in GetComponentsInChildren<Key>())
        {
            _key._input = _attachImputField.InputField;
        }
    }

    public void ShowName(GameObject name)
    {
     //   error.text = name.name;
    }


}
