using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walking : MonoBehaviour
{
    [SerializeField] private Generation _generation;
    [SerializeField] private Transform legR, legL;
    [SerializeField] private Transform PosToMoveLegR, PosToMoveLegL;
    [SerializeField] private Transform PosWhenMoveLegR, PosWhenMoveLegL;
    [SerializeField] private Rigidbody rbLegR, rbLegL;
    private Vector3 velocityLegR = Vector3.zero;
    private Vector3 velocityLegL = Vector3.zero;
    private bool jumpLeg;
    private bool alternation = true;
    [SerializeField] private Ease ease;
    private float lerpLeg = 0;

    void Update()
    {
        if (!DOTween.IsTweening(transform))
        {
            if (alternation)
            {
                rbLegR.isKinematic = false;
                legR.position = Vector3.SmoothDamp(legR.position, PosToMoveLegR.position, ref velocityLegR, _generation.getSpeedOfObstacle * Time.deltaTime * 2);
                if (rbLegR.velocity.magnitude > 0)
                {
                    rbLegR.velocity -= transform.up * Time.deltaTime * 10;
                    rbLegR.velocity = new Vector3(0, Mathf.Clamp(rbLegR.velocity.y, 0, 10), 0);
                }
            }
            else
            {
                rbLegL.isKinematic = false;
                legL.position = Vector3.SmoothDamp(legL.position, PosToMoveLegL.position, ref velocityLegL, _generation.getSpeedOfObstacle * Time.deltaTime * 2);
                if (rbLegL.velocity.magnitude > 0)
                {
                    rbLegL.velocity -= transform.up * Time.deltaTime * 10;
                    rbLegL.velocity = new Vector3(0, Mathf.Clamp(rbLegL.velocity.y, 0, 10), 0);
                }
            }
        }

        if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegR.position, legR.position) < 0.1f * transform.localScale.x && alternation)
        {
            alternation = false;
            legR.DOMove(PosWhenMoveLegR.position, _generation.getSpeedOfObstacle / 2).OnComplete(() => rbLegR.velocity = transform.up * 3).SetSpeedBased(true).SetEase(ease);
        }
        else if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegL.position, legL.position) < 0.1f * transform.localScale.x && !alternation)
        {
            alternation = true;
            legL.DOMove(PosWhenMoveLegL.position, _generation.getSpeedOfObstacle / 2).OnComplete(() => rbLegL.velocity = transform.up * 3).SetSpeedBased(true).SetEase(ease);
        }
    }

    void LateUpdate()
    {
        if (DOTween.IsTweening(transform))
        {
            legR.DOKill(false);
            legL.DOKill(false);
            rbLegR.velocity = Vector3.zero;
            rbLegL.velocity = Vector3.zero;
            rbLegR.isKinematic = true;
            rbLegL.isKinematic = true;
            print("égpozejgz");
        }
    }
}
