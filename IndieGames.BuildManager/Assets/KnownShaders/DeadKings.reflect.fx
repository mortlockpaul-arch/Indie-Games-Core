// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
float4x4 World;
float4x4 ReflectionView;
float3 CameraPosition;
float slide;
float ref1 = 1;
float ref2 = 1000;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

texture ReflectionMap;

sampler ReflectionSampler = sampler_state
{
	Texture = <ReflectionMap>;
	MagFilter = Linear;
	MinFilter = Linear;
	MipFilter = Linear;
	AddressU = Mirror;
	AddressV = Mirror;
};

struct vs_Reflect_P0_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
};

struct vs_Reflect_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_Reflect_P0_VS_OUT vs_Reflect_P0(vs_Reflect_P0_VS_IN i)
{
	vs_Reflect_P0_VS_OUT o;

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
	r2.xyz = i.position.xyz;
	r2.w = 1;
	r0.yzw = i.normal.xyz;
	r3.xy = i.texcoord.xy;
	r1 = transpose(View)[3] * transpose(Projection)[0].w;
	r4 = transpose(View)[3] * transpose(Projection)[1].w;
	r5 = transpose(View)[3] * transpose(Projection)[2].w;
	r6 = transpose(View)[3] * transpose(Projection)[3].w;
	r6 = transpose(View)[2].xywz * transpose(Projection)[3].z + r6.xywz;
	r5 = transpose(View)[2].xywz * transpose(Projection)[2].z + r5.xywz;
	r4 = transpose(View)[2].xywz * transpose(Projection)[1].z + r4.xywz;
	r1 = transpose(View)[2].xywz * transpose(Projection)[0].z + r1.xywz;
	r1 = transpose(View)[1].xwyz * transpose(Projection)[0].y + r1.xzyw;
	r4 = transpose(View)[1].xwyz * transpose(Projection)[1].y + r4.xzyw;
	r5 = transpose(View)[1].xwyz * transpose(Projection)[2].y + r5.xzyw;
	r6 = transpose(View)[1].xwyz * transpose(Projection)[3].y + r6.xzyw;
	r10 = transpose(View)[0].xwyz * transpose(Projection)[3].x + r6;
	r9 = transpose(View)[0].xwyz * transpose(Projection)[2].x + r5;
	r8 = transpose(View)[0].xwyz * transpose(Projection)[1].x + r4;
	r7 = transpose(View)[0].xwyz * transpose(Projection)[0].x + r1;
	r6 = r7.y * transpose(World)[3].xzyw;
	r5 = r8.y * transpose(World)[3].xzyw;
	r4 = r9.y * transpose(World)[3].xzyw;
	r1 = r10.y * transpose(World)[3].xzyw;
	r1 = r10.w * transpose(World)[2] + r1.xzyw;
	r4 = r9.w * transpose(World)[2] + r4.xzyw;
	r5 = r8.w * transpose(World)[2] + r5.xzyw;
	r6 = r7.w * transpose(World)[2] + r6.xzyw;
	r6 = r7.z * transpose(World)[1] + r6;
	r5 = r8.z * transpose(World)[1] + r5;
	r4 = r9.z * transpose(World)[1] + r4;
	r1 = r10.z * transpose(World)[1] + r1;
	r1 = r10.x * transpose(World)[0] + r1;
	r4 = r9.x * transpose(World)[0] + r4;
	r5 = r8.x * transpose(World)[0] + r5;
	r6 = r7.x * transpose(World)[0] + r6;
	o.position.x = dot(r6.zxyw, r2.zxyw);
	o.position.y = dot(r5.zxyw, r2.zxyw);
	o.position.z = dot(r4.zxyw, r2.zxyw);
	o.position.w = dot(r1.zxyw, r2.zxyw);
	r1.y = dot(r0.wyz, transpose(World)[0].zxy);
	r1.z = dot(r0.wyz, transpose(World)[1].zxy);
	r1.w = dot(r0.wyz, transpose(World)[2].zxy);
	r0.x = dot(r2.zxy, transpose(World)[0].zxy);
	r0.y = dot(r2.zxy, transpose(World)[1].zxy);
	r0.z = dot(r2.zxy, transpose(World)[2].zxy);
	r0.yzw = -r0.xyz + CameraPosition.xyz;
	r1.x = dot(r1.wyz, r1.wyz);
	r0.x = dot(r0.wyz, r0.wyz);
	r1.x = rsqrt(abs(r1.x));
	r1.xyz = r1.yzw * r1.xxx;
	r0.x = rsqrt(abs(r0.x));
	r0.xyz = r0.yzw * r0.xxx;
	r2 = (float4(-0, -0, -0, -1) >= 0) ? -r0.zxyy : 1;
	r1.w = dot(-r0.zxy, r1.zxy);
	o.color.x = (abs(r0.x) >= 0) ? 1 : 0;
	o.color.yzw = 1;
	o.texcoord = r3.xy;
	r0.w = dot(r1.zxyw, r2);
	o.texcoord1 = -r1.xyz * r0.www + -r0.xyz;

