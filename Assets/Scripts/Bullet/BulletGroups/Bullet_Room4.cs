using UnityEngine;

public class Bullet_Room4 : MonoBehaviour
{
    public bool Clockwise = true;
    public float rotationSpeed = 100f;

    void Start()
    {
        if (!Clockwise)
            rotationSpeed *= -1;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime);
    }
}
