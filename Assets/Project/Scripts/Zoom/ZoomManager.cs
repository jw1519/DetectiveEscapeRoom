using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{
    public static ZoomManager Instance;
    public List<Zoom> currentZooms = new List<Zoom>();
    public Button zoomOutButton;

    public event Action onZoomIn;
    public event Action onZoomOut;

    float currentY;

    Camera cam => Camera.main;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        // check for UI interaction

        Ray ray;
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount == 0) return;

            //Check if touch is over UI
            Touch touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began) return;

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
            ray = cam.ScreenPointToRay(touch.position);
        }
        else
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (currentZooms.Count == 0)
                currentY = cam.transform.eulerAngles.y;

            hit.collider.GetComponent<Zoom>()?.ZoomIn();
        }
    }

    public void RegisterZoom(Zoom zoom)
    {
        if (!currentZooms.Contains(zoom))
        {
            currentZooms.Add(zoom);
        }
        if (currentZooms.Count == 1)
        {
            GyroManager.Instance.DisableGyro();
            zoomOutButton.gameObject.SetActive(true);

            if (!ManagerUI.IsTouchOverUI(Input.mousePosition))
                onZoomIn?.Invoke();
        }
    }
    public void UnregisterZoom()
    {
        if (currentZooms.Count > 0)
        {
            Zoom zoom = currentZooms[currentZooms.Count - 1];
            zoom.ZoomOut();
            currentZooms.Remove(zoom);
        }
        if (currentZooms.Count == 0)
        {
            GyroManager.Instance.EnableGyro();
            zoomOutButton.gameObject.SetActive(false);
            cam.fieldOfView = 60; //reset to default zoom
            cam.transform.position = new Vector3(0, 1.6f, 0);
            cam.transform.rotation = Quaternion.Euler(0, currentY, 0);
            onZoomOut?.Invoke();
        }
        else //zoom to the previous zoom
        {
            Zoom zoom = currentZooms[currentZooms.Count - 1];
            if (zoom == null) UnregisterZoom();
            zoom.ZoomIn();
        }
    }
}
