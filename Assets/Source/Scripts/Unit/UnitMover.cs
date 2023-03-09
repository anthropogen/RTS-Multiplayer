using Mirror;
using RTS.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Unit
{
    public class UnitMover : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        public void Construct(UnitConfig config)
        {
            agent.speed = config.MovementData.Speed;
            agent.angularSpeed = config.MovementData.AngularSpeed;
            agent.acceleration = config.MovementData.Acceleration;
        }

        [Command]
        public void CmdMoveTo(Vector3 point)
        {
            if (!NavMesh.SamplePosition(point, out NavMeshHit hit, 1, NavMesh.AllAreas))
                return;

            agent.SetDestination(hit.position);
        }
    }
}