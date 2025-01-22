using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalGunWeapon : WeaponLong
{
    private new void Start()
    {
        base.Start();

        if (GameManager.Instance.currentRole.name == "Ò½Éú")
        {
            data.cooling /= 3;
        }
    }
    public override GameObject GenerateBullet(Vector2 dir)
    {
        Bullet bullet = Instantiate(GameManager.Instance.medicalBullet_prefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.dir = dir;
        return bullet.gameObject;
    }
}
