using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PrefabsGeneration
{
    public List<PrefabsComplete> prefabsGameObjects = new List<PrefabsComplete>();

}

[Serializable]
public class PrefabsComplete
{
    public GameObject _Object;
    public Sprite _Sprite;
}
