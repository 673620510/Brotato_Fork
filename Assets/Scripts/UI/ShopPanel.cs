using Mono.Cecil;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public Transform _weaponsLayout;
    public Transform _itemLayout;

    public List<ItemData> props = new List<ItemData>();
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
        _weaponsLayout = GameObject.Find("WeaponsLayout").transform;
        _itemLayout = GameObject.Find("ItemLayout").transform;
    }

    private void Start()
    {
        _shopText.text = "商店(第" + (GameManager.Instance.currentWave - 1) + "波)";
        _startButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "出发(第" + (GameManager.Instance.currentWave) + "波)";
        _startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("03-GamePlay");
        });
        _moneyText.text = GameManager.Instance.money.ToString();

        SetAttrUI();
        ShowCurrentWeapon();
        ShowCurrentProp();
        RandomProps();

        _refreshButton.onClick.AddListener(() =>
        {
            RefreshItem();
        });
    }

    private void RefreshItem()
    {
        if (GameManager.Instance.money < 3) return;

        GameManager.Instance.money -= 3;
        _moneyText.text = GameManager.Instance.money.ToString();
        RandomProps();
    }

    private void RandomProps()
    {
        props.Clear();
        int propCount = Random.Range(2, 4);
        for (int i = 0; i < propCount; i++)
        {
            props.Add(GameManager.Instance.RandomOne(GameManager.Instance.propDatas) as ItemData);
        }
        for (int i = 0; i < 4 - propCount; i++)
        {
            props.Add(GameManager.Instance.RandomOne(GameManager.Instance.weaponDatas) as ItemData);
        }
        ShowPropUI();
    }

    private void ShowPropUI()
    {
        int index = 0;
        foreach (ItemData item in props)
        {
            _itemLayout.GetChild(index).GetComponent<ItemCardUI>().itemData = item;
            _itemLayout.GetChild(index).GetComponent<ItemCardUI>()._canvasGroup.alpha = 1;
            _itemLayout.GetChild(index).GetChild(1).GetComponent<TMP_Text>().text = item.name;
            if (item is WeaponData)
            {
                _itemLayout.GetChild(index).GetChild(2).GetComponent<TMP_Text>().text = "武器";
                _itemLayout.GetChild(index).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.avatar);
            }
            else
            {
                _itemLayout.GetChild(index).GetChild(2).GetComponent<TMP_Text>().text = "道具";
                _itemLayout.GetChild(index).GetChild(0).GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.propAtlas.GetSprite(item.name);
            }
            _itemLayout.GetChild(index).GetChild(3).GetComponent<TMP_Text>().text = item.describe;
            _itemLayout.GetChild(index).GetChild(4).GetChild(0).GetComponent<TMP_Text>().text = item.price.ToString();
            index++;
        }
    }

    private void ShowCurrentWeapon()
    {
        int count = _weaponsLayout.childCount;
        for (int i = 0; i < count; i++)
        {
            if (i < GameManager.Instance.currentWeapons.Count)
            {
                _weaponsLayout.GetChild(i).GetChild(0).GetComponent<Image>().enabled = true;
                _weaponsLayout.GetChild(i).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.Instance.currentWeapons[i].avatar);
            }
            else
            {
                _weaponsLayout.GetChild(i).GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
        _weaponTitle.text = "武器(" + GameManager.Instance.currentWeapons.Count + "/" + GameManager.Instance.propData.slot + ")";
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
        //等级
        _attrLayout.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = ((int)(GameManager.Instance.exp / 12)).ToString();
        //最大生命值
        _attrLayout.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.maxHp.ToString();
        //生命再生
        _attrLayout.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.revive.ToString();
        //近战伤害
        _attrLayout.GetChild(3).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.short_damge.ToString();
        //远程伤害
        _attrLayout.GetChild(4).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.long_damge.ToString() ;
        //暴击率
        _attrLayout.GetChild(5).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.critical_strikes_probability.ToString();
        //拾取范围
        _attrLayout.GetChild(6).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.pickRange.ToString();
        //速度
        _attrLayout.GetChild(7).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.speedPer.ToString();
        //收获
        _attrLayout.GetChild(8).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.harvest.ToString();
        //经验倍率
        _attrLayout.GetChild(9).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.expMuti.ToString();
        //商店折扣
        _attrLayout.GetChild(10).GetChild(2).GetComponent<TMP_Text>().text = GameManager.Instance.propData.shopDiscount.ToString();
    }
}
