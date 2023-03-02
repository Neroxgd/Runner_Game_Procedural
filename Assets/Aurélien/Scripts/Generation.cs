using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] PrefabsObstacle;
    private int indexBloc10Prefabs = 1;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform end;
    [SerializeField][Range(1, 10)] private float SpeedOfObstacle;
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
            indexBloc10Prefabs++;
            if (indexBloc10Prefabs == 5)
                indexBloc10Prefabs--;
            valueLevel = 0;
        }
    }
    IEnumerator spawner()
    {
        yield return new WaitForSeconds(TimeBetweenSpawn);

        GameObject currentobstacle = Instantiate(PrefabsObstacle[Mathf.Clamp(Random.Range((indexBloc10Prefabs - 1) * PrefabsObstacle.Length, indexBloc10Prefabs * PrefabsObstacle.Length), 0, 60)], spawn.position, Quaternion.identity);
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
