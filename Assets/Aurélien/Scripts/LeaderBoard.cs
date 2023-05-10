using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scores;

    void Start()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            if (PlayerPrefs.GetInt("score" + i, 0) == 0)
                return;
            scores[i].text = (i + 1) + ") " + PlayerPrefs.GetString("pseudo" + i) + " : " + PlayerPrefs.GetInt("score" + i).ToString() + "m";
        }
    }

    public static void SetNewScore(int newScore, string pseudo)
    {
        for (int i = 0; i < 10; i++)
            if (newScore > PlayerPrefs.GetInt("score" + i, 0))
            {
                for (int j = 10; j > i; j--)
                {
                    PlayerPrefs.SetInt("score" + j, PlayerPrefs.GetInt("score" + (j - 1)));
                    PlayerPrefs.SetString("pseudo" + j, PlayerPrefs.GetString("pseudo" + (j - 1)));
                }
                PlayerPrefs.SetInt("score" + i, newScore);
                PlayerPrefs.SetString("pseudo" + i, pseudo == null ? "unknown" : pseudo);
                return;
            }
    }
}
