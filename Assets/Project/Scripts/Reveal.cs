using UnityEngine;

public class Reveal : MonoBehaviour
{
    public Light lightSource;
    private void Start()
    {
        lightSource = FindAnyObjectByType<RevealLightSource>().lightSource;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, lightSource.transform.position);
        if (distance <= lightSource.range && lightSource.enabled)
        {
            GetComponent<Renderer>().enabled = true;
        }
         else
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
