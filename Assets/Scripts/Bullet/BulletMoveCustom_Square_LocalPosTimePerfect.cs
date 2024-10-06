using UnityEngine;

public class BulletMoveCustom_Square_LocalPosTimePerfect : MonoBehaviour
{
    private Transform child;
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
        child = transform.GetChild(0);
        rb = child.GetComponent<Rigidbody2D>();
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
                if (!IsStartInverseVertical && (child.localPosition.y > minVerticalCoordination + verticalDistance) || IsStartInverseVertical && (child.localPosition.y < minVerticalCoordination - verticalDistance))
                {
                    int inverseMulti = IsStartInverseVertical ? -1 : 1;
                    child.localPosition = new Vector2(child.localPosition.x, minVerticalCoordination + (inverseMulti * verticalDistance));
                    currentDirection = Direction.Right;
                    velocity = new Vector2(InverseSpeedHorizontal(IsStartInverseHorizontal), 0);
                }
                break;

            case Direction.Right:
                if (!IsStartInverseHorizontal && (child.localPosition.x > minHorizontalCoordination + horizontalDistance) || IsStartInverseHorizontal && (child.localPosition.x < minHorizontalCoordination - horizontalDistance))
                {
                    int inverseMulti = IsStartInverseHorizontal ? -1 : 1;
                    child.localPosition = new Vector2(minHorizontalCoordination + (inverseMulti * horizontalDistance), child.localPosition.y);
                    currentDirection = Direction.Down;
                    velocity = new Vector2(0, InverseSpeedVertical(!IsStartInverseVertical));
                }
                break;

            case Direction.Down:
                if (!IsStartInverseVertical && (child.localPosition.y < minVerticalCoordination) || IsStartInverseVertical && (child.localPosition.y > minVerticalCoordination))
                {
                    child.localPosition = new Vector2(child.localPosition.x, minVerticalCoordination);
                    currentDirection = Direction.Left;
                    velocity = new Vector2(InverseSpeedHorizontal(!IsStartInverseHorizontal), 0);
                }
                break;

            case Direction.Left:
                if (!IsStartInverseHorizontal && (child.localPosition.x < minHorizontalCoordination) || IsStartInverseHorizontal && (child.localPosition.x > minHorizontalCoordination))
                {
                    child.localPosition = new Vector2(minHorizontalCoordination, child.localPosition.y);
                    currentDirection = Direction.Up;
                    velocity = new Vector2(0, InverseSpeedVertical(IsStartInverseVertical));
                }
                break;
        }

        rb.velocity = child.TransformDirection(velocity);
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
        minHorizontalCoordination = child.localPosition.x;
        minVerticalCoordination = child.localPosition.y;
    }
}