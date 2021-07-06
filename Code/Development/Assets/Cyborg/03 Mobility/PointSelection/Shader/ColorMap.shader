// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/ColorMap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _floorColor("floor", Color) = (0, 0, 1, 1)
        _obstacleColor("obstacle", Color) = (0.5, 0.5, 0.5, 1)
        _wallColor("wall", Color) = (1, 1, 1, 1)
        _Distance("floorDistance", Range(0, 20)) = 2.9
        _camera("orthoCamera", Vector) = (0, 1.9, 0)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : Color;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _floorColor;
                fixed4 _obstacleColor;
                fixed4 _wallColor;
                float _Distance;
                float3 _camera;
                float3 normal;
                float4 worldPos;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    normal = UnityObjectToWorldNormal(v.normal);
                    worldPos = mul(unity_ObjectToWorld, v.vertex);
                    //calculate normal at each vertex and paint vertices based on normals
                    if (normal.y > 0.2f && (abs(_camera.y - worldPos.y) > _Distance)) {
                        //  if (abs (_camera.y - v.vertex.y) > _Distance){
                        o.color = _floorColor;
                        //  }
                    }
                    else if (normal.y > 0.2f && (abs(_camera.y - worldPos.y) < _Distance)){
                             o.color = _obstacleColor;
                        }               
                    else {
                        o.color = _wallColor;
                     }

                    //o.color.xyz = v.normal.xyz;
                
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col = i.color
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
