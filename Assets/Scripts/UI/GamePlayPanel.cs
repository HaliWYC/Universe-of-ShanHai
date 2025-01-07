using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : Singleton<GamePlayPanel>
{
    public ObjectEventSO playerTurnEndEvent;
    private VisualElement rootElement;
    private Label energyAmountLabel, drawAmountLabel, discardAmountLabel;
    public UnityEngine.UI.Button endTurnButton;

    public GameObject popTextPrefab;

    protected override void Awake()
    {
        base.Awake();
        endTurnButton.onClick.AddListener(OnEndTurnButtonClick);
    }
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");
        drawAmountLabel = rootElement.Q<Label>("DrawAmount");
        discardAmountLabel = rootElement.Q<Label>("DiscardAmount");

        endTurnButton.gameObject.SetActive(true);
        energyAmountLabel.text = "0";
        drawAmountLabel.text = "0";
        discardAmountLabel.text = "0";
    }

    private void OnEndTurnButtonClick()
    {
        playerTurnEndEvent.RaiseEvent(null, this);
        RelicEvent.OnPlayerTurnEnd();
        if (!GuidanceManager.Instance.guidanceCheckList[15])
            StartCoroutine(GuidanceManager.Instance.SelfActionGuidance(4));
    }

    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();
    }

    public void UpdateDrawAmount(int amount)
    {
        drawAmountLabel.text = amount.ToString();
    }

    public void UpdateDiscardAmount(int amount)
    {
        discardAmountLabel.text = amount.ToString();
    }

    public void OnEnemyTurnBegin()
    {
        endTurnButton.interactable = false;
    }

    public void OnPlayerTurnBegin()
    {
        endTurnButton.interactable = true;
    }

    public void PopText(Vector3 position, float value, Effect effect)
    {
        PoppingUpDamageText Text = Instantiate(popTextPrefab, transform.GetComponent<RectTransform>()).GetComponent<PoppingUpDamageText>();
        Text.transform.position = Camera.main.WorldToScreenPoint(position + Vector3.up * 5);
        Text.SetUp(value, effect);
    }
}
