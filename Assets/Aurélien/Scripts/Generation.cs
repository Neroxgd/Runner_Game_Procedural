using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] PrefabsObstacle;
    private int indexBloc10Prefabs = 0;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform end;
    [SerializeField][Range(1, 20)] private float SpeedOfObstacle;
    public float getSpeedOfObstacle { get { return SpeedOfObstacle; } }
    [SerializeField][Range(1, 10)] private int TimeBetweenSpawn;
    [SerializeField] private int valueLevel = 0;
    [SerializeField] private Camera cam;
    public static Generation Instance;
    [SerializeField] private AudioClip music;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(music);
        StartCoroutine(spawner());
    }

    void Update()
    {
        if (valueLevel == 10)
        {
            SpeedOfObstacle += 1;
            valueLevel = 0;
            if (indexBloc10Prefabs + 10 > PrefabsObstacle.Length) return;
            indexBloc10Prefabs += 10;
        }
    }

    IEnumerator spawner()
    {
        yield return new WaitForSeconds(TimeBetweenSpawn);

        GameObject currentobstacle = Instantiate(PrefabsObstacle[Random.Range(indexBloc10Prefabs, indexBloc10Prefabs + 10)], spawn.position, Quaternion.identity);
        valueLevel++;
        ApplyCollider(currentobstacle);
        currentobstacle.transform.DOMove(end.position, SpeedOfObstacle, false).SetEase(Ease.InSine).SetSpeedBased(true);
        StartCoroutine(ObstacleDespawn(currentobstacle));
        IEnumerator ObstacleDespawn(GameObject destroyObstacle)
        {
            yield return new WaitUntil(() => Vector3.Distance(destroyObstacle.transform.position, cam.transform.position) < 7.5f);
            DestroyObstacles(destroyObstacle);
        }
        StartCoroutine(spawner());
    }

    private void DestroyObstacles(GameObject currentobject)
    {
        for (int i = 0; i < currentobject.transform.childCount; i++)
            currentobject.transform.GetChild(i).DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCirc).OnComplete(() => Destroy(currentobject));
    }

    void ApplyCollider(GameObject _GameObject)
    {
        for (int i = 0; i < _GameObject.transform.childCount; i++)
        {
            _GameObject.transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    public int getValueLevel()
    {
        return valueLevel;
    }
}
