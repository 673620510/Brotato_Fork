using System;
using UnityEngine;

public class WeaponLong : WeaponBase
{
    public override void Fire()
    {
        if (isCooling) return;

        Vector2 dir = (enemy.position - transform.position).normalized;

        Instantiate(GameManager.Instance.shootMusic);

        GameObject bullet = GenerateBullet(dir);

        SetZ(bullet);

        bool isCritical = CriticalHits();
        bullet.GetComponent<Bullet>().isCritical = isCritical;

        if (isCritical)
        {
            bullet.GetComponent<Bullet>().damage = data.damage * data.critical_strikes_multiple;
        }
        else
        {
            bullet.GetComponent<Bullet>().damage = data.damage;
        }


        bullet.GetComponent<Bullet>().speed = 15f;

        isCooling = true;
    }

    private void SetZ(GameObject bullet)
    {
        bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, transform.eulerAngles.z - originZ);
    }

    public virtual GameObject GenerateBullet(Vector2 dir)
    {
        return null;
    }
}
