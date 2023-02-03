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
    [SerializeField] private Transform fixDown, fixLeft, fixUp, fixRight;
    [SerializeField] private Transform fixedposdown, fixedposleft, fixedposup, fixedposright;
    [SerializeField] private float Powergravity = 9.81f;
    [SerializeField] private Transform[] posToMove;
    [SerializeField] private Transform PosMove;
    private int indexMove = 2;
    [SerializeField] private Camera cam;
    private bool isOnRotate = false;
    private bool isOnGround = false;
    private bool isOnObstacle = false;
    private float gravity = 0;
    [SerializeField] private Ease ease;

    public void Move(bool direction)
    {

        if (isOnGround && !isOnRotate && !isOnObstacle && !DOTween.IsTweening(rbPlayer))
        {
            indexMove = Mathf.Clamp(indexMove += direction ? 1 : -1, 0, posToMove.Length - 1);
            // StartCoroutine(TimeMove());
            rbPlayer.DOMove(posToMove[indexMove].position, _PlayerAttributs.getSpeed()).SetEase(ease);
        }

        // Ray ray = new Ray(transform.position + transform.up / 4, direction ? transform.right : -transform.right);
        // RaycastHit hit;
        // Debug.DrawRay(ray.origin, direction ? transform.right : -transform.right, Color.red);
        // if (IsOnGround && !isOnRotate && !DOTween.IsTweening(rbPlayer) && !Physics.Raycast(ray, out hit, 1f))
        // {
        //     if (direction)
        //         rbPlayer.DOMove(transform.position + transform.right, _PlayerAttributs.getSpeed()).OnComplete(FixedPosition);
        //     else
        //         rbPlayer.DOMove(transform.position + -transform.right, _PlayerAttributs.getSpeed()).OnComplete(FixedPosition);
        // }
    }


    private void FixedIndexHorizontal(int currentEulerAngle)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Round(transform.eulerAngles.z));
        if (transform.eulerAngles.z == 0)
        {
            if (currentEulerAngle == 90)
            {
                indexMove = posToMove.Length - 1;
                transform.position = new Vector3(fixRight.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                indexMove = 0;
                transform.position = new Vector3(fixLeft.position.x, transform.position.y, transform.position.z);
            }
        }
        else if (transform.eulerAngles.z == 90)
        {
            if (currentEulerAngle == 0)
            {
                indexMove = 0;
                transform.position = new Vector3(transform.position.x, fixDown.position.y, transform.position.z);
            }
            else
            {
                indexMove = posToMove.Length - 1;
                transform.position = new Vector3(transform.position.x, fixUp.position.y, transform.position.z);
            }
        }
        else if (transform.eulerAngles.z == 180)
        {
            if (currentEulerAngle == 90)
            {
                indexMove = 0;
                transform.position = new Vector3(fixRight.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                indexMove = posToMove.Length - 1;
                transform.position = new Vector3(fixLeft.position.x, transform.position.y, transform.position.z);
            }
        }
        else if (transform.eulerAngles.z == 270)
        {
            if (currentEulerAngle == 0)
            {
                indexMove = posToMove.Length - 1;
                transform.position = new Vector3(transform.position.x, fixDown.position.y, transform.position.z);
            }
            else
            {
                indexMove = 0;
                transform.position = new Vector3(transform.position.x, fixUp.position.y, transform.position.z);
            }
        }
        print(transform.eulerAngles.z + " " + currentEulerAngle);
        FixedPosMove();
    }

    private void FixedIndexVertical()
    {
        indexMove = Mathf.Abs((indexMove - (posToMove.Length - 1)));
        FixedPosMove();
    }

    private void FixedPosMove()
    {
        // FixedIndexHorizontal();
        // FixedPosition();
        // PosMove.RotateAround(posToMove[indexMove].position, transform.right, 180);
        // PosMove.SetParent(null);
        if (transform.eulerAngles.z == 0)
        {
            PosMove.position = fixedposdown.position;
            PosMove.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.eulerAngles.z == 270)
        {
            PosMove.position = fixedposleft.position;
            PosMove.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (transform.eulerAngles.z == 180)
        {
            PosMove.position = fixedposup.position;
            PosMove.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (transform.eulerAngles.z == 90)
        {
            PosMove.position = fixedposright.position;
            PosMove.eulerAngles = new Vector3(0, 0, 90);
        }


        // StartCoroutine(waitFixedIndex());
        // IEnumerator waitFixedIndex()
        // {
        //     int fixedIndex = 100;
        //     for (int i = 0; i < posToMove.Length - 1; i++)
        //         if (Vector3.Distance(transform.position, posToMove[i].position) < fixedIndex)
        //         {
        //             fixedIndex = i;
        //             indexMove = fixedIndex;
        //         }
        //     yield return new WaitUntil(()=> IsOnGround && Vector3.Distance(transform.position, posToMove[indexMove].position) > 0.5f);
        //     transform.position = posToMove[indexMove].position;
        //     PosMove.SetParent(transform);
        // }
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
        if (isOnGround && !DOTween.IsTweening(rbPlayer))
        {
            int currentEulerAngle = (int)transform.eulerAngles.z;
            print(currentEulerAngle);
            isOnGround = false;
            StartCoroutine(IsOnRotate());
            if (directionGravity == 0)
            {
                transform.DOLocalRotate(new Vector3(0, 0, -180), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd).OnComplete(FixedIndexVertical);
                cam.transform.DOLocalRotate(new Vector3(0, 0, -180), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
            }

            else if (directionGravity == -1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, -90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd).OnComplete(() => FixedIndexHorizontal(currentEulerAngle));
                cam.transform.DOLocalRotate(new Vector3(0, 0, -90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd);
            }
            else if (directionGravity == 1)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 90), _PlayerAttributs.getSpeed(), RotateMode.LocalAxisAdd).OnComplete(() => FixedIndexHorizontal(currentEulerAngle));
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
        if (!isOnGround && !DOTween.IsTweening(rbPlayer))
            FixedPosMove();
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
        Ray ray = new Ray(transform.position + transform.up / 1.5f, -transform.up);
        gravity += Powergravity;
        rbPlayer.velocity += -transform.up * gravity;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.8f))
        {
            if (hit.transform.CompareTag("Wall"))
                isOnObstacle = true;
            else
                isOnObstacle = false;
            isOnGround = true;
            gravity = 1;
            rbPlayer.velocity = Vector3.zero;
            if (!DOTween.IsTweening(rbPlayer))
                transform.position = hit.point;
        }
        else
            isOnGround = false;
        Debug.DrawRay(ray.origin, -transform.up * 0.8f, Color.green);
    }

    IEnumerator IsOnRotate()
    {
        isOnRotate = true;
        isOnGround = false;
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
