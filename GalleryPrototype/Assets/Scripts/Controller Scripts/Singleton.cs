using UnityEngine;
using System.Collections;

public class Singleton
{
    public readonly GameController GameController;

    public readonly GameObject WorldCenter;

    public Singleton()
    {
        GameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
        if (GameController == null)
            Debug.LogError("Singleton: GameController is null");

        WorldCenter = GameObject.FindGameObjectWithTag(Tags.WorldCenter);
        if (WorldCenter == null)
            Debug.LogError("Singleton: WorldCenter is null");
    }
}
