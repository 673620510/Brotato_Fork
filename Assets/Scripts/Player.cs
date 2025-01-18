using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float speed = 5f;
    public Transform playerVisual;
    public Animator anim;
    public float hp = 15f;
    public bool isDead = false;

    public int money = 30;
    public float maxHp = 15f;
    public float exp = 0;


    private void Awake()
    {
        Instance = this;
        playerVisual = GameObject.Find("PlayerVisual").transform;
        anim = playerVisual.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDead) return;

        Move();
    }
    public void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 moveMent = new Vector2(moveHorizontal, moveVertical);

        moveMent.Normalize();
        transform.Translate(moveMent * speed * Time.deltaTime);

        if (moveMent.magnitude != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        TurnAround(moveHorizontal);
    }
    public void TurnAround(float h)
    {
        if (h == -1)
        {
            playerVisual.localScale = new Vector3(-1, playerVisual.localScale.y, playerVisual.localScale.z);
        }
        else if (h == 1)
        {
            playerVisual.localScale = new Vector3(1, playerVisual.localScale.y, playerVisual.localScale.z);
        }
    }
    public void Injured(float attack)
    {
        if (isDead) return;

        if (hp - attack <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            hp -= attack;
        }

        GamePanel.Instance.RenewHp();
    }
    public void Attack()
    {

    }
    public void Dead()
    {
        isDead = true;
        anim.speed = 0;
        LevelController.Instance.BadGame();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Money"))
        {
            Destroy(collision.gameObject);
            money += 1;
            GamePanel.Instance.RenewMoney();
        }
    }
}
