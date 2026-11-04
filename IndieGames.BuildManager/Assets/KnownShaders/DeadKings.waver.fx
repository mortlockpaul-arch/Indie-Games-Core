float4x4 wvp : WorldViewProjection;
float4x4 world : World;
float2 pos1;
float2 pos2;
float sc;
float fade;
float3 LightDirection : Direction = float3(0, 1, 1);
float intense;
float amp;
float Time;
texture ColorMap;

sampler ColorMapSampler = sampler_state
{
	Texture = <ColorMap>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

texture hole;

sampler holeSampler = sampler_state
{
	Texture = <hole>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

texture rip;

sampler ripSampler = sampler_state
{
	Texture = <rip>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct vs_ColorMap1_Pass0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_ColorMap1_Pass0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 position : POSITION;
};

vs_ColorMap1_Pass0_VS_OUT vs_ColorMap1_Pass0(vs_ColorMap1_Pass0_VS_IN i)
{
	vs_ColorMap1_Pass0_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float r14;
	float2 r8;
	float4 r7;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r3 = i.position;
	r2.xy = i.texcoord.xy;
	r0.yz = Time.xx * float2(0.009947184, 0.01136821);
	r14 = 0.07957747 * r3.y;
	r0.x = r14.x;
	r0 = r0.xyzx + float4(0.5, 0.5, 0.5, 0.46021128);
	r0 = frac(r0);
	r0 = r0.yxzw * 6.2831855 + -3.1415927;
	r4.z = saturate(r2.x + -0.04);
	r8.y = sin(r0.y);
	r14 = r8.y;
	r1.x = r14.x;
	r7.y = r3.z + Time.x;
	r8.x = cos(r0.z);
	r14 = r8.x;
	r1.y = r14.x;
	r0.y = r7.y;
	r1.z = r1.y * 0.5;
	r8.x = cos(r0.x);
	r14 = r8.x;
	r1.y = r14.x;
	r7.xz = r1.zy + float2(1.7, 0.2);
	r8.y = sin(r0.w);
	r14 = r8.y;
	r1.y = r14.x;
	r0.xz = r7.xz;
	r1.xy = r1.xy + r1.xy;
	r14 = 1 / r0.x;
	r4.w = r14.x;
	r7.xy = r3.yx + float2(-0.5, 0.5);
	r14 = r4.w;
	r4.xy = r7.xy;
	r6.yzw = r4.yzz * r4.wxy;
	r14 = r3.x * r14.x;
	r6.x = r14.x;
	r0.xy = r0.yy + -r6.xy;
	r0.xyz = r1.xyx * r0.zzz + r0.xxy;
	r0.xyz = r0.xyz * 0.15915494 + 0.5;
	r0.xyz = frac(r0.xyz);
	r1.xzw = r0.yxz * 6.2831855 + -3.1415927;
	r8.y = sin(r1.w);
	r14 = r8.y;
	r5.y = r14.x;
	r0.xyz = r4.zzz * r3.xyw;
	r8.y = sin(r1.z);
	r14 = r8.y;
	r5.z = r14.x;
	r7.zw = r4.xy + r6.zw;
	r8.y = sin(r1.x);
	r14 = r8.y;
	r5.x = r14.x;
	r1.zw = r7.zw;
	r5.xyz = r5.zxy * amp.xxx;
	r0.w = r5.x * r4.z;
	r0 = r0.yxzw + r3.yxwz;
	o.position.x = dot(r0.zwyx, transpose(wvp)[0].wzxy);
	o.position.y = dot(r0.zwyx, transpose(wvp)[1].wzxy);
	o.position.z = dot(r0.zwyx, transpose(wvp)[2].wzxy);
	o.position.w = dot(r0.zwyx, transpose(wvp)[3].wzxy);
	r1.xy = r5.zy * r4.zz + r3.zz;
	r1.xy = -r0.ww + r1.xy;
	r7.zw = -r0.xy + r1.zw;
	r14 = r0.x + -r0.x;
	r1.z = r14.x;
	r0.zw = r7.zw;
	o.texcoord = r2.xy;
	r7.x = r0.z * r1.x;
	r14 = r0.y + -r0.y;
	r1.w = r14.x;
	r0.x = r7.x;
	r7.y = r1.y * r0.w;
	r14 = r0.z * r0.w;
	r0.z = r14.x;
	r0.y = r7.y;
	r0.w = r1.w * r1.z + -r0.z;
	r0.yz = -r1.yw * r1.zx + r0.xy;
	r0.x = dot(r0.wyz, r0.wyz);
	r14 = rsqrt(abs(r0.x));
	r0.x = r14.x;
	o.texcoord1 = r0.yzw * r0.xxx;

	return o;
}

struct ps_ColorMap1_Pass0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
};

float4 ps_ColorMap1_Pass0(ps_ColorMap1_Pass0_PS_IN i) : COLOR
{
	float4 o;

	float4 r2;
	float4 r0;
	float4 r1;
	float4 r5;
	float4 r4;
	float4 r3;
	r2 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord1.xyz;
	r5 = tex2D(ColorMapSampler, r0.xy);
	r0 = r5.xwyz;
	r2.x = dot(LightDirection.zxy, LightDirection.zxy);
	r1.w = Time.x * 0.039788734 + 0.5;
	r2.w = (r0.y < 0.1) ? 1 : 0;
	r1.w = frac(r1.w);
	r2.x = rsqrt(abs(r2.x));
	r1.w = r1.w * 6.2831855 + -3.1415927;
	r2.xyz = r2.xxx * LightDirection.xyz;
	r2.w = (-r2.w >= 0) ? 1 : -1;
	r4 = (r2.w < 0) ? 1 : 0;
	r4 = -r4;
	clip(r4);
	r1.x = dot(r2.zxy, r1.zxy);
	r3.x = r1.x * intense.x;
	r4.x = cos(r1.w);
	r1.y = r4.x;
	r1.x = r3.x;
	r1.y = r1.y * 0.083333336 + 0.8;
	r1.x = max(r1.y, r1.x);
	r1.x = min(r1.x, 2);
	r3.xzw = r1.xxx * r0.xzw;
	r0.y = r0.y;
	r0.xzw = r3.xzw;
	o.xyz = r0.xzw * fade.xxx;
	o.w = fade.x * r0.y;

	return o;
}

struct ps_BurnMap_Pass0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
};

