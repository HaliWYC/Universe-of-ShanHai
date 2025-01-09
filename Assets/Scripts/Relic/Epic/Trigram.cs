using UnityEngine;

[CreateAssetMenu(fileName = "Trigram", menuName = "Relic/Epic/Trigram")]
public class Trigram : RelicData
{
    public override void OnAfterCharacterDead(CharacterBase character)
    {

    }

    public override void OnAfterFatalDamage(CharacterBase character, int damage)
    {

    }

    public override void OnBeforeCharacterDead(CharacterBase character)
    {

    }

    public override void OnBeforeFatalDamage(CharacterBase character, int damage)
    {
        if (!character.characterData.relics.Exists(r => r.relicID == relicID)) return;
        if (relicValue > 0)
        {
            //TODO:Sound
            character.isDamageValid = false;
            relicValue--;
        }
        UIPanel.Instance.UpdateRelicsValue();
    }

    public override void OnCardDiscard()
    {

    }

    public override void OnCardDraw()
    {

    }

    public override void OnCardPlay(CharacterBase self, CharacterBase target)
    {

    }

    public override void OnCreate()
    {

    }

    public override void OnEnemyAttackBegin()
    {

    }

    public override void OnEnemyAttackEnd()
    {

    }

    public override void OnEnemyTurnBegin()
    {

    }

    public override void OnEnemyTurnEnd()
    {

    }

    public override void OnEquip(CharacterBase character)
    {

    }

    public override void OnGainMoney()
    {

    }

    public override void OnLoadMap()
    {

    }

    public override void OnLoseMoney()
    {

    }

    public override void OnNewGame()
    {

    }

    public override void OnNewRoom()
    {

    }

    public override void OnPlayerAttackBegin()
    {

    }

    public override void OnPlayerAttackEnd()
    {

    }

    public override void OnPlayerTurnEnd()
    {

    }

    public override void OnPlayerTurnBegin()
    {

    }

    public override void OnUnequip(CharacterBase character)
    {

    }
}
