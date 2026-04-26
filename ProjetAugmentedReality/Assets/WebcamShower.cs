using UnityEngine;

public class WebcamShower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        Debug.Log(webcamTexture.deviceName);
        
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

}
