using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSensitivityController : MonoBehaviour
{
    public ScrollRect _ScrollRect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResetScrollViewSensitivity());
    }
    IEnumerator ResetScrollViewSensitivity()
    {
        yield return new WaitForSeconds(2f);
        _ScrollRect.scrollSensitivity = 0;
        StartCoroutine(ResetScrollViewSensitivity());
    }
}
