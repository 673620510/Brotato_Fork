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

    [SerializeField]
    public PropData propData = new PropData();
    public List<PropData> currentProps = new List<PropData>();

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

    public float hp = 15f;
    public int money = 30;
    public float exp = 0;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("多面手"))
        {
            PlayerPrefs.SetInt("多面手", 0);
        }
        if (!PlayerPrefs.HasKey("公牛"))
        {
            PlayerPrefs.SetInt("公牛", 0);
        }


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

    public void InitProp()
    {
        if (currentRole.name == "全能者")
        {
            propData.maxHp += 5;
            propData.speedPer += 0.05f;
            propData.harvest += 8;
        }
        else if (currentRole.name == "斗士")
        {
            propData.short_attackSpeed += 0.5f;
            propData.long_range -= 0.5f;
            propData.short_range -= 0.5f;
            propData.long_damge -= 0.5f;
        }
        else if (currentRole.name == "医生")
        {
            propData.revive += 5f;
            propData.short_attackSpeed -= 0.5f;
            propData.long_attackSpeed -= 0.5f;
        }
        else if (currentRole.name == "公牛")
        {
            propData.maxHp += 20f;
            propData.revive += 15f;
            propData.slot = 0;
        }
        else if (currentRole.name == "多面手")
        {
            propData.long_damge += 0.2f;
            propData.short_damge += 0.2f;
            propData.slot = 12;
        }

        hp = propData.maxHp;
        money = 30;
        exp = 0;
    }
}
