// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
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
	float4 r10;
	float r16;
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
	r7 = i.position;
	r2.xyw = i.normal.yzx;
	r1.xy = i.texcoord.xy;
	r3 = i.blendweight;
	r5 = i.blendweight1;
	r8 = i.blendweight2;
	r6 = i.blendweight3;
	r4.xyz = r2.yyy * r8.xzy;
	r10 = frac(abs(r6.w));
	r10 = abs(r6.w) + -r10.x;
	r16 = (r6.w >= 0) ? r10.x : -r10.x;
	r0.x = r16.x;
	r0.x = r0.x * 10000;
	r16 = 0.004;
	r0.y = r16.x;
	r0.x = r6.w * 10000 + -r0.x;
	r0.x = r0.x * 0.01010101 + 0.004;
	r10.xyz = r0.xyy;
	r10.w = 0;
	r11 = tex2Dlod(Sampler, r10);
	r0.xyz = r11.xyz;
	r6 = r7.w * r6;
	r6 = r7.z * r8 + r6;
	r6 = r7.y * r5.xzwy + r6.xzwy;
	r7 = r7.x * r3.xwyz + r6.xzwy;
	r6.x = dot(r7.ywxz, transpose(View)[0].wzxy);
	r6.y = dot(r7.ywxz, transpose(View)[1].wzxy);
	r6.z = dot(r7.ywxz, transpose(View)[2].wzxy);
	r6.w = dot(r7.ywxz, transpose(View)[3].wzxy);
	o.position.x = dot(r6.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r6.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r6.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r6.wzxy, transpose(Projection)[3].wzxy);
	r2.xyz = r2.xxx * r5.xzy + r4.xyz;
	r2.xyz = r2.www * r3.xyz + r2.xzy;
	r0.w = dot(r2.zxy, r2.zxy);
	r16 = rsqrt(abs(r0.w));
	r0.w = r16.x;
	r2.xyz = r2.xyz * r0.www;
	r0.w = dot(r2.zxy, -LightDirection.zxy);
	o.texcoord = r1.xy;
	r0.w = max(r0.w, 0.1);
	r1.xyz = r0.www * DiffuseLight.xyz + AmbientLight.xyz;
	o.color.xyz = r1.xyz * r0.xyz;
	o.color.w = 1;

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
