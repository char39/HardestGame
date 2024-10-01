using UnityEngine;

public class BulletMoveCustom_Square_LocalPos : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;

    private const bool Clockwise = true;
    [Range(0, 20)]
    public float speed = 10f;
    private Vector2 velocity;

    [Range(0, 20)]
    public float DistanceX = 1;
    [Range(0, 20)]
    public float DistanceY = 1;

    private float distX;
    private float distY;
    private Vector2 distVec;

    public enum Direction { Up, Left, Down, Right }
    public Direction currentDirection = Direction.Up;
    private Direction startDirection;

    void Awake()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        tr = transform.GetChild(0).GetComponent<Transform>();
    }

    void Start()
    {
        startDirection = currentDirection;

        switch (startDirection)
        {
            case Direction.Up:
                distX = DistanceX;
                distY = DistanceY;
                break;

            case Direction.Left:
                distX = -DistanceX;
                distY = DistanceY;
                break;

            case Direction.Down:
                distX = -DistanceX;
                distY = -DistanceY;
                break;

            case Direction.Right:
                distX = DistanceX;
                distY = -DistanceY;
                break;
        }
        distVec = new Vector2(distX, distY);
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
                    currentDirection = Clockwise ? Direction.Right : Direction.Left;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Right:
                localVelocity = new Vector2(speed, 0);
                if (tr.localPosition.x > distVec.x)
                {
                    tr.localPosition = new Vector2(distVec.x, tr.localPosition.y);
                    currentDirection = Clockwise ? Direction.Down : Direction.Up;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Down:
                localVelocity = new Vector2(0, -speed);
                if (tr.localPosition.y < 0)
                {
                    tr.localPosition = new Vector2(tr.localPosition.x, 0);
                    currentDirection = Clockwise ? Direction.Left : Direction.Right;
                    localVelocity = Vector2.zero;
                }
                break;

            case Direction.Left:
                localVelocity = new Vector2(-speed, 0);
                if (tr.localPosition.x < 0)
                {
                    tr.localPosition = new Vector2(0, tr.localPosition.y);
                    currentDirection = Clockwise ? Direction.Up : Direction.Down;
                    localVelocity = Vector2.zero;
                }
                break;
        }

        velocity = tr.TransformDirection(localVelocity);
        rb.velocity = velocity;
    }
}