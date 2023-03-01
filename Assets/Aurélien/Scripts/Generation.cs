using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Generation : MonoBehaviour
{
    [SerializeField] List<PrefabsGeneration> prefabsIndex = new List<PrefabsGeneration>();
    [SerializeField] List<PrefabsGeneration> currentPrefabIndex = new List<PrefabsGeneration>();
    private int index;
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform end;
    [SerializeField][Range(1, 10)] private float SpeedOfObstacle;
    [SerializeField][Range(1, 10)] private int TimeBetweenSpawn;
    [SerializeField] private int valueLevel = 0;

    void Start()
    {
        Shuffle();
        index = 0;
        StartCoroutine(spawner());
    }

    void Update()
    {
        if (valueLevel == 10)
        {
            index++;
            valueLevel = 0;
        }
    }
    IEnumerator spawner()
    {
        yield return new WaitForSeconds(TimeBetweenSpawn);

        //GameObject currentObstacle = Instantiate(prefabsIndex[index].prefabsGameObjects[Random.Range(0 , prefabsIndex[index].prefabsGameObjects.Count)] , spawn.position , Quaternion.identity);
        GameObject currentObstacle = Instantiate(currentPrefabIndex[0].prefabsGameObjects[valueLevel]._Object , spawn.position , Quaternion.identity);
        valueLevel++;
        ApplyCollider(currentObstacle);
        Sequence sequenceObstacle = DOTween.Sequence();
        sequenceObstacle.Append(currentObstacle.transform.DOMove(end.position, SpeedOfObstacle, false).SetEase(Ease.InSine));
        sequenceObstacle.InsertCallback(SpeedOfObstacle * 0.5f, () => DestroyObstacles(currentObstacle)).SetSpeedBased(true);
        StartCoroutine(spawner());
    }

    void Shuffle()
    {
        for (int i=0 ; i<currentPrefabIndex.Count ; i++)
        {
            for (int j=0 ; j<10 ; j++)
            {
               currentPrefabIndex[i].prefabsGameObjects[j] = prefabsIndex[i].prefabsGameObjects[Random.Range(0 , prefabsIndex[i].prefabsGameObjects.Count)]; 
            }
        }
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

    public int getValueLevel()
    {
        return valueLevel;
    }

    public List<PrefabsGeneration> GetCurrentPrefabsGenerations()
    {
        return currentPrefabIndex;
    }
}
