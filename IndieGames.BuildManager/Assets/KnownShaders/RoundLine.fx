// Desktop replacement for the Xbox 360 RoundLine effect.
// Matches Maximinus.RoundLineVertex and batches of 200 (x, y, length, angle) instances.
float4x4 viewProj;
float4 instanceData[200];
float time;
float lineRadius;
float4 lineColor;
float blurThreshold;

struct VertexInput
{
    float3 position : POSITION0;
    float2 rhoTheta : NORMAL0;
    float2 scaleTrans : TEXCOORD0;
    float instanceIndex : TEXCOORD1;
};

struct VertexOutput
{
    float4 position : POSITION0;
    float2 rhoTheta : TEXCOORD0;
    float alongLine : TEXCOORD1;
};

VertexOutput RoundLineVS(VertexInput input)
{
    VertexOutput output;
    float4 instance = instanceData[(int)input.instanceIndex];
    float2 localPosition = input.position.xy * lineRadius;
    localPosition.x += input.scaleTrans.y * instance.z;
    float sine, cosine;
    sincos(instance.w, sine, cosine);
    float2 worldPosition = instance.xy + float2(
        localPosition.x * cosine - localPosition.y * sine,
        localPosition.x * sine + localPosition.y * cosine);
    output.position = mul(float4(worldPosition, 0, 1), viewProj);
    output.rhoTheta = input.rhoTheta;
    output.alongLine = localPosition.x / max(abs(lineRadius), 0.0001);
    return output;
}

float Coverage(float distance)
{
    return 1 - smoothstep(clamp(blurThreshold, 0, 0.9999), 1, distance);
}

float4 ColoredLine(float coverage, float brightness)
{
    float alpha = saturate(coverage * lineColor.a);
    return float4(lineColor.rgb * brightness * alpha, alpha);
}

float4 StandardPS(VertexOutput input) : COLOR0
{
    return ColoredLine(Coverage(input.rhoTheta.x), 1);
}

float4 NoBlurPS(VertexOutput input) : COLOR0
{
    return ColoredLine(1, 1);
}

float4 TubularPS(VertexOutput input) : COLOR0
{
    float lighting = 0.35 + 0.65 * sqrt(saturate(1 - input.rhoTheta.x * input.rhoTheta.x));
    return ColoredLine(Coverage(input.rhoTheta.x), lighting);
}

float4 GlowPS(VertexOutput input) : COLOR0
{
    float glow = exp2(-3 * input.rhoTheta.x * input.rhoTheta.x);
    return ColoredLine(Coverage(input.rhoTheta.x) * glow, 1);
}

float4 AnimatedLinearPS(VertexOutput input) : COLOR0
{
    float pulse = 0.55 + 0.45 * sin(input.alongLine * 0.5 - time * 4);
    return ColoredLine(Coverage(input.rhoTheta.x), pulse);
}

float4 AnimatedRadialPS(VertexOutput input) : COLOR0
{
    float pulse = 0.55 + 0.45 * sin(input.rhoTheta.x * 8 - time * 4);
    return ColoredLine(Coverage(input.rhoTheta.x), pulse);
}

#define LINE_PASS(PIXEL_SHADER) \
    pass P0 { \
        VertexShader = compile vs_3_0 RoundLineVS(); \
        PixelShader = compile ps_3_0 PIXEL_SHADER(); \
        AlphaBlendEnable = true; \
        SrcBlend = One; \
        DestBlend = InvSrcAlpha; \
        BlendOp = Add; \
        SeparateAlphaBlendEnable = true; \
        SrcBlendAlpha = One; \
        DestBlendAlpha = InvSrcAlpha; \
        BlendOpAlpha = Add; \
        CullMode = None; \
    }

technique Standard { LINE_PASS(StandardPS) }
technique NoBlur { LINE_PASS(NoBlurPS) }
technique Tubular { LINE_PASS(TubularPS) }
technique Glow { LINE_PASS(GlowPS) }
technique AnimatedLinear { LINE_PASS(AnimatedLinearPS) }
technique AnimatedRadial { LINE_PASS(AnimatedRadialPS) }