float4 ps_BurnMap_Pass0(ps_BurnMap_Pass0_PS_IN i) : COLOR
{
	float4 o;

	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r0;
	float4 r1;
	float r14;
	float3 r7;
	float4 r8;
	float4 r9;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord1.xyz;
	r4.zw = r0.xy + float2(-0.4, 0.4);
	r14 = 1 / sc.x;
	r3.x = r14.x;
	r3.zw = r0.xy + pos2.xy;
	r14 = sc.x + sc.x;
	r0.w = r14.x;
	r0.z = Time.x * 0.039788734 + 0.5;
	r7.z = frac(r0.z);
	r14 = 1 / r0.w;
	r3.y = r14.x;
	r0.z = r7.z;
	r0.z = r0.z * 6.2831855 + -3.1415927;
	r2.yz = -r3.xy * 0.5;
	r8.x = cos(r0.z);
	r14 = r8.x;
	r0.w = r14.x;
	r4.xy = r0.xy + pos1.xy;
	r14 = 0.083333336 * r0.w;
	r2.x = r14.x;
	r2.xyw = r2.yzx + float3(0.5, 0.5, 0.6);
	r0.zw = r3.zw * r3.xx + r2.xx;
	r5 = r4 * r3.xxyy + r2.xxyy;
	r9 = tex2D(holeSampler, r5.zw);
	r3 = r9;
	r9 = tex2D(holeSampler, r0.zw);
	r4 = r9;
	r9 = tex2D(ColorMapSampler, r0.xy);
	r6 = r9;
	r9 = tex2D(holeSampler, r5.xy);
	r5 = r9;
	r0.x = dot(LightDirection.zxy, LightDirection.zxy);
	r5 = r6 * r5;
	r14 = rsqrt(abs(r0.x));
	r0.x = r14.x;
	r0.xyz = r0.xxx * LightDirection.xyz;
	r4 = r5 * r4;
	r2.xyz = r4.xyz * r3.xyz;
	r14 = r4.w;
	r0.y = dot(r0.zxy, r1.zxy);
	r14 = r3.w * r14.x;
	r1.w = r14.x;
	r0.x = (r1.w < 0.1) ? 1 : 0;
	r0.x = (-r0.x >= 0) ? 1 : -1;
	r8 = (r0.x < 0) ? 1 : 0;
	r8 = -r8;
	clip(r8);
	r14 = intense.x * r0.y;
	r0.x = r14.x;
	r0.x = max(r2.w, r0.x);
	r0.x = min(r0.x, 4);
	r1.xyz = r2.xyz * r0.xxx;
	o = r1 * fade.x;

	return o;
}

struct vs_ColorMap2_Pass0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_ColorMap2_Pass0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 position : POSITION;
};

