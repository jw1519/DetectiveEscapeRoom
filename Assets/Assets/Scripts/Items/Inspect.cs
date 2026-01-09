using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inspect : MonoBehaviour
{
    public static Inspect instance;

    GameObject objectToInspect;
    public float rotationSpeed;
    public GameObject backButton;
    public GameObject otherButton;
    Vector3 cameraPosition;
    Quaternion cameraRotation;

    Vector3 previousMousePosition;
    Camera cam;
    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        gameObject.SetActive(false);
    }
    bool wasActive = false;
    public void EnableInspect(Item item)
    {
        if (objectToInspect != null)
        {
            DisableInspect();
        }
        objectToInspect = ItemPool.Instance.GetItem(item.itemID);
        if (objectToInspect != null)
        {
            gameObject.SetActive(true);
            backButton.SetActive(true);

            if (otherButton != null && otherButton.activeInHierarchy)
            {
                otherButton.SetActive(false);
                wasActive = true;
            }
                

            objectToInspect.transform.SetParent(transform);
            objectToInspect.transform.localPosition = Vector3.zero;
            objectToInspect.GetComponent<Collider>().enabled = false;
            objectToInspect.SetActive(true);
            // keep original position and roation
            cameraPosition = cam.transform.localPosition;
            cameraRotation = cam.transform.rotation;

            cam.transform.localPosition = new Vector3(0, 10, 0);
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void DisableInspect()
    {
        objectToInspect.GetComponent<Collider>().enabled = true;
        ItemPool.Instance.AddItem(objectToInspect);

        objectToInspect = null;
        gameObject.SetActive(false);
        backButton.SetActive(false);
        if (wasActive && otherButton != null)
        {
            otherButton.SetActive(true);
            wasActive = false;
        }
        //put camera back
        cam.transform.localPosition = cameraPosition;
        cam.transform.rotation = cameraRotation;
    }
    private void Update()
    {
        if (objectToInspect == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMoutsePosition = Input.mousePosition - previousMousePosition;
            float rotationX = -deltaMoutsePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMoutsePosition.x * rotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.transform.rotation = rotation * objectToInspect.transform.rotation;
            previousMousePosition = Input.mousePosition;
        }
    }
}
