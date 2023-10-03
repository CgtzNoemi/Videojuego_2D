using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    private FirebaseFirestore db;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;


                RecuperarPuntajeDesdeFirestore();
            
        });
    }

    private void RecuperarPuntajeDesdeFirestore()
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
                        Debug.LogError("No se pudieron obtener los puntos de Firebase");
                    }
                }
            }
        });
    }

    public void SumarPuntos(int puntosASumar)
    {
        
        int nuevoPuntaje = int.Parse(puntos.text) + puntosASumar;

        
        puntos.text = nuevoPuntaje.ToString();

        
        ActualizarPuntajeEnFirestore(nuevoPuntaje);
    }

    private void ActualizarPuntajeEnFirestore(int nuevoPuntaje)
    {
        Dictionary<string, object> puntajeData = new Dictionary<string, object>
        {
            { "puntos", nuevoPuntaje }
        };

        db.Collection("puntajes").Document("moneda").SetAsync(puntajeData);
    }
}
