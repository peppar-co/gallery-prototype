using UnityEngine;
using System.Collections;

namespace peppar
{
    public interface LerpFunctionality
    {
        bool Lerp { get; set; }

        Transform GoalTransform { get; set; }

        float LerpSpeed { get; set; }

        float StopLerpDistance { get; set; }

        float DistanceToGoal { get; }

        void SetToGoalAndStopLerp();
    }
}