using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ControladorSpawnPelota : NetworkBehaviour
{
    public GameObject prefabPelotaRed;
    int cuentaClientes = 0;
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += ConexionCliente;
    }

    private void ConexionCliente(ulong obj)
    {
        Debug.Log(cuentaClientes);
        cuentaClientes++;
        if (cuentaClientes == 1)
        {
            if (IsServer)
            {
                Invoke("SpawnPelota", 0.3f);
            }
        }
    }

    

    private void SpawnPelota()
    {
        GameObject pelota = Instantiate(prefabPelotaRed, Vector3.zero, Quaternion.identity);
        NetworkObject objetoRed = pelota.GetComponent<NetworkObject>();
        objetoRed.Spawn();
    }
   
}
