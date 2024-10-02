using System.Collections;
using UnityEngine;

public class Bullet_Room11 : MonoBehaviour
{
    private float rotationSpeed;
    public float waitTime = 0.766f;

    private bool canRotate = true;
    private bool isWaiting = true;

    private float rotationAmount = 0;
    private float currentAngle;

    IEnumerator Start()
    {
        rotationSpeed = 90f / waitTime;
        yield return new WaitForSeconds(0.25f);
        isWaiting = false;
    }

    void Update()
    {
        ConditionCheck();
        Rotation();
    }

    private void Rotation()
    {
        if (canRotate)
        {
            rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationAmount);
        }
    }

    private void ConditionCheck()
    {
        currentAngle = transform.eulerAngles.z;

        if (Mathf.Abs(currentAngle % 90) < rotationAmount && !isWaiting)
        {
            float targetAngle = Mathf.Round(currentAngle / 45) * 45;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);
            canRotate = false;
            isWaiting = true;
            StartCoroutine(WaitAndRotate());
        }
    }

    private IEnumerator WaitAndRotate()
    {
        yield return new WaitForSeconds(waitTime);
        canRotate = true;
        yield return new WaitForSeconds(0.25f);
        isWaiting = false;
    }
}
