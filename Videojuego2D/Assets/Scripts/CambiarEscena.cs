using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarAEscena(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
    }
}
