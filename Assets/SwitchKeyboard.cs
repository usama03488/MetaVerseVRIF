using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchKeyboard : MonoBehaviour
{
    public Button btn;
    public GameObject KeyboardOn;
    public GameObject KeyboardOff;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeKeyboard);
    }
    public void ChangeKeyboard()
    {
        KeyboardOn.SetActive(true);
        KeyboardOff.SetActive(false);
    }
}
