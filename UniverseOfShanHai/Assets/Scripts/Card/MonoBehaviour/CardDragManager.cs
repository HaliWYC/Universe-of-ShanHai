using UnityEngine;
using UnityEngine.EventSystems;
public class CardDragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;
    private GameObject currentArrow;
    private Card currentCard;
    private bool canMove;
    private bool canExecute;

    private CharacterBase targetCharacter;

    private void Awake()
    {
        currentCard = GetComponent<Card>();
    }

    private void OnDisable()
    {
        canMove = false;
        canExecute = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                break;
            case CardType.Defense:
            case CardType.Abilities:
                canMove = true;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;
        if (canMove)
        {
            currentCard.isMoving = true;
            transform.position = Camera.main.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, 10));
            canExecute = transform.position.y > 1f;
        }
        else
        {
            if (eventData.pointerEnter == null)
            {
                canExecute = false;
                targetCharacter = null;
                return;
            }
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable) return;
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }
        if (canExecute)
        {
            currentCard.ExecuteCardEffects(GameManager.Instance.player, targetCharacter);
            RelicEvent.OnCardPlay(GameManager.Instance.player, targetCharacter);
            CardDeck.Instance.UpdateCardUI();
        }
        else
        {
            currentCard.ResetCardPositionRotation();
            currentCard.isMoving = false;
        }
    }

}
