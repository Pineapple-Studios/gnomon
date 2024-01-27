using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnNextLevel : MonoBehaviour
{
    public void GoToNextLevel()
    {
        LevelManager inst = LevelManager.Instance;

        int tmpLevel = inst.GetNextLevel();
        inst.SetNextLevel(tmpLevel + 1);
        if (SceneManager.GetActiveScene().name != "Level5") SceneManager.LoadScene($"Level{tmpLevel}");
        else SceneManager.LoadScene("MainMenu");
        inst.ResetGame();
    }
}
