using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;
    [SerializeField] GameObject[] UIs;
    [SerializeField] GameObject popUp;

    void Update()
    {
        // sethp();
        setLevel();
        if (Keyboard.current.escapeKey.isPressed)
            popUp.SetActive(true);
        else 
            popUp.SetActive(false);
    }

    public void sethp(int hp)
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene("Scène Aur recupe");
            return;
        }

        // for (int i = 0; i < UIs[0].transform.childCount; i++)
        // {
        //     if (i >= _PlayerController.GetPlayerAttributs().getHP())
        //     {
        //         UIs[0].transform.GetChild(i).gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         UIs[0].transform.GetChild(i).gameObject.SetActive(true);
        //     }
        // }

        UIs[0].transform.GetChild(hp - 1).gameObject.SetActive(false);
    }

    void setLevel()
    {
        UIs[1].transform.GetChild(2).GetComponent<TMP_Text>().text = Generation.Instance.getValueLevel().ToString();
        UIs[1].transform.GetChild(1).GetComponent<Image>().DOFillAmount(Generation.Instance.getValueLevel() / 10f, 0.5f);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
