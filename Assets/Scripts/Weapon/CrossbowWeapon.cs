using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowWeapon : WeaponLong
{
    public override GameObject GenerateBullet(Vector2 dir)
    {
        Bullet bullet = Instantiate(GameManager.Instance.arrowBullet_prefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.dir = dir;
        return bullet.gameObject;
    }
}
