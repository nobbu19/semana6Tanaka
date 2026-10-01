using System;
using UnityEngine;
using Unity.Cinemachine;
public class cambio : MonoBehaviour
{
    [SerializeField] private CinemachineCamera changecamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Console.WriteLine("Cambio de cámara activado");
        {
            collision.gameObject.GetComponent<cinemachineCambio>().CambiarCamara(changecamera);

            
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<cinemachineCambio>().Resetdefa()  ;
        }
    }
}
