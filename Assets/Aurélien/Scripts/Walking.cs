using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walking : MonoBehaviour
{
    [SerializeField] private Generation _generation;
    [SerializeField] private Transform legR, legL;
    [SerializeField] private Transform PosToMoveLegR, PosToMoveLegL;
    private bool alternation = true;

    void Update()
    {
        if (!DOTween.IsTweening(transform))
        {
            legR.position -= transform.forward * _generation.getSpeedOfObstacle * Time.deltaTime;
            legL.position -= transform.forward * _generation.getSpeedOfObstacle * Time.deltaTime;
        }

        if (Vector3.Distance(PosToMoveLegR.position, legR.position) > 7f * transform.localScale.x && alternation)
            legR.DOMove(PosToMoveLegR.position, _generation.getSpeedOfObstacle).OnComplete(() => alternation = false).SetSpeedBased(true);
        if (Vector3.Distance(PosToMoveLegL.position, legL.position) > 7f * transform.localScale.x && !alternation)
            legL.DOMove(PosToMoveLegL.position, _generation.getSpeedOfObstacle ).OnComplete(() => alternation = true).SetSpeedBased(true);
    }
}
