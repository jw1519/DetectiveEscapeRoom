using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public List<Draw> draws;

    public void CloseAllDraws()
    {
        foreach (Draw draw in draws)
        {
            draw.CloseDraw();
        }
    }
}
