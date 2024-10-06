using UnityEngine;

public class GreenZone : MonoBehaviour
{
    public bool GreenZoneStart;
    public bool GreenZoneMidSave1;
    public bool GreenZoneMidSave2;
    public bool GreenZoneEnd;

    void Start()
    {
        if (GreenZoneStart)
            gameObject.AddComponent<GreenZoneStart>();
        if (GreenZoneMidSave1)
            gameObject.AddComponent<GreenZoneMidSave1>();
        if (GreenZoneMidSave2)
            gameObject.AddComponent<GreenZoneMidSave2>();
        if (GreenZoneEnd)
            gameObject.AddComponent<GreenZoneEnd>();
    }
}

public abstract class GreenZoneGroup : MonoBehaviour
{
    public const string playerTag = "Player";
    public abstract void OnTriggerEnter2D(Collider2D col);
}

public class GreenZoneStart : GreenZoneGroup
{
    public override void OnTriggerEnter2D(Collider2D col) { }
}

public class GreenZoneMidSave1 : GreenZoneGroup
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(playerTag) && !RoomData.roomData[GameManager.Instance.roomIndex].MidSave1Touched)
            GameManager.Instance.MidSave1On();
    }
}

public class GreenZoneMidSave2 : GreenZoneGroup
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(playerTag) && !RoomData.roomData[GameManager.Instance.roomIndex].MidSave2Touched)
            GameManager.Instance.MidSave2On();
    }
}

public class GreenZoneEnd : GreenZoneGroup
{
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(playerTag))
            GameManager.Instance.RoomDataConditionCheck();
    }
}
