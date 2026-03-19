using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> ObjectsToLoad;
    public List<GameObject> Objects;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

        foreach (GameObject gameObject in ObjectsToLoad)
        {
            Instantiate(gameObject);
            Objects.Add(gameObject);
        }
    }

}
