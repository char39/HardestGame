using System.Collections;
using UnityEngine;

public class Bullet_Room23 : MonoBehaviour
{
    public float rotationTime;
    private float speed;

    public bool inverse = true;

    public float rotationAmount = 0;
    public float currentAngle;

    void Start()
    {
        speed = 360 / rotationTime;
    }

    void Update()
    {
        ConditionCheck();
        Rotation();
    }

    private void Rotation()
    {
        rotationAmount = inverse ? speed * Time.deltaTime : -speed * Time.deltaTime;
        currentAngle += rotationAmount;
        transform.Rotate(0, 0, rotationAmount);
    }

    private void ConditionCheck()
    {
        if (currentAngle < -360)
            inverse = true;
        else if (currentAngle > 0)
            inverse = false;
    }
}
