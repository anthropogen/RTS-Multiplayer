using RTS.Combat;
using System;

namespace RTS.Units
{
    public class AttackState : IEnterState<Targetable>
    {
        private readonly UnitMover mover;
        private readonly Lazy<FSM> fsm;
        private Targetable target;
        public void Enter(Targetable target)
        {
            this.target = target;
        }

        public void Exit()
        {
        }

        public void Run()
        {
        }
    }
}
