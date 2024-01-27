using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Minion
{
    public string id;
    public int key;
    public GameObject prefab;
    public EMinionState kindOf;
    public EAnimationState animation;

    public Minion(EMinionState _kindOf, GameObject prefab, int key)
    {
        this.key = key;
        this.id = Guid.NewGuid().ToString();
        this.kindOf = _kindOf;
        this.animation = EAnimationState.IDLE;
        this.prefab = prefab;
    }

}

public enum EMinionState
{
    DEFAULT,
    THIN,
    FAT
}
public enum EAnimationState
{
    IDLE,
    DEAD,
    WALK,
    FLOATING
}