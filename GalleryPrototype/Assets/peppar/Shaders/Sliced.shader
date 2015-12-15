  Shader "Example/Slices" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
	  _Y ("Y", Float) = 1
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      Cull Off

	  	   Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
     
      struct v2f {
          float4 pos : SV_POSITION;
          fixed4 color : COLOR;
      };
      
      v2f vert (appdata_base v)
      {
          v2f o;
          o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
          o.color.xyz = v.normal * 0.5 + 0.5;
          o.color.w = 1.0;
          return o;
      }

      fixed4 frag (v2f i) : SV_Target { return i.color; }
      ENDCG
	      }

	      CGPROGRAM


	  #pragma surface surf Lambert
	  
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float3 worldPos; 
		  float4 pos : SV_POSITION;
      };

      sampler2D _MainTex;
      sampler2D _BumpMap;
	  float _Y;

      void surf (Input IN, inout SurfaceOutput o) {

	  if (_Y > 0)
	  {
          clip (IN.worldPos.y + _Y);         
      }
	  if (_Y < 0)
	  {
          clip (-IN.worldPos.y + _Y);         
      }
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
      }


      ENDCG



    } 
    Fallback "Diffuse"
  }