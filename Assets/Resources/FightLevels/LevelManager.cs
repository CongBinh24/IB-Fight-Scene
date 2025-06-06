using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("References")]
    public Transform gameGUI;             
    public Transform enemyHealthPanel;   
    public GameObject healthBarPrefab;     

    private int currentLevel = 1;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }


    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;

        FightLevel levelData = Resources.Load<FightLevel>($"FightLevels/Level_{levelIndex}");
        if (levelData == null)
        {
            Debug.LogError($"Level {levelIndex} không tồn tại!");
            return;
        }

        foreach (var opponentData in levelData.OpponentDatas)
        {
            // Spawn Enemy
            Vector3 spawnPos = new Vector3(opponentData.spawnPosition.x, opponentData.spawnPosition.y, 0f);
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

            //Lấy script Enemy
            Enemy enemyScript = opponent.GetComponent<Enemy>();
            if (enemyScript == null)
            {
                Debug.LogWarning("Prefab Enemy thiếu script Enemy!");
                continue;
            }

            enemyScript.maxHealth = opponentData.opponentHealth; 
            enemyScript.currentHealth = opponentData.opponentHealth;

            enemyScript.speed = opponentData.opponentSpeed;
            enemyScript.power = opponentData.opponentPower;

            GameObject healthBarGO = Instantiate(healthBarPrefab, enemyHealthPanel);

            EnemyHealthBar enemyHealthBar = healthBarGO.GetComponent<EnemyHealthBar>();
            if (enemyHealthBar != null)
            {
                enemyHealthBar.SetMaxHealth(opponentData.opponentHealth);

                enemyHealthBar.UpdateHealth(opponentData.opponentHealth);

                enemyScript.healthBar = enemyHealthBar;
            }
        }


    }
}
