using UnityEngine;

public class Bullet_Room9 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;

    [Range(0, 20)]
    public float speed = 10f;
    private Vector2 velocity;

    [Range(0, 20)]
    public float DistanceX = 1;
    [Range(0, 20)]
    public float DistanceY = 1;

    private Vector2 distVec;

    public enum Direction { Up, Left, Down, Right }
    public Direction currentDirection = Direction.Up;

    void Awake()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        tr = transform.GetChild(0).GetComponent<Transform>();
    }

    void Start()
    {
        distVec = new Vector2(DistanceX, DistanceY);
    }

    void Update()
    {
        Vector2 localVelocity = Vector2.zero;

        switch (currentDirection)
        {
            case Direction.Up:
                localVelocity = new Vector2(0, speed);
                if (tr.localPosition.y > distVec.y)
                {
                    tr.localPosition = new Vector2(tr.localPosition.x, distVec.y);
                    currentDirection = Direction.Left;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Left:
                localVelocity = new Vector2(-speed * 1.33f, 0);
                if (tr.localPosition.x < -distVec.x)
                {
                    tr.localPosition = new Vector2(-distVec.x, tr.localPosition.y);
                    currentDirection = Direction.Right;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Down:
                localVelocity = new Vector2(0, -speed);
                if (tr.localPosition.y < 0)
                {
                    tr.localPosition = new Vector2(tr.localPosition.x, 0);
                    currentDirection = Direction.Up;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Right:
                localVelocity = new Vector2(speed * 1.33f, 0);
                if (tr.localPosition.x > 0)
                {
                    tr.localPosition = new Vector2(0, tr.localPosition.y);
                    currentDirection = Direction.Down;
                    localVelocity = Vector2.zero;
                }
                break;
        }

        velocity = tr.TransformDirection(localVelocity);
        rb.velocity = velocity;
    }
}