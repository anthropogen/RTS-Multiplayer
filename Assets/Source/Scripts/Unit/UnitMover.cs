using Mirror;
using RTS.Infrastucture;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

namespace RTS.Unit
{
    public class UnitMover : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        private Camera cam;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            cam = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if (!isOwned)
                return;
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    CmdMoveTo(hit.point);
                }
            }
        }

        [Command]
        private void CmdMoveTo(Vector3 point)
        {
            if (!NavMesh.SamplePosition(point, out NavMeshHit hit, 1, NavMesh.AllAreas))
                return;

            agent.SetDestination(hit.position);
        }
    }
}