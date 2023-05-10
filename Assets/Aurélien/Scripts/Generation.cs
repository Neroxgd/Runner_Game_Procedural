using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] PrefabsObstacle;
    // private int indexBloc10Prefabs = 0;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform end;
    [SerializeField] private float speedOfObstacle;
    public float getSpeedOfObstacle { get { return speedOfObstacle; } }
    [SerializeField] private float timeBetweenSpawn;
    private int valueLevel;
    [SerializeField] private Camera cam;
    public static Generation Instance;
    [SerializeField] private AudioClip music;
    private List<Transform> listCurrentObstacleMoving = new List<Transform>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(music);
        StartCoroutine(spawner());
    }

    // void ChangeValueLevel()
    // {
    //     if (valueLevel == 10)
    //     {
    //         speedOfObstacle += 4;
    //         valueLevel = 0;
    //         int baseTimeBetweenSpawn = timeBetweenSpawn;
    //         timeBetweenSpawn *= 3;
    //         StartCoroutine(ResetTimeBetweenSpawn());
    //         IEnumerator ResetTimeBetweenSpawn()
    //         {
    //             yield return 0;
    //             timeBetweenSpawn = baseTimeBetweenSpawn;
    //         }
    //         if (indexBloc10Prefabs + 10 > PrefabsObstacle.Length) return;
    //         indexBloc10Prefabs += 10;
    //     }
    // }

    void Update()
    {
        speedOfObstacle += Time.deltaTime;
        timeBetweenSpawn = Mathf.Lerp(timeBetweenSpawn, 1, 0.0005f);
        foreach (Transform obstacle in listCurrentObstacleMoving)
            obstacle.Translate(-transform.forward * speedOfObstacle * Time.deltaTime);
    }

    IEnumerator spawner()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);

        GameObject currentobstacle = Instantiate(PrefabsObstacle[Random.Range(0, PrefabsObstacle.Length - 1)], spawn.position, Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, Random.Range(0f, 270f) % 90 * Mathf.Deg2Rad));
        // valueLevel++;
        ApplyCollider(currentobstacle);
        // currentobstacle.transform.DOMove(end.position, speedOfObstacle * 1f, false).SetEase(Ease.InSine).SetSpeedBased(true);
        StartCoroutine(ObstacleDespawn(currentobstacle));
        IEnumerator ObstacleDespawn(GameObject destroyObstacle)
        {
            yield return new WaitUntil(() => Vector3.Distance(destroyObstacle.transform.position, cam.transform.position) < 10f + speedOfObstacle / 8);
            DestroyObstacles(destroyObstacle);
        }
        listCurrentObstacleMoving.Add(currentobstacle.transform);
        // ChangeValueLevel();
        StartCoroutine(spawner());
    }

    private void DestroyObstacles(GameObject currentobject)
    {
        for (int i = 0; i < currentobject.transform.childCount; i++)
            currentobject.transform.GetChild(i).DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCirc)
            .OnComplete(() =>
            {
                listCurrentObstacleMoving.Remove(currentobject.transform);
                Destroy(currentobject);
            });
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
