float Time;
float Ripple;
float fs = 250;
float fe = 1300;
float ds = -250;
float de = 600;
float bs = -800;
float be = 900;
float4x4 ReflectionView;
float4x4 world;
float4x4 view;
float4x4 projection;
float3 campos;
float3 ghostpos;
float4 lightColor = float4(1, 1, 1, 1);
float off;
texture shadowTexture;

sampler shadow = sampler_state
{
	Texture = <shadowTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture rgbTexture;

sampler roomcolor = sampler_state
{
	Texture = <rgbTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

struct vs_insideDark_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_insideDark_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_insideDark_P0_VS_OUT vs_insideDark_P0(vs_insideDark_P0_VS_IN i)
{
	vs_insideDark_P0_VS_OUT o;

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
	r2.xyz = i.position.xyz;
	r2.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r3 = r8.z * transpose(world)[1] + r3;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r3 = r8.x * transpose(world)[0] + r3;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r2.zxyw);
	o.position.y = dot(r4.zxyw, r2.zxyw);
	o.position.z = dot(r3.zxyw, r2.zxyw);
	o.position.w = dot(r0.zxyw, r2.zxyw);
	r0.x = dot(transpose(world)[0].zxyw, r2.zxyw);
	r0.z = dot(transpose(world)[1].zxyw, r2.zxyw);
	r0.w = dot(transpose(world)[2].zxyw, r2.zxyw);
	r3.xyz = r0.xzw + -campos.xyz;
	r2.xyz = r0.xzw + -ghostpos.xyz;
	r2.x = dot(r2.zxy, r2.zxy);
	r2.z = 1 / be.x;
	r0.y = dot(r3.zxy, r3.zxy);
	r11 = rsqrt(abs(r2.x));
	r2.x = 1 / r11.x;
	r2.x = r2.x + bs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r2.y = r0.y + fs.x;
	r2.w = 1 / fe.x;
	r0.y = saturate(r2.x * r2.z);
	o.fog1 = 1 + -r0.y;
	o.fog = saturate(r2.y * r2.w);
	o.texcoord = r1.zw;
	o.texcoord1 = r1.xy;
	o.texcoord3 = r0.xzw + -campos.xyz;

	return o;
}

struct ps_insideDark_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_insideDark_P0(ps_insideDark_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r5;
	float4 r6;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float r4;
	float4 r9;
	float r14;
	float4 r8;
	r5 = 0;
	r6 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4 = i.fog1.x;
	r9 = tex2D(shadow, r0.xy);
	r0 = r9.yzwx;
	r9 = tex2D(roomcolor, r1.xy);
	r5 = r9;
	r3.y = dot(r2.zxy, r2.zxy);
	r0.xyz = r0.zxy * r5.wyz;
	r1 = r5.xyzx * float4(1.2, 1.1, 0.8, 1);
	r14 = 1 + -r0.x;
	r3.w = r14.x;
	r2 = r1.wxyz * lightColor.xxyz;
	r8 = rsqrt(abs(r3.y));
	r14 = 1 / r8.x;
	r3.y = r14.x;
	r3.yz = saturate(r3.yy * float2(0.007246377, 0.0016528926) + float2(-0.9782609, -0.9917355));
	r6.x = r2.x * r0.w;
	r14 = r3.z + -r3.y;
	r0.w = r14.x;
	r1.w = r3.w * r3.x + r0.x;
	r6.yzw = r0.yzw * lightColor.yzw;
	r3.y = r3.y + r6.w;
	r14 = -r1.w;
	r1.xyz = -r6.xzy * r3.xxx + r6.xzy;
	r3.y = -r3.y + 1;
	r14 = r5.w + r14.x;
	r0.w = r14.x;
	r0.xyz = -r1.xzy + r2.yzw;
	r14 = off.x * r3.y;
	r3.x = r14.x;
	r0 = r3.x * r0 + r1.xzyw;
	r3.w = -r0.w + 1;
	r3.xyz = -r0.xzy * r4.xxx;
	r3.w = r3.w * r4.x;
	o = r0 + r3.xzyw;

	return o;
}

struct vs_darkBlue_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_darkBlue_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_darkBlue_P0_VS_OUT vs_darkBlue_P0(vs_darkBlue_P0_VS_IN i)
{
	vs_darkBlue_P0_VS_OUT o;

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
	float2 r11;
	float4 r12;
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
	r3.xyz = i.position.xyz;
	r3.w = 1;
	r2.xy = i.texcoord.xy;
	r0.xz = i.texcoord1.xy;
	r1 = transpose(view)[3] * transpose(projection)[3].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[1].w;
	r6 = transpose(view)[3] * transpose(projection)[0].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[0].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[1].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[3].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[3].y + r1.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[1].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[0].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[0].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[3].x + r1;
	r1 = r7.y * transpose(world)[3].xzyw;
	r6 = r8.y * transpose(world)[3].xzyw;
	r5 = r9.y * transpose(world)[3].xzyw;
	r4 = r10.y * transpose(world)[3].xzyw;
	r4 = r10.w * transpose(world)[2] + r4.xzyw;
	r5 = r9.w * transpose(world)[2] + r5.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r1 = r7.w * transpose(world)[2] + r1.xzyw;
	r1 = r7.z * transpose(world)[1] + r1;
	r6 = r8.z * transpose(world)[1] + r6;
	r5 = r9.z * transpose(world)[1] + r5;
	r4 = r10.z * transpose(world)[1] + r4;
	r4 = r10.x * transpose(world)[0] + r4;
	r5 = r9.x * transpose(world)[0] + r5;
	r6 = r8.x * transpose(world)[0] + r6;
	r1 = r7.x * transpose(world)[0] + r1;
	r1.w = dot(r1.zxyw, r3.zxyw);
	r1.z = dot(r6.zxyw, r3.zxyw);
	r1.y = dot(r5.zxyw, r3.zxyw);
	r1.x = dot(r4.zxyw, r3.zxyw);
	o.position = r1;
	r4.x = dot(transpose(world)[0].zxyw, r3.zxyw);
	r4.y = dot(transpose(world)[1].zxyw, r3.zxyw);
	r4.z = dot(transpose(world)[2].zxyw, r3.zxyw);
	r3.yzw = r4.xyz + -campos.xyz;
	r4.xyz = r4.xyz + -ghostpos.xyz;
	r0.w = dot(r4.zxy, r4.zxy);
	r3.x = 1 / be.x;
	r11.y = dot(r3.wyz, r3.wyz);
	r12 = rsqrt(abs(r0.w));
	r0.w = 1 / r12.x;
	r0.y = r11.y;
	r2.z = r0.w + bs.x;
	r12 = rsqrt(abs(r0.y));
	r0.y = 1 / r12.x;
	r2.w = r0.y + ds.x;
	r3.y = 1 / de.x;
	r0.y = saturate(r2.z * r3.x);
	o.fog1 = 1 + -r0.y;
	o.fog = saturate(r2.w * r3.y);
	o.texcoord = r2.xy;
	o.texcoord1 = r0.xz;
	o.texcoord3 = r1.xyz;

	return o;
}

struct ps_darkBlue_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_darkBlue_P0(ps_darkBlue_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float3 r2;
	float4 r3;
	float4 r4;
	float4 r7;
	float r12;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r7 = tex2D(roomcolor, r1.xy);
	r0 = r7;
	r3.z = dot(r0.zxy, 0.33333334);
	r12 = 0.2 * r0.w;
	r3.y = r12.x;
	r5.zw = r3.zz * float2(0.2, 0.26);
	r12 = 1 + -r3.y;
	r4.y = r12.x;
	r3.zw = r5.zw;
	r5.zw = -r3.zw * r3.xx;
	r12 = r4.y;
	r4.zw = r5.zw;
	r0.x = r3.w + r4.w;
	r12 = r3.x * r12.x;
	r4.y = r12.x;
	o.z = -r0.x * r4.x + r0.x;
	r3.xz = r3.yz + r4.yz;
	r5.w = -r3.z * r4.x;
	r12 = 1 + -r3.x;
	r3.y = r12.x;
	r3.w = r5.w;
	r3.y = r3.y * r4.x;
	o.xyw = r3.zzx + r3.wwy;

	return o;
}

struct vs_outdoors_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_outdoors_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_outdoors_P0_VS_OUT vs_outdoors_P0(vs_outdoors_P0_VS_IN i)
{
	vs_outdoors_P0_VS_OUT o;

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
	r2.xyz = i.position.xyz;
	r2.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r3 = r8.z * transpose(world)[1] + r3;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r3 = r8.x * transpose(world)[0] + r3;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r2.zxyw);
	o.position.y = dot(r4.zxyw, r2.zxyw);
	o.position.z = dot(r3.zxyw, r2.zxyw);
	o.position.w = dot(r0.zxyw, r2.zxyw);
	r0.y = dot(transpose(world)[0].zxyw, r2.zxyw);
	r0.z = dot(transpose(world)[1].zxyw, r2.zxyw);
	r0.w = dot(transpose(world)[2].zxyw, r2.zxyw);
	r2.xyz = r0.yzw + -campos.xyz;
	r0.x = dot(r2.zxy, r2.zxy);
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord = r1.zw;
	o.texcoord1 = r1.xy;
	o.texcoord3 = r0.yzw + -campos.xyz;
	o.fog = saturate(r0.x * 0.000105263156 + -0.57894737);

	return o;
}

