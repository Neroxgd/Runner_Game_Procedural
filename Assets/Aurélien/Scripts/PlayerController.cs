using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMove = 0.5f;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private Transform maxLeftXPos, maxRightXPos, maxUpYPos, maxDownYPos;

    public void Move(bool direction)
    {
        if (direction)
            transform.DOMove(ClampPosPlayer(Vector3.right).position, speedMove);
        else
            transform.DOMove(ClampPosPlayer(Vector3.left).position, speedMove);
    }

    private Transform ClampPosPlayer(Vector3 dir)
    {
        transform.position += dir;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxLeftXPos.position.x, maxRightXPos.position.x), Mathf.Clamp(transform.position.y, maxDownYPos.position.y, maxUpYPos.position.y), transform.position.z);
        return transform;
    }

    public void ChangeGravity(int directionGravity)
    {

    }

    void Start()
    {
        print(Physics.gravity);
    }
}
