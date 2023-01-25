using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;

    [SerializeField] Generation _Generation;
    [SerializeField] GameObject[] UIs;

    void Update()
    {
        sethp();
        setLevel();
    }

    void sethp()
    {
        for (int i=0 ; i < UIs[0].transform.childCount ; i++)
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
        UIs[1].transform.GetChild(2).GetComponent<TMP_Text>().text = _Generation.getValueLevel().ToString();
        UIs[1].transform.GetChild(1).GetComponent<Image>().fillAmount = _Generation.getValueLevel() / 10f;
    }
}
