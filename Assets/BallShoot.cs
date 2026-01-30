using UnityEngine;
using TMPro;

public class BallLauncher : MonoBehaviour
{
    public bool shoot = true; // true = WIN, false = MISS
    public Vector3 targetPosition = new Vector3(-5f, 510f, -70f);
    public float flightTime = 1.2f;
    public float missedShotHeight = 3f;
    public float missedShotTime = 1.5f;
    public TextMeshPro shootResultText;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (shoot)
        {
            LaunchSuccessfulShot();
            Invoke("ShowWinText", flightTime + 1f);
        }
        else
        {
            LaunchMissedShot();
            Invoke("ShowMissText", missedShotTime + 1f);
        }
    }

    void ShowWinText()
    {
        ShowResultText("WIN!");
    }

    void ShowMissText()
    {
        ShowResultText("MISS!");
    }

    void LaunchSuccessfulShot()
    {
        Vector3 displacement = targetPosition - transform.position;
        Vector3 velocity = new Vector3(
            displacement.x / flightTime,
            displacement.y / flightTime + 0.5f * Mathf.Abs(Physics.gravity.y) * flightTime,
            displacement.z / flightTime
        );
        rb.linearVelocity = velocity;
    }

    void LaunchMissedShot()
    {
        Vector3 missedTarget = new Vector3(
            targetPosition.x,
            transform.position.y,
            -400f
        );
        Vector3 displacement = missedTarget - transform.position;
        float vy = (missedShotHeight + 0.5f * Mathf.Abs(Physics.gravity.y) * missedShotTime * missedShotTime) / missedShotTime;
        float vx = displacement.x / missedShotTime;
        float vz = displacement.z / missedShotTime;
        rb.linearVelocity = new Vector3(vx, vy, vz);
    }

    void ShowResultText(string message)
    {
        if (shootResultText != null)
        {
            shootResultText.text = message;
            shootResultText.transform.position = new Vector3(-130f, 314f, 0f);
        }
    }
}