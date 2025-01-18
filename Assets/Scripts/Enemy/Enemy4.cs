using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyBase
{
    public float timer = 0;

    public override void LaunchSkill(Vector2 dir)
    {
        StartCoroutine(Charge(dir));
    }
    IEnumerator Charge(Vector2 dir)
    {
        skilling = true;

        while (timer < 0.6f)
        {
            transform.position += (Vector3)dir * enemyData.speed * 1.8f * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        skilling = false;
    }
}
