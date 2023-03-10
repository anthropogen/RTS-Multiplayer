using Mirror;
using RTS.Configs;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Units
{
    public class UnitMover : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        public event Action ReachedDestination;

        public void Construct(UnitConfig config)
        {
            agent.speed = config.MovementData.Speed;
            agent.angularSpeed = config.MovementData.AngularSpeed;
            agent.acceleration = config.MovementData.Acceleration;
        }

        [ClientCallback]
        private void Update()
        {
            if (!agent.hasPath)
                return;

            if (agent.remainingDistance <= agent.stoppingDistance)
                ReachedDestination?.Invoke();
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