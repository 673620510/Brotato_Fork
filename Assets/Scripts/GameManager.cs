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
    public PropData currentProp = new PropData();
    public DifficultyData currentDifficulty;
    public int currentWave = 1;

    public GameObject enemyBullet_prefab;
    public GameObject money_prefab;
    public GameObject redCircle_prefab;
    public GameObject arrowBullet_prefab;
    public GameObject pistolBullet_prefab;
    public GameObject madicalBullet_prefab;

    public List<RoleData> roleDatas = new List<RoleData>();//角色数据信息
    public TextAsset roleTextAsset;

    public List<DifficultyData> difficultyDatas = new List<DifficultyData>();
    public TextAsset difficultyTextAsset;

    public List<WeaponData> weaponDatas = new List<WeaponData>();//武器数据信息
    public TextAsset weaponTextAsset;

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
        arrowBullet_prefab = Resources.Load<GameObject>("Prefabs/ArrowBullet");
        pistolBullet_prefab = Resources.Load<GameObject>("Prefabs/PistolBullet");
        madicalBullet_prefab = Resources.Load<GameObject>("Prefabs/MedicalBullet");

        //读取json文件
        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        difficultyTextAsset = Resources.Load<TextAsset>("Data/difficulty");
        difficultyDatas = JsonConvert.DeserializeObject<List<DifficultyData>>(difficultyTextAsset.text);

        //读取json文件
        weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
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
