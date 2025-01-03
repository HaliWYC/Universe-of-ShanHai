using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CharacterBase : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    [Header("Property")]
    public CharacterDataSO templateCharacterData;
    public CharacterDataSO characterData;
    public IntVariable hp;
    public IntVariable shield;
    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); }
    public int MaxHP { get => hp.maxValue; }

    [Header("Bool")]
    public bool isDead;

    [Header("Component")]
    protected Animator animator;
    public float animatorSkillSpeed;
    public HealthBarController healthBarController;
    public VFXController vfxController;
    public ObjectEventSO characterDeadEvent;

    [Header("Audio")]
    public string damageSound;
    public string hurtSound;
    public string shieldSound;
    public string healSound;
    public string buffSound;
    public string debuffSound;


    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        healthBarController = GetComponent<HealthBarController>();
    }

    protected virtual void OnEnable()
    {
        healthBarController.InitHealthBar();
        healthBarController.UpdateHealthBar();
        ResetShield();
        characterData.buffList.Clear();
        healthBarController.UpdateBuff();
        if (characterData != null)
        {
            RefreshProperty();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        var currentDamage = (damage - shield.currentValue) >= 0 ? (damage - shield.currentValue) : 0;
        var currentShield = (damage - shield.currentValue) >= 0 ? 0 : (shield.currentValue - damage);
        shield.SetValue((int)currentShield);
        if (CurrentHP > currentDamage)
        {
            CurrentHP -= (int)currentDamage;
            animator.SetTrigger("hurt");
        }
        else
        {
            CurrentHP = 0;
            isDead = true;
            animator.SetBool("isDead", isDead);
            characterDeadEvent.RaiseEvent(this, this);
        }
        healthBarController.UpdateBuff();
    }

    public void UpdateShield(float amount)
    {
        shield.SetValue(shield.currentValue + (int)amount);
    }

    public void ResetShield()
    {
        shield.SetValue(0);
    }

    public void UpdateBuffList()
    {
        for (int i = 0; i < characterData.buffList.Count; i++)
        {
            switch (characterData.buffList[i].effectDurationType)
            {
                case EffectDurationType.Sustainable:
                    characterData.buffList[i].round--;
                    if (characterData.buffList[i].round <= 0)
                    {
                        characterData.buffList[i].End(this);
                        if (CompareTag("Enemy"))
                        {
                            GetComponent<Enemy>().healthBarController.UpdateIntentElement();
                        }
                        if (CompareTag("Player"))
                        {
                            CardDeck.Instance.UpdateCardUI();
                        }
                        characterData.buffList.RemoveAt(i);
                        i--;
                        break;
                    }
                    characterData.buffList[i].Execute(this);
                    if (CompareTag("Enemy"))
                    {
                        GetComponent<Enemy>().healthBarController.UpdateIntentElement();
                        break;
                    }
                    if (CompareTag("Player"))
                    {
                        CardDeck.Instance.UpdateCardUI();
                    }
                    break;
                case EffectDurationType.Permanent:
                    break;
            }
        }
    }

    public void RefreshProperty()
    {
        characterData.currentAttackMultiplier = characterData.baseAttackMultiplier;
        characterData.nextAttackIncrement = characterData.baseAttackIncrement;
        characterData.currentDefenseMultiplier = characterData.baseDefenseMultiplier;
        characterData.nextDefenseIncrement = characterData.baseDefenseIncrement;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && characterData.buffList.Count > 0)
        {
            UIPanel.Instance.ShowCharacterStateToolTip(this, transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIPanel.Instance.characterStateTip.gameObject.SetActive(false);
    }
}
