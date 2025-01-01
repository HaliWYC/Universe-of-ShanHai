using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    private Button backToStartButton;
    public ObjectEventSO loadMenuEvent;

    private void OnEnable() 
    {
        backToStartButton = GetComponent<UIDocument>().rootVisualElement.Q<Button>("BackToStartButton");
        backToStartButton.clicked += OnBackToStartButtonClicked;
    }

    private void OnBackToStartButtonClicked()
    {
        loadMenuEvent.RaiseEvent(null,this);
    }
}
