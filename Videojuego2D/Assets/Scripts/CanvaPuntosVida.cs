using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class HUB : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    private FirebaseFirestore db;
    
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;

                ObtenerPuntajeDesdeFirestore();

        });
    }

    void Update()
    {
        if (db != null)
        {
            ObtenerPuntajeDesdeFirestore();
        }
    }


    private void ObtenerPuntajeDesdeFirestore()
    {
        db.Collection("puntajes").Document("moneda").GetSnapshotAsync().ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists)
                {
                    if (snapshot.TryGetValue<int>("puntos", out var puntajeFirebase))
                    {
                        puntos.text = puntajeFirebase.ToString();
                    }
                    else
                    {
                        Debug.LogError("No se pudieron obtener los puntos de firebase");
                    }
                }

            }
        });
    }



}
