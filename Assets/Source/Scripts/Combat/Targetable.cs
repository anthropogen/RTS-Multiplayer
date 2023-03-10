using UnityEngine;

namespace RTS.Combat
{
    public class Targetable : MonoBehaviour
    {
        [SerializeField] public Transform aim;

        public Vector3 AimPoint => aim == null ? transform.position : aim.position;
    }
}