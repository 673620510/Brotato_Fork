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
    public string tagName;//��ײ���ı�ǩ����

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
                collision.gameObject.GetComponent<EnemyBase>().Injured(damage);
            }
            Destroy(gameObject);
        }
    }
}
