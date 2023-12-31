using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OwletMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float FuerzaDeSalto;
    public float Speed;
    public string nombreEscena = "Perder";

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float MoveDirectionX;
    private bool EnElSuelo;
    private float UltimoDisparo;
    private int Health = 5;
    public Image corazon01, corazon02, corazon03, corazon04, corazon05;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        MoveDirectionX = Input.GetAxisRaw("Horizontal");
        if (MoveDirectionX < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (MoveDirectionX > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("running", MoveDirectionX != 0.0f);
        Animator.SetBool("jumping", Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow));
        


        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            EnElSuelo = true;
        }
        else
            EnElSuelo = false;


        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && EnElSuelo)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > UltimoDisparo + 0.25f)
        {
            Shoot();
            UltimoDisparo = Time.time;
            Animator.SetBool("attacking", true);
        }
        else
        {
            Animator.SetBool("attacking", false);
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * FuerzaDeSalto);
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        Vector3 newPosition = bullet.transform.position;
        newPosition.y -= 0.1f; 
        
        bullet.transform.position = newPosition;
        bullet.GetComponent<bulletScript>().SetDirection(direction);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(MoveDirectionX, Rigidbody2D.velocity.y);
    }


    public void Golpe()
    {
        Animator.SetBool("hurt", true);
        Invoke("AnimHurt", 0.1f);
    }

    void AnimHurt()
    {
        Animator.SetBool("hurt", false);
        if (Health > 0)
        {
            Health--;
            Image[] corazones = { corazon01, corazon02, corazon03, corazon04, corazon05 };

            if (Health < corazones.Length)
            {
                Destroy(corazones[Health].gameObject);
            }
            if (Health == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(nombreEscena);
            }
        }
    }

}



