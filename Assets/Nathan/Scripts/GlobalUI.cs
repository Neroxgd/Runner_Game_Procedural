using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;
    [SerializeField] GameObject[] UIs;

    void Update()
    {
        sethp();
        setLevel();
    }

    void sethp()
    {
        for (int i = 0; i < UIs[0].transform.childCount; i++)
        {
            if (i >= _PlayerController.GetPlayerAttributs().getHP())
            {
                UIs[0].transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                UIs[0].transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    void setLevel()
    {
        UIs[1].transform.GetChild(2).GetComponent<TMP_Text>().text = Generation.Instance.getValueLevel().ToString();
        UIs[1].transform.GetChild(1).GetComponent<Image>().DOFillAmount(Generation.Instance.getValueLevel() / 10f, 0.5f);
    }
}
