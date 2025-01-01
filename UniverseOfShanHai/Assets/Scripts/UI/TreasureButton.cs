using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureButton : MonoBehaviour, IPointerDownHandler
{
    public ObjectEventSO GameWinEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        GameWinEvent.RaiseEvent(null,this);
    }
}
