using UnityEngine;

public class BulletMoveTriggerEnter : MonoBehaviour
{
    public const string WallTag = "Wall";
    private Rigidbody2D rb;

    public bool IsHorizontal = false;
    public bool IsInverse = false;
    public float speed = 10f;
    public Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (IsInverse)
            speed = -speed;

        if (IsHorizontal)
            velocity = new Vector2(speed, 0);
        else
            velocity = new Vector2(0, speed);
    }

    void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(WallTag))
            SetSpeed();
    }

    private void SetSpeed()
    {
        speed = -speed;

        if (IsHorizontal)
            velocity = new Vector2(speed, 0);
        else
            velocity = new Vector2(0, speed);
    }
}
