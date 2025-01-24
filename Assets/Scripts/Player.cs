using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public Transform playerVisual;
    public Animator anim;
    public bool isDead = false;

    public Transform weaponsPos;
    public float reviveTimer;

    private void Awake()
    {
        Instance = this;
        playerVisual = GameObject.Find("PlayerVisual").transform;
        weaponsPos = GameObject.Find("WeaponsPos").transform;
        anim = playerVisual.GetComponent<Animator>();

        if (GameManager.Instance.currentWave == 1)
        {
            GameManager.Instance.InitProp();
        }
    }
    private void Start()
    {
        if (GameManager.Instance.propData.maxHp >= 50)
        {
            if (PlayerPrefs.GetInt("公牛") == 0)
            {
                Debug.Log("公牛解锁");
                PlayerPrefs.SetInt("公牛", 1);
                for (int i = 0; i < GameManager.Instance.roleDatas.Count; i++)
                {
                    if (GameManager.Instance.roleDatas[i].name == "公牛")
                    {
                        GameManager.Instance.roleDatas[i].unlock = 1;
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (isDead || LevelController.Instance.waveTimer <= 0) return;

        Move();
        Revive();
        EatMoney();
    }

    private void Revive()
    {
        reviveTimer += Time.deltaTime;

        if (reviveTimer >= 1f)
        {
            if (GameManager.Instance.propData.revive <= 0) return;

            GameManager.Instance.hp = Mathf.Clamp(GameManager.Instance.hp += GameManager.Instance.propData.revive, 0, GameManager.Instance.propData.maxHp);

            if (GameManager.Instance.currentRole.name == "公牛")
            {
                GameManager.Instance.hp = Mathf.Clamp(GameManager.Instance.hp += GameManager.Instance.propData.revive, 0, GameManager.Instance.propData.maxHp);
            }
        }
        
    }

    public void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 moveMent = new Vector2(moveHorizontal, moveVertical);

        moveMent.Normalize();
        transform.Translate(moveMent * GameManager.Instance.propData.speed * GameManager.Instance.propData.speedPer * Time.deltaTime);

        if (moveMent.magnitude != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        TurnAround(moveHorizontal);
    }
    public void TurnAround(float h)
    {
        if (h == -1)
        {
            playerVisual.localScale = new Vector3(-1, playerVisual.localScale.y, playerVisual.localScale.z);
        }
        else if (h == 1)
        {
            playerVisual.localScale = new Vector3(1, playerVisual.localScale.y, playerVisual.localScale.z);
        }
    }
    public void Injured(float attack)
    {
        if (isDead) return;

        if (GameManager.Instance.hp - attack <= 0)
        {
            GameManager.Instance.hp = 0;
            Dead();
        }
        else
        {
            GameManager.Instance.hp -= attack;
        }

        GamePanel.Instance.RenewHp();
    }
    public void Attack()
    {

    }
    public void Dead()
    {
        isDead = true;
        anim.speed = 0;
        LevelController.Instance.BadGame();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Money"))
    //    {
    //        Destroy(collision.gameObject);
    //        GameManager.Instance.money += 1;
    //        GamePanel.Instance.RenewMoney();
    //    }
    //}
    private void EatMoney()
    {
        Collider2D[] moneyInRange = Physics2D.OverlapCircleAll(transform.position, 0.5f * GameManager.Instance.propData.pickRange, LayerMask.GetMask("Item"));
        if (moneyInRange.Length > 0)
        {
            for (int i = 0; i < moneyInRange.Length; i++)
            {
                Destroy(moneyInRange[i].gameObject);

                GameManager.Instance.money += 1;
                GamePanel.Instance.RenewMoney();
            }
        }
    }
}
