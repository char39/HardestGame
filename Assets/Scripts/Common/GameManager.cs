using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    // 테스트용
    public int roomIndex = 0;

    public Transform UI;
    
    void Awake()
    {
        UI = GameObject.Find("UI").transform;
    }

    void Start()
    {
        LoadSceneIndex(0);
    }

    void Update()
    {

    }

    public void UI_StartButton()
    {
        UnloadSceneIndex(0);
        AddRoomIndex();
        LoadSceneIndex();
        UI.GetChild(0).gameObject.SetActive(false);
    }

    private int AddRoomIndex() => roomIndex += 1;
    private int SelectRoomIndex(int index) => roomIndex = index;

    private void LoadSceneIndex() => SceneManager.LoadScene($"Room{roomIndex}", LoadSceneMode.Additive);
    private void LoadSceneIndex(int index) => SceneManager.LoadScene($"Room{index}", LoadSceneMode.Additive);
    private void UnloadSceneIndex() => SceneManager.UnloadSceneAsync($"Room{roomIndex}");
    private void UnloadSceneIndex(int index) => SceneManager.UnloadSceneAsync($"Room{index}");
}
