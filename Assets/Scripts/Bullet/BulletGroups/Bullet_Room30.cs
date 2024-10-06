using UnityEngine;

public class Bullet_Room30 : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;

    public enum Direction { Up, Left, Down, Right }
    public Direction currentDirection = Direction.Up;

    public bool IsStartInverseHorizontal = false;
    public bool IsStartInverseVertical = false;

    public bool IsInverseHorizontal = false;
    public bool IsInverseVertical = false;
    
    public Vector2 velocity;

    public float horizontalDistance = 1f;
    public float horizontalMoveTime = 1f;
    public float horizontalSpeed;
    public float verticalDistance = 1f;
    public float verticalMoveTime = 1f;
    public float verticalSpeed;

    private float minHorizontalCoordination;
    private float minVerticalCoordination;

    void Awake()
    {
        tr = transform;
        rb = tr.GetComponent<Rigidbody2D>();
        CalculateSpeed();
        InitializeMinCoordination();

        if (currentDirection == Direction.Up || currentDirection == Direction.Down)
            velocity = new Vector2(0, InverseSpeedVertical(IsStartInverseVertical));
        if (currentDirection == Direction.Right || currentDirection == Direction.Left)
            velocity = new Vector2(InverseSpeedHorizontal(IsInverseHorizontal), 0);
    }

    void Update()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                if (!IsStartInverseVertical && (tr.localPosition.y > minVerticalCoordination + verticalDistance) || IsStartInverseVertical && (tr.localPosition.y < minVerticalCoordination - verticalDistance))
                {
                    int inverseMulti = IsStartInverseVertical ? -1 : 1;
                    tr.localPosition = new Vector2(tr.localPosition.x, minVerticalCoordination + (inverseMulti * verticalDistance));
                    currentDirection = Direction.Right;
                    velocity = new Vector2(InverseSpeedHorizontal(IsStartInverseHorizontal), 0);
                }
                break;

            case Direction.Right:
                if (!IsStartInverseHorizontal && (tr.localPosition.x > minHorizontalCoordination + horizontalDistance) || IsStartInverseHorizontal && (tr.localPosition.x < minHorizontalCoordination - horizontalDistance))
                {
                    int inverseMulti = IsStartInverseHorizontal ? -1 : 1;
                    tr.localPosition = new Vector2(minHorizontalCoordination + (inverseMulti * horizontalDistance), tr.localPosition.y);
                    currentDirection = Direction.Down;
                    velocity = new Vector2(0, InverseSpeedVertical(!IsStartInverseVertical));
                }
                break;

            case Direction.Down:
                if (!IsStartInverseVertical && (tr.localPosition.y < minVerticalCoordination) || IsStartInverseVertical && (tr.localPosition.y > minVerticalCoordination))
                {
                    tr.localPosition = new Vector2(tr.localPosition.x, minVerticalCoordination);
                    currentDirection = Direction.Left;
                    velocity = new Vector2(InverseSpeedHorizontal(!IsStartInverseHorizontal), 0);
                }
                break;

            case Direction.Left:
                if (!IsStartInverseHorizontal && (tr.localPosition.x < minHorizontalCoordination) || IsStartInverseHorizontal && (tr.localPosition.x > minHorizontalCoordination))
                {
                    tr.localPosition = new Vector2(minHorizontalCoordination, tr.localPosition.y);
                    currentDirection = Direction.Up;
                    velocity = new Vector2(0, InverseSpeedVertical(IsStartInverseVertical));
                }
                break;
        }

        rb.velocity = tr.TransformDirection(velocity);
    }

    private float InverseSpeedHorizontal(bool IsNowInverse)
    {
        IsInverseHorizontal = IsNowInverse;
        return IsNowInverse ? -horizontalSpeed : horizontalSpeed;
    }

    private float InverseSpeedVertical(bool IsNowInverse)
    {
        IsInverseVertical = IsNowInverse;
        return IsNowInverse ? -verticalSpeed : verticalSpeed;
    }

    private void CalculateSpeed()
    {
        horizontalSpeed = horizontalDistance / horizontalMoveTime;
        verticalSpeed = verticalDistance / verticalMoveTime;
    }

    private void InitializeMinCoordination()
    {
        minHorizontalCoordination = tr.localPosition.x;
        minVerticalCoordination = tr.localPosition.y;
    }
}