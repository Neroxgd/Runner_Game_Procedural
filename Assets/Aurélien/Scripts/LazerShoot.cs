using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerShoot : MonoBehaviour
{
    [SerializeField] private AudioClip lazerSound;
    [SerializeField] private GameObject lazer;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speedLazer = 1;
    [SerializeField] private Ease easeLazer;
    [SerializeField] private AnimationCurve shakeLazerX;
    [SerializeField] private AnimationCurve shakeLazerY;

    private List<Transform> listLazer;
    public void ShootLazer()
    {
        AudioManager.Instance.PlaySound(lazerSound);
        GameObject currentLazer = Instantiate(lazer, spawnPoint.position, Quaternion.identity);
        currentLazer.transform.DOMoveZ(currentLazer.transform.forward.z * 30, speedLazer)
        .SetSpeedBased(true)
        .SetEase(easeLazer)
        .OnComplete(() => Destroy(currentLazer));
        currentLazer.transform.DOMoveX(currentLazer.transform.right.x * 30, speedLazer).SetEase(shakeLazerX).SetSpeedBased(true);
        currentLazer.transform.DOMoveY(currentLazer.transform.up.y * 30, speedLazer).SetEase(shakeLazerY).SetSpeedBased(true);

    }
}
