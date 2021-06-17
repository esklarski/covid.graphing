Shader "Custom/Point Surface"
{
    Properties {
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Color ("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        CGPROGRAM
        #pragma surface ConfigureSurface Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

        float _Smoothness;
        fixed4 _Color;

        void ConfigureSurface(Input input, inout SurfaceOutputStandard surface)
        // void ConfigureSurface(inout SurfaceOutputStandard surface)
        {
            surface.Albedo = _Color.rgb;
            surface.Smoothness = 0.5;
        }

        ENDCG
    }

    FallBack "Diffuse"
}