	return o;
}

struct ps_Reflect_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_Reflect_P0(ps_Reflect_P0_PS_IN i) : COLOR
{
	float4 o;

	float2 r0;
	float3 r1;
	float4 r2;
	float4 r5;
	float r10;
	r0 = i.texcoord.xy;
	r1 = i.texcoord1.xyz;
	r2 = i.color;
	r5 = tex2D(Sampler, r0.xy);
	r2.z = r5.x;
	r2.xy = -r1.xy * 0.5;
	r10 = 1 / ref2.x;
	r2.w = r10.x;
	r2.z = r2.w * r2.z + r2.x;
	r10 = slide.x + r2.y;
	r2.w = r10.x;
	r5 = tex2D(ReflectionSampler, r2.zw);
	r2.xyz = r5.xyz;
	o.xyz = saturate(r2.xyz * ref1.xxx);
	o.w = 1;

	return o;
}

struct ps_ReflectAlpha_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_ReflectAlpha_P0(ps_ReflectAlpha_P0_PS_IN i) : COLOR
{
	float4 o;

	float2 r0;
	float3 r1;
	float4 r2;
	float r10;
	float4 r5;
	float2 r3;
	r0 = i.texcoord.xy;
	r1 = i.texcoord1.xyz;
	r2 = i.color;
	r2.xy = -r1.xy * float2(0.5, 0.1);
	r10 = slide.x + r2.y;
	r2.z = r10.x;
	r5 = tex2D(ReflectionSampler, r2.xz);
	r1 = r5.xyz;
	r5 = tex2D(Sampler, r0.xy);
	r2.x = r5.w;
	r10 = 1 + -r2.x;
	r2.y = r10.x;
	r10 = ref1.x * r2.y;
	r2.z = r10.x;
	r3 = r2.yy * r1.yz;
	r10 = r2.z;
	r2.xy = r3.xy;
	o.yz = saturate(r2.xy * ref1.xx);
	r10 = r1.x * r10.x;
	o.x = saturate(r10.x);
	o.w = 1;

	return o;
}

struct vs_none_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_none_P0_VS_OUT vs_none_P0(float4 texcoord : TEXCOORD)
{
	vs_none_P0_VS_OUT o;

	float4 r0;
	r0 = 0;
	o.position.x = (abs(r0.x) >= 0) ? 0 : 1;
	o.position.yzw = 0;
	r0.xy = texcoord.xy;
	o.color.x = (abs(r0.x) >= 0) ? 0 : 1;
	o.color.yzw = 0;
	o.texcoord = r0.xy;
	o.texcoord1 = (0 < -abs(r0.xxx)) ? 1 : 0;

	return o;
}

struct ps_none_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_none_P0(ps_none_P0_PS_IN i) : COLOR
{
	float4 o;

	float2 r0;
	float3 r1;
	float4 r2;
	r0 = i.texcoord.xy;
	r1 = i.texcoord1.xyz;
	r2 = i.color;
	o.x = (abs(r2.x) >= 0) ? 0 : 1;
	o.yzw = 0;

	return o;
}

struct vs_Reflect2_P0_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
};

