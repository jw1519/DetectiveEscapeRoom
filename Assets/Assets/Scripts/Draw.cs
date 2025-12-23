using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour
{
    DrawManager drawManager;
    public Collider drawCollider;
    public bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drawManager = GetComponentInParent<DrawManager>();
        ColourCombinationLockManager.CombinationCorrect += CanOpen;
        drawCollider = GetComponent<Collider>();
    }
    public void CanOpen()
    {
        drawCollider.enabled = true;
    }
    private void OnMouseDown()
    {
        if (!isOpen)
        {
            OpenDraw();
        }
    }
    public void OpenDraw()
    {
        drawManager.CloseAllDraws();
        Vector3 move = new Vector3(-0.6f, 0, 0);
        transform.Translate(move * 0.5f);
        isOpen = true;
    }
    public void CloseDraw()
    {
        if (!isOpen) return;
        Vector3 move = new Vector3(0.6f, 0, 0);
        transform.Translate(move * 0.5f);
        isOpen = false;
    }
}
