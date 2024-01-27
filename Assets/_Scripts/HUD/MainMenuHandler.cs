using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _showObj;
    [SerializeField]
    private List<GameObject> _hiddenList = new();

    public void GoIntro()
    {
        _showObj.SetActive(false);
        foreach(GameObject hObj in _hiddenList) hObj.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
