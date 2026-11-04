// Legacy XNA bloom contract; slot 0 is bloom, slot 1 is the original scene.
sampler2D BloomSampler : register(s0);
sampler2D BaseSampler : register(s1);
float BloomIntensity, BaseIntensity, BloomSaturation, BaseSaturation;
float4 Saturation(float4 value, float amount)
{
    float luminance = dot(value.rgb, float3(0.3, 0.59, 0.11));
    return lerp(luminance, value, amount);
}
float4 Combine(float2 uv : TEXCOORD0) : COLOR0
{
    float4 glow = Saturation(tex2D(BloomSampler, uv), BloomSaturation) * BloomIntensity;
    float4 scene = Saturation(tex2D(BaseSampler, uv), BaseSaturation) * BaseIntensity;
    return scene * (1 - saturate(glow)) + glow;
}
technique BloomCombine
{
    pass Pass1 { PixelShader = compile ps_3_0 Combine(); }
}
