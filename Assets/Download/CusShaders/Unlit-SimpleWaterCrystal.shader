// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/SimpleWaterCrystal" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Color("Main Color", color) = (1,1,1,1)
	
	_DisturbTex1 ("Disturb tex1", 2D) = "white" {}
	_DisturbTex2 ("Disturb tex2", 2D) = "white" {}
	_MaskTex ("Mask tex black and white", 2D) = "white" {}
	
	_Disturb1Power("Disturb1Power", Float) = 0.5
	_Disturb2Power("Disturb2Power", Float) = 0.5

	_WaterSpeedX("DisturbWaterSpeedX", Float) = 0
	_WaterSpeedY("DisturbWaterSpeedY", Float) = 0
	_DisturbUSpeed("DisturbUSpeed", Float) = 0
	_DisturbVSpeed("DisturbVSpeed", Float) = 0

	_AlphaMPower("AlphaMPower", range(0,5)) = 1.0
	_AlphaPower("AlphaPower", range(0,5)) = 0

	_DiffuseUVClamp("DiffuseUVClamp", range(0,1)) = 1.0

}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
						// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				half4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f {
				half4 vertex : SV_POSITION;
				half4 texcoord : TEXCOORD0;
				half4 texcoord2 : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};

			sampler2D _MainTex;
			half4 _MainTex_ST;
			half _DisturbUSpeed;
			half _DisturbVSpeed;
			half _DisturbUSpeed2;
			half _DisturbVSpeed2;		
			half _WaterSpeedX;	
			half _WaterSpeedY;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord.xy = TRANSFORM_TEX(v.texcoord, _MainTex) + half2(_WaterSpeedX, _WaterSpeedY) * _Time.x;
				o.texcoord.zw = v.texcoord;
				o.texcoord2.xy = o.texcoord + half2(_DisturbUSpeed, _DisturbVSpeed) * _Time.x;
				o.texcoord2.zw = o.texcoord + half2(_DisturbUSpeed2, _DisturbVSpeed2) * _Time.x;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			//---------------------------------------------------------------
			// Fn
			//---------------------------------------------------------------
			half _BlackPointAlpha(half alpha,half level)
			{
				half   Diff = 1.0-level;
				return (alpha-level)/Diff;
			}
				
			half _Disturb1Power;
			half _Disturb1Power2;
			sampler2D _DisturbTex1;
			sampler2D _DisturbTex2;
			half _DiffuseUVClamp;
			sampler2D _MaskTex;
			half _DisturbMaskPower1;
			half _DisturbMaskPower2;
			fixed4 _Color;
			half _AlphaPower;
			half _AlphaMPower;
		
			fixed4 frag (v2f i) : Color
			{
				fixed2 DisturbColor = tex2D(_DisturbTex1, i.texcoord2.xy ).xy*2-1;
				fixed2 DisturbColor2 = tex2D(_DisturbTex2, i.texcoord2.zw ).xy*2-1;

				half2 tempClampUV = i.texcoord.xy + DisturbColor.xy*_Disturb1Power + DisturbColor2.xy*_Disturb1Power2;

				fixed4 TexColor  = _DiffuseUVClamp>0.5f ? tex2D(_MainTex,tempClampUV):tex2D(_MainTex,saturate(tempClampUV));

				fixed MaskColor  = tex2D(_MaskTex , i.texcoord.zw +DisturbColor.xy*_DisturbMaskPower1+DisturbColor2.xy*_DisturbMaskPower2).g;

				TexColor.a *= MaskColor;
				TexColor.a *=_BlackPointAlpha(dot(TexColor.rgb,fixed3(0.3,0.59,0.11)),_AlphaPower)*_AlphaMPower;
								
				TexColor.rgba *= _Color.rgba;

				UNITY_APPLY_FOG(i.fogCoord, TexColor);

				return TexColor;
			}
		ENDCG
	}
}

}
