using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Transform gameGUI; // Canvas hoặc GameObject chứa HealthBar
    public GameObject healthBarPrefab; // Prefab của HealthBar
    public Transform enemyHealthPanel;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadLevel(int levelIndex)
    {
        // Load data level (nếu có)
        FightLevel levelData = Resources.Load<FightLevel>($"FightLevels/Level_{levelIndex}");
        if (levelData == null)
        {
            Debug.LogError($"Level {levelIndex} không tồn tại!");
            return;
        }

        // Spawn các opponent
        foreach (var opponentData in levelData.OpponentDatas)
        {
            // 1️⃣ Spawn Enemy 3D trong scene
            Vector3 spawnPos = new Vector3(opponentData.spawnPosition.x, opponentData.spawnPosition.y, 0f); // Sàn đấu dùng Z
            GameObject opponent = Instantiate(opponentData.opponentPrefab, spawnPos, Quaternion.identity, gameGUI);

            RectTransform rectTransform = opponent.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(opponentData.spawnPosition.x, opponentData.spawnPosition.y);
            }
            else
            {
                opponent.transform.localPosition = new Vector3(opponentData.spawnPosition.x, opponentData.spawnPosition.y, 0f);
            }

            // 2️⃣ Lấy OpponentController và set data
            OpponentController controller = opponent.GetComponent<OpponentController>();
            if (controller != null)
            {
                controller.SetData(opponentData.opponentHealth, opponentData.opponentSpeed, opponentData.opponentPower);
            }

            // 3️⃣ Spawn HealthBar UI trong EnemyHealthPanel (nằm im trong UI)
            GameObject healthBarGO = Instantiate(healthBarPrefab, enemyHealthPanel);

            // 4️⃣ Thiết lập HealthBar
            HealthBar healthBar = healthBarGO.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetMaxHealth(opponentData.opponentHealth);
            }

            // 5️⃣ Gán HealthBar cho enemy
            controller.SetHealthBar(healthBar);
        }
    }


}
