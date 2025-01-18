using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static GamePanel Instance;

    public Slider _hpSlider;
    public Slider _expSlider;
    public TMP_Text _moneyCount;
    public TMP_Text _expCount;
    public TMP_Text _hpCount;
    public TMP_Text _countDown;
    public TMP_Text _waveCount;

    private void Awake()
    {
        Instance = this;
        _hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        _expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        _moneyCount = GameObject.Find("MoneyCount").GetComponent<TMP_Text>();
        _expCount = GameObject.Find("ExpCount").GetComponent<TMP_Text>();
        _hpCount = GameObject.Find("HpCount").GetComponent<TMP_Text>();
        _countDown = GameObject.Find("CountDown").GetComponent<TMP_Text>();
        _waveCount = GameObject.Find("WaveCount").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        RenewExp();
        RenewHp();
        RenewMoney();
        RenewWaveCount();
    }
    private void Update()
    {

    }

    public void RenewExp()
    {
        _expSlider.value = Player.Instance.exp % 12 / 12;
        _expCount.text = "LV." + Player.Instance.exp / 12;
    }
    public void RenewHp()
    {
        _hpCount.text = Player.Instance.hp + "/" + Player.Instance.maxHp;
        _hpSlider.value = Player.Instance.hp / Player.Instance.maxHp;
    }
    public void RenewMoney()
    {
        _moneyCount.text = Player.Instance.money.ToString();
    }

    public void RenewCountDown(float time)
    {
        _countDown.text = time.ToString("F0");
    }
    public void RenewWaveCount()
    {
        _waveCount.text = "µÚ" + GameManager.Instance.currentWave.ToString() + "²¨";
    }
}
