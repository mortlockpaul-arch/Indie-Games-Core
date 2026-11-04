float4x4 View;
float4x4 Projection;
float4x4 World;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
	AddressU = Clamp;
	AddressV = Clamp;
	AddressW = Clamp;
};

struct vs_HardwareInstancing_Pass0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog : FOG;
	float4 blendweight4 : BLENDWEIGHT4;
};

struct vs_HardwareInstancing_Pass0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_HardwareInstancing_Pass0_VS_OUT vs_HardwareInstancing_Pass0(vs_HardwareInstancing_Pass0_VS_IN i)
{
	vs_HardwareInstancing_Pass0_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r5 = i.position;
	r1.xy = i.texcoord.xy;
	r4 = i.blendweight;
	r6 = i.blendweight1;
	r7 = i.blendweight2;
	r3 = i.blendweight3;
	r2.x = i.fog.x;
	r0 = i.blendweight4;
	r3 = r5.w * r3;
	r1.w = 1 / r0.z;
	r3 = r5.z * r7 + r3;
	r3 = r5.y * r6.xzwy + r3.xzwy;
	o.position = r5.x * r4 + r3.xwyz;
	r1.z = 1 / r0.x;
	o.fog1 = saturate(r2.x);
	o.texcoord = r1.zw * r1.xy + r0.yw;

	return o;
}

struct ps_HardwareInstancing_Pass0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float fog1 : FOG1;
};

float4 ps_HardwareInstancing_Pass0(ps_HardwareInstancing_Pass0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float4 r4;
	float4 r3;
	r0.xy = i.texcoord.xy;
	r1.x = i.fog1.x;
	r4 = tex2D(Sampler, r0.xy);
	r0 = r4;
	r1.y = (r0.w < 0.2) ? 1 : 0;
	r1.y = (-r1.y >= 0) ? 1 : -1;
	r3 = (r1.y < 0) ? 1 : 0;
	r3 = -r3;
	clip(r3);
	o = r0 * r1.x;

	return o;
}

technique HardwareInstancing
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass0();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass0();
	}
}
