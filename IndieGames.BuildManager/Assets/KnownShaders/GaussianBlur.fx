// Legacy 15-tap bloom blur. The game supplies normalized weights and UV offsets.
sampler2D TextureSampler : register(s0);
float2 SampleOffsets[15];
float SampleWeights[15];
float4 Blur(float2 uv : TEXCOORD0) : COLOR0
{
    float4 result = 0;
    [unroll] for (int tap = 0; tap < 15; ++tap)
        result += tex2D(TextureSampler, uv + SampleOffsets[tap]) * SampleWeights[tap];
    return result;
}
technique GaussianBlur
{
    pass Pass1 { PixelShader = compile ps_3_0 Blur(); }
}
