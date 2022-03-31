using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine;

public class ControladorPuntuacion:  NetworkBehaviour
{
    NetworkVariable<int> puntuacionjugador1;
    NetworkVariable<int> puntuacionjugador2;
    public Text textoJugador1;
    public Text textoJugador2;
    public Text textoGanador;
    public GameObject menuGanador;
    public static event Action juegoAcabado;
    // Start is called before the first frame update
    void Start()
    {
        this.puntuacionjugador1 = new NetworkVariable<int>();
        this.puntuacionjugador2 = new NetworkVariable<int>();

        this.puntuacionjugador1.OnValueChanged += OnCambiarJugador1;
        this.puntuacionjugador2.OnValueChanged += OnCambiarJugador2;
        Pelota.eventoMarcar += OnMarcar;
    }

    private void OnCambiarJugador2(int previousValue, int newValue)
    {
        this.textoJugador2.text = newValue+"";
        if (newValue == 5){
            textoGanador.text = "El ganador es el jugador 2";
            juegoAcabado?.Invoke();
            menuGanador.SetActive(true);
        }
    }

    private void OnCambiarJugador1(int previousValue, int newValue)
    {
        this.textoJugador1.text = newValue+"";
        if (newValue == 5){
            textoGanador.text = "El ganador es el jugador 1";
            juegoAcabado?.Invoke();
            menuGanador.SetActive(true);
        }
    }

    private void OnMarcar(int idJugador)
    {
        if (idJugador == 1){
            if (IsServer){
                puntuacionjugador1.Value++;
            }
        } else if (idJugador == 2){
            if (IsServer){
                puntuacionjugador2.Value++;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
