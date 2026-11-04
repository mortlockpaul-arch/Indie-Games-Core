// Equivalent shadow-mask reconstruction: recovered vertex program is world/view/projection,
// recovered pixel program writes opaque black. Keep expanded decompilation in the analysis archive.
float4x4 world;
float4x4 view;
float4x4 projection;
float4 ShadowVS(float4 position : POSITION0) : POSITION0
{
    return mul(mul(mul(float4(position.xyz, 1), world), view), projection);
}
float4 ShadowPS() : COLOR0 { return float4(0, 0, 0, 1); }
technique shadow
{
    pass P0
    {
        VertexShader = compile vs_3_0 ShadowVS();
        PixelShader = compile ps_3_0 ShadowPS();
    }
}
