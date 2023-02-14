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
    private List<Transform> listLazer;
    public void ShootLazer()
    {
        AudioManager.Instance.PlaySound(lazerSound);
        GameObject currentLazer = Instantiate(lazer, spawnPoint.position, Quaternion.identity);
        currentLazer.transform.DOMove(currentLazer.transform.forward * 50, speedLazer)
        .SetSpeedBased(true)
        .SetEase(easeLazer)
        .OnComplete(() => Destroy(currentLazer));
    }
}