struct ps_outdoors_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_outdoors_P0(ps_outdoors_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r5;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r8;
	float r13;
	float4 r7;
	r5 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r8 = tex2D(shadow, r0.xy);
	r4 = r8.yzwx;
	r8 = tex2D(roomcolor, r1.xy);
	r5 = r8;
	r3.y = dot(r2.zxy, r2.zxy);
	r4.xyz = r4.zxy * r5.wyz;
	r0 = r5.xyzx * float4(1.5, 1.5, 1.4, 1);
	r13 = 1 + -r4.x;
	r3.w = r13.x;
	r1 = r0.wxyz * lightColor.xxyz;
	r7 = rsqrt(abs(r3.y));
	r13 = 1 / r7.x;
	r3.y = r13.x;
	r3.yz = saturate(r3.yy * float2(0.00625, 0.0016528926) + float2(-0.9375, -0.9917355));
	r2.x = r1.x * r4.w;
	r13 = r3.z + -r3.y;
	r4.w = r13.x;
	r0.w = r3.w * r3.x + r4.x;
	r2.yzw = r4.yzw * lightColor.yzw;
	r3.y = r3.y + r2.w;
	r13 = -r0.w;
	r0.xyz = -r2.xzy * r3.xxx + r2.xzy;
	r3.y = -r3.y + 1;
	r13 = r5.w + r13.x;
	r4.w = r13.x;
	r4.xyz = -r0.xzy + r1.yzw;
	r13 = off.x * r3.y;
	r3.x = r13.x;
	o = r3.x * r4 + r0.xzyw;

	return o;
}

