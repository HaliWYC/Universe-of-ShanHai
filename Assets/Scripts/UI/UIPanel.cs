using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class UIPanel : Singleton<UIPanel>
{
    public CardToolTip cardToolTip;
    public RelicToolTip relicToolTip;
    public CharacterStateTip characterStateTip;
    public RectTransform relicHolder;
    public GameObject relicPrefab;

    [Header("TMPro")]
    public TextMeshProUGUI currencyText;

    private void Start()
    {
        InitRelics();
    }


    public void InitRelics()
    {
        foreach (var relic in GameManager.Instance.player.characterData.relics)
        {
            var relicUI = Instantiate(relicPrefab, relicHolder).GetComponent<RelicUI>();
            relicUI.SetRelic(relic);
            relicUI.UpdateRelicValue(relic);
            relic.isEquipped = true;
        }
    }

    public void UpdateRelicsValue()
    {
        for (int i = 0; i < relicHolder.childCount; i++)
        {
            relicHolder.GetChild(i).GetComponent<RelicUI>().UpdateRelicValue(GameManager.Instance.player.characterData.relics[i]);
        }
    }

    public void UpdateCurrencyText()
    {
        if (GameManager.Instance.player.characterData != null)
            currencyText.text = GameManager.Instance.player.characterData.Money.ToString();
    }

    public void ShowCardToolTip(CardDataSO cardData, Transform card, bool isCardUI)
    {
        if (cardData != null)
        {

            if (!isCardUI)
            {
                cardToolTip.GetComponent<RectTransform>().pivot = new Vector2(-0.2f, 0f);
                cardToolTip.transform.position = Camera.main.WorldToScreenPoint(card.position) + Vector3.up * 50;
            }
            else
            {
                cardToolTip.GetComponent<RectTransform>().pivot = new Vector2(-0.3f, 0f);
                cardToolTip.transform.position = card.position;
            }

            cardToolTip.SetCardData(cardData);
        }
    }

    public void ShowRelicToolTip(RelicData relicData, Transform relicUI, bool isShop)
    {
        if (relicData != null)
        {
            if (!isShop)
            {
                relicToolTip.GetComponent<RectTransform>().pivot = new Vector2(-0.1f, 1.2f);
                relicToolTip.transform.position = relicUI.position + Vector3.up * 50;
            }
            else
            {
                relicToolTip.GetComponent<RectTransform>().pivot = new Vector2(1f, 0f);
                relicToolTip.transform.position = relicUI.position + Vector3.left * 50;
            }
            relicToolTip.SetRelicData(relicData);
        }
    }

    public void ShowCharacterStateToolTip(CharacterBase character, Transform characterUI)
    {
        if (character.CompareTag("Player"))
        {
            characterStateTip.GetComponent<RectTransform>().pivot = new Vector2(-0.2f, 0f);
            characterStateTip.transform.position = Camera.main.WorldToScreenPoint(characterUI.position) + Vector3.up * 50;
            characterStateTip.SetCharacterStateTip(character.characterData);
            return;
        }
        if (character.CompareTag("Enemy"))
        {
            characterStateTip.GetComponent<RectTransform>().pivot = new Vector2(1.3f, 0f);
            characterStateTip.transform.position = Camera.main.WorldToScreenPoint(characterUI.position) + Vector3.up * 50;
            characterStateTip.SetCharacterStateTip(character.characterData);
        }
    }
}
