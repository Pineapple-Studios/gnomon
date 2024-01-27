using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private AudioSource _as;

    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && _as.isPlaying)
        {
            _as.Stop();
        }

        if (SceneManager.GetActiveScene().name == "Level1" && !_as.isPlaying)
        {
            _as.Play();
        }
    }
}
