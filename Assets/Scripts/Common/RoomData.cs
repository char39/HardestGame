using System.Collections.Generic;
using UnityEngine;

public static class RoomData
{
    public readonly static Dictionary<int, Data> roomData;
    
    static RoomData()
    {
        roomData = new Dictionary<int, Data>()
        {
            { 1, new Data(new Vector2(-7.5f, 0f), 0, "You don't know what you're getting into.") },
            { 2, new Data(new Vector2(-7.5f, 0f), 1, "Don't even bother trying.") },
            { 3, new Data(Vector2.zero, 1, "I can almost guarantee you will fall.") },
            { 4, new Data(new Vector2(0f, 5.5f), 3, "That one was easy.") },
        };
    }
}