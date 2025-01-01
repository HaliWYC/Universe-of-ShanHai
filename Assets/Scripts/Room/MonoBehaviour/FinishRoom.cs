using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    public ObjectEventSO loadMapEvent;
    private void OnMouseDown() 
    {
        //返回房间
        loadMapEvent.RaiseEvent(null,this);
    }
}
