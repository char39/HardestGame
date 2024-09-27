using UnityEngine;

public class Data
{
    public Vector2 StartPos { get; set; }
    public Vector2 MidPos1 { get; set; }
    public Vector2 MidPos2 { get; set; }

    public int RequireCoin { get; set; }
    public int CollectCoin { get; set; }

    public string Sentence { get; private set; }

    public bool ClearCondition { get { return CollectCoin >= RequireCoin; } set { } }
    public bool MidSave1Touched { get; set; }
    public bool MidSave2Touched { get; set; }

    public Data(Vector2 StartPos, int RequireCoin, string Sentence)
    {
        this.StartPos = StartPos;
        this.RequireCoin = RequireCoin;
        this.CollectCoin = 0;
        this.Sentence = Sentence;
    }

    public Data(Vector2 StartPos, Vector2 MidPos1, int RequireCoin, string Sentence)
    {
        this.StartPos = StartPos;
        this.MidPos1 = MidPos1;
        this.RequireCoin = RequireCoin;
        this.CollectCoin = 0;
        this.MidSave1Touched = false;
        this.Sentence = Sentence;
    }

    public Data(Vector2 StartPos, Vector2 MidPos1, Vector2 MidPos2, int RequireCoin, string Sentence)
    {
        this.StartPos = StartPos;
        this.MidPos1 = MidPos1;
        this.MidPos2 = MidPos2;
        this.RequireCoin = RequireCoin;
        this.CollectCoin = 0;
        this.MidSave1Touched = false;
        this.MidSave2Touched = false;
        this.Sentence = Sentence;
    }
}