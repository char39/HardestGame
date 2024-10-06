using UnityEngine;

public class ClearTime
{
    public float EndTime { get; set; }

    public bool IsPlayed { get { return EndTime == 0; } set { } }

    public ClearTime()
    {

    }
}