using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShoot : MonoBehaviour
{
    private void OnDisable()
    {
        AsteriodGameManager.instance.GameOver();
    }
}
