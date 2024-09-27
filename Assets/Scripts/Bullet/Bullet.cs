using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(PlayerDamage.PlayerTag))
        {
            col.gameObject.TryGetComponent(out PlayerDamage playerDamage);
            StartCoroutine(playerDamage.Disappear());

            GameManager.Instance.RestartRoom();
            GameManager.AddDeathCount();
        }
    }
}
