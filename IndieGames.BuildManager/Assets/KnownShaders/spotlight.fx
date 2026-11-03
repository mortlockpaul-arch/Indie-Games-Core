// Desktop reconstruction of the billiards floor spotlight effect.
float4x4 xWorld;
float4x4 xView;
float4x4 xProjection;
float xAmbient;
float3 xLightPosition;
float3 xConeDirection;
float xConeAngle;
float xConeDecay;
float xLightStrength;

struct VertexInput
{
    float4 position : POSITION0;
    float3 normal : NORMAL0;
    float2 texCoord : TEXCOORD0;
};
struct VertexOutput
{
    float4 position : POSITION0;
    float3 worldPosition : TEXCOORD0;
    float3 normal : TEXCOORD1;
};
VertexOutput SpotLightVS(VertexInput input)
{
    VertexOutput output;
    float4 world = mul(input.position, xWorld);
    output.worldPosition = world.xyz;
    output.normal = normalize(mul(input.normal, (float3x3)xWorld));
    output.position = mul(mul(world, xView), xProjection);
    return output;
}
float4 SpotLightPS(VertexOutput input) : COLOR0
{
    float3 toLight = xLightPosition - input.worldPosition;
    float3 lightDirection = toLight * rsqrt(max(dot(toLight, toLight), 0.000001));
    float3 coneDirection = xConeDirection * rsqrt(max(dot(xConeDirection, xConeDirection), 0.000001));
    float coneDot = dot(-lightDirection, coneDirection);
    float cutoff = cos(clamp(xConeAngle, 0.0001, 3.14149));
    float edge = smoothstep(cutoff, min(1, cutoff + 0.04), coneDot);
    float cone = pow(max(saturate(coneDot), 0.000001), max(xConeDecay, 0.0001));
    float diffuse = saturate(dot(normalize(input.normal), lightDirection));
    float light = saturate(xAmbient + xLightStrength * diffuse * cone * edge);
    return float4(light, light, light, 1);
}
technique SpotLight
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 SpotLightVS();
        PixelShader = compile ps_3_0 SpotLightPS();
    }
}
