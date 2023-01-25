using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;
    [SerializeField] GameObject[] UIs;

    void Start()
    {
    }

    void Update()
    {
        sethp();
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
}
