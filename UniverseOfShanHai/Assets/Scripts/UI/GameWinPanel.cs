using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement rootElement;

    private Button pickCardButton;
    private Button backToMapButton;

    public ObjectEventSO loadMapEvent;
    public ObjectEventSO pickCardEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;

        pickCardButton = rootElement.Q<Button>("pickCardButton");
        backToMapButton = rootElement.Q<Button>("backToMapButton");
        backToMapButton.clicked += OnBackToMapButtonClicked;
        pickCardButton.clicked += OnPickCardButtonClicked;
        if (!GuidanceManager.Instance.isVictoryChecked)
            StartCoroutine(GuidanceManager.Instance.VictoryGuidance(5));
        if (!GuidanceManager.Instance.isChooseCardChecked)
        {
            backToMapButton.style.display = DisplayStyle.None;
        }
        else
        {
            backToMapButton.style.display = DisplayStyle.Flex;
        }
    }

    private void OnPickCardButtonClicked()
    {
        pickCardEvent.RaiseEvent(null, this);
        if (!GuidanceManager.Instance.isChooseCardChecked)
        {
            StartCoroutine(GuidanceManager.Instance.ChooseCardGuidance(10));
            backToMapButton.style.display = DisplayStyle.Flex;
        }
    }

    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);
    }

    public void OnFinishPickCardEvent()
    {
        pickCardButton.style.display = DisplayStyle.None;
    }
}
