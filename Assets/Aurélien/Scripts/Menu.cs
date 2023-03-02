using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] RectTransform playbutton;
    [SerializeField] AnimationCurve curve;
    [SerializeField] private AudioClip audioClip;

    public void Play()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        robot.SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Scène Aur recupe");
    }

    IEnumerator StartBeats()
    {
        yield return new WaitForSeconds(2.8f);
        StartCoroutine(Beats());
        IEnumerator Beats()
        {
            playbutton.DOSizeDelta(new Vector2(playbutton.rect.width * 1.5f, playbutton.rect.height * 1.5f), 0.2f).SetEase(curve);
            yield return new WaitForSeconds(0.8f);
            print("hezz");
            StartCoroutine(Beats());
        }
    }

    void Start()
    {
        StartCoroutine(StartBeats());
        AudioManager.Instance.PlayMusic(audioClip);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
