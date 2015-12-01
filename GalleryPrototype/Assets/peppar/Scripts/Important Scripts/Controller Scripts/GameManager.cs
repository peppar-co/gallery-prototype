using UnityEngine;

namespace peppar
{
    public class GameManager
    {
        [SerializeField]
        private bool _debug = false;

        private readonly GameController gameController;

        public bool Debug
        {
            get
            {
                return _debug;
            }

            set
            {
                _debug = value;
            }
        }

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