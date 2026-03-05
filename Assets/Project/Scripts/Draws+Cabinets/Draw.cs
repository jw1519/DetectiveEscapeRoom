using UnityEngine;

public class Draw : MonoBehaviour, IOpen
{
    public bool isOpen = false;
    public bool canOpen = false;
    public Vector3 move;
    public void CanOpen()
    {
        canOpen = true;
    }
    private void OnMouseDown()
    {
        if (ManagerUI.IsTouchOverUI(Input.mousePosition)) return;
        if (!isOpen && canOpen)
        {
            Open();
        }
    }
    public void Open()
    {
        transform.Translate(move * 0.5f);
        isOpen = true;
    }
    public void Close()
    {
        if (!isOpen) return;
        transform.Translate(-move * 0.5f);
        isOpen = false;
    }
}
