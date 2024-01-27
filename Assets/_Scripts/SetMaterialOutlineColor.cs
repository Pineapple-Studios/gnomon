using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialOutlineColor : MonoBehaviour
{
    private MaterialPropertyBlock m_PropertyBlock;
    private SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(myRenderer.material);
        myRenderer.material.SetColor("Color", Color.red);
        //Debug.Log();
    }
}
