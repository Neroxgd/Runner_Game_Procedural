using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walking : MonoBehaviour
{
    [SerializeField] private Transform legR, legL;
    [SerializeField] private Transform PosToMoveLegR, PosToMoveLegL;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AudioClip[] audioClips;
    private bool alternation = true;

    void Update()
    {
        if (!DOTween.IsTweening(transform) && GetComponent<PlayerController>().IsOnGround)
        {
            legR.position -= transform.forward * Generation.Instance.getSpeedOfObstacle * Time.deltaTime;
            legL.position -= transform.forward * Generation.Instance.getSpeedOfObstacle * Time.deltaTime;
        }

        if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegR.position, legR.position) > 7f * transform.localScale.x && alternation)
        {
            if (!DOTween.IsTweening(legR))
            {
                AudioManager.Instance.PlaySound(audioClips[0]);
                AudioManager.Instance.PlaySound(audioClips[Random.Range(1, 4)]);
            }
            legR.DOMoveZ(PosToMoveLegR.position.z, Generation.Instance.getSpeedOfObstacle).SetSpeedBased(true).OnComplete(() => alternation = false);
            legR.DOLocalMoveY(legR.up.y, Generation.Instance.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legR.position = new Vector3(legR.position.x, PosToMoveLegR.position.y, legR.position.z));
        }
        if (!DOTween.IsTweening(transform) && Vector3.Distance(PosToMoveLegL.position, legL.position) > 7f * transform.localScale.x && !alternation)
        {
            if (!DOTween.IsTweening(legL))
            {
                AudioManager.Instance.PlaySound(audioClips[0]);
                AudioManager.Instance.PlaySound(audioClips[Random.Range(1, 4)]);
            }
            legL.DOMoveZ(PosToMoveLegL.position.z, Generation.Instance.getSpeedOfObstacle).SetSpeedBased(true).OnComplete(() => alternation = true);
            legL.DOLocalMoveY(legL.up.y, Generation.Instance.getSpeedOfObstacle / 2).SetSpeedBased(true).SetEase(curve).OnComplete(() => legL.position = new Vector3(legL.position.x, PosToMoveLegL.position.y, legL.position.z));
        }
    }
}

