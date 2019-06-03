Shader "Hidden/Custom/MaskLight2D"
{
	HLSLINCLUDE
		#define S2D(x, y) SAMPLE_TEXTURE2D(x, sampler_x, y);

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		TEXTURE2D_SAMPLER2D(_GlobalLightmapTex, sampler_GlobalLightmapTex);

		TEXTURE2D_SAMPLER2D(_Ramp, sampler_Ramp);

		float _BlackCorrection;

		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
			float4 light = SAMPLE_TEXTURE2D(_GlobalLightmapTex, sampler_GlobalLightmapTex, i.texcoord);
			float4 ramp = SAMPLE_TEXTURE2D(_Ramp, sampler_Ramp, float2(1 - light.r, 0)) - _BlackCorrection;
			return c * ramp;
		}

	ENDHLSL

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM
				#pragma vertex VertDefault
				#pragma fragment Frag
			ENDHLSL
		}
	}
}