struct vs_flint_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_flint_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_flint_P0_VS_OUT vs_flint_P0(vs_flint_P0_VS_IN i)
{
	vs_flint_P0_VS_OUT o;

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
	float4 r12;
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
	r4.xyz = i.position.xyz;
	r4.w = 1;
	r2.xy = i.texcoord.xy;
	r0.yz = i.texcoord1.xy;
	r1 = transpose(view)[3] * transpose(projection)[3].w;
	r3 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[1].w;
	r6 = transpose(view)[3] * transpose(projection)[0].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[0].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[1].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r3.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[3].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[3].y + r1.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[1].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[0].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[0].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[3].x + r1;
	r1 = r7.y * transpose(world)[3].xzyw;
	r6 = r8.y * transpose(world)[3].xzyw;
	r5 = r9.y * transpose(world)[3].xzyw;
	r3 = r10.y * transpose(world)[3].xzyw;
	r3 = r10.w * transpose(world)[2] + r3.xzyw;
	r5 = r9.w * transpose(world)[2] + r5.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r1 = r7.w * transpose(world)[2] + r1.xzyw;
	r1 = r7.z * transpose(world)[1] + r1;
	r6 = r8.z * transpose(world)[1] + r6;
	r5 = r9.z * transpose(world)[1] + r5;
	r3 = r10.z * transpose(world)[1] + r3;
	r3 = r10.x * transpose(world)[0] + r3;
	r5 = r9.x * transpose(world)[0] + r5;
	r6 = r8.x * transpose(world)[0] + r6;
	r1 = r7.x * transpose(world)[0] + r1;
	r1.w = dot(r1.zxyw, r4.zxyw);
	r1.z = dot(r6.zxyw, r4.zxyw);
	r1.y = dot(r5.zxyw, r4.zxyw);
	r1.x = dot(r3.zxyw, r4.zxyw);
	o.position = r1;
	r3.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r3.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r3.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r3.xyz = r3.xyz + -campos.xyz;
	r0.x = dot(r3.zxy, r3.zxy);
	r12 = rsqrt(abs(r0.x));
	r0.x = 1 / r12.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord = r2.xy;
	o.texcoord1 = r0.yz;
	o.texcoord3 = r1.xyz;
	o.fog = saturate(r0.x * 0.0009090909 + -0.27272728);

