using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
public class GuidanceManager : Singleton<GuidanceManager>
{
    public GameObject mask;
    public Button endTurnButton;
    public RectTransform canvas;
    public GameObject welcome;
    public GameObject instruction;
    public GameObject firstRoom;
    public GameObject enemy;
    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public GameObject playerMana;
    public GameObject drawDeck;
    public GameObject discardDeck;
    public GameObject cardDeck;
    public GameObject AttackCard;
    public GameObject DefenseCard;
    public GameObject CardDetails;
    public GameObject EnemyIntention;
    public GameObject EndTurn;
    public GameObject SelfAction;
    public GameObject Victory;
    public GameObject ChooseCard;
    public GameObject SecondRoom;
    public GameObject RestRoom;
    public GameObject Shop;
    public GameObject Treasure;
    public GameObject Boss;
    public bool isWelcomeChecked = false;
    public bool isInstructionChecked = false;
    public bool isFirstRoomChecked = false;
    public bool isEnemyChecked = false;
    public bool isPlayerHealthBarChecked = false;
    public bool isEnemyHealthBarChecked = false;
    public bool isPlayerManaChecked = false;
    public bool isCardDeckChecked = false;
    public bool isDiscardDeckChecked = false;
    public bool isDrawDeckChecked = false;
    public bool isAttackCardChecked = false;
    public bool isDefenseCardChecked = false;
    public bool isCardDetailsChecked = false;
    public bool isEnemyIntentionChecked = false;
    public bool isTurnEndChecked = false;
    public bool isSelfActionChecked = false;
    public bool isVictoryChecked = false;
    public bool isChooseCardChecked = false;
    public bool isSecondRoomChecked = false;
    public bool isRestRoomChecked = false;
    public bool isShopChecked = false;
    public bool isTreasureChecked = false;
    public bool isBossChecked = false;
    private bool isEnterFirstRoom = false;

    private float skipTime = 2f;
    private float currentSkipTime;

    private bool skipGuidance = false;

    private void Start()
    {
        currentSkipTime = 0f;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            currentSkipTime += Time.deltaTime;
        }

