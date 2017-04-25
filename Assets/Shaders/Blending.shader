Shader "Custom/Blending" {
	Properties
	{
		_AlphaOffset("AlphaOffset", Range(1, 2)) = 1
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader{
		Tags{ "Queue" = "Transparent" }
		// draw after all opaque geometry has been drawn
		Pass{
		ZWrite Off // don't write to depth buffer 
				   // in order not to occlude other objects

		Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

		CGPROGRAM

#pragma vertex vert 
#pragma fragment frag

		sampler2D _MainTex;
	float _AlphaOffset;

	struct VertexData {
		float4 position : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct FragmentData {
		float4 position : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	FragmentData vert(VertexData v)
	{
		FragmentData f;
		f.uv = v.uv.xy;
		f.position = mul(UNITY_MATRIX_MVP, v.position);
		return f;
	}

	float4 frag(FragmentData f) : SV_TARGET
	{
		float4 color = tex2D(_MainTex, f.uv);
		color.a = _AlphaOffset - color.r;
		color.r = 0;
		color.g = 0;
		color.b = 0;
		return color;
	}

		ENDCG
	}
	}
}