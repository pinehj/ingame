using UnityEngine;

// ��Ÿ�ӽ� ������ �ʴ°��� SO�� �����ϸ�:
// - ��ȹ�ڰ� �����Ϳ��� ���� ������ �����ϴ�.
// - ���������� Ȯ�强�� �����Ѵ�.
// - ������ ��ü(Achievement)�� ����(CurrentValue, isClaimed)�� �����ϸ�ȴ�.
[CreateAssetMenu(fileName = "AchievementSO", menuName ="Scriptable Objects/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [SerializeField] private string _id;
    public string ID => _id;

    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;
    
    [SerializeField] private int _goalValue;
    public int GoalValue => _goalValue;

    [SerializeField] private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;
    
    [SerializeField] private int _rewardAmount;
    public int RewardAmount => _rewardAmount;

    [SerializeField] private string[] _traillingAchievementID;
    public string[] TraillingAchievementID => _traillingAchievementID;
}
