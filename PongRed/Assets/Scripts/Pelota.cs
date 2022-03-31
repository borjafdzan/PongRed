using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;



public class Pelota : NetworkBehaviour
{
    float velocidad = 5;
    Rigidbody2D cuerpo;
    
    NetworkVariable<Vector3> posicionPelota = new NetworkVariable<Vector3>();
    public static event Action<int> eventoMarcar;
    // Start is called before the first frame update
    void Start()
    {
        this.cuerpo = GetComponent<Rigidbody2D>();
        this.posicionPelota.OnValueChanged += OnCambioPosicionPelota;
        ControladorPuntuacion.juegoAcabado += OnJuegoAcabado;
        if (IsServer)
        {
            this.cuerpo.velocity = Vector2.left * velocidad;
        }
    }

    private void OnJuegoAcabado()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCambioPosicionPelota(Vector3 previousValue, Vector3 newValue)
    {
        this.transform.position = newValue;
    }

    private void OnTriggerEnter2D(Collider2D collider2){
        Debug.Log("Has hecho un punto");
        this.transform.position = new Vector2(30, 30);
        this.cuerpo.velocity = Vector2.zero;
        if (collider2.gameObject.tag == "ZonaJugador1"){
            eventoMarcar?.Invoke(2);
        } else if (collider2.gameObject.tag == "ZonaJugador2"){
            eventoMarcar?.Invoke(1);
        }
        Invoke("Resetear" , 0.5f);
    }

    private void Resetear(){
        this.transform.position = Vector2.zero;
        this.cuerpo.velocity = Vector2.right * velocidad;

    }
    void Update(){
        CambiarPosicionPelotaServerRpc(this.transform.position);
    }

    [ServerRpc]
    private void CambiarPosicionPelotaServerRpc(Vector3 posicionPelota){
        this.posicionPelota.Value = posicionPelota;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (IsServer)
        {
            float factorGolpeo = FactorGolpeo(transform.position, collision2D.transform.position, collision2D.collider.bounds.size.y);

            Vector2 nuevaDireccion = Vector2.zero;
            if (collision2D.gameObject.tag == "Balla")
            {
                nuevaDireccion = new Vector2(this.transform.position.normalized.x, -this.transform.position.normalized.y);

            }
            else if (collision2D.gameObject.tag == "Jugador")
            {
                nuevaDireccion = new Vector2(-this.transform.position.normalized.x, factorGolpeo).normalized;
            }
            cuerpo.velocity = nuevaDireccion * velocidad;
        }
    }

    private float FactorGolpeo(Vector2 posicionBola, Vector2 posicionRaqueta, float alturaRaqueta)
    {
        return (posicionBola.y - posicionRaqueta.y) / alturaRaqueta;
    }
}
