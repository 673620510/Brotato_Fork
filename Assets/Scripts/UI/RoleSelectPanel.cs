using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : MonoBehaviour
{
    public static RoleSelectPanel Instance;

    public List<RoleData> roleDatas = new List<RoleData>();//��ɫ������Ϣ
    public TextAsset roleTextAsset;

    public Transform _roleList;
    public GameObject role_prefab;

    public TextMeshProUGUI _roleName;
    public Image _avatar;
    public TextMeshProUGUI _roleDescribe;
    public TextMeshProUGUI _text3;

    public CanvasGroup _canvasGroup;
    public CanvasGroup _contentCanvasGroup;
    public GameObject _roleDetails;

    private void Awake()
    {
        Instance = this;

        _roleList = GameObject.Find("RoleList").transform;
        role_prefab = Resources.Load<GameObject>("Prefabs/Role");

        //��ȡjson�ļ�
        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        _roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        _avatar = GameObject.Find("Avatar_Role").GetComponent<Image>();
        _roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
        _text3 = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
        _roleDetails = GameObject.Find("RoleDetails");

        _canvasGroup = GetComponent<CanvasGroup>();
        _contentCanvasGroup = GameObject.Find("RoleContent").GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (RoleData roleData in roleDatas)
        {
            RoleUI r = GameObject.Instantiate(role_prefab, _roleList).GetComponent<RoleUI>();
            r.SetData(roleData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