	return o;
}

struct ps_flint_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_flint_P0(ps_flint_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float3 r2;
	float4 r3;
	float4 r4;
	float4 r7;
	float r12;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r7 = tex2D(shadow, r0.xy);
	r4 = r7;
	r7 = tex2D(roomcolor, r1.xy);
	r0 = r7;
	r3.zw = r0.xy * float2(1.8, 1.6);
	r4 = r4.xzyw * r0.xzyw;
	r12 = r0.w;
	r3.y = r12.x;
	r0.xy = -r3.zw * r3.xx + r3.zw;
	r4.xyz = -r4.xzy * r3.xxx + r4.xzy;
	r5.w = -r4.w + 1;
	r12 = 1 + -r3.y;
	r3.z = r12.x;
	r3.w = r5.w;
	r3.zw = r3.zw * r3.xx;
	r12 = -r0.z;
	r4.w = r4.w + r3.w;
	r12 = r3.x * r12.x;
	r3.y = r12.x;
	r0.zw = r3.yz + r0.zw;
	r3 = r0 + -r4;
	o = r3 * 0.8 + r4;

	return o;
}

struct vs_pit_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_pit_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_pit_P0_VS_OUT vs_pit_P0(vs_pit_P0_VS_IN i)
{
	vs_pit_P0_VS_OUT o;

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
	float4 r12;
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
	r4.xyz = i.position.xyz;
	r4.w = 1;
	r2.xy = i.texcoord.xy;
	r0.yz = i.texcoord1.xy;
	r1 = transpose(view)[3] * transpose(projection)[3].w;
	r3 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[1].w;
	r6 = transpose(view)[3] * transpose(projection)[0].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[0].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[1].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r3.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[3].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[3].y + r1.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[1].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[0].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[0].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[3].x + r1;
	r1 = r7.y * transpose(world)[3].xzyw;
	r6 = r8.y * transpose(world)[3].xzyw;
	r5 = r9.y * transpose(world)[3].xzyw;
	r3 = r10.y * transpose(world)[3].xzyw;
	r3 = r10.w * transpose(world)[2] + r3.xzyw;
	r5 = r9.w * transpose(world)[2] + r5.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r1 = r7.w * transpose(world)[2] + r1.xzyw;
	r1 = r7.z * transpose(world)[1] + r1;
	r6 = r8.z * transpose(world)[1] + r6;
	r5 = r9.z * transpose(world)[1] + r5;
	r3 = r10.z * transpose(world)[1] + r3;
	r3 = r10.x * transpose(world)[0] + r3;
	r5 = r9.x * transpose(world)[0] + r5;
	r6 = r8.x * transpose(world)[0] + r6;
	r1 = r7.x * transpose(world)[0] + r1;
	r1.w = dot(r1.zxyw, r4.zxyw);
	r1.z = dot(r6.zxyw, r4.zxyw);
	r1.y = dot(r5.zxyw, r4.zxyw);
	r1.x = dot(r3.zxyw, r4.zxyw);
	o.position = r1;
	r3.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r3.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r3.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r3.xyz = r3.xyz + -campos.xyz;
	r0.x = dot(r3.zxy, r3.zxy);
	r12 = rsqrt(abs(r0.x));
	r0.x = 1 / r12.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord = r2.xy;
	o.texcoord1 = r0.yz;
	o.texcoord3 = r1.xyz;
	o.fog = saturate(r0.x * 0.00028571428 + -0.42857143);