vs_ColorMap2_Pass0_VS_OUT vs_ColorMap2_Pass0(vs_ColorMap2_Pass0_VS_IN i)
{
	vs_ColorMap2_Pass0_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float r13;
	float2 r7;
	float4 r6;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r1 = i.position.ywxz;
	r3.xy = i.texcoord.xy;
	r0.yz = Time.xx * float2(0.009947184, 0.01136821);
	r13 = 0.026525823 * r1.x;
	r0.x = r13.x;
	r0 = r0.xyzx + float4(0.5, 0.5, 0.5, 0.4867371);
	r0 = frac(r0);
	r0 = r0.yxzw * 6.2831855 + -3.1415927;
	r7.y = sin(r0.y);
	r13 = r7.y;
	r0.y = r13.x;
	r2.z = r1.w + Time.x;
	r7.x = cos(r0.z);
	r13 = r7.x;
	r2.x = r13.x;
	r2.x = r2.x * 0.5;
	r7.x = cos(r0.x);
	r13 = r7.x;
	r2.y = r13.x;
	r6.xz = r2.xy + float2(1.6, 0.2);
	r7.y = sin(r0.w);
	r13 = r7.y;
	r0.w = r13.x;
	r0.xz = r6.xz;
	r2.xy = r0.yw + r0.yw;
	r13 = 1 / r0.x;
	r0.y = r13.x;
	r5.xz = r1.xz + float2(-1, 0.5);
	r13 = r0.y;
	r0.w = r5.z * r0.y;
	r13 = r1.z * r13.x;
	r0.y = r13.x;
	r0.xy = r2.zz + -r0.yw;
	r0.xyz = r2.xxy * r0.zzz + r0.xyx;
	r0.xyz = r0.zxy * 0.15915494 + 0.5;
	r0.xyz = frac(r0.xyz);
	r0.xyz = r0.xzy * 6.2831855 + -3.1415927;
	r7.y = sin(r0.z);
	r13 = r7.y;
	r3.z = r13.x;
	r2.xyz = r1.zxy + r1.zxy;
	r7.y = sin(r0.y);
	r13 = r7.y;
	r3.w = r13.x;
	r4.xz = r3.wz * amp.xx;
	r2.w = r4.z + r1.w;
	o.position.x = dot(r2.zwxy, transpose(wvp)[0].wzxy);
	o.position.y = dot(r2.zwxy, transpose(wvp)[1].wzxy);
	o.position.z = dot(r2.zwxy, transpose(wvp)[2].wzxy);
	o.position.w = dot(r2.zwxy, transpose(wvp)[3].wzxy);
	r0.yz = -r2.yx + r2.yx;
	r6.w = r0.z * r0.y;
	r7.y = sin(r0.x);
	r13 = r7.y;
	r0.x = r13.x;
	r0.w = r6.w;
	r5.y = r0.x * amp.x;
	r13 = r1.z;
	r0.x = r13.x;
	r1.xy = r5.xy + r1.xw;
	r13 = 1 + r0.x;
	r4.y = r13.x;
	r1.zw = r4.xy + r1.wz;
	o.texcoord = r3.xy;
	r1 = -r2.wwyx + r1.yzxw;
	r0.w = -r1.z * r1.w + r0.w;
	r0.x = r1.x * r0.y;
	r0.y = r0.z * r1.y;
	r0.yz = r1.zx * r1.yw + -r0.xy;
	r0.x = dot(r0.wyz, r0.wyz);
	r13 = rsqrt(abs(r0.x));
	r0.x = r13.x;
	o.texcoord1 = r0.yzw * r0.xxx;

	return o;
}

struct ps_ColorMap2_Pass0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
};

float4 ps_ColorMap2_Pass0(ps_ColorMap2_Pass0_PS_IN i) : COLOR
{
	float4 o;

	float4 r2;
	float4 r0;
	float4 r1;
	float4 r5;
	float4 r4;
	float4 r3;
	r2 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord1.xyz;
	r5 = tex2D(ColorMapSampler, r0.xy);
	r0 = r5.xwyz;
	r2.x = dot(LightDirection.zxy, LightDirection.zxy);
	r1.w = Time.x * 0.039788734 + 0.5;
	r2.w = (r0.y < 0.1) ? 1 : 0;
	r1.w = frac(r1.w);
	r2.x = rsqrt(abs(r2.x));
	r1.w = r1.w * 6.2831855 + -3.1415927;
	r2.xyz = r2.xxx * LightDirection.xyz;
	r2.w = (-r2.w >= 0) ? 1 : -1;
	r4 = (r2.w < 0) ? 1 : 0;
	r4 = -r4;
	clip(r4);
	r1.x = dot(r2.zxy, r1.zxy);
	r3.x = r1.x * intense.x;
	r4.x = cos(r1.w);
	r1.y = r4.x;
	r1.x = r3.x;
	r1.y = r1.y * 0.083333336 + 0.6;
	r1.x = max(r1.y, r1.x);
	r1.x = min(r1.x, 4);
	r3.xzw = r1.xxx * r0.xzw;
	r0.y = r0.y;
	r0.xzw = r3.xzw;
	o.xyz = r0.xzw * fade.xxx;
	o.w = fade.x * r0.y;

	return o;
}

technique ColorMap1
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 vs_ColorMap1_Pass0();
		PixelShader = compile ps_3_0 ps_ColorMap1_Pass0();
	}
}

technique BurnMap
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 vs_ColorMap1_Pass0();
		PixelShader = compile ps_3_0 ps_BurnMap_Pass0();
	}
}

technique ColorMap2
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 vs_ColorMap2_Pass0();
		PixelShader = compile ps_3_0 ps_ColorMap2_Pass0();
	}
}
