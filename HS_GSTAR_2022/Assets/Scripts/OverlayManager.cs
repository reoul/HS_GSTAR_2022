using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    void Update()
    {
#if UNITY_WINRT || UNITY_EDITOR_WIN
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,1000))
        {
            hit.collider.GetComponentInChildren<IOverlayable>()?.ShowOverlay();
        }
#elif UNITY_ANDROID
#endif
    }
}
