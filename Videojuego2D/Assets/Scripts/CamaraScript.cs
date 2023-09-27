using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject Owlet;
    void Update()
    {
        if (Owlet != null)
        {
            Vector3 position = transform.position;
            position.x = Owlet.transform.position.x;
            transform.position = position;
        }
    }
}
