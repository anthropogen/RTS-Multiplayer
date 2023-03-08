using UnityEngine;

namespace RTS.Configs
{
    [System.Serializable]
    public class MovementUnitData
    {
        [field: SerializeField, Min(0)] public float Speed { get; private set; } = 3.5f;
        [field: SerializeField, Min(0)] public float Acceleration { get; private set; } = 8f;
        [field: SerializeField, Min(0)] public float AngularSpeed { get; private set; } = 120f;
    }
}