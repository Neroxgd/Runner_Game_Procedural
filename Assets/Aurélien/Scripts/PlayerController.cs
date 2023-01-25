using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerAttributs _PlayerAttributs = new PlayerAttributs();
    [SerializeField] InteractionWall _InteractionWall = new InteractionWall();
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private Transform maxLeftXPos, maxRightXPos, maxUpYPos, maxDownYPos;
    [SerializeField] private float Powergravity = 9.81f;
    [SerializeField] private Camera cam;
    private bool isOnRotate = false;
    private bool IsOnGround = false;
    private float gravity = 0;

    public void Move(bool direction)
    {
        if (IsOnGround && !isOnRotate && !DOTween.IsTweening(rbPlayer))
        {
            if (direction)
                rbPlayer.DOMove(ClampPosPlayer(transform.right), _PlayerAttributs.getSpeed());
            else
                rbPlayer.DOMove(ClampPosPlayer(-transform.right), _PlayerAttributs.getSpeed());
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
                transform.DOLocalRotate(new Vector3(0, 0, -180), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, -180), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
            }

            else if (directionGravity == -1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, -90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, -90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
            }
            else if (directionGravity == 1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
                cam.transform.DOLocalRotate(new Vector3(0, 0, 90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
            }
        }

    }

    void FixedUpdate()
    {
        Gravity();
    }

    void Update()
    {
        _InteractionWall.CheckRayCastWall(transform.position);

        if(_InteractionWall.getTakingWall() && !_PlayerAttributs.getInteractionApply())
        {
            _PlayerAttributs.LoseHP();
        }
    }

    void Start()
    {
        _PlayerAttributs.setHP(3);
        _PlayerAttributs.setSpeed(0.5f);
        _InteractionWall.setDistance(0.3f);
    }

    private void Gravity()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        gravity += Powergravity * Time.fixedDeltaTime;
        rbPlayer.velocity += -transform.up * gravity;
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up * 0.5f, Color.green);
        if (Physics.Raycast(ray, out hit, 0.55f))
        {
            IsOnGround = true;
            gravity = 1;
            rbPlayer.velocity = Vector3.zero;
            if (!DOTween.IsTweening(rbPlayer))
                transform.position = hit.point + transform.up / 2;
        }
        else
            IsOnGround = false;
    }

    IEnumerator IsOnRotate()
    {
        isOnRotate = true;
        rbPlayer.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(_PlayerAttributs.getSpeed());
        rbPlayer.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        isOnRotate = false;
    }
}