	return o;
}

struct ps_pit_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_pit_P0(ps_pit_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float3 r2;
	float2 r3;
	float4 r4;
	float4 r7;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r7 = tex2D(shadow, r0.xy);
	r0 = r7;
	r7 = tex2D(roomcolor, r1.xy);
	r4 = r7;
	r0 = r0 * r4;
	r4.xyz = -r0.xyz * r3.xxx;
	r3.y = 1 + -r0.w;
	r4.w = r3.y * r3.x;
	o = r0 + r4;

	return o;
}

struct vs_water_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_water_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_water_P0_VS_OUT vs_water_P0(vs_water_P0_VS_IN i)
{
	vs_water_P0_VS_OUT o;

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
	float r19;
	float4 r13;
	float4 r12;
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
	r7.xyz = i.position.xyz;
	r2.xy = i.texcoord.xy;
	r0.yz = i.texcoord1.xy;
	r1 = transpose(view)[3] * transpose(projection)[0].w;
	r4 = transpose(view)[3] * transpose(projection)[1].w;
	r19 = Time.x;
	r5 = transpose(view)[3] * transpose(projection)[2].w;
	r19 = 0.015915494 * r19.x;
	r3.y = r19.x;
	r6 = transpose(view)[3] * transpose(projection)[3].w;
	r19 = Time.x;
	r3.xw = r7.zx * float2(0.15915494, 5);
	r19 = 0.5 * r19.x;
	r3.z = r19.x;
	r6 = transpose(view)[2].xywz * transpose(projection)[3].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[2].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r4.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[0].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[0].y + r1.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[2].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[3].y + r6.xzyw;
	r11 = transpose(view)[0].xwyz * transpose(projection)[3].x + r6;
	r10 = transpose(view)[0].xwyz * transpose(projection)[2].x + r5;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r8 = transpose(view)[0].xwyz * transpose(projection)[0].x + r1;
	r0.xw = r3.xy + 0.5;
	r6 = r8.y * transpose(world)[3].xzyw;
	r5 = r9.y * transpose(world)[3].xzyw;
	r4 = r10.y * transpose(world)[3].xzyw;
	r19 = frac(r0.x);
	r0.x = r19.x;
	r1 = r11.y * transpose(world)[3].xzyw;
	r19 = frac(r0.w);
	r0.w = r19.x;
	r1 = r11.w * transpose(world)[2] + r1.xzyw;
	r4 = r10.w * transpose(world)[2] + r4.xzyw;
	r5 = r9.w * transpose(world)[2] + r5.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r0.xw = r0.wx * 6.2831855 + -3.1415927;
	r13.y = sin(r0.w);
	r19 = r13.y;
	r2.z = r19.x;
	r6 = r8.z * transpose(world)[1] + r6;
	r5 = r9.z * transpose(world)[1] + r5;
	r4 = r10.z * transpose(world)[1] + r4;
	r1 = r11.z * transpose(world)[1] + r1;
	r1 = r11.x * transpose(world)[0] + r1;
	r4 = r10.x * transpose(world)[0] + r4;
	r5 = r9.x * transpose(world)[0] + r5;
	r6 = r8.x * transpose(world)[0] + r6;
	r12.w = r2.z + r2.z;
	r13.x = cos(r0.x);
	r19 = r13.x;
	r0.x = r19.x;
	r0.w = r12.w;
	r0.x = r0.w * r0.x + -r3.w;
	r0.x = r0.x + r3.z;
	r0.x = r0.x * 0.15915494 + 0.5;
	r19 = frac(r0.x);
	r0.x = r19.x;
	r0.x = r0.x * 6.2831855 + -3.1415927;
	r3.w = (abs(r0.x) >= 0) ? 1 : 0;
	r13.y = sin(r0.x);
	r19 = r13.y;
	r0.x = r19.x;
	r19 = 8 * r0.x;
	r7.w = r19.x;
	r3.xyz = r7.xwz + r7.xyz;
	o.position.x = dot(r6.zxyw, r3.zxyw);
	o.position.y = dot(r5.zxyw, r3.zxyw);
	o.position.z = dot(r4.zxyw, r3.zxyw);
	o.position.w = dot(r1.zxyw, r3.zxyw);
	r1.x = dot(transpose(world)[0].zxyw, r3.zxyw);
	r1.y = dot(transpose(world)[1].zxyw, r3.zxyw);
	r1.z = dot(transpose(world)[2].zxyw, r3.zxyw);
	o.texcoord = r2.xy;
	o.texcoord1 = r0.yz;
	o.texcoord3 = r1.xyz + -campos.xyz;
	r0.yzw = r1.xyz + -campos.xyz;
	r1.xyz = r1.xyz + -ghostpos.xyz;
	r0.x = dot(r1.zxy, r1.zxy);
	r12.y = dot(r0.wyz, r0.wyz);
	r13 = rsqrt(abs(r0.x));
	r19 = 1 / r13.x;
	r0.x = r19.x;
	r0.y = r12.y;
	r13 = rsqrt(abs(r0.y));
	r19 = 1 / r13.x;
	r0.y = r19.x;
	o.fog = saturate(r0.y * 0.0011111111 + -0.5);
	r0.y = r0.x + bs.x;
	r19 = 1 / be.x;
	r0.x = r19.x;
	r0.x = saturate(r0.y * r0.x);
	r19 = 1 + -r0.x;
	o.fog1 = r19.x;

