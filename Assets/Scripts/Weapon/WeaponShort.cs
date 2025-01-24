using System.Collections;
using UnityEngine;

public class WeaponShort : WeaponBase
{
    private new void Awake()
    {
        base.Awake();

        moveSpeed = 10;
    }
    public override void Fire()
    {
        if (isCooling) return;

        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

        StartCoroutine(GoPosition());

        isCooling = true;
    }
    IEnumerator GoPosition()
    {
        var enemyPos = enemy.position + new Vector3(0, enemy.GetComponent<SpriteRenderer>().size.y / 2, 0);

        while (Vector2.Distance(transform.position, enemyPos) > 0.1f)
        {
            Vector3 direction = (enemyPos - transform.position).normalized;

            Vector3 moveAmount = direction * moveSpeed * Time.deltaTime;

            transform.position += moveAmount;

            yield return null;
        }

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

        isAiming = false;

        StartCoroutine(ReturnPosition());
    }
    IEnumerator ReturnPosition()
    {
        while ((Vector3.zero - transform.localPosition).magnitude > 0.1f)
        {
            Vector3 direction = (Vector3.zero - transform.localPosition).normalized;
            transform.localPosition += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }

        isAiming = true;
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            bool isCritical = CriticalHits();
            if (isCritical)
            {
                collision.GetComponent<EnemyBase>().Injured(data.damage * data.critical_strikes_multiple);

                Number number = Instantiate(GameManager.Instance.number_prefab).GetComponent<Number>();
                number.text.text = (data.damage * data.critical_strikes_multiple).ToString();
                number.text.color = new Color(255 / 255f, 178 / 255f, 0);
                number.transform.position = transform.position;
            }
            else
            {
                collision.GetComponent<EnemyBase>().Injured(data.damage);

                Number number = Instantiate(GameManager.Instance.number_prefab).GetComponent<Number>();
                number.text.text = data.damage.ToString();
                number.text.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                number.transform.position = transform.position;
            }

            Instantiate(GameManager.Instance.attackMusic);
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}