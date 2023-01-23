using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CinemachineVirtualCamera;
    
    CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelPerlin;
    float shakerTimer;

    void Start()
    {
        CinemachineBasicMultiChannelPerlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
    
    void Update()
    {
        if(shakerTimer >= 0)
        {
            shakerTimer -= Time.deltaTime;
        }
        else
        {
            CinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }

    }

    public void DoScreenShake(float duration, float intensity)
    {
        shakerTimer = duration;

        if(shakerTimer >= 0)
        {
            CinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        }
    }
}
