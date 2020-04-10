Shader "Hidden/TerrainEngine/Details/WavingDoublePass" 
{
    Properties 
    {
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
        _Cutoff ("Cutoff", float) = 0.5
    }
    
    SubShader 
    {
        Tags 
        {
            "Queue" = "Geometry+200"
            //"IgnoreProjector"="True"
            "RenderType"="Grass"
        }
        Cull Off
        ColorMask RGB
        
        CGPROGRAM
            #pragma surface surf Lambert vertex:WavingGrassVert2 addshadow 
            #include "TerrainEngine.cginc"
            
            sampler2D _MainTex;
            fixed _Cutoff;
            
            struct Input {
                float2 uv_MainTex;
                float2 uv_BumpMap;
                fixed4 color : COLOR;
            };
            
            void WavingGrassVert2 (inout appdata_full v)
            {
                // MeshGrass v.color.a: 1 on top vertices, 0 on bottom vertices
                // _WaveAndDistance.z == 0 for MeshLit
                v.color.a = v.color.r;
                float waveAmount = v.color.a * _WaveAndDistance.z;
                v.color = TerrainWaveGrass (v.vertex, waveAmount, v.color);
            }
            
            void surf (Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * fixed4(IN.color.xyz * 0.5, 1);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
                clip (o.Alpha - _Cutoff);
            }
        ENDCG
    }
    
	Fallback Off
}
