//Original Source: https://nielson.io/2016/04/2d-sprite-outlines-in-unity
Shader "Custom/TintShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData]_Tint ("Tint", Color) = (1,1,1,1)
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)

	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			float4 _Tint;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}


			fixed4 frag(v2f IN) : SV_Target
			{
				float4 rawColor=tex2D(_MainTex, IN.texcoord);
				if( (rawColor.r+rawColor.g+rawColor.b)/3<.9){
					return rawColor* float4(IN.color.r*_Color.r,IN.color.g*_Color.g,IN.color.b*_Color.b,1);
				}
				else{
					return rawColor;
				}
			}
		ENDCG
		}
	}
}
