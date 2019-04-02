using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(InvertedRenderer), PostProcessEvent.AfterStack, "Custom/Inverted")]
public sealed class Inverted : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Blend between normal and inverted color")]
    public FloatParameter Blend = new FloatParameter { value = 0f };

    public override bool IsEnabledAndSupported(PostProcessRenderContext context) {
        return enabled.value && Blend.value > 0f;
    }
}

public sealed class InvertedRenderer : PostProcessEffectRenderer<Inverted>
{

    public override void Render(PostProcessRenderContext context) {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Inverted"));
        sheet.properties.SetFloat("_Blend", settings.Blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
