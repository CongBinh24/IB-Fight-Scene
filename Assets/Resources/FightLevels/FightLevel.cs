using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFightLevel", menuName = "Boxing/FightLevel")]
public class FightLevel : ScriptableObject
{
    public int levelIndex;
    //public string levelName;

    [System.Serializable]
    public class OpponentData
    {
        public GameObject opponentPrefab;
        public int opponentHealth;
        public int opponentSpeed;
        public int opponentPower;
        public Vector2 spawnPosition;
    }

    public List<OpponentData> OpponentDatas;
}
