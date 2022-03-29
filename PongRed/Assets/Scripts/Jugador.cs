using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5;
    public float TOPE = 3.9F;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float entradaVertical = Input.GetAxis("Vertical");

        Vector3 siguientePosicion = this.transform.position + (Vector3.up * (entradaVertical * Time.deltaTime * velocidad));
        if (Mathf.Abs(siguientePosicion.y) < TOPE)
        {
            this.transform.position = siguientePosicion;
        }
    }
}
