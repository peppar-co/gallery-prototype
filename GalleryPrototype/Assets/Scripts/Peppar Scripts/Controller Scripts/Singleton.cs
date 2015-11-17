using UnityEngine;

namespace peppar
{
    public class Singleton
    {
        public readonly GameController GameController;

        public readonly Transform WorldCenter;

        public Singleton()
        {
            GameController = GameObject.FindGameObjectWithTag(Tag.GameController).GetComponent<GameController>();
            UnityEngine.Assertions.Assert.IsNotNull(GameController, "Singleton: GameController script is null");

            WorldCenter = GameObject.FindGameObjectWithTag(Tag.WorldCenter).transform;
            UnityEngine.Assertions.Assert.IsNotNull(WorldCenter, "Singleton: WorldCenter transform is null");
        }
    }
}