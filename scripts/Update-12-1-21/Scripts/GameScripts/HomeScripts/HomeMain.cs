using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

public class HomeMain
{
    CinemachineVirtualCamera cam;
    CinemachineTransposer Transposer;
    public HomeMain(CinemachineVirtualCamera cam)
    {
        this.cam = cam;
        Transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        Transposer.m_FollowOffset = Constants.CameraConstants.HomeDefault;
    }

    public void Controls()
    {
        CameraControls(cam);
    }

    private void CameraControls(CinemachineVirtualCamera cam)
    {

        Transposer.m_FollowOffset.y -= Input.mouseScrollDelta.y * 2;
        if (Transposer.m_FollowOffset.y > 25)
        {
            Transposer.m_FollowOffset.y = 25;
        }
        if (Transposer.m_FollowOffset.y < 3)
        {
            Transposer.m_FollowOffset.y = 3;
        }
    }
}
