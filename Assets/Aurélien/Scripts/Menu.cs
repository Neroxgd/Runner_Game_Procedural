using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject robot;
    [SerializeField] RectTransform playbutton;
    [SerializeField] AnimationCurve curve;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject leaderBoard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_InputField inputFieldPseudo;
    public static string pseudo;

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
        // SceneManager.LoadScene("Menu");
    }

    IEnumerator StartBeats()
    {
        yield return new WaitForSeconds(2.8f);
        StartCoroutine(Beats());
        IEnumerator Beats()
        {
            playbutton.DOSizeDelta(new Vector2(playbutton.rect.width * 1.5f, playbutton.rect.height * 1.5f), 0.2f).SetEase(curve);
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(Beats());
        }
    }

    void Start()
    {
        StartCoroutine(StartBeats());
        inputFieldPseudo.text = pseudo;
        AudioManager.Instance.PlayMusic(audioClip);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LeaderBoard()
    {
        leaderBoard.SetActive(!leaderBoard.activeInHierarchy);
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }

    public void SetPseudo()
    {
        if (inputFieldPseudo.text != null)
            pseudo = inputFieldPseudo.text;
    }
}