struct vs_Reflect2_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_Reflect2_P0_VS_OUT vs_Reflect2_P0(vs_Reflect2_P0_VS_IN i)
{
	vs_Reflect2_P0_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r1.xyz = i.position.xyz;
	r1.w = 1;
	r3.xyz = i.normal.xyz;
	r0.yz = i.texcoord.xy;
	r2.x = dot(transpose(World)[0].zxyw, r1.zxyw);
	r2.y = dot(transpose(World)[1].zxyw, r1.zxyw);
	r2.z = dot(transpose(World)[2].zxyw, r1.zxyw);
	r2.w = dot(transpose(World)[3].zxyw, r1.zxyw);
	r1.x = dot(r2.wzxy, transpose(View)[0].wzxy);
	r1.y = dot(r2.wzxy, transpose(View)[1].wzxy);
	r1.z = dot(r2.wzxy, transpose(View)[2].wzxy);
	r1.w = dot(r2.wzxy, transpose(View)[3].wzxy);
	o.position.x = dot(r1.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r1.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r1.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r1.wzxy, transpose(Projection)[3].wzxy);
	r1.x = dot(r3.zxy, transpose(ReflectionView)[0].zxy);
	r1.y = dot(r3.zxy, transpose(ReflectionView)[1].zxy);
	r1.z = dot(r3.zxy, transpose(ReflectionView)[2].zxy);
	r1.w = dot(r3.zxy, transpose(ReflectionView)[3].zxy);
	r2.xyz = -r2.xyz + CameraPosition.xyz;
	r0.w = dot(r1.wzxy, r1.wzxy);
	r4.x = dot(r2.zxy, r2.zxy);
	r0.w = rsqrt(abs(r0.w));
	r0.x = r4.x;
	r1.xyz = r1.xyz * r0.www;
	o.color.x = (abs(r0.x) >= 0) ? 1 : 0;
	o.color.yzw = 1;
	o.texcoord = r0.yz;
	r4.w = dot(r1.zxy, r1.zxy);
	r0.x = rsqrt(abs(r0.x));
	r0.w = r4.w;
	r4.xyz = r2.xyz * r0.xxx;
	r0.w = rsqrt(abs(r0.w));
	r0.xyz = r4.xyz;
	r2 = (float4(-0, -0, -0, -1) >= 0) ? -r0.zxyy : 1;
	r1.xyz = r1.xyz * r0.www;
	r1.w = dot(-r0.zxy, r1.zxy);
	r0.w = dot(r1.zxyw, r2);
	o.texcoord1 = -r1.xyz * r0.www + -r0.xyz;

	return o;
}

struct ps_Reflect2_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_Reflect2_P0(ps_Reflect2_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float3 r1;
	float4 r2;
	float4 r5;
	float r10;
	float4 r3;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xyz;
	r2 = i.color;
	r5 = tex2D(Sampler, r0.xy);
	r0.xy = r5.xz;
	r2.xy = -r1.xy * 0.5;
	r10 = 1 / ref2.x;
	r2.z = r10.x;
	r0.z = r2.z * r0.x + r2.x;
	r3.w = r0.y * ref1.x;
	r10 = slide.x + r2.y;
	r0.w = r10.x;
	r2.w = r3.w;
	r5 = tex2D(ReflectionSampler, r0.zw);
	r2.xyz = r5.xyz;
	r0.xy = r2.yz * r0.yy;
	r10 = r2.w;
	o.yz = saturate(r0.xy * ref1.xx);
	r10 = r2.x * r10.x;
	o.x = saturate(r10.x);
	o.w = 1;

	return o;
}

technique Reflect
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_Reflect_P0();
		PixelShader = compile ps_3_0 ps_Reflect_P0();
	}
}

technique ReflectAlpha
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_Reflect_P0();
		PixelShader = compile ps_3_0 ps_ReflectAlpha_P0();
	}
}

technique none
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_none_P0();
		PixelShader = compile ps_3_0 ps_none_P0();
	}
}

technique Reflect2
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_Reflect2_P0();
		PixelShader = compile ps_3_0 ps_Reflect2_P0();
	}
}
