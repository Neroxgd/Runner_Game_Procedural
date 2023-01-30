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
        Ray ray = new Ray(transform.position + transform.up / 4, direction ? transform.right : -transform.right);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, direction ? transform.right : -transform.right, Color.red);
        if (IsOnGround && !isOnRotate && !DOTween.IsTweening(rbPlayer) && !Physics.Raycast(ray, out hit, 1f))
        {
            if (direction)
                rbPlayer.DOMove(transform.position + transform.right, _PlayerAttributs.getSpeed()).OnComplete(FixedPosition);
            else
                rbPlayer.DOMove(transform.position + -transform.right, _PlayerAttributs.getSpeed()).OnComplete(FixedPosition);
        }
    }

    private void FixedPosition()
    {
        if (transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270)
            transform.position = new Vector3((Mathf.Round(transform.position.x / 2.5f) * 2.5f), Mathf.Round(transform.position.y) + 0.4f, transform.position.z);
        else
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y) + 0.003f, transform.position.z);
    }

    public void ChangeGravity(int directionGravity)
    {
        if (IsOnGround && !DOTween.IsTweening(rbPlayer))
        {
            IsOnGround = false;
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
        _InteractionWall.CheckRayCastWall(transform.position + transform.up / 4);
        if (_InteractionWall.invoke)
            LoseHPPlayer();

        if (!DOTween.IsTweening(rbPlayer))
            transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z / 90f) * 90f);
    }

    void Start()
    {
        _PlayerAttributs.setHP(3);
        _PlayerAttributs.setSpeed(0.5f);
        _InteractionWall.setDistance(1f);
        //InteractionWall.wallhit += LoseHPPlayer;
    }

    private void Gravity()
    {
        Ray ray = new Ray(transform.position + transform.up / 3f, -transform.up);
        gravity += Powergravity;
        rbPlayer.velocity += -transform.up * gravity;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.4f))
        {
            IsOnGround = true;
            gravity = 1;
            rbPlayer.velocity = Vector3.zero;
            if (!DOTween.IsTweening(rbPlayer))
                transform.position = hit.point;
        }
        else
            IsOnGround = false;
        Debug.DrawRay(ray.origin, -transform.up * 0.35f, Color.green);
    }

    IEnumerator IsOnRotate()
    {
        isOnRotate = true;
        IsOnGround = false;
        if (transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270)
            rbPlayer.constraints = RigidbodyConstraints.FreezePositionY;
        else
            rbPlayer.constraints = RigidbodyConstraints.FreezePositionX;
        yield return new WaitForSeconds(_PlayerAttributs.getSpeed());
        rbPlayer.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        isOnRotate = false;
    }

    void LoseHPPlayer()
    {
        _InteractionWall.setTakingWall(true);
        _PlayerAttributs.LoseHP();
        _InteractionWall.invoke = false;
        StartCoroutine(invicibility());
    }

    IEnumerator invicibility()
    {
        yield return new WaitForSeconds(0.3f);
        _InteractionWall.setTakingWall(false);
    }

    public PlayerAttributs GetPlayerAttributs()
    {
        return _PlayerAttributs;
    }
}
