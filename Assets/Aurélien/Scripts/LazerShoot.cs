using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LazerShoot : MonoBehaviour
{
    [SerializeField] private AudioClip lazerSound;
    [SerializeField] private GameObject lazer;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speedLazer = 1;
    [SerializeField] private Ease easeLazer;
    [SerializeField] private AnimationCurve shakeLazerX;
    [SerializeField] private AnimationCurve shakeLazerY;
    [SerializeField] private Image loadShoot;
    [SerializeField] private Sprite pointerRed, pointerGreen;
    [SerializeField] private AudioClip audioClip;
    private bool canShoot;

    private List<Transform> listLazer;
    public void ShootLazer()
    {
        if (!canShoot) return;
        loadShoot.fillAmount = 0;
        loadShoot.sprite = pointerRed;
        LoadShoot();
        AudioManager.Instance.PlaySound(lazerSound);
        GameObject currentLazer = Instantiate(lazer, spawnPoint.position, Quaternion.identity);
        currentLazer.transform.DOMoveZ(currentLazer.transform.forward.z * 30, speedLazer)
        .SetSpeedBased(true)
        .SetEase(easeLazer)
        .OnComplete(() => Destroy(currentLazer));
        currentLazer.transform.DOMoveX(currentLazer.transform.right.x * 30, speedLazer).SetEase(shakeLazerX).SetSpeedBased(true);
        currentLazer.transform.DOMoveY(currentLazer.transform.up.y * 30, speedLazer).SetEase(shakeLazerY).SetSpeedBased(true);
    }

    private void LoadShoot()
    {
        loadShoot.DOFillAmount(1, 30).OnComplete(() => { canShoot = true; loadShoot.sprite = pointerGreen; AudioManager.Instance.PlaySound(audioClip); });
    }

    void Start()
    {
        LoadShoot();
    }
}
