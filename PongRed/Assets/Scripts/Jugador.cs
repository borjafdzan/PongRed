using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Jugador : NetworkBehaviour
{
    public float velocidad = 5;
    public float TOPE = 3.9F;
    NetworkVariable<Vector3> posicionJugador = new NetworkVariable<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        posicionJugador.OnValueChanged += CambiarPosicionJugador;
        Vector3 posicionInicial = Vector3.zero;
        if (OwnerClientId == 0)
        {
            posicionInicial.x = -10;
        }
        else if (OwnerClientId == 1)
        {
            posicionInicial.x = 10;
        }
        this.transform.position = posicionInicial;
    }

    private void CambiarPosicionJugador(Vector3 previousValue, Vector3 newValue)
    {
        this.transform.position = newValue;
    }

    void Update()
    {
        if (IsOwner)
        {
            float entradaVertical = Input.GetAxis("Vertical");

            Vector3 siguientePosicion = this.transform.position + (Vector3.up * (entradaVertical * Time.deltaTime * velocidad));
            if (Mathf.Abs(siguientePosicion.y) < TOPE)
            {
                this.transform.position = siguientePosicion;
            }
        }

    }

}
