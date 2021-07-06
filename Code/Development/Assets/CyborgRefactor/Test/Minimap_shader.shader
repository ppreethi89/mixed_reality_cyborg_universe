Shader "Custom/Minimap_shader" {

    properties{

        // _Point("a point in world space", Vector) = (0, 9.53, 0, 1)

         _Distance("Distance", Range(0, 20)) = 0.996

    }

        SubShader{


           Pass {

              CGPROGRAM

              #pragma vertex vert  

              #pragma fragment frag 

              #include "UnityCG.cginc"

              uniform float4 _Point;

              uniform float _Distance;

              // uniform float4x4 unity_ObjectToWorld; 

              // automatic definition of a Unity-specific uniform parameter

              struct vertexInput {

                 float4 vertex : POSITION;

                 float3 normal : NORMAL;

              };

              struct vertexOutput {

                 float4 pos : SV_POSITION;

                 float4 position_in_world_space : TEXCOORD0;

                 float3 normal : NORMAL;

              };




              vertexOutput vert(vertexInput input)

              {

                 vertexOutput output;

                 output.pos = UnityObjectToClipPos(input.vertex);

                 // Calculate the normal in WorldSpace.

                 output.normal = UnityObjectToWorldNormal(input.normal);

                 output.position_in_world_space =

                    mul(unity_ObjectToWorld, input.vertex);

                 // transformation of input.vertex from object 

                 // coordinates to world coordinates;

                 // Calculate where the object is in world space.

              return output;

           }

              float3 _orthographicCamera;



           float4 frag(vertexOutput input) : SV_Target

           {

               fixed4 ret = float4(1,1,1,1);

           // fixed4 _point = _orthographic;

           // _point.y = 9.53;

           // _point.y = _orthographic;




        // Normals need to be renormalized in the fragment shader to overcome 

        // interpolation.

            float3 normal = normalize(input.normal);

            float dist = distance(normal,

                _orthographicCamera);

            float dist1 = abs(normal.y - _orthographicCamera.y);

            // computes the distance between the fragment position 

            // and the origin (the 4th coordinate should always be 

            // 1 for points).

         if ((dist1 > _Distance) && (abs(normal.y) > 0.2f))
         {
             //return float4(0.0, 1.0, 0.0, 1.0);
             ret.b = 1;

             ret.r = 0;

             ret.g = 0;
          }
          else if (abs(normal.y) < 0.2f) {

              ret.b = 1;

              ret.r = 1;

              ret.g = 1;

          }
          else
          {

             //return float4(0.0, 1.0, 0.0, 1.0);

             ret.b = 0.5;

             ret.r = 0.5;

             ret.g = 0.5;

         }

            return ret;

            // color near origin

     }

     ENDCG

     }

    }

}