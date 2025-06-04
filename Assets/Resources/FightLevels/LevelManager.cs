using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private void Awake() => Instance = this;

    public void LoadLevel(int levelIndex)
    {
        FightLevel levelData = Resources.Load<FightLevel>($"FightLevels/Level_{levelIndex}");
        if (levelData == null)
        {
            Debug.LogError($"Level {levelIndex} không tồn tại!");
            return;
        }

        GameObject opponent = Instantiate(levelData.opponentPrefab, transform.position, Quaternion.identity);
        OpponentController opponentCtrl = opponent.GetComponent<OpponentController>();
        opponentCtrl.Init(levelData.opponentHealth, levelData.opponentSpeed, levelData.opponentPower);

        Debug.Log($"Level {levelData.levelName} đã load!");
    }
}
