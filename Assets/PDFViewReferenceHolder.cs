
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Michsky.MUIP;

public class PDFViewReferenceHolder : MonoBehaviour
{
    public Button Previous;
    public Button Next;
    public Button LoadDoc;
    public Button PasteBtn;
    public ButtonManager pasteButton;
    public ButtonManager LoadButton;
    public Button ZoomIn;
    public Button ZoomOut;
  
    public TMP_InputField Input;

    public void ScrollViewPosition( )
    {
        foreach (var player in FindObjectsOfType<Authentication>())
        {
            player.ScrollViewSync();
        }
        // Debug.Log(pos + " this is position ");
    }

 

}
