using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinionManager : MonoBehaviour {

    public static MinionManager Instance { get; private set; }

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
    public int minionsToLevel = 2;

    [SerializeField]
    private GameObject defaultMinionPrefab;

    [SerializeField]
    private GameObject thinMinionPrefab;

    [SerializeField]
    private GameObject fatMinionPrefab;

    private List<Minion> minions = new();
    private List<GameObject> minionsInstantiated = new List<GameObject> { };

    private void ResetStates()
    {
        minionsInstantiated.Clear();
        minions.Clear();
    }

    private void OnEnable()
    {
        LevelManager.ResetGameAction += ResetStates;
    }

    private void OnDisable()
    {
        LevelManager.ResetGameAction -= ResetStates;
    }

    public void InstanceAllMinion(Transform parent)
    {
        Minion tmpGameObject;
        for(int i = 0; i < minionsToLevel; i++)
        {
            tmpGameObject = new Minion(EMinionState.DEFAULT, defaultMinionPrefab, i);
            minions.Add(tmpGameObject);
            StartCoroutine(WaitToCreate(i, tmpGameObject, parent, i * 2f));
        }
    }

    private IEnumerator WaitToCreate(int index, Minion mn, Transform parent, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject obj = Instantiate(mn.prefab, parent);
        minionsInstantiated.Add(obj);
        MaterialController _mc = obj.GetComponentInChildren<MaterialController>();
        PlayerController _pc = obj.GetComponent<PlayerController>();
        _pc.SetMinionType(mn.kindOf);
        _mc.IndexActive = index;
    }

    public List<Minion> GetMinionList()
    {
        return minions;
    }

    public List<GameObject> GetMinionsIntantiatedList()
    {
        return minionsInstantiated;
    }

    public void UpdateMinionState(string id)
    {
        Minion min = minions.Find(x => x.id == id);
        int indexOf = minions.FindIndex(x => x.id == id);
        if (indexOf < -1) return;

        GameObject obj = minionsInstantiated[indexOf];
        Animator anim = obj.GetComponentInChildren<Animator>();
        EternalRightWalk ewr = obj.GetComponentInChildren<EternalRightWalk>();

        switch (min.kindOf)
        {
            case EMinionState.DEFAULT:
                anim.SetInteger("selectedMinion", 1);
                ewr.SetForce(2f);
                min.kindOf = EMinionState.FAT;
                break;
            case EMinionState.FAT:
                anim.SetInteger("selectedMinion", 2);
                ewr.SetForce(0.5f);
                min.kindOf = EMinionState.THIN;
                break;
            case EMinionState.THIN:
                anim.SetInteger("selectedMinion", 0);
                ewr.SetForce(4f);
                min.kindOf = EMinionState.DEFAULT;
                break;
            default:
                anim.SetInteger("selectedMinion", 0);
                ewr.SetForce(4f);
                min.kindOf = EMinionState.DEFAULT;
                break;
        }
    }

    public void SetMinionsLevel(int qtdMinions)
    {
        minionsToLevel = qtdMinions;
    }
}
