using UnityEngine;

public class BulletMoveCustom_Line_LocalPos : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;

    public bool IsInverse = false;
    [Range(0, 20)]
    public float speed = 10f;
    private float nowSpeed;
    public Vector2 velocity;
    [Range(0, 20)]
    public int Distance = 1;

    void Awake()
    {
        Inverse();
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        tr = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        nowSpeed = IsInverse ? -speed : speed;

        if (tr.localPosition.y <= 0)
            velocity = new Vector2(0, nowSpeed);
        else if (tr.localPosition.y >= Distance)
            velocity = new Vector2(0, -nowSpeed);

        rb.velocity = velocity;
    }

    private void Inverse() => transform.eulerAngles = IsInverse ? new Vector3(180, 0, 0) : Vector3.zero;
}
