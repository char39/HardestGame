using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer sr;
    private SpriteRenderer sr_child;
    private CircleCollider2D col;
    private Color origin;
    private Color origin_child;

    void Start()
    {
        TryGetComponent(out sr);
        transform.GetChild(0).TryGetComponent(out sr_child);
        TryGetComponent(out col);
        origin = sr.color;
        origin_child = sr_child.color;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(PlayerDamage.PlayerTag))
        {
            GameManager.Instance.CollectCoin();
            SoundManagement.Instance.PlayCoinCollected();
            this.sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            this.sr_child.color = new Color(sr_child.color.r, sr_child.color.g, sr_child.color.b, 0);
            this.col.enabled = false;
        }
    }

    public void ResetCoin()
    {
        sr.color = origin;
        sr_child.color = origin_child;
        col.enabled = true;
    }
}
