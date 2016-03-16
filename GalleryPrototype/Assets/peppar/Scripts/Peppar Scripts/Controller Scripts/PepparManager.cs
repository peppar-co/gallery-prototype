using System.Collections.Generic;
using UnityEngine;

namespace peppar
{
    public static class PepparManager
    {
        public static readonly PepparController GameController;

        public static readonly List<PepparObject> Objects = new List<PepparObject>();

        static PepparManager()
        {
            GameController = GameObject.FindGameObjectWithTag(Tag.GameController).GetComponent<PepparController>();
            UnityEngine.Assertions.Assert.IsNotNull(GameController, "Singleton: GameController script is null");
        }
    }
}