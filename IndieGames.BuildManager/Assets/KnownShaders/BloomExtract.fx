// Legacy XNA bloom contract: extract brightness above the supplied threshold.
sampler2D TextureSampler : register(s0);
float BloomThreshold;
float4 Extract(float2 uv : TEXCOORD0) : COLOR0
{
    return saturate((tex2D(TextureSampler, uv) - BloomThreshold) / (1 - BloomThreshold));
}
technique BloomExtract
{
    pass Pass1 { PixelShader = compile ps_3_0 Extract(); }
}
