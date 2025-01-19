using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBase : MonoBehaviour
{
    public WeaponData data;

    public bool isAttack = false;
    public bool isCooling = false;
    public bool isAiming = true;
    public float attackTimer = 0;
    public float moveSpeed;
    public Transform enemy;
    public float originZ;//原始z轴旋转

    public void Awake()
    {
        originZ = transform.eulerAngles.z;
    }
    public void Start()
    {
        data.critical_strikes_probability *= GameManager.Instance.propData.critical_strikes_probability;

        if (data.isLong == 0)
        {
            data.range *= GameManager.Instance.propData.short_range;
            data.damage *= GameManager.Instance.propData.short_damge;
            data.cooling /= GameManager.Instance.propData.short_attackSpeed;
        }
        else if (data.isLong == 1)
        {
            data.range *= GameManager.Instance.propData.long_range;
            data.damage *= GameManager.Instance.propData.long_damge;
            data.cooling /= GameManager.Instance.propData.long_attackSpeed;
        }
    }
    public void Update()
    {
        if (Player.Instance.isDead) return;

        if (isAiming) Aiming();
        if (isAttack && !isCooling) Fire();

        if (isCooling)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= data.cooling)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }
    }

    private void Aiming()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, data.range, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length > 0)
        {
            isAttack = true;
            Collider2D nearestEnemy = enemiesInRange.OrderBy(enemy => Vector2.Distance(transform.position, enemy.transform.position)).First();

            enemy = nearestEnemy.transform;

            Vector2 enemyPos = enemy.position;//敌人坐标
            Vector2 direction = enemyPos - (Vector2)transform.position;//自身与敌人坐标向量
            float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//向量与x轴的角度（从弧度转换为角度）
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleDegrees + originZ);
        }
        else
        {
            isAttack = false;
            enemy = null;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, originZ);
        }
    }
    public virtual void Fire() { }
    public bool CriticalHits()
    {
        float randomValue = Random.Range(0, 1);
        return randomValue < data.critical_strikes_probability;
    }
}