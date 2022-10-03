Shader "Unlit/NewUnlitShader"
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

            // vertex shader input data
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // vertex shader to fragment shader data
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            // the vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                //o.vertex += float4(1, 1, 1, 0);
                //v.vertex.y = v.vertex.y + 1.0;
                // transforms the vertex/point from 'object space' to 'homogeneous clipping space'
                o.vertex = UnityObjectToClipPos(v.vertex);
                // passthrough the texture coordinate 'uv'
                o.uv = v.uv;

                return o;
            }

            // the fragment shader
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