	return o;
}

struct ps_water_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_water_P0(ps_water_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r7;
	float4 r6;
	float r8;
	float3 r5;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r1.z = Time.x * 0.0007142857 + r1.y;
	r7 = tex2D(roomcolor, r1.xz);
	r0 = r7.yxzw;
	r3.z = dot(r2.zxy, r2.zxy);
	r3.y = r0.w;
	r2.zw = r0.zx * lightColor.zy;
	r3.y = 1 + -r3.y;
	r1.y = r3.y * r3.x + r0.w;
	r4.zw = -r2.wz * r3.xx;
	r6 = rsqrt(abs(r3.z));
	r3.y = 1 / r6.x;
	r2.xy = saturate(r3.yy * float2(0.010869565, 0.0016528926) + float2(-0.9782609, -0.9917355));
	r3.x = r2.y + -r2.x;
	r4.y = lightColor.w * r3.x;
	r1.xzw = r2.xwz + r4.yzw;
	r4.zw = -r1.zw * r4.xx;
	r3.xz = -r1.yx + 1;
	r4.y = r3.x * r4.x;
	r4.x = off.x * r3.z;
	r3.xzw = r1.ywz + r4.ywz;
	r8 = r0.y;
	r4.xyz = r4.xxx * -r3.wzx + r3.wzx;
	if (r8.x > 0) {
		r0.z = -Ripple.x + r3.y;
		r3.zw = Ripple.xx + float2(-90, 90);
		r3.x = 1 / Ripple.x;
		r0.x = -r3.z + r3.y;
		r3.z = 1 / r3.w;
		r3.xy = saturate(r0.zx * r3.zx);
		r3.z = 3 * r0.y;
		r5.z = r3.z * r3.y;
		r3.x = 1 + -r3.x;
		r3.z = r5.z;
		r4.xy = r3.zz * r3.xx + r4.xy;
	}
	o.yzw = r4.xyz;
	o.x = 0;

	return o;
}

