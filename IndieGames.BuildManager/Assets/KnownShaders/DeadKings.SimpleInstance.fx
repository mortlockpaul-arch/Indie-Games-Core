// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
float4x4 World;
float mult = 1;
float3 LightDirection;
float3 DiffuseLight;
float3 AmbientLight;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

struct vs_HardwareInstancing_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog : FOG;
};

struct vs_HardwareInstancing_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_HardwareInstancing_Pass1_VS_OUT vs_HardwareInstancing_Pass1(vs_HardwareInstancing_Pass1_VS_IN i)
{
	vs_HardwareInstancing_Pass1_VS_OUT o;

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
	float4 r11;
	float4 r12;
	float2 r10;
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
	r7 = i.position;
	r2.xyw = i.normal.yzx;
	r3.zw = i.texcoord.xy;
	r4 = i.blendweight;
	r6 = i.blendweight1;
	r8 = i.blendweight2;
	r9 = i.blendweight3;
	r0.x = i.fog.x;
	r0.x = r0.x * 0.01010101 + 0.004;
	r5.xyz = r2.yyy * r8.xzy;
	r0.y = 0.004;
	r11.xyz = r0.xyy;
	r11.w = 0;
	r12 = tex2Dlod(Sampler, r11);
	r1.xyz = r12.xyz;
	r0 = r7.w * r9;
	r0 = r7.z * r8 + r0;
	r0 = r7.y * r6.xzwy + r0.xzwy;
	r0 = r7.x * r4.xwyz + r0.xzwy;
	r7.x = dot(r0.ywxz, transpose(View)[0].wzxy);
	r7.y = dot(r0.ywxz, transpose(View)[1].wzxy);
	r7.z = dot(r0.ywxz, transpose(View)[2].wzxy);
	r7.w = dot(r0.ywxz, transpose(View)[3].wzxy);
	r0.y = dot(r7.wzxy, transpose(Projection)[3].wzxy);
	r0.w = dot(r7.wzxy, transpose(Projection)[2].wzxy);
	r0.x = dot(r7.wzxy, transpose(Projection)[1].wzxy);
	r0.z = dot(r7.wzxy, transpose(Projection)[0].wzxy);
	o.position = r0.zxwy;
	r2.xyz = r2.xxx * r6.xzy + r5.xyz;
	r2.xyz = r2.www * r4.xzy + r2.xyz;
	r4.w = r0.w + -transpose(Projection)[2].w;
	r4.y = r0.z + -transpose(Projection)[0].w;
	r4.x = -transpose(Projection)[1].w + r0.x;
	r3.xy = mult.xx * float2(1000, 1900);
	r4.z = -transpose(Projection)[3].w + r0.y;
	o.texcoord = r3.zw;
	r0.z = dot(r4.zwyx, r4.zwyx);
	r0.y = 1 / r3.y;
	r10.x = dot(r2.yxz, r2.yxz);
	r11 = rsqrt(abs(r0.z));
	r0.z = 1 / r11.x;
	r0.x = r10.x;
	r2.w = -r3.x + r0.z;
	r0.x = rsqrt(abs(r0.x));
	r0 = r2.wxzy * r0.yxxx;
	r1.w = dot(r0.wyz, -LightDirection.zxy);
	r10.y = max(r1.w, 0.1);
	r0.x = r0.x;
	r0.y = r10.y;
	r0.yzw = r0.yyy * DiffuseLight.xyz + AmbientLight.xyz;
	r1.xyz = r0.yzw * r1.xyz;
	r0.w = saturate(1 + -r0.x);
	r0.xyz = r1.xyz * r0.www;
	o.color = r0;

	return o;
}

struct ps_HardwareInstancing_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass1(ps_HardwareInstancing_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float4 r4;
	r0.xy = i.texcoord.xy;
	r1 = i.color;
	r4 = tex2D(Sampler, r0.xy);
	r0 = r4;
	o = r0 * r1;

	return o;
}

technique HardwareInstancing
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass1();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass1();
	}
}
