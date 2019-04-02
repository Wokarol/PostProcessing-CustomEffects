using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(InvertedRenderer), PostProcessEvent.AfterStack, "Custom/Inverted")]
public sealed class Inverted : PostProcessEffectSettings
{

}

public sealed class InvertedRenderer : PostProcessEffectRenderer<Inverted>
{
    public override void Render(PostProcessRenderContext context) {
    }
}
