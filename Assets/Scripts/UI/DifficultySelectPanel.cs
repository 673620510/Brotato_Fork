using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectPanel : MonoBehaviour
{
    public static DifficultySelectPanel Instance;

    public Transform _difficultyContent;
    public CanvasGroup _canvasGroup;
    public CanvasGroup _contentCanvasGroup;

    public GameObject difficulty_profab;
    public Transform _difficultyList;

    public Image _difficultyAvatar;
    public TextMeshProUGUI _difficultyName;
    public TextMeshProUGUI _difficultyDescribe;

    private void Awake()
    {
        Instance = this;

        _difficultyContent = GameObject.Find("DifficultyContent").transform;
        _canvasGroup= GetComponent<CanvasGroup>();

        _difficultyList = GameObject.Find("DifficultyList").transform;
        difficulty_profab = Resources.Load<GameObject>("Prefabs/Difficulty");

        _difficultyAvatar = GameObject.Find("Avatar_Difficulty").GetComponent<Image>();
        _difficultyName = GameObject.Find("DifficultyName").GetComponent<TextMeshProUGUI>();
        _difficultyDescribe = GameObject.Find("DifficultyDescribe").GetComponent<TextMeshProUGUI>();
        _contentCanvasGroup = GameObject.Find("DifficultyContent").GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        foreach (DifficultyData difficultyData in GameManager.Instance.difficultyDatas)
        {
            DifficultyUI d = Instantiate(difficulty_profab, _difficultyList).GetComponent<DifficultyUI>();
            d.SetData(difficultyData);
        }
    }
}
