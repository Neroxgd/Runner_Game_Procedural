using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;
    [SerializeField] Generation _Generation;
    [SerializeField] GameObject[] UIs;
    [SerializeField] int ValueLevelGeneration;

    void Update()
    {
        ValueLevelGeneration = _Generation.getValueLevel();
        sethp();
        setLevel();
        SetTetros();
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
        UIs[1].transform.GetChild(2).GetComponent<TMP_Text>().text = ValueLevelGeneration.ToString();
        UIs[1].transform.GetChild(1).GetComponent<Image>().DOFillAmount(ValueLevelGeneration / 10f, 0.5f);
    }

    void SetTetros()
    {
        List<PrefabsGeneration> list = _Generation.GetCurrentPrefabsGenerations();

        UIs[2].transform.GetChild(0).GetComponent<Image>().sprite = list[0].prefabsGameObjects[ValueLevelGeneration]._Sprite;

        if(ValueLevelGeneration <= 9)
        {
            UIs[2].transform.GetChild(1).GetComponent<Image>().sprite = list[0].prefabsGameObjects[ValueLevelGeneration + 1]._Sprite;
        }
        else
        {
            UIs[2].transform.GetChild(1).GetComponent<Image>().sprite = null;
        }
        if(ValueLevelGeneration <= 8)
        {
            UIs[2].transform.GetChild(2).GetComponent<Image>().sprite = list[0].prefabsGameObjects[ValueLevelGeneration + 2]._Sprite;
        }
        else
        {
            UIs[2].transform.GetChild(1).GetComponent<Image>().sprite = null;
        }
        
        

    }
}
