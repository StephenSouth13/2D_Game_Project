using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float rotatePower = 500f;
    public float stopPower = 50f;

    private Rigidbody2D rbody;
    private bool isRotating;
    private float timer;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= stopPower * Time.deltaTime;
            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && isRotating)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                GetReward();
                isRotating = false;
                timer = 0;
            }
        }
    }

    public void Rotate()
    {
        if (!isRotating)
        {
            rbody.AddTorque(rotatePower, ForceMode2D.Impulse);
            isRotating = true;
        }
    }

    private void GetReward()
    {
        float rot = transform.eulerAngles.z;
        int sectorSize = 45;
        float offset = 22.5f;

        int index = Mathf.FloorToInt((rot + offset) / sectorSize) % 8;
        int rewardAngle = index * sectorSize;

        GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, rewardAngle);

        int[] rewardScores = { 100, 200, 300, 400, 500, 600, 700, 800 };
        Win(rewardScores[index]);
    }

    private void Win(int score)
    {
        Debug.Log("You won: " + score);
    }
}