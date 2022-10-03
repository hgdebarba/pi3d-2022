Shader "Unlit/NewDeformationShader"
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
                float2 uv : TEXCOORD0;
            };

            // vertex to fragment data
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;

                // DEFORMATION:
                // transforms the vertex/point from 'object space' to 'world space'
                float4 pos = mul(UNITY_MATRIX_M, v.vertex); 
                // deform 'x' coord according to time and 'y' coord in 'world space' using 'sin' function
                pos.x = pos.x + sin(_Time.y + pos.y * 5.0) * .25; 
                // transforms the vertex/point from 'world space' to 'homogeneous clip space'
                o.vertex = UnityWorldToClipPos(pos);

                // passthrough the texture coordinate 'uv'
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample a color from the texture at coordinates 'uv'
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
