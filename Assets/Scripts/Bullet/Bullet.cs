using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public float deadTime = 5;
    public float speed = 8;
    public float timer;
    public Vector2 dir = Vector2.zero;
    public string tagName;//碰撞检测的标签对象
    public bool isCritical = false;

    public void Awake()
    {
        
    }
    public void Start()
    {
        
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= deadTime) Destroy(gameObject);

        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            if (tagName == " Player")
            {
                Player.Instance.Injured(damage);
            }
            else if (true)
            {
                if (isCritical)
                {
                    Number number = Instantiate(GameManager.Instance.number_prefab).GetComponent<Number>();
                    number.text.text = damage.ToString();
                    number.text.color = new Color(255 / 255f, 178 / 255f, 0);
                    number.transform.position = transform.position;
                }
                else
                {
                    Number number = Instantiate(GameManager.Instance.number_prefab).GetComponent<Number>();
                    number.text.text = damage.ToString();
                    number.text.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    number.transform.position = transform.position;
                }

                collision.gameObject.GetComponent<EnemyBase>().Injured(damage);
            }
            Destroy(gameObject);
        }
    }
}
