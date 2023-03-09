using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RTS.Management
{
    public class UnitCommandGiver : NetworkBehaviour
    {
        [SerializeField] private UnitSelector selector;
        private Camera cam;

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            cam = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                var ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    foreach (var unit in selector.SelectedUnits)
                    {
                        unit.UnitMover.CmdMoveTo(hit.point);
                    }
                }
            }
        }
    }
}