struct vs_victory2_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_victory2_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_victory2_P0_VS_OUT vs_victory2_P0(vs_victory2_P0_VS_IN i)
{
	vs_victory2_P0_VS_OUT o;

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
	r2.xyz = i.position.xyz;
	r2.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r3 = r8.z * transpose(world)[1] + r3;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r3 = r8.x * transpose(world)[0] + r3;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r2.zxyw);
	o.position.y = dot(r4.zxyw, r2.zxyw);
	o.position.z = dot(r3.zxyw, r2.zxyw);
	o.position.w = dot(r0.zxyw, r2.zxyw);
	r0.y = dot(transpose(world)[0].zxyw, r2.zxyw);
	r0.z = dot(transpose(world)[1].zxyw, r2.zxyw);
	r0.w = dot(transpose(world)[2].zxyw, r2.zxyw);
	r2.xyz = r0.yzw + -campos.xyz;
	r0.x = dot(r2.zxy, r2.zxy);
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord = r1.zw;
	o.texcoord1 = r1.xy;
	o.texcoord3 = r0.yzw + -campos.xyz;
	o.fog = saturate(r0.x * 0.00055555557 + 0.22222222);

	return o;
}

struct ps_victory2_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_victory2_P0(ps_victory2_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r5;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r8;
	float r13;
	float4 r7;
	r5 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord3.xyz;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r8 = tex2D(shadow, r0.xy);
	r4 = r8.yzwx;
	r8 = tex2D(roomcolor, r1.xy);
	r5 = r8;
	r3.y = dot(r2.zxy, r2.zxy);
	r4.xyz = r4.zxy * r5.wyz;
	r0 = r5.xyzx * float4(1.2, 1.1, 0.8, 1);
	r13 = 1 + -r4.x;
	r3.w = r13.x;
	r1 = r0.wxyz * lightColor.xxyz;
	r7 = rsqrt(abs(r3.y));
	r13 = 1 / r7.x;
	r3.y = r13.x;
	r3.yz = saturate(r3.yy * float2(0.007246377, 0.0016528926) + float2(-0.9782609, -0.9917355));
	r2.x = r1.x * r4.w;
	r13 = r3.z + -r3.y;
	r4.w = r13.x;
	r0.w = r3.w * r3.x + r4.x;
	r2.yzw = r4.yzw * lightColor.yzw;
	r3.y = r3.y + r2.w;
	r13 = -r0.w;
	r0.xyz = -r2.xzy * r3.xxx + r2.xzy;
	r3.y = -r3.y + 1;
	r13 = r5.w + r13.x;
	r4.w = r13.x;
	r4.xyz = -r0.xzy + r1.yzw;
	r13 = off.x * r3.y;
	r3.x = r13.x;
	o = r3.x * r4 + r0.xzyw;

	return o;
}

technique insideDark
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_insideDark_P0();
		PixelShader = compile ps_3_0 ps_insideDark_P0();
	}
}

technique darkBlue
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_darkBlue_P0();
		PixelShader = compile ps_3_0 ps_darkBlue_P0();
	}
}

technique outdoors
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_outdoors_P0();
		PixelShader = compile ps_3_0 ps_outdoors_P0();
	}
}

technique flint
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_flint_P0();
		PixelShader = compile ps_3_0 ps_flint_P0();
	}
}

technique victory
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_insideDark_P0();
		PixelShader = compile ps_3_0 ps_insideDark_P0();
	}
}

technique pit
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_pit_P0();
		PixelShader = compile ps_3_0 ps_pit_P0();
	}
}

technique water
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_water_P0();
		PixelShader = compile ps_3_0 ps_water_P0();
	}
}

technique victory2
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_victory2_P0();
		PixelShader = compile ps_3_0 ps_victory2_P0();
	}
}
