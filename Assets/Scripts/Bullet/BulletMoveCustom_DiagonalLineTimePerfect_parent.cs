using UnityEngine;

public class BulletMoveCustom_DiagonalLineTimePerfect_parent : MonoBehaviour
{
    private Transform child;
    private Rigidbody2D rb;

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
    }

    void Update()
    {
        Vector2 localPosition = child.localPosition;
        float parentRotationZ = transform ? transform.eulerAngles.z : 0f;
        Vector2 horizontalDirection = Quaternion.Euler(0, 0, parentRotationZ) * Vector2.right;
        Vector2 verticalDirection = Quaternion.Euler(0, 0, parentRotationZ) * Vector2.up;

        LocalPositionX(IsStartInverseHorizontal);
        LocalPositionY(IsStartInverseVertical);

        void LocalPositionX(bool inverse)   // 로컬 함수
        {
            if (!inverse && (localPosition.x <= minHorizontalCoordination) || inverse && (localPosition.x >= minHorizontalCoordination))
            {
                child.localPosition = new Vector2(minHorizontalCoordination, child.localPosition.y);
                velocity.x = horizontalDirection.x * InverseSpeedHorizontal(inverse);
            }
            else if (!inverse && (localPosition.x >= minHorizontalCoordination + horizontalDistance) || inverse && (localPosition.x <= minHorizontalCoordination - horizontalDistance))
            {
                int inverseMulti = inverse ? -1 : 1;
                child.localPosition = new Vector2(minHorizontalCoordination + (inverseMulti * horizontalDistance), child.localPosition.y);
                velocity.x = horizontalDirection.x * InverseSpeedHorizontal(!inverse);
            }
        }

        void LocalPositionY(bool inverse)   // 로컬 함수
        {
            if (!inverse && (localPosition.y <= minVerticalCoordination) || inverse && (localPosition.y >= minVerticalCoordination))
            {
                child.localPosition = new Vector2(child.localPosition.x, minVerticalCoordination);
                velocity.y = verticalDirection.y * InverseSpeedVertical(inverse);
            }
            else if (!inverse && (localPosition.y >= minVerticalCoordination + verticalDistance) || inverse && (localPosition.y <= minVerticalCoordination - verticalDistance))
            {
                int inverseMulti = inverse ? -1 : 1;
                child.localPosition = new Vector2(child.localPosition.x, minVerticalCoordination + (inverseMulti * verticalDistance));
                velocity.y = verticalDirection.y * InverseSpeedVertical(!inverse);
            }
        }

        rb.velocity = velocity;
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