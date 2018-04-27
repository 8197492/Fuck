Shader "XL/LiuGuang" 
{
Properties {
	_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	_FlashTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	_FlashFactor("FlashFactor", Vector) = (0, 1, 0.5, 0.5)
	_DissolveValue("DissolveValue", Vector) = (0, 0, 0, 0)
}

SubShader {
	Tags { "Queue" = "Transparent" "RenderType"="Opaque" }
	LOD 100
	
	Blend SrcAlpha OneMinusSrcAlpha
	Pass 
	{
		AlphaTest Never 0
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			struct appdata_t 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float4 objPos : TEXCOORD2; 
			};

			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
				float4 objPos : TEXCOORD2; 
			};

			sampler2D _MainTex;
			sampler2D _FlashTex;
			float4 _MainTex_ST;
			fixed4 _FlashFactor;
			float4 _DissolveValue;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);  
				o.objPos = v.vertex;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				
				half2 flashuv = i.worldPos.xy * _FlashFactor.zw + _FlashFactor.xy * _Time.y;  
				fixed4 flash = tex2D(_FlashTex, flashuv);
		
				//clip(i.objPos.xyz - _DissolveValue.xyz); 
				
				fixed4 c;
				c.rgb = col.rgb + flash.rgb;
				c.a = col.a;
				
				return c;
			}
		ENDCG
	}
}

}
