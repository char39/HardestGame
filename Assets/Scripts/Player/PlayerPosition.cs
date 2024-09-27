using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void OnEnable()
    {
        int index = GameManager.Instance.roomIndex;

        if (RoomData.roomData[index].MidSave2Touched)
            transform.position = RoomData.roomData[index].MidPos2;
        else if (RoomData.roomData[index].MidSave1Touched)
            transform.position = RoomData.roomData[index].MidPos1;
        else
            transform.position = RoomData.roomData[index].StartPos;

        TryGetComponent(out PlayerMove playerMove);
        playerMove.IsMoveStop = false;
    }
}
