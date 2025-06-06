using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("References")]
    public Transform gameGUI;              // Bố trí các Enemy (nếu có)
    public Transform enemyHealthPanel;     // Panel chứa thanh máu Enemy
    public GameObject healthBarPrefab;     // Prefab HealthBar của Enemy

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadLevel(int levelIndex)
    {
        // Load data level (từ Resources)
        FightLevel levelData = Resources.Load<FightLevel>($"FightLevels/Level_{levelIndex}");
        if (levelData == null)
        {
            Debug.LogError($"Level {levelIndex} không tồn tại!");
            return;
        }

        // Lặp qua các đối tượng Enemy cần spawn
        foreach (var opponentData in levelData.OpponentDatas)
        {
            // 1️⃣ Spawn Enemy
            Vector3 spawnPos = new Vector3(opponentData.spawnPosition.x, opponentData.spawnPosition.y, 0f);
            GameObject opponent = Instantiate(opponentData.opponentPrefab, spawnPos, Quaternion.identity, gameGUI);

            // 2️⃣ Điều chỉnh vị trí (nếu có RectTransform)
            RectTransform rectTransform = opponent.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(opponentData.spawnPosition.x, opponentData.spawnPosition.y);
            }
            else
            {
                opponent.transform.localPosition = new Vector3(opponentData.spawnPosition.x, opponentData.spawnPosition.y, 0f);
            }

            // 3️⃣ Lấy script Enemy
            Enemy enemyScript = opponent.GetComponent<Enemy>();
            if (enemyScript == null)
            {
                Debug.LogWarning("Prefab Enemy thiếu script Enemy!");
                continue;
            }

            // 4️⃣ Thiết lập máu tối đa và máu hiện tại
            enemyScript.maxHealth = opponentData.opponentHealth;  // ⭐️ DÒNG THÊM
            enemyScript.currentHealth = opponentData.opponentHealth;

            // 5️⃣ Spawn HealthBar UI trong enemyHealthPanel
            GameObject healthBarGO = Instantiate(healthBarPrefab, enemyHealthPanel);

            // 6️⃣ Gán HealthBar cho Enemy
            EnemyHealthBar enemyHealthBar = healthBarGO.GetComponent<EnemyHealthBar>();
            if (enemyHealthBar != null)
            {
                enemyHealthBar.SetMaxHealth(opponentData.opponentHealth);

                // ⭐️ THÊM DÒNG NÀY để fillbar hiển thị đúng ngay khi spawn
                enemyHealthBar.UpdateHealth(opponentData.opponentHealth);

                enemyScript.healthBar = enemyHealthBar;
            }

            // 7️⃣ (nếu cần) Gán các chỉ số khác
            // enemyScript.speed = opponentData.opponentSpeed;
            // enemyScript.power = opponentData.opponentPower;
        }
    }
}
