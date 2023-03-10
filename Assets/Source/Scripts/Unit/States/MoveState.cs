
using System;
using UnityEngine;

namespace RTS.Units
{
    public class MoveState : IEnterState<Vector3>
    {
        private readonly UnitMover mover;
        private readonly Lazy<FSM> fsm;

        public MoveState(UnitMover mover, Lazy<FSM> fsm)
        {
            this.mover = mover;
            this.fsm = fsm;
        }

        public void Enter(Vector3 destination)
        {
            mover.CmdMoveTo(destination);
            mover.ReachedDestination += TransferToIdle;
        }

        public void Exit()
        {
            mover.ReachedDestination -= TransferToIdle;
        }

        public void Run()
        {
        }

        private void TransferToIdle()
            => fsm.Value.Enter<IdleState>();
    }
}