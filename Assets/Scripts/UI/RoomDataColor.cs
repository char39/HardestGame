using UnityEngine;

public class RoomDataColor : MonoBehaviour
{
    internal Transform gridMap;
    private int index;

    void OnEnable()
    {
        gridMap = GameObject.Find("GridMap").transform;

        index = GameManager.Instance.roomIndex;

        gridMap.GetChild(0).gameObject.SetActive(index < 20);
        gridMap.GetChild(1).gameObject.SetActive(index >= 20 && index < 30);
        gridMap.GetChild(2).gameObject.SetActive(index == 30);
    }
}
