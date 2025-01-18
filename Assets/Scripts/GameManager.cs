using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public RoleData currentRole;
    public List<WeaponData> currentWeapons = new List<WeaponData>();
    public DifficultyData currentDifficulty;
    public int currentWave = 1;
    public GameObject enemyBullet_prefab;
    public GameObject money_prefab;
    public GameObject redCircle_prefab;

    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public TextAsset enemyTextAsset;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        enemyTextAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyDatas = JsonConvert.DeserializeObject<List<EnemyData>>(enemyTextAsset.text);

        enemyBullet_prefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        money_prefab = Resources.Load<GameObject>("Prefabs/Money");
        redCircle_prefab = Resources.Load<GameObject>("Prefabs/RedCircle");
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public object RandomOne<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }

        Random random = new Random();
        int index = random.Next(0, list.Count);

        return list[index];
    }
}
