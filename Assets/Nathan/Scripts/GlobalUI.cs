using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GlobalUI : MonoBehaviour
{
    [SerializeField] PlayerController _PlayerController;
    [SerializeField] Generation _generation;
    [SerializeField] GameObject[] UIs;
    [SerializeField] GameObject popUp, prefabHeart;
    [SerializeField] TextMeshProUGUI UIdistance;
    private float distance;
    private float speedOfObstacleAtStart;

    void Start()
    {
        speedOfObstacleAtStart = _generation.getSpeedOfObstacle;
        print(_PlayerController._PlayerAttributs.getHP());
        for (int i = 0; i < _PlayerController._PlayerAttributs.getHP(); i++)
        {
            Instantiate(prefabHeart, Vector3.zero, Quaternion.identity, UIs[0].transform);
            print("jza");
        }
            
    }

    void Update()
    {
        // sethp();
        setLevel();
        if (Keyboard.current.escapeKey.isPressed)
            popUp.SetActive(true);
        else
            popUp.SetActive(false);
        distance += _generation.getSpeedOfObstacle / speedOfObstacleAtStart * Time.deltaTime;
        UIdistance.text = $"{(int)distance}m";
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
