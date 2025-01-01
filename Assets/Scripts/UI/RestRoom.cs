using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RestRoom : MonoBehaviour
{
    private VisualElement rootElement;
    private Button restButton, backToMapButton;

    public Effect restEffect;

    public ObjectEventSO loadMapEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        restButton = rootElement.Q<Button>("RestButton");
        backToMapButton = rootElement.Q<Button>("BackToMapButton");

        restButton.clicked += Rest;
        backToMapButton.clicked += BackToMap;
    }

    private void Rest()
    {
        restEffect.Setup(GameManager.Instance.player, null);
        restButton.SetEnabled(false);
    }
    private void BackToMap()
    {
        loadMapEvent.RaiseEvent(null, this);
    }


}
