using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public EnemyData enemyData;

    public float attackTimer = 0;
    public bool isContact = false;//�Ƿ�Ӵ������
    public bool isCooling = false;//������ȴ

    public float skillTimer = 0;
    public bool skilling = false;//�Ƿ����ͷż�����



    public void Awake()
    {
    }
    public void Start()
    {
        
    }

    public void Update()
    {
        if (Player.Instance.isDead) return;

        Move();
        if (isContact && !isCooling) Attack();
        if (isCooling)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }

        UpdateSkill();
    }
    /// <summary>
    /// ���þ�Ӣ��
    /// </summary>
    public void SetElite()
    {
        enemyData.hp *= 2;
        enemyData.damage *= 2;

        GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 113 / 255f, 113 / 255f);
    }
    /// <summary>
    /// ���¼����˶�
    /// </summary>
    private void UpdateSkill()
    {
        if (enemyData.skillTime < 0) return;

        if (skillTimer <= 0)
        {
            float dis = Vector2.Distance(transform.position, Player.Instance.transform.position);
            if (dis <= enemyData.range)
            {
                Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
                LaunchSkill(dir);

                skillTimer = enemyData.skillTime;
            }
        }
        else
        {
            skillTimer -= Time.deltaTime;

            if (skillTimer < 0) skillTimer = 0;
        }
    }
    /// <summary>
    /// �ͷż���
    /// </summary>
    /// <param name="dir"></param>
    public virtual void LaunchSkill(Vector2 dir) { }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isContact = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isContact = false;
    }
    /// <summary>
    /// �ƶ�
    /// </summary>
    public void Move()
    {
        if (skilling) return;

        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        transform.Translate(direction * enemyData.speed * Time.deltaTime);

        TurnAround();
    }
    /// <summary>
    /// ת��
    /// </summary>
    public void TurnAround()
    {
        if (Player.Instance.transform.position.x - transform.position.x >= 0.1)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Player.Instance.transform.position.x - transform.position.x < 0.1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    /// <summary>
    /// ����
    /// </summary> 
    public void Attack()
    {
        if (isCooling) return;

        Player.Instance.Injured(enemyData.damage);

        isCooling = true;
        attackTimer = enemyData.attackTime;
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Dead()
    {
        GameManager.Instance.exp += enemyData.provideExp * GameManager.Instance.propData.expMuti;
        GamePanel.Instance.RenewExp();

        Instantiate(GameManager.Instance.money_prefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="attack"></param>
    public void Injured(float attack)
    {
        //if (isDead) return;

        if (enemyData.hp - attack <= 0)
        {
            enemyData.hp = 0;
            Dead();
        }
        else
        {
            enemyData.hp -= attack;
        }
    }
}
