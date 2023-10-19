using UnityEngine;

using System.Collections.Generic;

using System;
using UnityEngine.Serialization;

[Serializable]
public class SubMeshes
{
    public MeshRenderer meshRenderer;
    public Vector3 originalPosition;
    public Vector3 explodedPosition;
}


public class ExplodeBike : MonoBehaviour
{
    public List<SubMeshes> childMeshRenderers;
    bool isInExplodedView = false;
    public float explosionSpeed = 0.1f;
    public float offset = 3f;
    bool isMoving = false;

    public bool ToggleExplode = false;
    private void Awake()
    {
        Init();
    }


    private void Update()
    {
        if (ToggleExplode)
        {
            ToggleExplode = false;
            ToggleExplodedView();
        }
        Explode();
    }

    private void Init()
    {
        childMeshRenderers = new List<SubMeshes>();
        foreach (var item in GetComponentsInChildren<MeshRenderer>())
        {
            SubMeshes mesh = new SubMeshes();
            mesh.meshRenderer = item;
            mesh.originalPosition = item.transform.position;
            mesh.explodedPosition = item.transform.position * offset; //item.bounds.center * offset;
            childMeshRenderers.Add(mesh);
        }
    }
    
    private void Explode()
    {
        if (isMoving)
        {
            if (isInExplodedView)
            {
                foreach (var item in childMeshRenderers)
                {
                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.explodedPosition, explosionSpeed);
                    if (Vector3.Distance(item.meshRenderer.transform.position, item.explodedPosition) < 0.001f)
                    {
                        isMoving = false;
                    }
                }
            }
            else
            {
                foreach (var item in childMeshRenderers)
                {
                    item.meshRenderer.transform.position = Vector3.Lerp(item.meshRenderer.transform.position, item.originalPosition, explosionSpeed);
                    if (Vector3.Distance(item.meshRenderer.transform.position, item.originalPosition) < 0.001f)
                    {
                        isMoving = false;
                    }
                }
            }
        }
    }


    #region CustomFunctions


    public void ToggleExplodedView()

    {

        if (isInExplodedView)

        {

            isInExplodedView = false;

            isMoving = true;

        }

        else

        {

            isInExplodedView = true;

            isMoving = true;

        }

    }


    #endregion

}