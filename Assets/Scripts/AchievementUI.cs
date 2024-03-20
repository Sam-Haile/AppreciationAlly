using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public AchievementData achievementData;

    public Image badgeMaterial;
    public Image currentNumBg;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI currentNum;

    // Badge close up info
    public TextMeshProUGUI selectedBadgeName;
    public TextMeshProUGUI selectedProgress;
    public Image selectedMilestone;
    public Image progressBg;

    public TextMeshProUGUI badgeDesc;
    public TextMeshProUGUI numBronze;
    public TextMeshProUGUI milestoneBronze;
    public TextMeshProUGUI numSilver;
    public TextMeshProUGUI milestoneSilver;
    public TextMeshProUGUI numGold;
    public TextMeshProUGUI milestoneGold;
    public TextMeshProUGUI numPlatinum;
    public TextMeshProUGUI milestonePlatinum;

    private void Start()
    {
        if (achievementData != null)
        {
            UpdateAchievementUI();
        }
    }

    public void UpdateAchievementUI()
    {
        textMeshPro.text = achievementData.badgeName;
        currentNum.text = achievementData.currentUserProgress.ToString();

        if (achievementData.currentUserProgress < achievementData.silverThreshold)
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/bronze");
            currentNumBg.color = new Color(70 / 255f, 31 / 255f, 22 / 255f); // Brown Color
        }
        else if (achievementData.currentUserProgress < achievementData.goldThreshold)
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/silver");
            currentNumBg.color = new Color(133 / 255f, 133 / 255f, 133 / 255f); // Silver Color
        }
        else if (achievementData.currentUserProgress < achievementData.platinumThreshold)
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/gold");
            currentNumBg.color = new Color(152 / 255f, 119 / 255f, 19 / 255f); // Gold Color
        }
        else
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/platinum");
            currentNumBg.color = new Color(79 / 255f, 26 / 255f, 91 / 255f); // Platinum Color
        }
    }

    public void OnClick()
    {
        selectedBadgeName.text = achievementData.badgeName;
        selectedProgress.text = achievementData.currentUserProgress.ToString();
        selectedMilestone.sprite = badgeMaterial.sprite;
        badgeDesc.text = achievementData.description;
        progressBg.color = currentNumBg.color;

        milestoneBronze.text = achievementData.milestoneOneDesc;
        numBronze.text = achievementData.bronzeThreshold.ToString();

        milestoneSilver.text = achievementData.milestoneTwoDesc;
        numSilver.text = achievementData.silverThreshold.ToString();

        milestoneGold.text = achievementData.milestoneThreeDesc;
        numGold.text = achievementData.goldThreshold.ToString();

        milestonePlatinum.text = achievementData.milestoneFourDesc;
        numPlatinum.text = achievementData.platinumThreshold.ToString();
    }
}
