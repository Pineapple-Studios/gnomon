using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGameActions : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField]
    private GameObject _successPanel;

    [Header("Minion related")]
    [SerializeField]
    private Transform _parentElement;
    [SerializeField]
    private GameObject _PrefabBtnMinion;

    private List<Minion> _minions;
    private List<Material> _materials;
    private List<Color> _colors = new();
    private int _instancesCount = 0;

    private void ResetStates()
    {
        DestroyAllButtons();
        _colors.Clear();
        _instancesCount = 0;
    }

    private void OnEnable()
    {
        PlayerController.GetTheGoal += TriggerEndGame; 
        LevelManager.ResetGameAction += ResetStates;
    }

    private void OnDisable()
    {
        PlayerController.GetTheGoal -= TriggerEndGame; 
        LevelManager.ResetGameAction -= ResetStates;
    }

    private void Start()
    {
        _successPanel.SetActive(false);
    }

    private void Update()
    {
        if (MinionManager.Instance.GetMinionList().Count > _instancesCount && _colors.Count == 0)
        {
            _minions = MinionManager.Instance.GetMinionList();
            _materials = _minions[0].prefab.GetComponentInChildren<MaterialController>().Materials;
            SaveColors(_materials);
            InstanceButtons();
        }
    }

    private void InstanceButtons()
    {
        for (int i = 0; i < MinionManager.Instance.minionsToLevel; i++)
        {
            GameObject obj = Instantiate(_PrefabBtnMinion, _parentElement);
            MinionButton mb = obj.GetComponent<MinionButton>();
            mb.SetMinion(MinionManager.Instance.GetMinionList()[i]);
            obj.GetComponentInChildren<Image>().color =  new Color(_colors[i].r, _colors[i].g, _colors[i].b, 1);
            _instancesCount++;
        }
    }

    private void SaveColors(List<Material> materials)
    {
        foreach (Material material in materials)
        {
            _colors.Add(material.GetColor("_Color"));
        }
    }

    private void TriggerEndGame()
    {
        Time.timeScale = 0f;
        _successPanel.SetActive(true);
    }

    private void DestroyAllButtons()
    {
        foreach(Transform tChild in _parentElement.GetComponentInChildren<Transform>())
        {
            Destroy(tChild.gameObject);
        }
    }
}
