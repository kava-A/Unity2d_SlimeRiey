using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeatherController : MonoBehaviour
{
    private Material[] newSkybox;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
       Material skybox= RenderSettings.skybox;
    }
    
    
}