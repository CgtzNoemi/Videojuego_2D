using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonedaNivel : MonoBehaviour
{
    public int valor = 1;
    public GameManager gameManager;
    public string nombreEscena = "Ganar1";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Heroe"))
        {
            gameManager.SumarPuntos(valor);
            Destroy(this.gameObject);
            SceneManager.LoadScene(nombreEscena);
        }
    }
}

