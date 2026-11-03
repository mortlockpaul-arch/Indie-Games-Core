// SpriteBatch-compatible Gaussian blur. ScreenResolution is the source texture size.
float2 ScreenResolution;
sampler2D TextureSampler : register(s0) = sampler_state
{
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = None;
    AddressU = Clamp;
    AddressV = Clamp;
};
float4 GaussianBlurPS(float4 color : COLOR0, float2 uv : TEXCOORD0) : COLOR0
{
    float2 step = 1 / max(ScreenResolution, float2(1,1));
    float4 sum = tex2D(TextureSampler, uv) * 4;
    sum += tex2D(TextureSampler, uv + float2(step.x,0)) * 2;
    sum += tex2D(TextureSampler, uv - float2(step.x,0)) * 2;
    sum += tex2D(TextureSampler, uv + float2(0,step.y)) * 2;
    sum += tex2D(TextureSampler, uv - float2(0,step.y)) * 2;
    sum += tex2D(TextureSampler, uv + step);
    sum += tex2D(TextureSampler, uv - step);
    sum += tex2D(TextureSampler, uv + float2(step.x,-step.y));
    sum += tex2D(TextureSampler, uv + float2(-step.x,step.y));
    return sum * (color / 16);
}
technique GaussianBlur
{
    pass Pass1 { PixelShader = compile ps_3_0 GaussianBlurPS(); }
}
