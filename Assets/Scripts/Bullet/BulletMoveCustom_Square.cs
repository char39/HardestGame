using UnityEngine;

public class BulletMoveCustom_Square : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool Clockwise = false;
    [Range(0, 20)]
    public float speed = 10f;
    private float nowSpeed;
    private Vector2 velocity;

    [Range(-10, 10)]
    public float maxCoordination = 1f;
    [Range(-10, 10)]
    public float minCoordination = -1f;

    public enum Direction { Right, Up, Left, Down }
    public Direction currentDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        nowSpeed = speed;
    }

    void Update()
    {
        switch (currentDirection)
        {
            case Direction.Right:
                velocity = new Vector2(nowSpeed, 0);
                if (transform.position.x >= maxCoordination)
                {
                    transform.position = new Vector2(maxCoordination, transform.position.y);
                    currentDirection = Clockwise ? Direction.Down : Direction.Up;
                    velocity = Vector2.zero;
                }
                break;

            case Direction.Up:
                velocity = new Vector2(0, nowSpeed);
                if (transform.position.y >= maxCoordination)
                {
                    transform.position = new Vector2(transform.position.x, maxCoordination);
                    currentDirection = Clockwise ? Direction.Right : Direction.Left;
                    velocity = Vector2.zero;
                }
                break;

            case Direction.Left:
                velocity = new Vector2(-nowSpeed, 0);
                if (transform.position.x <= minCoordination)
                {
                    transform.position = new Vector2(minCoordination, transform.position.y);
                    currentDirection = Clockwise ? Direction.Up : Direction.Down;
                    velocity = Vector2.zero;
                }
                break;

            case Direction.Down:
                velocity = new Vector2(0, -nowSpeed);
                if (transform.position.y <= minCoordination)
                {
                    transform.position = new Vector2(transform.position.x, minCoordination);
                    currentDirection = Clockwise ? Direction.Left : Direction.Right;
                    velocity = Vector2.zero;
                }
                break;
        }

        // 경계를 넘어갔을 때 위치를 고정하기 위함.
        if (transform.position.x > maxCoordination)
            transform.position = new Vector2(maxCoordination, transform.position.y);
        else if (transform.position.x < minCoordination)
            transform.position = new Vector2(minCoordination, transform.position.y);

        if (transform.position.y > maxCoordination)
            transform.position = new Vector2(transform.position.x, maxCoordination);
        else if (transform.position.y < minCoordination)
            transform.position = new Vector2(transform.position.x, minCoordination);
        
        // 이동
        rb.velocity = velocity;
    }
}