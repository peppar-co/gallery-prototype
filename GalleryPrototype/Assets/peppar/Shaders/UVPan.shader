Shader "Custom/UVPan" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_PanTex("Pan Tex (RGB)", 2D) = "black" {}
		_PanAlpha("Pan Alpha", Range(0,1)) = 0.5
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_XScrollSpeed("X Scroll Speed", Float) = 1
		_YScrollSpeed("X Scroll Speed", Float) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _PanTex;
			float _XScrollSpeed;
			float _YScrollSpeed;
			float _PanAlpha;

			struct Input {
				float2 uv_MainTex;
				float2 uv_PanTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o) {

				fixed2 scrollUV = IN.uv_PanTex;
				fixed xScrollValue = _XScrollSpeed * _Time.x;
				fixed yScrollValue = _YScrollSpeed * _Time.x;
				scrollUV += fixed2(xScrollValue, yScrollValue);

				// Albedo comes from a texture tinted by color
				fixed4 pan = tex2D(_PanTex, scrollUV) * _PanAlpha;
				fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				fixed4 c = pan + col;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}