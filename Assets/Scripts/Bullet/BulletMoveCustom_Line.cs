using UnityEngine;

public class BulletMoveCustom_Line : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool IsHorizontal = false;
    public bool IsInverse = false;
    [Range(0, 20)]
    public float speed = 10f;
    private float nowSpeed;
    public Vector2 velocity;

    [Range(-10, 10)]
    public float maxCoordination = 1f;
    [Range(-10, 10)]
    public float minCoordination = -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        nowSpeed = IsInverse ? -speed : speed;

        if (IsHorizontal)
        {
            if (transform.position.x <= minCoordination)
                velocity = new Vector2(InverseSpeed(false), 0);
            else if (transform.position.x >= maxCoordination)
                velocity = new Vector2(InverseSpeed(true), 0);
        }
        else
        {
            if (transform.position.y <= minCoordination)
                velocity = new Vector2(0, InverseSpeed(false));
            else if (transform.position.y >= maxCoordination)
                velocity = new Vector2(0, InverseSpeed(true));
        }
        rb.velocity = velocity;
    }

    private float InverseSpeed(bool IsNowInverse)
    {
        if (IsInverse)
        {
            if (IsNowInverse)
                return nowSpeed;
            else
                return -nowSpeed;
        }
        else
        {
            if (IsNowInverse)
                return -nowSpeed;
            else
                return nowSpeed;
        }
    }
}
