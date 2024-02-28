using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;
    public SceneLoader sceneLoader;
    //public QuestManager questManager;
    public SoundManager SoundManager;
    public ItemDataBase itemDataBase;

    public PlayerSpawnPoints spawnPoints;

    [SerializeField]
    private Image FadeInOutImage;
    [HideInInspector]
    public int curSceneNum = 1;
    private void Awake()
    {
        base.Initialize();
    }
    public void StartInGameScene(int spawnPointNum)
    {
        //�� ������ ���� �� ��� �Ǵ� ��
        //UIManager ���ε� ����
        UiManager = FindObjectOfType<UiManager>();

        ItemManager = FindObjectOfType<ItemManager>();

        //�÷��̾� ����
        SpawnPlayer(spawnPointNum);

        //UI����
        UiManager.DefalutSetting();

        //ī�޶� ���ε� ����
        FindObjectOfType<FollowCam>().SetTarget(inGameManager.myPlayer.transform);
        //�̴ϸ�ī�޶� ���ε� ����
        FindObjectOfType<MiniMap>().SetTarget();


        //�غ� ������ Fade In
        FadeIn();

        //�÷��̾� Input Ȱ��ȭ
       // inGameManager.myPlayer.CanMove = true;
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCo(1.0f));
    }
    IEnumerator FadeInCo(float t)
    {
        float curtime = 0f;
        while (curtime <= t)
        {
            curtime += Time.deltaTime;
            FadeInOutImage.color = new Vector4(0, 0, 0, 1f - curtime / t);
            yield return null;
        }
        FadeInOutImage.raycastTarget = false;
    }
    public void FadeOut(UnityAction done = null)
    {
        StartCoroutine(FadeOutCo(1.0f, done));
    }
    IEnumerator FadeOutCo(float t, UnityAction done)
    {
        FadeInOutImage.raycastTarget = true;
        float curtime = 0f;
        while (curtime <= t)
        {
            curtime += Time.deltaTime;
            FadeInOutImage.color = new Vector4(0, 0, 0, curtime / t);
            yield return null;
        }

        //FadeOut �Ŀ� ����
        done?.Invoke();
    }
    public void SpawnPlayer(int spawnPointNum)
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        inGameManager.myPlayer = player.GetComponent<PlayerCon>();

        //ui hpBar ���ε�
        inGameManager.myPlayer.myHpBar = UiManager.myHpSlider;
        inGameManager.myPlayer.myExpBar = UiManager.myExpSlider;

        //�÷��̾� ���� �ޱ� (SaveData)
      //  inGameManager.Load(inGameManager.myPlayer);

        //-1�� �ƴϸ� �ش� ��������Ʈ�� �̵�
        if (spawnPointNum != -1)
        {
            //���� ����Ʈ�� �÷��̾� �̵�
            spawnPoints = FindObjectOfType<PlayerSpawnPoints>();
            if (spawnPoints != null)
            {
                inGameManager.myPlayer.transform.position =
                    spawnPoints.spawnPoint[spawnPointNum].transform.position;
            }

        }
      //  inGameManager.Save();
    }   /// <summary>
        /// ���� �����ϴ� �Լ�
        /// </summary>
    public void OnGameExit()
    {
        Application.Quit();
    }

    public void StartNewGame(int sceneNum)
    {
        //test
        inGameManager.curSaveSlotNum = 0;

        curSceneNum = sceneNum;
        FadeOut(() => sceneLoader.LoadScene(sceneNum, 0));
        //sceneLoader.LoadScene(2, 0);
    }
    //public void StartLoadGame(int saveSlotNum)
    //{
    //    SaveData saveData = inGameManager.saveDatas[saveSlotNum];

    //    inGameManager.curSaveSlotNum = saveSlotNum;
    //    if (saveData.IsEmpty)
    //    {
    //        curSceneNum = 2;
    //        FadeOut(() => sceneLoader.LoadScene(curSceneNum, 0));

    //        //����Ʈ �ʱ�ȭ
    //        questManager.InitQuestDatabase(saveSlotNum);
    //        questManager.ResetQuestData();
    //    }
    //    else
    //    {
    //        curSceneNum = saveData.playerInfo.SceneNum;
    //        FadeOut(() => sceneLoader.LoadScene(curSceneNum, -1));

    //        //����Ʈ �ʱ�ȭ
    //        questManager.InitQuestDatabase(saveSlotNum);
    //    }
    //}
    public void MapChange(int sceneNum, int spawnPointNum)
    {
      //  inGameManager.Save();
        curSceneNum = sceneNum;
        FadeOut(() => sceneLoader.LoadScene(sceneNum, spawnPointNum));

    }

    public void GotoTitle()
    {
      //  inGameManager.Save();
        curSceneNum = 1;
        FadeOut(() => SceneManager.LoadScene(0));
    }

    public void GameOver()
    {
        UiManager.myGameOverWindow.SetActive(true);
    }

    public void PlayerRespawn()
    {
        //���� ����Ʈ�� �÷��̾� �̵�
        spawnPoints = FindObjectOfType<PlayerSpawnPoints>();
        if (spawnPoints != null)
        {
            inGameManager.myPlayer.transform.position =
                spawnPoints.spawnPoint[0].transform.position;
        }
        inGameManager.myPlayer.PlayerRespawn();
        inGameManager.Save();
    }


}
