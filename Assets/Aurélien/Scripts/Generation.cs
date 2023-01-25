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
    [SerializeField][Range(1, 10)] private int TimeBetweenSpawn;

    void Start()
    {
        if (PrefabsObstacle.Length < 10)
        {
            Debug.LogError("List of prefabs obstacles is lower than 10");
            return;
        }
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        yield return new WaitForSeconds(TimeBetweenSpawn);
        GameObject currentobstacle = Instantiate(PrefabsObstacle[Random.Range((indexBloc10Prefabs - 1) * 10, indexBloc10Prefabs * 10)], spawn.position, Quaternion.identity);
        ApplyCollider(currentobstacle);
        Sequence sequenceObstacle = DOTween.Sequence();
        sequenceObstacle.Append(currentobstacle.transform.DOMove(end.position, SpeedOfObstacle, false).SetEase(Ease.InSine));
        sequenceObstacle.InsertCallback(SpeedOfObstacle * 0.5f, () => DestroyObstacles(currentobstacle)).SetSpeedBased(true);
        StartCoroutine(spawner());
    }

    private void DestroyObstacles(GameObject currentobject)
    {
        for (int i = 0; i < currentobject.transform.childCount; i++)
            currentobject.transform.GetChild(i).DOScale(Vector3.zero, SpeedOfObstacle * 0.5f).SetEase(Ease.InCirc).OnComplete(() => Destroy(currentobject));
    }

    void ApplyCollider(GameObject _GameObject)
    {
        for (int i = 0; i < _GameObject.transform.childCount; i++)
        {
            _GameObject.transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
