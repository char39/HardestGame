using System.Collections;
using UnityEngine;

public class BulletMoveCustom_RotateTimePerfect : MonoBehaviour
{
    private float rotationSpeed;
    public float time = 2.166f;
    public bool isClockwise = false;

    private float rotationAmount = 0;

    void Start()
    {
        rotationSpeed = 360f / time;
    }

    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        rotationAmount = rotationSpeed * Time.deltaTime;
        rotationAmount = isClockwise ? -rotationAmount : rotationAmount;
        transform.Rotate(0, 0, rotationAmount);
    }
}
