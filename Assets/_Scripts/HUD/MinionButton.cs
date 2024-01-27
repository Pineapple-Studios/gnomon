using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinionButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _iconComponent;
    [SerializeField]
    private Sprite _default;
    [SerializeField]
    private Sprite _thin;
    [SerializeField]
    private Sprite _fat;



    private Minion _savedMinion = null;
    private EMinionState _state = EMinionState.DEFAULT;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_savedMinion == null) return;

        MinionManager.Instance.UpdateMinionState(_savedMinion.id);
        UpdateImage();
    }

    private void UpdateImage()
    {
        switch (_state) {
            case EMinionState.DEFAULT:
                _iconComponent.sprite = _thin;
                _state = EMinionState.THIN;
                break;
            case EMinionState.THIN:
                _iconComponent.sprite = _fat;
                _state = EMinionState.FAT;
                break;
            case EMinionState.FAT:
                _iconComponent.sprite = _default;
                _state = EMinionState.DEFAULT;
                break;
            default:
                _iconComponent.sprite = _default;
                _state = EMinionState.DEFAULT;
                break;
        }
    }

    public void SetMinion(Minion minion)
    {
        _savedMinion = minion;
    }
}
