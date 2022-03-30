using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Pelota : NetworkBehaviour
{
    float velocidad = 5;
    Rigidbody2D cuerpo;
    // Start is called before the first frame update
    void Start()
    {
        this.cuerpo = GetComponent<Rigidbody2D>();
        if (IsServer)
        {
            this.cuerpo.velocity = Vector2.left * velocidad;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2){
        Debug.Log("Has hecho un punto");
        this.transform.position = new Vector2(30, 30);
        this.cuerpo.velocity = Vector2.zero;
        Invoke("Resetear" , 0.5f);
    }

    private void Resetear(){
        this.transform.position = Vector2.zero;
        this.cuerpo.velocity = Vector2.right * velocidad;
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
