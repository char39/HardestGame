using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform UI;
    private Text roomNum;
    private Text SentenceText;
    private static Text DeathCountText;
    private static int deathCount = 0;
    internal int roomIndex = 0;

    private Text[] clearTimes;
    private Text totalTime;
    private Text levelTime;
    private bool IsTotalTimeRecord { get; set; }
    private bool IsLevelTimeRecord { get; set; }

    void Awake()
    {
        Instance = this;

        UI = GameObject.Find("UI").transform;
        roomNum = UI.GetChild(1).GetChild(0).GetComponent<Text>();
        DeathCountText = UI.GetChild(1).GetChild(1).GetComponent<Text>();
        SentenceText = UI.GetChild(2).GetComponent<Text>();

        totalTime = UI.GetChild(1).GetChild(2).GetComponent<Text>();
        levelTime = UI.GetChild(1).GetChild(3).GetComponent<Text>();

        clearTimes = UI.GetChild(3).GetComponentsInChildren<Text>();
    }

    void Start()
    {
        UI.GetChild(0).gameObject.SetActive(true);
        UI.GetChild(3).gameObject.SetActive(false);
        UI.GetChild(4).gameObject.SetActive(false);
        LoadSceneIndex(0);
    }

    void Update()
    {
        TotalTimeRecord();
        LevelTimeRecord();

        #region 테스트용으로 추가한 코드. r은 재시작, y는 다음맵으로 이동, t는 원하는 room 이동
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     if (GameObject.Find("Player") == null) return;

        //     GameObject.Find("Player").TryGetComponent(out PlayerDamage playerDamage);
        //     GameObject.Find("Player").TryGetComponent(out PlayerMove playerMove);

        //     if (!playerMove.IsMoveStop)
        //     {
        //         StartCoroutine(playerDamage.Disappear());
        //         playerMove.IsMoveStop = true;
        //         ResetCollectCoin();
        //         AddDeathCount();
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     UnloadSceneIndex();                         // 클리어한 룸 삭제
        //     LoadSceneIndex(0);                          // Room0 로드
        //     StartCoroutine(NextRoomTest());
        // }
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     RoomData.roomData[roomIndex].CollectCoin = 100;
        //     RoomDataConditionCheck();
        // }
        #endregion
    }

    #region Code For NextRoomTest
    // private IEnumerator NextRoomTest()
    // {
    //     SentenceText.enabled = true;                // (Text) Sentence 활성화

    //     SelectRoomIndex(21);
    //     ApplyRoomNumText();                         // (Text) roomIndex 우상단 출력
    //     ApplySentenceText();                        // roomIndex와 동일한 값의 Sentence을 할당
    //     yield return new WaitForSeconds(2.0f);      // 2초 대기

    //     SentenceText.enabled = false;               // (Text) Sentence 비활성화
    //     LoadSceneIndex();                           // 다음 룸 로드
    //     UnloadSceneIndex(0);                        // Room0 삭제
    //     yield return null;                          // 코루틴 종료
    // }
    #endregion


    // 여기부터 Room 관련 메서드
    private int AddRoomIndex() => roomIndex += 1;
    private int SelectRoomIndex(int index) => roomIndex = index;
    private void LoadSceneIndex() => SceneManager.LoadScene($"Room{roomIndex}", LoadSceneMode.Additive);
    private void LoadSceneIndex(int index) => SceneManager.LoadScene($"Room{index}", LoadSceneMode.Additive);
    private void UnloadSceneIndex() => SceneManager.UnloadSceneAsync($"Room{roomIndex}");
    private void UnloadSceneIndex(int index) => SceneManager.UnloadSceneAsync($"Room{index}");
    private void ApplyRoomNumText() => roomNum.text = $"{roomIndex} / 30";
    private void ApplySentenceText() => SentenceText.text = RoomData.roomData[roomIndex].Sentence;

    private void MoveNextRoom()
    {
        SoundManagement.Instance.PlayNextLevel();
        UnloadSceneIndex();
        LoadSceneIndex(0);
        StartCoroutine(NextRoom());
    }

    private IEnumerator NextRoom()
    {
        UI.GetChild(1).GetChild(4).GetComponent<Button>().interactable = false;
        SentenceText.enabled = true;

        AddRoomIndex();
        ApplyRoomNumText();
        ApplySentenceText();
        yield return new WaitForSeconds(1.5f);

        UI.GetChild(1).GetChild(4).GetComponent<Button>().interactable = true;
        SentenceText.enabled = false;
        LoadSceneIndex();
        UnloadSceneIndex(0);
        LevelTimeRecordStart(true);
        yield return null;
    }

    private void MoveClearRoom()
    {
        UI.GetChild(1).GetChild(4).GetComponent<Button>().interactable = false;
        SoundManagement.Instance.PlaySFX();
        UnloadSceneIndex();
        SelectRoomIndex(0);
        LoadSceneIndex(0);

        for (int i = 0; i < 30; i++)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(RoomData.clearTime[i + 1].EndTime);
            int mins = timeSpan.Minutes;
            int secs = timeSpan.Seconds;
            int milli = timeSpan.Milliseconds / 10;
            clearTimes[i].text = $"Level {i + 1}: {mins:D2}:{secs:D2}.{milli:D2}";
        }
    }

    // 여기부터 시간 기록용 메서드
    private void TotalTimeRecordStart(bool IsRecord) => IsTotalTimeRecord = IsRecord;
    private void TotalTimeRecord()
    {
        if (!IsTotalTimeRecord)
            return;

        RoomData.clearTime[0].EndTime += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(RoomData.clearTime[0].EndTime);
        int mins = timeSpan.Minutes;
        int secs = timeSpan.Seconds;
        int milli = timeSpan.Milliseconds / 10;
        totalTime.text = $"Total: {mins:D2}:{secs:D2}.{milli:D2}";
    }

    private void LevelTimeRecordStart(bool IsRecord) => IsLevelTimeRecord = IsRecord;
    private void LevelTimeRecord()
    {
        if (!IsLevelTimeRecord)
            return;

        RoomData.clearTime[roomIndex].EndTime += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(RoomData.clearTime[roomIndex].EndTime);
        int mins = timeSpan.Minutes;
        int secs = timeSpan.Seconds;
        int milli = timeSpan.Milliseconds / 10;
        levelTime.text = $"Level: {mins:D2}:{secs:D2}.{milli:D2}";
    }
}