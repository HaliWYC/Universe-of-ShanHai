using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public CharacterBase currentCharacter;
    public VisualTreeAsset intentTemplate;
    [Header("Elements")]
    public Transform healthBarTransform;
    private UIDocument healthBarDocument;

    private ProgressBar healthBar;

    private VisualElement buffContainer;
    private VisualElement enemyIntentContainer;

    [Header("Sprite")]

    public Sprite shieldSprite;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void MoveToWorldPosition(VisualElement element, Vector3 worldPosition, Vector2 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPosition, size, Camera.main);
        element.transform.position = rect.position;
    }

    [ContextMenu("Get UI Position")]
    public void InitHealthBar()
    {
        healthBarDocument = GetComponent<UIDocument>();
        healthBar = healthBarDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        healthBar.highValue = currentCharacter.MaxHP;
        MoveToWorldPosition(healthBar, healthBarTransform.position, Vector2.zero);

        buffContainer = healthBar.Q<VisualElement>("BuffContainer");
        buffContainer.style.display = DisplayStyle.None;
        enemyIntentContainer = healthBar.Q<VisualElement>("EnemyIntentContainer");
        enemyIntentContainer.style.display = DisplayStyle.None;
    }

    public void UpdateHealthBar()
    {
        if (currentCharacter.isDead)
        {
            healthBar.style.display = DisplayStyle.None;
            return;
        }

        if (healthBar != null)
        {
            healthBar.title = $"{currentCharacter.CurrentHP}/{currentCharacter.MaxHP}";
            healthBar.value = currentCharacter.CurrentHP;
            healthBar.RemoveFromClassList("highHealth");
            healthBar.RemoveFromClassList("middleHealth");
            healthBar.RemoveFromClassList("lowHealth");

            var percentage = (float)currentCharacter.CurrentHP / (float)currentCharacter.MaxHP;
            if (percentage < 0.3f)
            {
                healthBar.AddToClassList("lowHealth");
            }
            else if (percentage < 0.6f)
            {
                healthBar.AddToClassList("middleHealth");
            }
            else
            {
                healthBar.AddToClassList("highHealth");
            }
        }
    }

    public void UpdateBuff()
    {
        if (buffContainer.childCount > 0)
        {
            for (int i = 0; i < buffContainer.childCount; i++)
            {
                buffContainer[0].RemoveFromHierarchy();
                i--;
            }
        }
        if (currentCharacter.shield.currentValue > 0)
        {
            var buff = intentTemplate.Instantiate();
            buffContainer.Add(buff);
            var buffIcon = buff.Q<VisualElement>("Intent");
            buffIcon.style.backgroundImage = new StyleBackground(shieldSprite);
            var buffText = buff.Q<Label>("IntentText");
            buffText.text = currentCharacter.shield.currentValue.ToString();
        }
        int numChild = buffContainer.childCount;
        for (int i = 0; i < currentCharacter.characterData.buffList.Count; i++)
        {
            var buff = intentTemplate.Instantiate();

            buff.style.right = new StyleLength(new Length((numChild + i) * -60, LengthUnit.Pixel));
            buffContainer.Add(buff);
            var Buff = buff.Q<VisualElement>("Intent");
            Buff.style.backgroundImage = new StyleBackground(currentCharacter.characterData.buffList[i].effectIcon);
            var buffText = buff.Q<Label>("IntentText");
            buffText.text = currentCharacter.characterData.buffList[i].effectDurationType == EffectDurationType.Sustainable ? currentCharacter.characterData.buffList[i].round.ToString() : currentCharacter.characterData.buffList[i].value.ToString();
        }
        buffContainer.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// 玩家回合开始之后
    /// </summary>

    public void UpdateIntentElement()
    {
        if (enemyIntentContainer.childCount > 0)
        {
            for (int i = 0; i < enemyIntentContainer.childCount; i++)
            {
                enemyIntentContainer[0].RemoveFromHierarchy();
                i--;
            }
        }
        for (int i = 0; i < enemy.currentTurnAction.Count; i++)
        {
            var intent = intentTemplate.Instantiate();
            enemyIntentContainer.Add(intent);
            var Intent = intent.Q<VisualElement>("Intent");
            Intent.style.backgroundImage = new StyleBackground(enemy.currentTurnAction[i].effect.effectIcon);
            var intentText = intent.Q<Label>("IntentText");
            var value = enemy.currentTurnAction[i].effect.value;
            value = enemy.currentTurnAction[i].effect switch
            {
                DamageEffect => math.round(value * enemy.characterData.currentAttackMultiplier),
                DefenseEffect => math.round(value * enemy.characterData.currentDefenseMultiplier),
                _ => value
            };
            if (enemy.currentTurnAction[i].effect.round != 1)
                intentText.text = value.ToString() + "x" + enemy.currentTurnAction[i].effect.round.ToString();
            else
                intentText.text = value.ToString();
            intent.style.right = new StyleLength(new Length(-i * 60, LengthUnit.Pixel));
        }
        enemyIntentContainer.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// 敌人回合结束之后
    /// </summary>
    public void HideIntentElement()
    {
        enemyIntentContainer.style.display = DisplayStyle.None;
    }
}
