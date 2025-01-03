using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GuidanceManager : Singleton<GuidanceManager>, ISavable
{
    public GameObject mask;
    public Button endTurnButton;
    public RectTransform canvas;

    public List<GameObject> guidanceList = new List<GameObject>();
    private bool isEnterFirstRoom = false;

    public List<bool> guidanceCheckList = new List<bool>();

    private float skipTime = 2f;
    private float currentSkipTime;

    private bool skipGuidance = false;

    public string GUID => GetComponent<DataGUID>().guid;

    private void Start()
    {
        currentSkipTime = 0f;
        ISavable savable = this;
        savable.RegisterSavable();
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
        if (!guidanceCheckList[0])
            StartCoroutine(WelcomeGuidance(9));
    }

    public void SkipGuidance()
    {
        StopAllCoroutines();
        for (int i = 0; i < guidanceCheckList.Count; i++)
        {
            guidanceCheckList[i] = true;
        }
        isEnterFirstRoom = true;
        mask.SetActive(false);
        endTurnButton.interactable = true;
        foreach (var item in guidanceList)
        {
            item.SetActive(false);
        }
    }

    public IEnumerator WelcomeGuidance(float time)
    {
        ClosePreviousGuidance(0);
        guidanceCheckList[0] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[1] && !isEnterFirstRoom)
            StartCoroutine(InstructionGuidance(6));
    }

    public IEnumerator InstructionGuidance(float time)
    {
        ClosePreviousGuidance(1);
        guidanceCheckList[1] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[2] && !isEnterFirstRoom)
            StartCoroutine(FirstRoomGuidance(5));
    }
    public IEnumerator FirstRoomGuidance(float time)
    {
        ClosePreviousGuidance(2);
        guidanceCheckList[2] = true;
        yield return new WaitForSeconds(time);
    }
    public IEnumerator EnemyGuidance(float time)
    {
        mask.SetActive(true);
        endTurnButton.interactable = false;
        isEnterFirstRoom = true;
        ClosePreviousGuidance(3);
        guidanceCheckList[3] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[4])
            StartCoroutine(PlayerHealthBarGuidance(5));
    }
    public IEnumerator PlayerHealthBarGuidance(float time)
    {
        ClosePreviousGuidance(4);
        guidanceCheckList[4] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[5])
            StartCoroutine(EnemyHealthBarGuidance(4));
    }
    public IEnumerator EnemyHealthBarGuidance(float time)
    {
        ClosePreviousGuidance(5);
        guidanceCheckList[5] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[6])
            StartCoroutine(PlayerManaGuidance(6));
    }
    public IEnumerator PlayerManaGuidance(float time)
    {
        ClosePreviousGuidance(6);
        guidanceCheckList[6] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[7])
            StartCoroutine(DrawDeckGuidance(6));
    }
    public IEnumerator DrawDeckGuidance(float time)
    {
        ClosePreviousGuidance(7);
        guidanceCheckList[7] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[8])
            StartCoroutine(DiscardDeckGuidance(9));
    }
    public IEnumerator DiscardDeckGuidance(float time)
    {
        ClosePreviousGuidance(8);
        guidanceCheckList[8] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[9])
            StartCoroutine(CardDeckGuidance(6));
    }

    public IEnumerator CardDeckGuidance(float time)
    {
        ClosePreviousGuidance(9);
        guidanceCheckList[9] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[10])
            StartCoroutine(AttackCardGuidance(8));
    }
    public IEnumerator AttackCardGuidance(float time)
    {
        ClosePreviousGuidance(10);
        guidanceCheckList[10] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[11])
            StartCoroutine(DefenseCardGuidance(8));
    }
    public IEnumerator DefenseCardGuidance(float time)
    {
        ClosePreviousGuidance(11);
        guidanceCheckList[11] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[12])
            StartCoroutine(CardDetailsGuidance(5));
        mask.SetActive(false);
    }
    public IEnumerator CardDetailsGuidance(float time)
    {
        ClosePreviousGuidance(12);
        guidanceCheckList[12] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[13])
            StartCoroutine(EnemyIntentionGuidance(8));
    }

    public IEnumerator EnemyIntentionGuidance(float time)
    {
        ClosePreviousGuidance(13);
        guidanceCheckList[13] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[14])
            StartCoroutine(TurnEndGuidance(5));
    }
    public IEnumerator TurnEndGuidance(float time)
    {
        ClosePreviousGuidance(14);
        guidanceCheckList[14] = true;
        yield return new WaitForSeconds(time);
        guidanceList[14].SetActive(false);
        endTurnButton.interactable = true;
    }
    public IEnumerator SelfActionGuidance(float time)
    {
        ClosePreviousGuidance(15);
        guidanceCheckList[15] = true;
        yield return new WaitForSeconds(time);
        guidanceList[15].SetActive(false);
    }
    public IEnumerator VictoryGuidance(float time)
    {
        ClosePreviousGuidance(16);
        guidanceCheckList[16] = true;
        yield return new WaitForSeconds(time);
        guidanceList[16].SetActive(false);
    }
    public IEnumerator ChooseCardGuidance(float time)
    {
        ClosePreviousGuidance(17);
        guidanceCheckList[17] = true;
        yield return new WaitForSeconds(time);
        guidanceList[17].SetActive(false);
    }

    public IEnumerator NextRoomGuidance(float time)
    {
        ClosePreviousGuidance(18);
        guidanceCheckList[18] = true;
        yield return new WaitForSeconds(time);
        guidanceList[18].SetActive(false);
    }

    public IEnumerator TreasureGuidance(float time)
    {
        ClosePreviousGuidance(19);
        guidanceCheckList[19] = true;
        yield return new WaitForSeconds(time);
        guidanceList[19].SetActive(false);
    }

    public IEnumerator ShopGuidance(float time)
    {
        ClosePreviousGuidance(20);
        guidanceCheckList[20] = true;
        yield return new WaitForSeconds(time);
        guidanceList[20].SetActive(false);
    }

    public IEnumerator RestRoomGuidance(float time)
    {
        ClosePreviousGuidance(21);
        guidanceCheckList[21] = true;
        yield return new WaitForSeconds(time);
        guidanceList[21].SetActive(false);
    }
    public IEnumerator BossRoomGuidance(float time)
    {
        ClosePreviousGuidance(22);
        guidanceCheckList[22] = true;
        yield return new WaitForSeconds(time);
        guidanceList[22].SetActive(false);
    }

    public IEnumerator CharacterStateDetailsGuidance(float time)
    {
        ClosePreviousGuidance(23);
        guidanceCheckList[23] = true;
        yield return new WaitForSeconds(time);
        guidanceList[23].SetActive(false);
    }
    public void StartCharacterStateDetailsGuidance()
    {
        StartCoroutine(CharacterStateDetailsGuidance(6));
    }

    private void ClosePreviousGuidance(int index)
    {
        for (int i = 0; i < index; i++)
        {
            guidanceList[i].SetActive(false);
        }
        guidanceList[index].SetActive(true);
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.guidanceCheckList = guidanceCheckList;
        return saveData;
    }

    public void RestoreData(GameSaveData data)
    {
        guidanceCheckList = data.guidanceCheckList;
    }
}
