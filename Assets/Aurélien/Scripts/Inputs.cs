using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private LazerShoot _lazerShoot;

    // false = left
    // true = right
    public void MoveLeft(InputAction.CallbackContext context) { if (context.started) _playerController.Move(false); }
    public void MoveRight(InputAction.CallbackContext context) { if (context.started) _playerController.Move(true); }

    // 0 = up
    // 1 = right
    // -1 = left
    public void ChangeGravityUp(InputAction.CallbackContext context) { if (context.started) _playerController.ChangeGravity(0); }

    public void ChangeGravityLeft(InputAction.CallbackContext context) { if (context.started) _playerController.ChangeGravity(-1); }

    public void ChangeGravityRight(InputAction.CallbackContext context) { if (context.started) _playerController.ChangeGravity(1); }

    public void ShootLazer(InputAction.CallbackContext context) { if (context.started) _lazerShoot.ShootLazer(); }
}
