using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walking : MonoBehaviour
{
    [SerializeField] private Generation _generation;
    [SerializeField] private Transform legR, legL;
    [SerializeField] private Transform PosToMoveLegR, PosToMoveLegL;
    [SerializeField] private AnimationCurve curve;
    private bool alternation = true;

    void Update()
    {
        if (!DOTween.IsTweening(transform) && GetComponent<PlayerController>().IsOnGround)
        {
            legR.position -= transform.forward * _generation.getSpeedOfObstacle * Time.deltaTime;
            legL.position -= transform.forward * _generation.getSpeedOfObstacle * Time.deltaTime;
        }

        if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegR.position, legR.position) > 7f * transform.localScale.x && alternation)
        {
            legR.DOMoveZ(PosToMoveLegR.position.z, _generation.getSpeedOfObstacle).SetSpeedBased(true).OnComplete(() => alternation = false);
            legR.DOLocalMoveY(legR.up.y, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legR.position = new Vector3(legR.position.x, PosToMoveLegR.position.y, legR.position.z));
        }
        if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegL.position, legL.position) > 7f * transform.localScale.x && !alternation)
        {
            legL.DOMoveZ(PosToMoveLegL.position.z, _generation.getSpeedOfObstacle).SetSpeedBased(true).OnComplete(() => alternation = true);
            legL.DOLocalMoveY(legL.up.y, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legL.position = new Vector3(legL.position.x, PosToMoveLegL.position.y, legL.position.z));
        }
        // if (GetComponent<PlayerController>().IsOnGround && !DOTween.IsTweening(legL))
        // {
        //     // legR.DOMoveY(legR.up.y, _generation.getSpeedOfObstacle/2).SetSpeedBased(true).SetEase(curve).OnComplete(() => alternation1 = false).SetLoops(-1);
        //     // legL.DOMoveY(legL.up.y, _generation.getSpeedOfObstacle/2).SetSpeedBased(true).SetEase(curve).OnComplete(() => alternation1 = true).SetLoops(-1);
        // }

        // transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270 ? PosToMoveLegL.position.x : PosToMoveLegL.position.y
    }

    public void KillTweenLeg()
    {
        legL.DOKill(false);
        legR.DOKill(false);
    }

    // private void CheckAngleToMove()
    // {
    //     if (transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270)
    //     {
    //         if (alternation)
    //             legR.DOMoveX(legR.up.x, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legR.position = new Vector3(PosToMoveLegR.position.x, legR.position.y, legR.position.z));
    //         else
    //             legL.DOMoveX(legL.up.x, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legL.position = new Vector3(PosToMoveLegL.position.x, legL.position.y, legL.position.z));
    //         return;
    //     }
    //     if (alternation)
    //         legR.DOMoveY(legR.up.y, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legR.position = new Vector3(legR.position.x, PosToMoveLegR.position.y, legR.position.z));
    //     else
    //         legL.DOMoveY(legL.up.y, _generation.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legL.position = new Vector3(legL.position.x, PosToMoveLegL.position.y, legL.position.z)); ;
    // }
}

