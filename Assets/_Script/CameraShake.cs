using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float timer;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public static CameraShake instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if(timer > 0) timer -= Time.deltaTime;
        else StopShake();
    }
    public void Shake(float intensity, float shakeTime)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        timer = shakeTime;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
             cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        timer = 0;
    }
}
