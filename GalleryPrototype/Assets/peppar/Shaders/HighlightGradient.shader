Shader "peppar/Custom/Highlight" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_CutTex("Cutout (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5

		_EnablePan("_EnablePan", Int) = 0
		_PanTex("_PanTex", 2D) = "black" {}
		_PanColor("_PanColor", Color) = (1,1,1,1)
		_XScrollSpeed("_XScrollSpeed", Float) = 1
		_YScrollSpeed("_YScrollSpeed", Float) = 1

	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _CutTex;
		fixed4 _Color;
		float _Cutoff;

		sampler2D _PanTex;
		fixed _EnablePan;
		float _XScrollSpeed;
		float _YScrollSpeed;

		fixed4 _PanColor;

		struct Input {
			float2 uv_MainTex;
			float2 uv_PanTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed2 scrollUV = IN.uv_PanTex;
			fixed xScrollValue = _XScrollSpeed * _Time.x;
			fixed yScrollValue = _YScrollSpeed * _Time.x;
			scrollUV += fixed2(xScrollValue, yScrollValue);

			fixed4 pan = tex2D(_PanTex, scrollUV) * _PanColor * _EnablePan;

			fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			fixed4 c = pan + col;

			float ca = tex2D(_CutTex, IN.uv_MainTex).a;

			o.Albedo = c.rgb;

			if (ca > _Cutoff)
				o.Alpha = c.a;
			else
				o.Alpha = 0;
		}
		ENDCG
		}

			Fallback "Transparent/VertexLit"
}