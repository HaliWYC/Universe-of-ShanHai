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
        guidanceList[0].SetActive(true);
        guidanceCheckList[0] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[1] && !isEnterFirstRoom)
            StartCoroutine(InstructionGuidance(6));
    }

    public IEnumerator InstructionGuidance(float time)
    {
        guidanceList[0].SetActive(false);
        guidanceList[1].SetActive(true);
        guidanceCheckList[1] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[2] && !isEnterFirstRoom)
            StartCoroutine(FirstRoomGuidance(5));
    }
    public IEnumerator FirstRoomGuidance(float time)
    {
        guidanceList[1].SetActive(false);
        guidanceList[2].SetActive(true);
        guidanceCheckList[2] = true;
        yield return new WaitForSeconds(time);
    }
    public IEnumerator EnemyGuidance(float time)
    {
        mask.SetActive(true);
        endTurnButton.interactable = false;
        isEnterFirstRoom = true;
        guidanceList[0].SetActive(false);
        guidanceList[1].SetActive(false);
        guidanceList[2].SetActive(false);
        guidanceList[3].SetActive(true);
        guidanceCheckList[3] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[4])
            StartCoroutine(PlayerHealthBarGuidance(5));
    }
    public IEnumerator PlayerHealthBarGuidance(float time)
    {
        guidanceList[3].SetActive(false);
        guidanceList[4].SetActive(true);
        guidanceCheckList[4] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[5])
            StartCoroutine(EnemyHealthBarGuidance(4));
    }
    public IEnumerator EnemyHealthBarGuidance(float time)
    {
        guidanceList[4].SetActive(false);
        guidanceList[5].SetActive(true);
        guidanceCheckList[5] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[6])
            StartCoroutine(PlayerManaGuidance(6));
    }
    public IEnumerator PlayerManaGuidance(float time)
    {
        guidanceList[5].SetActive(false);
        guidanceList[6].SetActive(true);
        guidanceCheckList[6] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[7])
            StartCoroutine(DrawDeckGuidance(6));
    }
    public IEnumerator DrawDeckGuidance(float time)
    {
        guidanceList[6].SetActive(false);
        guidanceList[7].SetActive(true);
        guidanceCheckList[7] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[8])
            StartCoroutine(DiscardDeckGuidance(9));
    }
    public IEnumerator DiscardDeckGuidance(float time)
    {
        guidanceList[7].SetActive(false);
        guidanceList[8].SetActive(true);
        guidanceCheckList[8] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[9])
            StartCoroutine(CardDeckGuidance(6));
    }

    public IEnumerator CardDeckGuidance(float time)
    {
        guidanceList[8].SetActive(false);
        guidanceList[9].SetActive(true);
        guidanceCheckList[9] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[10])
            StartCoroutine(AttackCardGuidance(8));
    }
    public IEnumerator AttackCardGuidance(float time)
    {
        guidanceList[9].SetActive(false);
        guidanceList[10].SetActive(true);
        guidanceCheckList[10] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[11])
            StartCoroutine(DefenseCardGuidance(8));
    }
    public IEnumerator DefenseCardGuidance(float time)
    {
        guidanceList[10].SetActive(false);
        guidanceList[11].SetActive(true);
        guidanceCheckList[11] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[12])
            StartCoroutine(CardDetailsGuidance(5));
        mask.SetActive(false);
    }
    public IEnumerator CardDetailsGuidance(float time)
    {
        guidanceList[11].SetActive(false);
        guidanceList[12].SetActive(true);
        guidanceCheckList[12] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[13])
            StartCoroutine(EnemyIntentionGuidance(8));
    }

    public IEnumerator EnemyIntentionGuidance(float time)
    {
        guidanceList[12].SetActive(false);
        guidanceList[13].SetActive(true);
        guidanceCheckList[13] = true;
        yield return new WaitForSeconds(time);
        if (!guidanceCheckList[14])
            StartCoroutine(TurnEndGuidance(5));
    }
    public IEnumerator TurnEndGuidance(float time)
    {
        guidanceList[13].SetActive(false);
        guidanceList[14].SetActive(true);
        guidanceCheckList[14] = true;
        yield return new WaitForSeconds(time);
        guidanceList[14].SetActive(false);
        endTurnButton.interactable = true;
    }
    public IEnumerator SelfActionGuidance(float time)
    {
        guidanceList[14].SetActive(false);
        guidanceList[15].SetActive(true);
        guidanceCheckList[15] = true;
        yield return new WaitForSeconds(time);
        guidanceList[15].SetActive(false);
    }
    public IEnumerator VictoryGuidance(float time)
    {
        guidanceList[16].SetActive(true);
        guidanceCheckList[16] = true;
        yield return new WaitForSeconds(time);
        guidanceList[16].SetActive(false);
    }
    public IEnumerator ChooseCardGuidance(float time)
    {
        guidanceList[17].SetActive(true);
        guidanceCheckList[17] = true;
        yield return new WaitForSeconds(time);
        guidanceList[17].SetActive(false);
    }

    public IEnumerator NextRoomGuidance(float time)
    {
        guidanceList[18].SetActive(true);
        guidanceCheckList[18] = true;
        yield return new WaitForSeconds(time);
        guidanceList[18].SetActive(false);
    }

    public IEnumerator TreasureGuidance(float time)
    {
        guidanceList[19].SetActive(true);
        guidanceCheckList[19] = true;
        yield return new WaitForSeconds(time);
        guidanceList[19].SetActive(false);
    }

    public IEnumerator ShopGuidance(float time)
    {
        guidanceList[20].SetActive(true);
        guidanceCheckList[20] = true;
        yield return new WaitForSeconds(time);
        guidanceList[20].SetActive(false);
    }

    public IEnumerator RestRoomGuidance(float time)
    {
        guidanceList[21].SetActive(true);
        guidanceCheckList[21] = true;
        yield return new WaitForSeconds(time);
        guidanceList[21].SetActive(false);
    }
    public IEnumerator BossRoomGuidance(float time)
    {
        guidanceList[22].SetActive(true);
        guidanceCheckList[22] = true;
        yield return new WaitForSeconds(time);
        guidanceList[22].SetActive(false);
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
