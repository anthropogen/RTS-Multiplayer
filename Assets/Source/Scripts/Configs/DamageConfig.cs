using UnityEngine;

namespace RTS.Configs
{
    [System.Serializable]
    public class DamageConfig
    {
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField, Min(0)] public float AttackDistance { get; private set; }
    }
}