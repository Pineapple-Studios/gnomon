using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnReset : MonoBehaviour
{
    public void OnResetGame()
    {
        LevelManager.Instance.OnResetGame();
    }
}
