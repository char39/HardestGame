using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform UI;
    private Text roomNum;
    private Text SentenceText;
    private static Text DeathCountText;
    private static int deathCount = 0;
    public int roomIndex = 0;

    void Awake()
    {
        Instance = this;

        UI = GameObject.Find("UI").transform;
        roomNum = UI.GetChild(1).GetComponent<Text>();
        SentenceText = UI.GetChild(2).GetComponent<Text>();
        DeathCountText = UI.GetChild(3).GetComponent<Text>();
    }

    void Start()
    {
        UI.GetChild(0).gameObject.SetActive(true);
        LoadSceneIndex(0);
    }

    public void UI_StartButton()
    {
        UI.GetChild(0).gameObject.SetActive(false);
        UI.GetChild(1).gameObject.SetActive(true);
        UI.GetChild(3).gameObject.SetActive(true);

        StartCoroutine(NextRoom());
    }

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
        UnloadSceneIndex();                         // 클리어한 룸 삭제
        LoadSceneIndex(0);                          // Room0 로드
        StartCoroutine(NextRoom());
    }

    private IEnumerator NextRoom()
    {
        SentenceText.enabled = true;                // (Text) Sentence 활성화

        AddRoomIndex();                             // roomIndex 증가
        ApplyRoomNumText();                         // (Text) roomIndex 우상단 출력
        ApplySentenceText();                        // roomIndex와 동일한 값의 Sentence을 할당
        yield return new WaitForSeconds(2.0f);      // 2초 대기

        SentenceText.enabled = false;               // (Text) Sentence 비활성화
        LoadSceneIndex();                           // 다음 룸 로드
        UnloadSceneIndex(0);                        // Room0 삭제
        yield return null;                          // 코루틴 종료
    }

    // 아래부터 호출용 함수
    public void RoomDataConditionCheck()
    {
        if (RoomData.roomData[roomIndex].ClearCondition)
            MoveNextRoom();
    }

    public void RestartRoom()
    {
        RoomData.roomData[roomIndex].CollectCoin = 0;
    }

    public void ResetCollectCoin() => RoomData.roomData[roomIndex].CollectCoin = 0;
    public void CollectCoin() => RoomData.roomData[roomIndex].CollectCoin++;
    public void MidSave1On() => RoomData.roomData[roomIndex].MidSave1Touched = true;
    public void MidSave2On() => RoomData.roomData[roomIndex].MidSave2Touched = true;

    public static void AddDeathCount()
    {
        deathCount++;
        DeathCountText.text = $"Death : {deathCount}";
    }






    // 테스트용으로 추가한 코드. r은 재시작, t는 다음맵으로 이동

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameObject.Find("Player") == null) return;
            
            GameObject.Find("Player").TryGetComponent(out PlayerDamage playerDamage);
            GameObject.Find("Player").TryGetComponent(out PlayerMove playerMove);

            if (!playerMove.IsMoveStop)
            {
                StartCoroutine(playerDamage.Disappear());
                playerMove.IsMoveStop = true;
                RestartRoom();
                AddDeathCount();
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            UnloadSceneIndex();                         // 클리어한 룸 삭제
            LoadSceneIndex(0);                          // Room0 로드
            StartCoroutine(NextRoomTest());
        }
    }

    private IEnumerator NextRoomTest()
    {
        SentenceText.enabled = true;                // (Text) Sentence 활성화

        SelectRoomIndex(9);
        ApplyRoomNumText();                         // (Text) roomIndex 우상단 출력
        ApplySentenceText();                        // roomIndex와 동일한 값의 Sentence을 할당
        yield return new WaitForSeconds(2.0f);      // 2초 대기

        SentenceText.enabled = false;               // (Text) Sentence 비활성화
        LoadSceneIndex();                           // 다음 룸 로드
        UnloadSceneIndex(0);                        // Room0 삭제
        yield return null;                          // 코루틴 종료
    }
}
