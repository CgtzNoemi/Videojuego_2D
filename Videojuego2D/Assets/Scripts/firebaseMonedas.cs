using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class firebaseMonedas : MonoBehaviour
{
    int ContadorMonedas;
    FirebaseFirestore db;
    public Text contadorMonedasTexto;

    void Start()
    {
        Debug.Log("Script de monedas iniciado.");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;
            CargarContadorInicial();
        });
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heroe"))
        {
            ContadorMonedas++;
            ActualizarContadorMonedasEnFirestore();
            ActualizarUIContador();
            Destroy(gameObject);
        }
    }

    void CargarContadorInicial()
    {
        if (db != null)
        {
            DocumentReference docRef = db.Collection("monedas").Document("contandoMonedas");

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        // Si existe el documento, obtenemos el valor del contador
                        ContadorMonedas = snapshot.GetValue<int>("ContadorMonedas");
                        ActualizarUIContador();
                    }
                    else
                    {
                        // Si el documento no existe, podemos inicializar el contador a cero u otro valor predeterminado
                        ContadorMonedas = 0;
                        ActualizarUIContador();
                    }
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error al cargar el valor inicial del contador: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("FirebaseFirestore no está inicializado correctamente.");
        }
    }

    void ActualizarContadorMonedasEnFirestore()
    {
        if (db != null)
        {
            DocumentReference docRef = db.Collection("monedas").Document("contandoMonedas");
            Dictionary<string, object> monedas = new Dictionary<string, object>
            {
                { "ContadorMonedas", ContadorMonedas },
            };

            docRef.UpdateAsync(monedas).ContinueWithOnMainThread(task => {
                if (task.IsCompleted)
                {
                    Debug.Log("Monedas recolectadas: " + ContadorMonedas);
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error al actualizar monedas en Firestore: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("FirebaseFirestore no está inicializado correctamente.");
        }
    }

    void ActualizarUIContador()
    {
        if (contadorMonedasTexto != null)
        {
            contadorMonedasTexto.text = ContadorMonedas.ToString();
        }
        else
        {
            Debug.LogWarning("El objeto contadorMonedasTexto no está asignado en el inspector.");
        }
    }
}
