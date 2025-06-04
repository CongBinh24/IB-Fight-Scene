using UnityEngine;

[CreateAssetMenu(fileName = "NewFightLevel", menuName = "Boxing/FightLevel")]
public class FightLevel : ScriptableObject
{
    public int levelIndex;
    public string levelName;

    public GameObject opponentPrefab;
    public int opponentHealth;
    public float opponentSpeed;
    public int opponentPower;

    public int rewardMoney;
}
