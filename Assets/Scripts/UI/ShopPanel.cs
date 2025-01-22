using Mono.Cecil;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public static ShopPanel Instance;
    public Button _startButton;
    public Button _refreshButton;
    public TMP_Text _shopText;
    public TMP_Text _moneyText;
    public TMP_Text _weaponTitle;

    public Transform _attrLayout;
    public Transform _propsLayout;

    private void Awake()
    {
        Instance = this;
        _startButton = GameObject.Find("StartButton").GetComponent<Button>();
        _refreshButton = GameObject.Find("RefreshButton").GetComponent<Button>();
        _shopText = GameObject.Find("ShopText").GetComponent <TMP_Text>();
        _moneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();
        _weaponTitle = GameObject.Find("WeaponTitle").GetComponent<TMP_Text>();

        _attrLayout = GameObject.Find("AttrLayout").transform;
        _propsLayout = GameObject.Find("PropsLayout").transform;
    }

    private void Start()
    {
        _shopText.text = "�̵�(��" + (GameManager.Instance.currentWave - 1) + "��)";
        _startButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "����(��" + (GameManager.Instance.currentWave) + "��)";
        _startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("03-GamePlay");
        });
        _moneyText.text = GameManager.Instance.money.ToString();

        SetAttrUI();

        ShowCurrentProp();
    }

    private void ShowCurrentProp()
    {
        int count = _propsLayout.childCount;
        for (int i = 0; i < count; i++)
        {
            if (i < GameManager.Instance.currentProps.Count)
            {
                _propsLayout.GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;
                _propsLayout.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.Instance.currentProps[i].avatar);
            }
            else
            {
                _propsLayout.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }

    private void SetAttrUI()
    {
        //�ȼ�
        _attrLayout.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = ((int)(GameManager.Instance.exp / 12)).ToString();
        //�������ֵ
        _attrLayout.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.maxHp.ToString();
        //��������
        _attrLayout.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.revive.ToString();
        //��ս�˺�
        _attrLayout.GetChild(3).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.short_damge.ToString();
        //Զ���˺�
        _attrLayout.GetChild(4).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.long_damge.ToString() ;
        //������
        _attrLayout.GetChild(5).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.critical_strikes_probability.ToString();
        //ʰȡ��Χ
        _attrLayout.GetChild(6).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.pickRange.ToString();
        //�ٶ�
        _attrLayout.GetChild(7).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.speedPer.ToString();
        //�ջ�
        _attrLayout.GetChild(8).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.harvest.ToString();
        //���鱶��
        _attrLayout.GetChild(9).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.expMuti.ToString();
        //�̵��ۿ�
        _attrLayout.GetChild(10).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.shopDiscount.ToString();
    }
}
