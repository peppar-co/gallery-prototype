using UnityEngine;

namespace peppar
{
    public class GameManager
    {
        private readonly GameController gameController;

        public GameController GameController
        {
            get
            {
                return gameController;
            }
        }

        public GameManager()
        {
            gameController = GameObject.FindGameObjectWithTag(Tag.GameController).GetComponent<GameController>();
            UnityEngine.Assertions.Assert.IsNotNull(gameController, "Singleton: GameController script is null");
        }
    }
}