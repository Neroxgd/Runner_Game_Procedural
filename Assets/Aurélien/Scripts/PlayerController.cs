using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMove = 0.5f, speedRotate = 0.5f;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private Transform maxLeftXPos, maxRightXPos, maxUpYPos, maxDownYPos;
    [SerializeField] private float Powergravity = 9.81f;
    [SerializeField] private Camera cam;
    private bool isOnRotate = false;
    private bool IsOnGround = false;
    private float gravity = 0;

    public void Move(bool direction)
    {
        if (IsOnGround)
        {
            if (direction)
                rbPlayer.DOMove(ClampPosPlayer(transform.right * 1), speedMove);
            else
                rbPlayer.DOMove(ClampPosPlayer(-transform.right * 1), speedMove);
        }
    }

    private Vector3 ClampPosPlayer(Vector3 dir)
    {
        Vector3 clampPos = transform.position + dir;
        clampPos = new Vector3(Mathf.Clamp(clampPos.x, maxLeftXPos.position.x, maxRightXPos.position.x), Mathf.Clamp(clampPos.y, maxDownYPos.position.y, maxUpYPos.position.y), clampPos.z);
        return clampPos;
    }

    public void ChangeGravity(int directionGravity)
    {
        if (IsOnGround)
        {
            StartCoroutine(IsOnRotate());
            if (directionGravity == 0)
            {
                transform.DOLocalRotate(new Vector3(0, 0, -180), speedRotate, RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, -180), speedRotate, RotateMode.LocalAxisAdd);
            }

            else if (directionGravity == -1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, -90), speedRotate, RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, -90), speedRotate, RotateMode.LocalAxisAdd);
            }
            else if (directionGravity == 1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 90), speedRotate, RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, 90), speedRotate, RotateMode.LocalAxisAdd);
            }
        }

    }

    void FixedUpdate()
    {
        Gravity();
    }

    private void Gravity()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        gravity += Powergravity * Time.fixedDeltaTime;
        rbPlayer.velocity += -transform.up * gravity * Time.fixedDeltaTime;

        Debug.DrawRay(transform.position, -transform.up * 0.55f, Color.green);
        if (Physics.Raycast(ray, 0.55f))
        {
            IsOnGround = true;
            gravity = 1;
            print("sol");
        }
        else
            IsOnGround = false;
    }

    IEnumerator IsOnRotate()
    {
        isOnRotate = true;
        rbPlayer.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(speedRotate);
        rbPlayer.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        isOnRotate = false;
    }
}
