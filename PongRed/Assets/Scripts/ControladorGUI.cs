using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ControladorGUI : MonoBehaviour
{
    public GameObject GUIMenuPrincipal;
    public void OnClickBtnCliente(){
        NetworkManager.Singleton.StartClient();
        GUIMenuPrincipal.SetActive(false);
    }

    public void OnClickBtnServidor(){
        NetworkManager.Singleton.StartServer();
        GUIMenuPrincipal.SetActive(false);
    }

    public void OnClickBtnHost(){
        NetworkManager.Singleton.StartHost();
        GUIMenuPrincipal.SetActive(false);
    }
}
