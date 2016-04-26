Shader "peppar/Custom/UVPan" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("_MainTex", 2D) = "white" {}

		_EnablePan("_EnablePan", Int) = 0

		_PanTex("_PanTex", 2D) = "black" {}
		_PanColor("_PanColor", Color) = (1,1,1,1)
			//_Glossiness("Smoothness", Range(0,1)) = 0.5
			//_Metallic("Metallic", Range(0,1)) = 0.0
			_XScrollSpeed("_XScrollSpeed", Float) = 1
			_YScrollSpeed("_YScrollSpeed", Float) = 1
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }

			Cull Off 

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

			struct Input {
				float2 uv_MainTex;
				float2 uv_PanTex;
			};

			//half _Glossiness;
			//half _Metallic;
			fixed4 _Color;
			fixed4 _PanColor;
			fixed _EnablePan;

			void surf(Input IN, inout SurfaceOutputStandard o) {

				fixed2 scrollUV = IN.uv_PanTex;
				fixed xScrollValue = _XScrollSpeed * _Time.x;
				fixed yScrollValue = _YScrollSpeed * _Time.x;
				scrollUV += fixed2(xScrollValue, yScrollValue);

				// Albedo comes from a texture tinted by color
				fixed4 pan = tex2D(_PanTex, scrollUV) * _PanColor * _EnablePan;
				fixed4 col = tex2D(_MainTex, IN.uv_MainTex)   * _Color;
				fixed4 c = pan + col;
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				//o.Metallic = _Metallic;
				//o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}