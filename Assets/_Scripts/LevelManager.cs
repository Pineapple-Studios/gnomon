using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LevelManager : MonoBehaviour
{
    public static event Action ResetGameAction;

    public static LevelManager Instance { get; private set; }

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

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private int _nextLevel;

    private bool _intantiated = false;
    private Transform _savedStartPoint;
    private bool _mustSkipLevel = false;

    private void Start()
    {
        this.ResetGame();
        if (_savedStartPoint == null) _savedStartPoint = _startPoint;
    }

    private void Update()
    {
        if (_savedStartPoint == null)
        {
            GameObject startMinionsPoint = GameObject.Find("StartMinionsPoint");
            if (startMinionsPoint != null) _savedStartPoint = startMinionsPoint.transform;
        }

        if (_mustSkipLevel) return;

        GameObject levelConfig = GameObject.Find("LevelConfig");
        if (levelConfig == null) return;

        int lv = levelConfig.GetComponent<LevelConfig>().MinionQuantity;
        //Debug.Log($"Current value = {MinionManager.Instance.minionsToLevel}");
        //Debug.Log($"New value = {lv}");
        if (MinionManager.Instance.minionsToLevel != lv)
        {
            MinionManager.Instance.SetMinionsLevel(lv);
            _mustSkipLevel = true;
        }
    }

    void FixedUpdate()
    {
        if (MinionManager.Instance != null && !_intantiated && _mustSkipLevel)
        {
            //Debug.Log($"Instantiating value = {MinionManager.Instance.minionsToLevel}");
            MinionManager.Instance.InstanceAllMinion(_savedStartPoint);
            _intantiated = true;
        }
    }

    public void OnResetGame()
    {
        this.ResetGame(true);
    }

    public void ResetGame(bool isReseting = false)
    {
        List<GameObject> mins = MinionManager.Instance.GetMinionsIntantiatedList();
        foreach(GameObject min in mins)
        {
            Destroy(min);
        }
        _intantiated = false;

        ResetGameAction();
        if (!isReseting) _mustSkipLevel = false;
        Time.timeScale = 1f;
    }

    public void SetNextLevel(int next)
    {
        _nextLevel = next;
    }

    public int GetNextLevel()
    {
        return _nextLevel;
    }
}
