using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    public GameObject prefabMoneda;
    public int cantidadMonedas = 10;

    void Start()
    {
        GenerarMonedas();
    }

    void GenerarMonedas()
    {
        for (int i = 0; i < cantidadMonedas; i++)
        {
            Vector3 posicionAleatoria = new Vector3(Random.Range(10f, 20f), 0f, Random.Range(10f, 20f));
            Instantiate(prefabMoneda, posicionAleatoria, Quaternion.identity);
        }
    }
}
