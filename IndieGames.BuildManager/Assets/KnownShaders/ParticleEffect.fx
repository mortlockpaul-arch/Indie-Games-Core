// Camera-facing GPU particles, using the standard 36-byte XNA particle vertex layout.
float4x4 View;
float4x4 Projection;
float2 ViewportScale;
float CurrentTime;
float Duration;
float DurationRandomness;
float3 Gravity;
float EndVelocity;
float4 MinColor;
float4 MaxColor;
float2 RotateSpeed;
float2 StartSize;
float2 EndSize;
texture Texture;
sampler2D Sampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};
struct ParticleInput
{
    float2 corner : POSITION0;
    float3 position : POSITION1;
    float3 velocity : NORMAL0;
    float4 random : COLOR0;
    float birthTime : TEXCOORD0;
};
struct ParticleOutput
{
    float4 position : POSITION0;
    float4 color : COLOR0;
    float2 uv : TEXCOORD0;
};
ParticleOutput ParticleVS(ParticleInput input)
{
    ParticleOutput output;
    float age = (CurrentTime - input.birthTime) * (1 + input.random.x * DurationRandomness);
    float life = saturate(age / max(Duration, 0.0001));
    float3 position = input.position + input.velocity * age * (1 + (EndVelocity - 1) * life * 0.5);
    position += Gravity * age * life;
    output.position = mul(mul(float4(position,1), View), Projection);
    float size = lerp(lerp(StartSize.x, StartSize.y, input.random.y), lerp(EndSize.x, EndSize.y, input.random.y), life);
    float angle = lerp(RotateSpeed.x, RotateSpeed.y, input.random.w) * age;
    float sine, cosine;
    sincos(angle, sine, cosine);
    float2 corner = float2(input.corner.x * cosine - input.corner.y * sine, input.corner.x * sine + input.corner.y * cosine);
    output.position.xy += corner * size * Projection._22 * ViewportScale;
    output.color = lerp(MinColor, MaxColor, input.random.z);
    output.color.a *= (age >= 0 ? 1 : 0) * life * (1-life) * (1-life) * 6.7;
    output.uv = (input.corner + 1) * 0.5;
    return output;
}
float4 ParticlePS(ParticleOutput input) : COLOR0
{
    return tex2D(Sampler, input.uv) * input.color;
}
technique Particles
{
    pass P0
    {
        VertexShader = compile vs_3_0 ParticleVS();
        PixelShader = compile ps_3_0 ParticlePS();
    }
}
