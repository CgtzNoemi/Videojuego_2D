using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " colisionó con: " + collision.gameObject.name);
        if (collision.CompareTag("Heroe"))
        {
            OwletMovement owlet = collision.GetComponent<OwletMovement>();
            if (owlet != null)
            {
                owlet.Golpe(); 
            }
        }


        if (collision.CompareTag("Enemigo"))
        {
            PinkMonsterScript pinkMonster = collision.GetComponent<PinkMonsterScript>();
            if (pinkMonster != null)
            {
                pinkMonster.Golpe();
            }

        }
        DestroyBullet();
    }


}

