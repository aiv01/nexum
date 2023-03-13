using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    [SerializeField] public static CinemachineVirtualCamera ActiveCamera = null;


    public static bool IsActiveCam(CinemachineVirtualCamera cam)
    {
        return cam == ActiveCamera;
    }

    public static void SwitchCam(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        ActiveCamera = cam;

        foreach (CinemachineVirtualCamera c in cameras)
        {
            if(c != cam && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }
    
    public static void Register(CinemachineVirtualCamera cam)
    {
        cameras.Add(cam);
    }

    public static void UnRegister(CinemachineVirtualCamera cam)
    {
        cameras.Remove(cam);
    }
}
