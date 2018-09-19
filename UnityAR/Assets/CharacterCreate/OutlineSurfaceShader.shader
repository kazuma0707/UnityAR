Shader "Custom/OutlineSurfaceShader" {

	Properties {
		_OutlineColor("Outline Color", Color) = (0, 0, 0, 0)
		_OutlineWidth("Outline Width", float) = 0.1
		_MainColor("Main Color", Color) = (1, 1, 1, 1)
	}

	SubShader {
		Tags {
			"Queue"="Geometry"
		}

		//1パス目.

		Cull Front
		
		CGPROGRAM

		#pragma surface surf Lambert vertex:vert

		float4 _MainColor;
		float4 _OutlineColor;
		float _OutlineWidth;

		struct Input {
			float4 vertexColor : COLOR;
		};

		void vert(inout appdata_full v, out Input o) {
			float distance = -UnityObjectToViewPos(v.vertex).z;
			v.vertex.xyz += v.normal * distance * _OutlineWidth;
			o.vertexColor = v.color;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _OutlineColor.rgb;
			o.Emission = _OutlineColor.rgb;
		}
		ENDCG


		//2パス目.

		Cull Back
		
		CGPROGRAM

		#pragma surface surf Lambert

		float4 _MainColor;

		struct Input {
			float4 vertexColor : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _MainColor;
		}
		ENDCG

	}
}


