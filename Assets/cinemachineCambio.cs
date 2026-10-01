using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
 

public class cinemachineCambio : MonoBehaviour
{
    [SerializeField] private List<CinemachineCamera> camera;

    [SerializeField]  private CinemachineCamera defaulfacamera;

    public void CambiarCamara(CinemachineCamera camera)
    {
        for (int i = 0; i < this.camera.Count; i++)
        {
            if (this.camera[i] == camera)
            {
                this.camera[i].Priority = 1;
            }
            else
            {
                this.camera[i].Priority = 0;
            }
    }
    }
    public void Resetdefa()
    {
        CambiarCamara(defaulfacamera);
    }



}

