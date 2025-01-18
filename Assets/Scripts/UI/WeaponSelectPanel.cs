using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectPanel : MonoBehaviour
{
    public static WeaponSelectPanel Instance;

    public CanvasGroup _canvasGroup;
    public CanvasGroup _contentCanvasGroup;
    public Transform _weaponContent;

    public List<WeaponData> weaponDatas = new List<WeaponData>();//武器数据信息
    public TextAsset weaponTextAsset;

    public GameObject weapon_prefab;
    public Transform _weaponList;

    public Image _weaponAvatar;
    public TextMeshProUGUI _weaponName;
    public TextMeshProUGUI _weaponType;
    public TextMeshProUGUI _weaponDescribe;
    public GameObject _weaponDetails;

    private void Awake()
    {
        Instance = this;

        _canvasGroup = GetComponent<CanvasGroup>();
        _weaponContent = GameObject.Find("WeaponContent").transform;
        _contentCanvasGroup = GameObject.Find("WeaponContent").GetComponent<CanvasGroup>();

        //读取json文件
        weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);

        weapon_prefab = Resources.Load<GameObject>("Prefabs/Weapon");
        _weaponList = GameObject.Find("WeaponList").transform;

        _weaponAvatar = GameObject.Find("Avatar_Weapon").GetComponent<Image>();
        _weaponName = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        _weaponType = GameObject.Find("WeaponType").GetComponent<TextMeshProUGUI>();
        _weaponDescribe = GameObject.Find("WeaponDescribe").GetComponent<TextMeshProUGUI>();

        _weaponDetails = GameObject.Find("WeaponDetails");
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (WeaponData weaponData in weaponDatas)
        {
            WeaponUI w = Instantiate(weapon_prefab, _weaponList).GetComponent<WeaponUI>();
            w.SetData(weaponData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
