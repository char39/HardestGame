using UnityEngine;

public class GreenZone : MonoBehaviour
{
    public bool GreenZoneStart;
    public bool GreenZoneMidSave;
    public bool GreenZoneEnd;

    void Start()
    {
        if (GreenZoneStart)
            gameObject.AddComponent<GreenZoneStart>();
        if (GreenZoneMidSave)
            gameObject.AddComponent<GreenZoneMidSave>();
        if (GreenZoneEnd)
            gameObject.AddComponent<GreenZoneEnd>();
    }
}

public class GreenZoneStart : MonoBehaviour
{

}

public class GreenZoneMidSave : MonoBehaviour
{

}

public class GreenZoneEnd : MonoBehaviour
{

}
