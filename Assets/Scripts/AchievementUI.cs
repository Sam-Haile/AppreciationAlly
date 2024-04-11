using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public AchievementData achievementData;

    public Image badgeMaterial;
    public Image currentNumBg;
    public Image badgeIcon;

    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI currentNum;

    public SelectedBadge selectedBadge;
      

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
            badgeIcon.color = new Color(185f / 255f, 105f / 255f, 81f / 255f);
            currentNumBg.color = new Color(70f / 255f, 31f / 255f, 22f / 255f); // Brown Color
        }
        else if (achievementData.currentUserProgress < achievementData.goldThreshold)
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/silver");
            badgeIcon.color = new Color(227f / 255f, 227f / 255f, 227f / 255f);
            currentNumBg.color = new Color(133f / 255f, 133f / 255f, 133f / 255f); // Silver Color
        }
        else if (achievementData.currentUserProgress < achievementData.platinumThreshold)
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/gold");
            badgeIcon.color = new Color(243f / 255f, 210f / 255f, 112f / 255f);
            currentNumBg.color = new Color(152f / 255f, 119f / 255f, 19f / 255f); // Gold Color
        }
        else
        {
            badgeMaterial.sprite = Resources.Load<Sprite>($"badges/platinum");
            badgeIcon.color = new Color(211f / 255f, 92f / 255f, 241f / 255f);
            currentNumBg.color = new Color(79 / 255f, 26 / 255f, 91 / 255f); // Platinum Color
        }
    }

    // Display selected badge info
    public void OnClick()
    {
        selectedBadge.selectedBadgeName.text = achievementData.badgeName;
        selectedBadge.selectedProgress.text = achievementData.currentUserProgress.ToString();
        selectedBadge.selectedMilestone.sprite = badgeMaterial.sprite;
        selectedBadge.badgeDesc.text = achievementData.description;
        selectedBadge.progressBg.color = currentNumBg.color;
        selectedBadge.icon.sprite = badgeIcon.sprite;
        selectedBadge.icon.color = badgeIcon.color;

        selectedBadge.milestoneBronze.text = achievementData.milestoneOneDesc;
        selectedBadge.numBronze.text = achievementData.bronzeThreshold.ToString();
        
        selectedBadge.milestoneSilver.text = achievementData.milestoneTwoDesc;
        selectedBadge.numSilver.text = achievementData.silverThreshold.ToString();
        
        selectedBadge.milestoneGold.text = achievementData.milestoneThreeDesc;
        selectedBadge.numGold.text = achievementData.goldThreshold.ToString();
        
        selectedBadge.milestonePlatinum.text = achievementData.milestoneFourDesc;
        selectedBadge.numPlatinum.text = achievementData.platinumThreshold.ToString();
    }
}
