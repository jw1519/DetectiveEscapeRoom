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

        if (Objects.Count != 0)
        {
            LoadObjects();
        }
        else
        {
            foreach (GameObject gameObject in ObjectsToLoad)
            {
                GameObject GO = Instantiate(gameObject);
                Objects.Add(GO);
            }
        }
        Time.timeScale = 1f;
    }
    public void LoadObjects()
    {
        foreach (var item in Objects)
        {
            item.SetActive(true);
        }
    }
    public void EndGame()
    {
        foreach (var item in Objects)
        {
            item.SetActive(false);
        }
    }

}
