using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour
{
    Collider drawCollider;
    public bool isOpen = false;
    public bool canOpen = false;
    public Vector3 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drawCollider = GetComponent<Collider>();
        ZoomManager.Instance.onZoomOut += CloseDraw;
    }
    void OnEnable()
    {
        if (ZoomManager.Instance != null)
            ZoomManager.Instance.onZoomOut += CloseDraw;
    }

    void OnDisable()
    {
        ZoomManager.Instance.onZoomOut -= CloseDraw;
    }
    public void CanOpen()
    {
        canOpen = true;
    }
    private void OnMouseDown()
    {
        if (!isOpen && canOpen)
        {
            OpenDraw();
        }
    }
    public void OpenDraw()
    {
        transform.Translate(move * 0.5f);
        isOpen = true;
    }
    public void CloseDraw()
    {
        if (!isOpen) return;
        transform.Translate(-move * 0.5f);
        isOpen = false;
    }
}
