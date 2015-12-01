using UnityEngine;

namespace peppar
{
    public static class Singleton
    {
        public static readonly PepparManager PepparManager = new PepparManager();

        public static readonly GameManager GameManager = new GameManager();
    }
}