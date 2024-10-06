using UnityEngine;

public class BulletMoveCustom_LineTimePerfect : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool IsHorizontal = false;
    public bool IsInverse = false;
    public float speed;
    public Vector2 velocity;

    public float distance = 1f;
    public float moveTime = 1f;

    private float minCoordination;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateSpeed();
        InitializeMinCoordination();
    }

    void Update()
    {
        Vector2 localPosition = transform.localPosition;
        float parentRotationZ = transform.parent ? transform.parent.eulerAngles.z : 0f;
        Vector2 direction = Quaternion.Euler(0, 0, parentRotationZ) * (IsHorizontal ? Vector2.right : Vector2.up);

        if (IsHorizontal)
        {
            if (localPosition.x <= minCoordination)
                velocity = direction * InverseSpeed(false);
            else if (localPosition.x >= minCoordination + distance)
                velocity = direction * InverseSpeed(true);
        }
        else
        {
            if (localPosition.y <= minCoordination)
                velocity = direction * InverseSpeed(false);
            else if (localPosition.y >= minCoordination + distance)
                velocity = direction * InverseSpeed(true);
        }
        rb.velocity = velocity;
    }

    private float InverseSpeed(bool IsNowInverse)
    {
        IsInverse = IsNowInverse;
        return IsNowInverse ? -speed : speed;
    }

    private void CalculateSpeed() => speed = distance / moveTime;

    private void InitializeMinCoordination()
    {
        if (IsHorizontal)
            minCoordination = transform.localPosition.x;
        else
            minCoordination = transform.localPosition.y;
    }
}