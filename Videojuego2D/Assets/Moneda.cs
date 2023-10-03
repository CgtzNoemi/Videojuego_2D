using UnityEngine;

public class Moneda : MonoBehaviour
{
    int ContadorMonedas;
    public void Recolectar()
    {

        ContadorMonedas++;
        Destroy(gameObject);
    }
}

