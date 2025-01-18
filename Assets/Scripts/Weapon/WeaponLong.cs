using System;
using UnityEngine;

public class WeaponLong : WeaponBase
{
    public override void Fire()
    {
        if (isCooling) return;

        Vector2 dir = (enemy.position - transform.position).normalized;

        GameObject bullet = GenerateBullet(dir);
    }

    public virtual GameObject GenerateBullet(Vector2 dir)
    {
        return null;
    }
}
