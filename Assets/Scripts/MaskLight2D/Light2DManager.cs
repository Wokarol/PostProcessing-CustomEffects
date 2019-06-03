using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Light2DManager
{
    private static Light2DManager instance;
    public static Light2DManager Instance => instance = instance ?? new Light2DManager();

    private readonly List<Light2D> lights = new List<Light2D>();

    public void Register(Light2D light) => lights.Add(light);
    public void Deregister(Light2D light) => lights.Remove(light);

    public void PopulateCommandBuffer(CommandBuffer buffer)
    {
        for (int i = 0; i < lights.Count; i++) {
            var light = lights[i];
            buffer.DrawRenderer(light.Renderer, light.Material);
        }
    }
}
