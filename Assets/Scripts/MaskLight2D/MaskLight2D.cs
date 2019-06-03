using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(MaskLight2DRenderer), PostProcessEvent.BeforeStack, "Custom/Mask Light Renderer")]
public sealed class MaskLight2D : PostProcessEffectSettings
{
    public TextureParameter LightColorRamp = new TextureParameter();
    [Range(0, 0.2f)]
    public FloatParameter BlackCorrection = new FloatParameter() { value = 0.1f };
}

public sealed class MaskLight2DRenderer : PostProcessEffectRenderer<MaskLight2D>
{
    private int globalLightmapTexID;
    private Shader distortionShader;

    public override void Init()
    {
        globalLightmapTexID = Shader.PropertyToID("_GlobalLightmapTex");
        distortionShader = Shader.Find("Hidden/Custom/MaskLight2D");
        base.Init();
    }

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/MaskLight2D"));

        if (settings.LightColorRamp.value != null) {
            sheet.properties.SetTexture("_Ramp", settings.LightColorRamp);
        }
        sheet.properties.SetFloat("_BlackCorrection", settings.BlackCorrection);

        context.command.GetTemporaryRT(globalLightmapTexID,
            context.camera.pixelWidth,
            context.camera.pixelHeight,
            0, FilterMode.Bilinear);
        context.command.SetRenderTarget(globalLightmapTexID);
        context.command.ClearRenderTarget(true, true, Color.clear);

        Light2DManager.Instance.PopulateCommandBuffer(context.command);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
