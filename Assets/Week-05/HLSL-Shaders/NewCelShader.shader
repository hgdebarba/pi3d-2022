Shader "Unlit/NewCelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // vertex input data
            struct appdata
            {
                float4 vertex : POSITION;
                // normals are needed for lighting
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
        
            // vertex to fragment data
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                // we pass the normal from the vertex to the fragment shader, needed for lighting
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                // transforms the vertex/point from 'object space' to 'homogeneous clipping space'
                o.vertex = UnityObjectToClipPos(v.vertex);
                // passthrough the texture coordinate 'uv'
                o.uv = v.uv;
                // transforms the normal from 'object space' to 'world space',
                // we compute lighting in 'world space'
                o.normal = mul((float3x3)UNITY_MATRIX_M, v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample a color from the texture at coordinates 'uv'
                float4 col = tex2D(_MainTex, i.uv);

                // LIGHTING 
                // we retrieve the light direction in world space
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                // we use the dot product to compute the color intensity, and ensure it is not negative with 'max'
                float intensity = max(0, dot(i.normal, lightDirection));
                // combine color and intensity to create the lighting effect
                col.rgb = col.rgb * intensity;

                // CELSHADING (color banding)
                // we make the value have the range [1-5), and round it to integer with a 'floor' operation 
                col.rgb = floor(col.rgb * 3.99 + 1.0);
                // we divide by 4, so our values are .25, .5, .75 and 1. 
                col.rgb = col.rgb * 0.25;

                // each color component is expected to be in the range [0,1], values below/above are clamped to [0,1]
                return col;
            }
            ENDCG
        }
    }
}
