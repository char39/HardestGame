using System.Collections.Generic;

public static class RoomData
{
    public readonly static Dictionary<int, Data> roomData;
    
    static RoomData()
    {
        roomData = new Dictionary<int, Data>()
        {
            { 1, new Data(0) },
            { 2, new Data(1) },
            { 3, new Data(0) },
        };
    }
}