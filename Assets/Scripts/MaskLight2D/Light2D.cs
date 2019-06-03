using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class Light2D : MonoBehaviour
{
    public Renderer Renderer { get; private set; }
    public Material Material { get; private set; }

    private void OnEnable()
    {
        Renderer = GetComponent<Renderer>();
        Renderer.enabled = false;
        Material = Renderer.sharedMaterial;
        Light2DManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        Light2DManager.Instance.Deregister(this);
    }
}
