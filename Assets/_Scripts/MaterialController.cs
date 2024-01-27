using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] 
    public List<Material> Materials;
    [SerializeField] 
    public int IndexActive = 0;

    private int _previousIndex = 0;
    private SpriteRenderer _sr;


    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.material = Materials[IndexActive];
    }

    private void Update()
    {
        if (IndexActive != _previousIndex)
        {
            _sr.material = Materials[IndexActive];
            _previousIndex = IndexActive;
        }    
    }
}
