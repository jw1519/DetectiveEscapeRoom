using UnityEngine;

public class Inspect : MonoBehaviour
{
    public static Inspect instance;

    GameObject objectToInspect;
    public float rotationSpeed;
    public GameObject backButton;
    public GameObject otherButton;

    Vector3 previousMousePosition;
    CameraController cam;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        cam = Camera.main.gameObject.GetComponentInParent<CameraController>();
        GetComponentInParent<Transform>().gameObject.SetActive(false);
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
            item.beingInspected = true;

            if (otherButton != null && otherButton.activeInHierarchy)
            {
                otherButton.SetActive(false);
                wasActive = true;
            }

            objectToInspect.transform.SetParent(transform);
            objectToInspect.transform.localPosition = Vector3.zero;
            objectToInspect.SetActive(true);

            cam.EnableInspect();
        }
    }
    public void RemoveInspectObject(Item item)
    {
        ItemPool.Instance.AddItem(objectToInspect);
        objectToInspect.GetComponent<WorldItem>().itemSO.beingInspected = false;
        objectToInspect = null;
        gameObject.SetActive(false);
        if (wasActive && otherButton != null)
        {
            otherButton.SetActive(true);
            wasActive = false;
        }
        EnableInspect(item);
    }
    public void DisableInspect()
    {
        ItemPool.Instance.AddItem(objectToInspect);
        objectToInspect.GetComponent<WorldItem>().itemSO.beingInspected = false;
        objectToInspect = null;
        gameObject.SetActive(false);

        backButton.SetActive(false);
        if (wasActive && otherButton != null)
        {
            otherButton.SetActive(true);
            wasActive = false;
        }
        GyroManager.Instance.EnableGyro();
        cam.DisableInspect();
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
