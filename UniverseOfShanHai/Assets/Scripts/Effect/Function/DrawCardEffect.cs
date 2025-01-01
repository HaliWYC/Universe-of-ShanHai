using UnityEngine;

[CreateAssetMenu(fileName = "DrawCardEffect", menuName = "Card Effect/DrawCardEffect")]
public class DrawCardEffect : Effect
{
    public override void Init()
    {
       CardDeck.Instance.DrawCard((int)value);
    }
    public override void UpdateUI()
    {
        
    }
    public override void Execute(CharacterBase currentTarget)
    {   
        
    }

    public override void End(CharacterBase currentTarget)
    {
        
    }

    
}
