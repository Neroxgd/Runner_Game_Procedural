using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] PrefabsObstacle;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform end;
    [SerializeField][Range(1, 10)] private float TimeOfTheObstacleSequence;
    [SerializeField][Range(1, 10)] private int TimeBetweenSpawn;

    void Start()
    {
        StartCoroutine(spawner());
    }

    IEnumerator spawner()
    {
        yield return new WaitForSeconds(TimeBetweenSpawn);
        GameObject currentobstacle = Instantiate(PrefabsObstacle[Random.Range(0, PrefabsObstacle.Length)], spawn.position, Quaternion.identity);
        Sequence sequenceObstacle = DOTween.Sequence();
        sequenceObstacle.Append(currentobstacle.transform.DOMove(end.position, TimeOfTheObstacleSequence, false).SetEase(Ease.InSine));
        sequenceObstacle.InsertCallback(TimeOfTheObstacleSequence * 0.5f, () => DestroyObstacles(currentobstacle));
        StartCoroutine(spawner());
    }

    private void DestroyObstacles(GameObject currentobject)
    {
        for (int i = 0; i < currentobject.transform.childCount; i++)
            currentobject.transform.GetChild(i).DOScale(Vector3.zero, TimeOfTheObstacleSequence * 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(currentobject));
    }
}
