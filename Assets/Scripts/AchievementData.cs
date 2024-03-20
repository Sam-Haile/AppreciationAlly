using UnityEngine;

[CreateAssetMenu(fileName = "New AchievementData", menuName = "Achievement Data", order = 0)]
public class AchievementData : ScriptableObject
{
    public string badgeName;
    public string description;
    public int bronzeThreshold;
    public int silverThreshold;
    public int goldThreshold;
    public int platinumThreshold;
    public int currentUserProgress;

    public string milestoneOneDesc;
    public string milestoneTwoDesc;
    public string milestoneThreeDesc;
    public string milestoneFourDesc;
}
