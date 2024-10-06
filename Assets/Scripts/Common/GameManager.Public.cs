using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    // UI Button용 메서드
    public void UI_StartButton()
    {
        UI.GetChild(0).gameObject.SetActive(false);
        UI.GetChild(1).gameObject.SetActive(true);

        SoundManagement.Instance.PlayNextLevel();
        StartCoroutine(NextRoom());
        TotalTimeRecordStart(true);
    }

    public void UI_MenuButton()
    {
        LevelTimeRecordStart(false);
        TotalTimeRecordStart(false);

        SoundManagement.Instance.StartMusic();
        UnloadSceneIndex();
        SelectRoomIndex(0);
        LoadSceneIndex(0);

        Initialize();
    }

    public void UI_BackToMenuButton()
    {
        SoundManagement.Instance.StartMusic();
        Initialize();
    }

    private void Initialize()
    {
        roomNum.text = "00 / 30";
        SentenceText.text = "";
        DeathCountText.text = "Death : 0";
        levelTime.text = "Level: 00:00.00";
        totalTime.text = "Total: 00:00.00";

        UI.GetChild(0).gameObject.SetActive(true);
        UI.GetChild(1).gameObject.SetActive(false);
        UI.GetChild(3).gameObject.SetActive(false);
        UI.GetChild(4).gameObject.SetActive(false);

        deathCount = 0;

        for (int i = 0; i <= 30; i++)
        {
            RoomData.clearTime[i] = new ClearTime();
            if (i == 30)
                break;
            RoomData.roomData[i + 1].CollectCoin = 0;
            RoomData.roomData[i + 1].MidSave1Touched = false;
            RoomData.roomData[i + 1].MidSave2Touched = false;
        }
    }

    // 여기부터 호출용 메서드
    public void RoomDataConditionCheck()
    {
        if (RoomData.roomData[roomIndex].ClearCondition)
        {
            if (roomIndex != 30)
            {
                LevelTimeRecordStart(false);
                MoveNextRoom();
            }
            else
            {
                LevelTimeRecordStart(false);
                TotalTimeRecordStart(false);
                MoveClearRoom();
                UI.GetChild(3).gameObject.SetActive(true);
                UI.GetChild(4).gameObject.SetActive(true);
            }
        }
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
}
