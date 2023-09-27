using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkMonsterScript : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public GameObject Owlet;
    private Animator Animator;
    private float LastShoot;
    private int Health = 3;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Owlet == null) return;
        Vector3 direction = Owlet.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Owlet.transform.position.x - transform.position.x);

        if(distance < 1.0f && Time.time > LastShoot + 0.5f)
        {
            Shoot();
            LastShoot = Time.time;
            Animator.SetBool("attacking", true);
        }
        else
        {
            Animator.SetBool("attacking", false);
        }

    }

    private void Shoot()
    {
        Debug.Log("Disparo");
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

    public void Golpe()
    {
        Health = Health - 1;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
