using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpinWheel : MonoBehaviour
{
    public float spinDuration = 3f;
    public float minSpeed = 500f;
    public float maxSpeed = 1000f;
    public int spinCount = 5;
    public int totalPoint = 0;

    public Button spinButton;
    public Transform wheelTransform;
    public TMP_Text pointText;
    public TMP_Text totalPointText;
    public TMP_Text spinCountText;

    private bool isSpinning = false;
    private float spinTime = 0f;
    private float spinSpeed = 0f;

    void Start()
    {
        spinButton.onClick.AddListener(() => StartSpin());
        UpdateUI();
    }

    void Update()
    {
        if (isSpinning)
        {
            spinTime += Time.deltaTime;
            float t = spinTime / spinDuration;
            float easedSpeed = Mathf.Lerp(spinSpeed, 0, t);
            wheelTransform.Rotate(0, 0, easedSpeed * Time.deltaTime);

            if (spinTime >= spinDuration)
            {
                isSpinning = false;
                spinTime = 0f;
                AwardReward();
                UpdateUI();
            }
        }
    }

    void StartSpin()
    {
        if (spinCount <= 0 || isSpinning) return;

        spinCount--;
        spinSpeed = Random.Range(minSpeed, maxSpeed);
        isSpinning = true;
        spinTime = 0f;
    }

    void AwardReward()
    {
        float angle = wheelTransform.eulerAngles.z % 360f;
        int segment = Mathf.FloorToInt(angle / (360f / 8));//Phân chia cho 12 ô
        int reward = GetRewardBySegment(segment);
        totalPoint += reward;
        pointText.text = "+" + reward.ToString();
    }

    int GetRewardBySegment(int index)
    {
        int[] rewards = { 100, 200, 300, 400, 500, 1000, 5, 3 };
        return rewards[Mathf.Clamp(index, 0, rewards.Length - 1)];
    }

    void UpdateUI()
    {
        totalPointText.text = "Total Point/Spin: " + totalPoint.ToString();
        spinCountText.text = "Spins Left: " + spinCount.ToString();
    }
}