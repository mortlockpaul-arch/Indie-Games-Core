// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
float4x4 World1;
float3 LightDirection;
float4 Diff;
float4 Amb;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

struct vs_HardwareInstancing_Pass0_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog1 : FOG1;
	float4 blendweight4 : BLENDWEIGHT4;
};

struct vs_HardwareInstancing_Pass0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord3 : TEXCOORD3;
	float4 color : COLOR;
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
	float4 r8;
	float4 r9;
	float4 r10;
	float4 r11;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r10 = 0;
	r11 = 0;
	r3 = i.position;
	r5.xyw = i.normal.yzx;
	r1.xy = i.texcoord.xy;
	r2 = i.blendweight;
	r6 = i.blendweight1;
	r7 = i.blendweight2;
	r8 = i.blendweight3;
	r1.w = i.fog1.x;
	r4 = i.blendweight4;
	r0.x = dot(r2.wzxy, transpose(World1)[3].wzxy);
	r0.y = dot(r6.wzxy, transpose(World1)[3].wzxy);
	r0.z = dot(r7.wzxy, transpose(World1)[3].wzxy);
	r0.w = dot(r8.wzxy, transpose(World1)[3].wzxy);
	r10.x = dot(r2.wzxy, transpose(World1)[2].wzxy);
	r10.y = dot(r6.wzxy, transpose(World1)[2].wzxy);
	r10.z = dot(r7.wzxy, transpose(World1)[2].wzxy);
	r10.w = dot(r8.wzxy, transpose(World1)[2].wzxy);
	r11.x = dot(r2.wzxy, transpose(World1)[1].wzxy);
	r11.y = dot(r6.wzxy, transpose(World1)[1].wzxy);
	r11.z = dot(r7.wzxy, transpose(World1)[1].wzxy);
	r11.w = dot(r8.wzxy, transpose(World1)[1].wzxy);
	r9.x = dot(r2.wzxy, transpose(World1)[0].wzxy);
	r9.y = dot(r6.wzxy, transpose(World1)[0].wzxy);
	r9.z = dot(r7.wzxy, transpose(World1)[0].wzxy);
	r9.w = dot(r8.wzxy, transpose(World1)[0].wzxy);
	r9.x = dot(r9.wzxy, r3.wzxy);
	r9.y = dot(r11.wzxy, r3.wzxy);
	r9.z = dot(r10.wzxy, r3.wzxy);
	r9.w = dot(r0.wzxy, r3.wzxy);
	r0.x = dot(r9.wzxy, transpose(View)[0].wzxy);
	r0.y = dot(r9.wzxy, transpose(View)[1].wzxy);
	r0.z = dot(r9.wzxy, transpose(View)[2].wzxy);
	r0.w = dot(r9.wzxy, transpose(View)[3].wzxy);
	o.position.x = dot(r0.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r0.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r0.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r0.wzxy, transpose(Projection)[3].wzxy);
	r0.xyz = r3.www * r8.xyz;
	r8.xyz = r5.yyy * r7.xzy;
	r5.xyz = r5.xxx * r6.xzy + r8.xyz;
	r0.xyz = r3.zzz * r7.xzy + r0.xzy;
	r0.yzw = r3.yyy * r6.xyz + r0.xzy;
	r5.xyz = r5.www * r2.xyz + r5.xzy;
	r0.x = dot(r5.zxy, r5.zxy);
	r0.x = rsqrt(abs(r0.x));
	r5.yzw = r5.xyz * r0.xxx;
	r5.x = 1 / r4.x;
	r0.x = dot(r5.wyz, -LightDirection.zxy);
	r5.y = 1 / r4.z;
	o.texcoord = r5.xy * r1.xy + r4.yw;
	o.texcoord3 = r3.xxx * r2.xyz + r0.yzw;
	r0.x = max(r0.x, 0.1);
	r1.xyz = r0.xxx * Diff.xyz + Amb.xyz;
	o.color = r1;

	return o;
}

struct ps_HardwareInstancing_Pass0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord3 : TEXCOORD3;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass0(ps_HardwareInstancing_Pass0_PS_IN i) : COLOR
{
	float4 o;

	float4 r3;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r6;
	float4 r5;
	r3 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord3.xyz;
	r2 = i.color;
	r6 = tex2D(Sampler, r0.xy);
	r3 = r6;
	r0.z = (r1.z < Diff.w) ? 1 : 0;
	r0.w = (43.2 < r1.x) ? 1 : 0;
	r0.x = (Amb.w < r1.z) ? 1 : 0;
	r0.y = (r1.x < -43.2) ? 1 : 0;
	r1 = r3 * r2;
	r2.xy = r0.zw + r0.xy;
	r2.z = (r1.w < 0.02) ? 1 : 0;
	r2.xy = (-abs(r2.xy) >= 0) ? 1 : -1;
	r2.z = (-r2.z >= 0) ? 1 : -1;
	r5 = (r2.z < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	r5 = (r2.y < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	r5 = (r2.x < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	o = r1;

	return o;
}

struct ps_HardwareInstancing_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord3 : TEXCOORD3;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass1(ps_HardwareInstancing_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r3;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r6;
	float4 r5;
	r3 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord3.xyz;
	r2 = i.color;
	r6 = tex2D(Sampler, r0.xy);
	r0 = r6;
	r1.w = saturate(r1.z * 0.125 + -0.25);
	r3.x = (43.2 < r1.x) ? 1 : 0;
	r1.x = (r1.x < -43.2) ? 1 : 0;
	r1.x = r3.x + r1.x;
	r0 = r0 * r2;
	r2.x = (-abs(r1.x) >= 0) ? 1 : -1;
	r1 = r0 * r1.w;
	r2.y = (r1.w < 0.02) ? 1 : 0;
	r2.y = (-r2.y >= 0) ? 1 : -1;
	r5 = (r2.y < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	r5 = (r2.x < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	o = r1;

	return o;
}

struct ps_HardwareInstancing_Pass2_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord3 : TEXCOORD3;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass2(ps_HardwareInstancing_Pass2_PS_IN i) : COLOR
{
	float4 o;

	float4 r3;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r6;
	float4 r5;
	r3 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord3.xyz;
	r2 = i.color;
	r6 = tex2D(Sampler, r0.xy);
	r0 = r6;
	r1.w = saturate(-r1.z * 0.125 + -0.25);
	r3.x = (43.2 < r1.x) ? 1 : 0;
	r1.x = (r1.x < -43.2) ? 1 : 0;
	r1.x = r3.x + r1.x;
	r0 = r0 * r2;
	r2.x = (-abs(r1.x) >= 0) ? 1 : -1;
	r1 = r0 * r1.w;
	r2.y = (r1.w < 0.02) ? 1 : 0;
	r2.y = (-r2.y >= 0) ? 1 : -1;
	r5 = (r2.y < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	r5 = (r2.x < 0) ? 1 : 0;
	r5 = -r5;
	clip(r5);
	o = r1;

	return o;
}

technique HardwareInstancing
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass0();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass0();
	}
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass0();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass1();
	}
	pass Pass2
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass0();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass2();
	}
}
