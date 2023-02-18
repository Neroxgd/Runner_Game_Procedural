using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeshTrailPlayer : MonoBehaviour
{
    private bool isTrailActive;
    [SerializeField] private float trailFade = 2f;
    [Header("Mesh Related")]
    [SerializeField] private float meshRefreshRate = 0.1f;
    [SerializeField] private Material materialPlayer;
    [SerializeField] private float meshDestroyDelay = 3f;
    [Header("Shader Related")]
    [SerializeField] private string shaderVarRef;
    [SerializeField] private float shaderVarRate = 0.1f, shaderVarRefeshRate = 0.05f;
    private SkinnedMeshRenderer[] skinnedMeshRendererPlayer;

    void Update()
    {
        if (DOTween.IsTweening(transform.GetComponent<Rigidbody>()) && !isTrailActive)
        {
            isTrailActive = true;
            
            StartCoroutine(ActivateTrail(trailFade));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRendererPlayer == null)
                skinnedMeshRendererPlayer = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < skinnedMeshRendererPlayer.Length; i++)
            {
                GameObject obj = new GameObject();
                obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
                MeshRenderer mr = obj.AddComponent<MeshRenderer>();
                MeshFilter mf = obj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRendererPlayer[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = materialPlayer;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefeshRate));
                
                Destroy(obj, meshDestroyDelay);
            }
            yield return new WaitForSeconds(meshRefreshRate);
        }
        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
