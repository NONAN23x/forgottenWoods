using Cinemachine;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera FPCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 40f;

    bool zoomedIn = false;

    private void OnDisable() {
        ZoomedOut();    
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            if (zoomedIn == false)
            {
                ZoomedIn();
            }
            else
            {
                ZoomedOut();
            }
        }
    }

    private void ZoomedOut()
    {
        zoomedIn = false;
        FPCamera.m_Lens.FieldOfView = zoomedOutFOV;
    }

    private void ZoomedIn()
    {
        zoomedIn = true;
        FPCamera.m_Lens.FieldOfView = zoomedInFOV;
    }
}
