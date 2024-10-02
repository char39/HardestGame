using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private SpriteRenderer sr;
    private SpriteRenderer sr_child;
    private Color origin;
    private Color origin_child;

    private BoxCollider2D col;
    public const string PlayerTag = "Player";
    public bool IsAlphaZero = false;
    public bool IsDamage = false;

    void Start()
    {
        TryGetComponent(out sr);
        origin = sr.color;
        transform.GetChild(0).TryGetComponent(out sr_child);
        origin_child = sr_child.color;
        TryGetComponent(out col);
    }

    void Update()
    {
        if (IsAlphaZero)
        {
            IsAlphaZero = false;

            TryGetComponent(out PlayerPosition playerPosition);
            playerPosition.enabled = false;
            playerPosition.enabled = true;

            col.enabled = true;

            sr.color = origin;
            sr_child.color = origin_child;

            GameManager.Instance.ResetCollectCoin();

            Coin[] coins = FindObjectsOfType<Coin>();
            foreach (Coin coin in coins)
                coin.ResetCoin();
        }
    }

    // 닿으면 스르르 사라지고 다사라지면 재시작.
    public IEnumerator Disappear()
    {
        if (IsDamage || IsAlphaZero) yield break;
        IsDamage = true;

        TryGetComponent(out PlayerMove playerMove);
        playerMove.IsMoveStop = true;

        col.enabled = false;

        float duration = 5f;
        float elapsed = 0f;
        float alpha = 0f;

        while (elapsed < (duration * 0.15f))
        {
            elapsed += Time.deltaTime;
            sr.color = Color.Lerp(sr.color, new Color(origin.r, origin.g, origin.b, alpha), elapsed / duration);
            sr_child.color = Color.Lerp(sr_child.color, new Color(origin_child.r, origin_child.g, origin_child.b, alpha), elapsed / duration);
            yield return null;
        }

        IsAlphaZero = true;
        IsDamage = false;
    }
}