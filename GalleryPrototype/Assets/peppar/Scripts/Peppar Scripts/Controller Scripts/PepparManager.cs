using System.Collections.Generic;
using UnityEngine;

namespace peppar
{
    public static class PepparManager
    {
        public static readonly PepparController gameController;

        public static readonly List<PepparObject> Objects = new List<PepparObject>();

        static PepparManager()
        {
            gameController = GameObject.FindGameObjectWithTag(Tag.GameController).GetComponent<PepparController>();
            UnityEngine.Assertions.Assert.IsNotNull(gameController, "Singleton: GameController script is null");
        }
    }
}