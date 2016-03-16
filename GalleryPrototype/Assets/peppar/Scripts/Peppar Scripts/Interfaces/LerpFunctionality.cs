using UnityEngine;
using System.Collections;

namespace peppar
{
    public interface LerpFunctionality
    {
        bool Lerp { get; set; }

        Transform TargetTransform { get; set; }

        float LerpSpeed { get; set; }

        float StopLerpDistance { get; set; }

        float DistanceToTarget { get; }

        void SetToTargetAndStopLerp();
    }
}