        if (currentSkipTime >= skipTime && !skipGuidance)
        {
            SkipGuidance();
            skipGuidance = true;
        }
    }

    public void StartGuidance()
    {
        if (!isWelcomeChecked)
            StartCoroutine(WelcomeGuidance(9));
    }

    public void SkipGuidance()
    {
        StopAllCoroutines();
        isWelcomeChecked = true;
        isInstructionChecked = true;
        isFirstRoomChecked = true;
        isEnemyChecked = true;
        isPlayerHealthBarChecked = true;
        isEnemyHealthBarChecked = true;
        isPlayerManaChecked = true;
        isCardDeckChecked = true;
        isDiscardDeckChecked = true;
        isDrawDeckChecked = true;
        isAttackCardChecked = true;
        isDefenseCardChecked = true;
        isCardDetailsChecked = true;
        isEnemyIntentionChecked = true;
        isTurnEndChecked = true;
        isSelfActionChecked = true;
        isVictoryChecked = true;
        isChooseCardChecked = true;
        isSecondRoomChecked = true;
        isRestRoomChecked = true;
        isShopChecked = true;
        isTreasureChecked = true;
        isBossChecked = true;
        isEnterFirstRoom = true;
        mask.SetActive(false);
        endTurnButton.interactable = true;
        welcome.SetActive(false);
        instruction.SetActive(false);
        firstRoom.SetActive(false);
        enemy.SetActive(false);
        playerHealthBar.SetActive(false);
        enemyHealthBar.SetActive(false);
        playerMana.SetActive(false);
        cardDeck.SetActive(false);
        discardDeck.SetActive(false);
        drawDeck.SetActive(false);
        AttackCard.SetActive(false);
        DefenseCard.SetActive(false);
        CardDetails.SetActive(false);
        EnemyIntention.SetActive(false);
        EndTurn.SetActive(false);
        SelfAction.SetActive(false);
        Victory.SetActive(false);
        ChooseCard.SetActive(false);
        SecondRoom.SetActive(false);
        RestRoom.SetActive(false);
        Shop.SetActive(false);
        Treasure.SetActive(false);
        Boss.SetActive(false);
    }

    public IEnumerator WelcomeGuidance(float time)
    {
        welcome.SetActive(true);
        isWelcomeChecked = true;
        yield return new WaitForSeconds(time);
        if (!isInstructionChecked && !isEnterFirstRoom)
            StartCoroutine(InstructionGuidance(6));
    }

    public IEnumerator InstructionGuidance(float time)
    {
        welcome.SetActive(false);
        instruction.SetActive(true);
        isInstructionChecked = true;
        yield return new WaitForSeconds(time);
        if (!isFirstRoomChecked && !isEnterFirstRoom)
            StartCoroutine(FirstRoomGuidance(5));
    }
    public IEnumerator FirstRoomGuidance(float time)
    {
        instruction.SetActive(false);
        firstRoom.SetActive(true);
        isFirstRoomChecked = true;
        yield return new WaitForSeconds(time);
    }
    public IEnumerator EnemyGuidance(float time)
    {
        mask.SetActive(true);
        endTurnButton.interactable = false;
        isEnterFirstRoom = true;
        welcome.SetActive(false);
        instruction.SetActive(false);
        firstRoom.SetActive(false);
        enemy.SetActive(true);
        isEnemyChecked = true;
        yield return new WaitForSeconds(time);
        if (!isPlayerHealthBarChecked)
            StartCoroutine(PlayerHealthBarGuidance(5));
    }
    public IEnumerator PlayerHealthBarGuidance(float time)
    {
        enemy.SetActive(false);
        playerHealthBar.SetActive(true);
        isPlayerHealthBarChecked = true;
        yield return new WaitForSeconds(time);
        if (!isEnemyHealthBarChecked)
            StartCoroutine(EnemyHealthBarGuidance(4));
    }
    public IEnumerator EnemyHealthBarGuidance(float time)
    {
        playerHealthBar.SetActive(false);
        enemyHealthBar.SetActive(true);
        isEnemyHealthBarChecked = true;
        yield return new WaitForSeconds(time);
        if (!isPlayerManaChecked)
            StartCoroutine(PlayerManaGuidance(6));
    }
    public IEnumerator PlayerManaGuidance(float time)
    {
        enemyHealthBar.SetActive(false);
        playerMana.SetActive(true);
        isPlayerManaChecked = true;
        yield return new WaitForSeconds(time);
        if (!isDrawDeckChecked)
            StartCoroutine(DrawDeckGuidance(6));
    }
    public IEnumerator DrawDeckGuidance(float time)
    {
        playerMana.SetActive(false);
        drawDeck.SetActive(true);
        isDrawDeckChecked = true;
        yield return new WaitForSeconds(time);
        if (!isDiscardDeckChecked)
            StartCoroutine(DiscardDeckGuidance(9));
    }
    public IEnumerator DiscardDeckGuidance(float time)
    {
        drawDeck.SetActive(false);
        discardDeck.SetActive(true);
        isDiscardDeckChecked = true;
        yield return new WaitForSeconds(time);
        if (!isCardDeckChecked)
            StartCoroutine(CardDeckGuidance(6));
    }

    public IEnumerator CardDeckGuidance(float time)
    {
        discardDeck.SetActive(false);
        cardDeck.SetActive(true);
        isCardDeckChecked = true;
        yield return new WaitForSeconds(time);
        if (!isAttackCardChecked)
            StartCoroutine(AttackCardGuidance(8));
    }
    public IEnumerator AttackCardGuidance(float time)
    {
        cardDeck.SetActive(false);
        AttackCard.SetActive(true);
        isAttackCardChecked = true;
        yield return new WaitForSeconds(time);
        if (!isDefenseCardChecked)
            StartCoroutine(DefenseCardGuidance(8));
    }
    public IEnumerator DefenseCardGuidance(float time)
    {
        AttackCard.SetActive(false);
        DefenseCard.SetActive(true);
        isDefenseCardChecked = true;
        yield return new WaitForSeconds(time);
        if (!isCardDetailsChecked)
            StartCoroutine(CardDetailsGuidance(5));
        mask.SetActive(false);
    }
    public IEnumerator CardDetailsGuidance(float time)
    {
        DefenseCard.SetActive(false);
        CardDetails.SetActive(true);
        isCardDetailsChecked = true;
        yield return new WaitForSeconds(time);
        if (!isEnemyIntentionChecked)
            StartCoroutine(EnemyIntentionGuidance(8));
    }

    public IEnumerator EnemyIntentionGuidance(float time)
    {
        CardDetails.SetActive(false);
        EnemyIntention.SetActive(true);
        isEnemyIntentionChecked = true;
        yield return new WaitForSeconds(time);
        if (!isTurnEndChecked)
            StartCoroutine(TurnEndGuidance(5));
    }
    public IEnumerator TurnEndGuidance(float time)
    {
        EnemyIntention.SetActive(false);
        EndTurn.SetActive(true);
        isTurnEndChecked = true;
        yield return new WaitForSeconds(time);
        EndTurn.SetActive(false);
        endTurnButton.interactable = true;
    }
    public IEnumerator SelfActionGuidance(float time)
    {
        EndTurn.SetActive(false);
        SelfAction.SetActive(true);
        isSelfActionChecked = true;
        yield return new WaitForSeconds(time);
        SelfAction.SetActive(false);
    }
    public IEnumerator VictoryGuidance(float time)
    {
        Victory.SetActive(true);
        isVictoryChecked = true;
        yield return new WaitForSeconds(time);
        Victory.SetActive(false);
    }
    public IEnumerator ChooseCardGuidance(float time)
    {
        ChooseCard.SetActive(true);
        isChooseCardChecked = true;
        yield return new WaitForSeconds(time);
        ChooseCard.SetActive(false);
    }

    public IEnumerator NextRoomGuidance(float time)
    {
        SecondRoom.SetActive(true);
        isSecondRoomChecked = true;
        yield return new WaitForSeconds(time);
        SecondRoom.SetActive(false);
    }

    public IEnumerator TreasureGuidance(float time)
    {
        Treasure.SetActive(true);
        isTreasureChecked = true;
        yield return new WaitForSeconds(time);
        Treasure.SetActive(false);
    }

    public IEnumerator ShopGuidance(float time)
    {
        Shop.SetActive(true);
        isShopChecked = true;
        yield return new WaitForSeconds(time);
        Shop.SetActive(false);
    }

    public IEnumerator RestRoomGuidance(float time)
    {
        RestRoom.SetActive(true);
        isRestRoomChecked = true;
        yield return new WaitForSeconds(time);
        RestRoom.SetActive(false);
    }
    public IEnumerator BossRoomGuidance(float time)
    {
        Boss.SetActive(true);
        isBossChecked = true;
        yield return new WaitForSeconds(time);
        Boss.SetActive(false);
    }
}
