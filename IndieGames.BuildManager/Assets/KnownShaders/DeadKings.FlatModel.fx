// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 InstanceTransforms[60];
float4x4 View;
float4x4 Projection;
float4 LD;
float3 DiffuseLight;
float3 AmbientLight;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

int VertexCount;

struct vs_leaf1_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
};

struct vs_leaf1_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float fog : FOG;
	float4 position : POSITION;
};

vs_leaf1_Pass1_VS_OUT vs_leaf1_Pass1(vs_leaf1_Pass1_VS_IN i)
{
	vs_leaf1_Pass1_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float r12;
	float4 r6;
	float2 r5;
	float4 r7;
	int a0;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r0.z = r0.x + 0.5;
	r0.x = r0.z * VertexCount.x;
	r12 = VertexCount.x + VertexCount.x;
	r0.w = r12.x;
	r0.x = (r0.x >= -r0.x) ? 1 : 0;
	r12 = 1.0 / VertexCount.x;
	r0.y = r12.x;
	r1.w = r0.w * r0.x + -VertexCount.x;
	r12 = 1 / r1.w;
	r0.x = r12.x;
	r0.yz = r0.zz * r0.xy;
	r6 = frac(abs(r0.z));
	r6 = abs(r0.z) + -r6.x;
	r12 = (r0.z >= 0) ? r6.x : -r6.x;
	r0.w = r12.x;
	r2.yzw = r0.www * float3(4, 0.016666668, 3);
	r0.x = (r2.w >= -r2.w) ? 1 : 0;
	r12 = 6 * r0.x;
	r2.x = r12.x;
	r1.xz = r2.zx + float2(0.004, -3);
	r5.y = 0.004;
	r12 = 1 / r1.z;
	r0.x = r12.x;
	r1.y = r5.y;
	r0.w = r0.x * r0.w;
	r0.xy = frac(r0.yw);
	r0.zw = r1.zw * r0.yx;
	r3 = i.position;
	r4.xyz = i.normal.xyz;
	r0.xy = i.texcoord.xy;
	r6.xyz = r1.xyy;
	r6.w = 0;
	r7 = tex2Dlod(Sampler, r6);
	r1.xyz = r7.xyz;
	a0 = r2.y;
	r2.x = dot(r4.zxy, transpose(InstanceTransforms[a0 / 4])[0].zxy);
	r2.y = dot(r4.zxy, transpose(InstanceTransforms[a0 / 4])[1].zxy);
	r2.z = dot(r4.zxy, transpose(InstanceTransforms[a0 / 4])[2].zxy);
	r4.x = dot(r3.wzxy, transpose(InstanceTransforms[a0 / 4])[0].wzxy);
	r4.y = dot(r3.wzxy, transpose(InstanceTransforms[a0 / 4])[1].wzxy);
	r4.z = dot(r3.wzxy, transpose(InstanceTransforms[a0 / 4])[2].wzxy);
	r4.w = dot(r3.wzxy, transpose(InstanceTransforms[a0 / 4])[3].wzxy);
	r3.x = dot(r4.wzxy, transpose(View)[0].wzxy);
	r3.y = dot(r4.wzxy, transpose(View)[1].wzxy);
	r3.z = dot(r4.wzxy, transpose(View)[2].wzxy);
	r3.w = dot(r4.wzxy, transpose(View)[3].wzxy);
	o.position.x = dot(r3.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r3.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r3.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r3.wzxy, transpose(Projection)[3].wzxy);
	r1.w = dot(r2.zxy, r2.zxy);
	r12 = rsqrt(abs(r1.w));
	r1.w = r12.x;
	r2.xyz = r2.xyz * r1.www;
	r12 = (abs(r0.x) >= 0) ? 0 : 1;
	o.fog = r12.x;
	r1.w = dot(r2.zxy, -LD.zxy);
	r1.w = max(r1.w, 0.1);
	r2.xyz = r1.www * DiffuseLight.xyz + AmbientLight.xyz;
	r0.yzw = r0.xyz * 0.33333334;
	o.color.xyz = r2.xyz * r1.xyz;
	o.color.w = 1;
	r12 = r0.z + r0.w;
	r0.x = r12.x;
	o.texcoord = r0.yx;

	return o;
}

struct ps_leaf1_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float fog : FOG;
};

float4 ps_leaf1_Pass1(ps_leaf1_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float r2;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.color;
	r2 = i.fog.x;
	r5 = tex2D(Sampler, r0.xy);
	r0 = r5;
	o.xyz = r0.xyz * r1.xyz;
	o.w = r0.w;

	return o;
}

technique leaf1
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_leaf1_Pass1();
		PixelShader = compile ps_3_0 ps_leaf1_Pass1();
	}
}
