float ss = 650;
float se = 1500;
float fs = 250;
float fe = 1300;
float ds = -250;
float de = 600;
float bs = -800;
float be = 900;
float4x4 world;
float4x4 view;
float4x4 projection;
float3 campos;
float slide;
float ref1 = 1;
float3 ghostpos;
float4 lightColor = float4(1, 1, 1, 1);
float4 uvA;
float4 uvB;
float off;
texture rgbTexture;

sampler roomcolor = sampler_state
{
	Texture = <rgbTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Mirror;
	AddressV = Mirror;
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
	float3 texcoord2 : TEXCOORD2;
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
	float4 r10;
	float r11;
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
	r1.xy = i.texcoord.xy;
	r0.zw = i.texcoord1.xy;
	r2 = transpose(view)[3] * transpose(projection)[0].w;
	r4 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[2].w;
	r6 = transpose(view)[3] * transpose(projection)[3].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[3].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[2].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r4.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[0].z + r2.xywz;
	r2 = transpose(view)[1].xwyz * transpose(projection)[0].y + r2.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[2].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[3].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[3].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[2].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[0].x + r2;
	r6 = r7.y * transpose(world)[3].xzyw;
	r5 = r8.y * transpose(world)[3].xzyw;
	r4 = r9.y * transpose(world)[3].xzyw;
	r2 = r10.y * transpose(world)[3].xzyw;
	r2 = r10.w * transpose(world)[2] + r2.xzyw;
	r4 = r9.w * transpose(world)[2] + r4.xzyw;
	r5 = r8.w * transpose(world)[2] + r5.xzyw;
	r6 = r7.w * transpose(world)[2] + r6.xzyw;
	r6 = r7.z * transpose(world)[1] + r6;
	r5 = r8.z * transpose(world)[1] + r5;
	r4 = r9.z * transpose(world)[1] + r4;
	r2 = r10.z * transpose(world)[1] + r2;
	r2 = r10.x * transpose(world)[0] + r2;
	r4 = r9.x * transpose(world)[0] + r4;
	r5 = r8.x * transpose(world)[0] + r5;
	r6 = r7.x * transpose(world)[0] + r6;
	o.position.x = dot(r6.zxyw, r3.zxyw);
	o.position.y = dot(r5.zxyw, r3.zxyw);
	o.position.z = dot(r4.zxyw, r3.zxyw);
	o.position.w = dot(r2.zxyw, r3.zxyw);
	r2.x = dot(transpose(world)[0].zxyw, r3.zxyw);
	r2.y = dot(transpose(world)[1].zxyw, r3.zxyw);
	r2.z = dot(transpose(world)[2].zxyw, r3.zxyw);
	r4.xyz = r2.xyz + -ghostpos.xyz;
	r1.z = 1 / uvA.x;
	r3.yzw = r2.xyz + -campos.xyz;
	r3.x = 1 / uvB.x;
	r0.y = dot(r3.wyz, r3.wyz);
	r3.y = 1 / uvB.z;
	r11 = dot(r4.zxy, r4.zxy);
	r12 = rsqrt(abs(r0.y));
	r0.y = 1 / r12.x;
	r0.x = r11.x;
	r3.z = r0.y + fs.x;
	r12 = rsqrt(abs(r0.x));
	r0.x = 1 / r12.x;
	r0.y = r0.x + bs.x;
	r0.x = 1 / be.x;
	r0.x = saturate(r0.y * r0.x);
	r0.y = 1 / fe.x;
	r0.yzw = r3.zxy * r0.yzw;
	r1.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.x;
	o.texcoord1 = r0.zw + uvB.yw;
	o.texcoord3 = r2.xyz + -campos.xyz;
	o.texcoord2 = (0 < -abs(r0.xxx)) ? 1 : 0;
	o.texcoord = r1.zw * r1.xy + uvA.yw;
	r0.x = max(r0.y, 0.2);
	o.fog = min(r0.x, 1);

	return o;
}

struct ps_insideDark_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_insideDark_P0(ps_insideDark_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r6;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float r5;
	float4 r9;
	float r14;
	float4 r8;
	r6 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord2.xyz;
	r3.xyz = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5 = i.fog1.x;
	r9 = tex2D(roomcolor, r0.xy);
	r0 = r9.yzwx;
	r9 = tex2D(roomcolor, r1.xy);
	r2 = r9;
	r4.y = dot(r3.zxy, r3.zxy);
	r0.xyz = r0.zxy * r2.wyz;
	r1 = r2.xyxz * float4(1.1, 1.1, 1, 1);
	r14 = 1 + -r0.x;
	r4.w = r14.x;
	r3 = r1.zwxy * lightColor.xzxy;
	r8 = rsqrt(abs(r4.y));
	r14 = 1 / r8.x;
	r4.y = r14.x;
	r4.yz = saturate(r4.yy * float2(0.007246377, 0.0016528926) + float2(-0.9782609, -0.9917355));
	r6.x = r3.x * r0.w;
	r14 = r4.z + -r4.y;
	r0.w = r14.x;
	r1.w = r4.w * r4.x + r0.x;
	r6.yzw = r0.yzw * lightColor.yzw;
	r4.y = r4.y + r6.w;
	r14 = -r1.w;
	r1.xyz = -r6.xzy * r4.xxx + r6.xzy;
	r4.y = -r4.y + 1;
	r14 = r2.w + r14.x;
	r0.w = r14.x;
	r0.xyz = -r1.xzy + r3.zwy;
	r14 = off.x * r4.y;
	r4.x = r14.x;
	r0 = r4.x * r0 + r1.xzyw;
	r4.xyz = -r0.xzy * r5.xxx;
	r14 = 1 + -r0.w;
	r4.w = r14.x;
	r4.w = r4.w * r5.x;
	o = r0 + r4.xzyw;

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
	float3 texcoord2 : TEXCOORD2;
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
	float2 r10;
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
	r0 = transpose(view)[3] * transpose(projection)[3].w;
	r3 = transpose(view)[3] * transpose(projection)[2].w;
	r4 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[0].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[0].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r3.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[3].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[3].y + r0.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[0].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[0].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[3].x + r0;
	r3 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r4 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r4 = r8.w * transpose(world)[2] + r4.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r3 = r6.w * transpose(world)[2] + r3.xzyw;
	r3 = r6.z * transpose(world)[1] + r3;
	r5 = r7.z * transpose(world)[1] + r5;
	r4 = r8.z * transpose(world)[1] + r4;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r4 = r8.x * transpose(world)[0] + r4;
	r5 = r7.x * transpose(world)[0] + r5;
	r3 = r6.x * transpose(world)[0] + r3;
	r3.w = dot(r3.zxyw, r2.zxyw);
	r3.z = dot(r5.zxyw, r2.zxyw);
	r3.y = dot(r4.zxyw, r2.zxyw);
	r3.x = dot(r0.zxyw, r2.zxyw);
	o.position = r3;
	r4.x = dot(transpose(world)[0].zxyw, r2.zxyw);
	r4.y = dot(transpose(world)[1].zxyw, r2.zxyw);
	r4.z = dot(transpose(world)[2].zxyw, r2.zxyw);
	r2.x = 1 / uvB.x;
	r0.yzw = r4.xyz + -campos.xyz;
	r2.y = 1 / uvB.z;
	r4.xyz = r4.xyz + -ghostpos.xyz;
	r2.z = 1 / uvA.x;
	r0.x = dot(r4.zxy, r4.zxy);
	r4.x = 1 / be.x;
	r10.y = dot(r0.wyz, r0.wyz);
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	r0.y = r10.y;
	r10.x = r0.x + bs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r0.x = r10.x;
	r0.z = r0.y + ds.x;
	r4.y = 1 / de.x;
	r0.y = saturate(r0.x * r4.x);
	r2.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.fog = saturate(r0.z * r4.y);
	o.texcoord3 = r3.xyz;
	o.texcoord2 = (0 < -abs(r0.xxx)) ? 1 : 0;
	o.texcoord = r2.zw * r1.zw + uvA.yw;
	o.texcoord1 = r2.xy * r1.xy + uvB.yw;

	return o;
}

struct ps_darkBlue_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
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
	float3 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r6;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.texcoord2.xyz;
	r3 = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r1.xy);
	r0 = r8;
	r4.z = dot(r0.zxy, 0.33333334);
	r13 = 0.2 * r0.w;
	r4.y = r13.x;
	r6.zw = r4.zz * float2(0.2, 0.26);
	r13 = 1 + -r4.y;
	r5.y = r13.x;
	r4.zw = r6.zw;
	r6.zw = -r4.zw * r4.xx;
	r13 = r5.y;
	r5.zw = r6.zw;
	r0.x = r4.w + r5.w;
	r13 = r4.x * r13.x;
	r5.y = r13.x;
	o.z = -r0.x * r5.x + r0.x;
	r4.xz = r4.yz + r5.yz;
	r6.w = -r4.z * r5.x;
	r13 = 1 + -r4.x;
	r4.y = r13.x;
	r4.w = r6.w;
	r4.y = r4.y * r5.x;
	o.xyw = r4.zzx + r4.wwy;

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
	float3 texcoord2 : TEXCOORD2;
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
	r3.xyz = i.position.xyz;
	r3.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r2 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[1].z + r2.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[1].y + r2.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r2;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r2 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r2 = r8.w * transpose(world)[2] + r2.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r2 = r8.z * transpose(world)[1] + r2;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r2 = r8.x * transpose(world)[0] + r2;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r3.zxyw);
	o.position.y = dot(r4.zxyw, r3.zxyw);
	o.position.z = dot(r2.zxyw, r3.zxyw);
	o.position.w = dot(r0.zxyw, r3.zxyw);
	r0.y = dot(transpose(world)[0].zxyw, r3.zxyw);
	r0.z = dot(transpose(world)[1].zxyw, r3.zxyw);
	r2.x = 1 / uvB.x;
	r0.w = dot(transpose(world)[2].zxyw, r3.zxyw);
	r2.y = 1 / uvB.z;
	r3.xyz = r0.yzw + -campos.xyz;
	r2.z = 1 / uvA.x;
	r0.x = dot(r3.zxy, r3.zxy);
	r2.w = 1 / uvA.z;
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord3 = r0.yzw + -campos.xyz;
	o.texcoord2 = (0 < -abs(r0.xxx)) ? 1 : 0;
	o.texcoord = r2.zw * r1.zw + uvA.yw;
	o.texcoord1 = r2.xy * r1.xy + uvB.yw;
	o.fog = saturate(r0.x * 0.0004 + -0.44);

	return o;
}

struct ps_outdoors_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_outdoors_P0(ps_outdoors_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float3 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float4 r7;
	float3 r6;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.texcoord2.xyz;
	r3.xyz = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r0.xy);
	r0 = r8;
	r8 = tex2D(roomcolor, r1.xy);
	r1 = r8;
	r4.y = dot(r3.zxy, r3.zxy);
	r5.xyw = r0.yzw * r1.yzw;
	r7 = rsqrt(abs(r4.y));
	r4.y = 1 / r7.x;
	r4.yz = saturate(r4.yy * float2(0.006666667, 0.0016528926) + float2(-0.8, -0.9917355));
	r3.xyz = r1.zxy * float3(0.8, 1, 1);
	r3.w = r4.z + -r4.y;
	r3 = r3.yzxw * lightColor;
	r6.z = r4.y + r3.w;
	r4.z = lightColor.y * r5.x;
	r5.z = r6.z;
	r4.y = r3.x * r0.x;
	r4.w = lightColor.z * r5.y;
	r0.xyz = -r4.ywz * r4.xxx + r4.ywz;
	r4.yz = -r5.zw + 1;
	r1.xyz = -r0.xzy + r3.xyz;
	r0.w = r4.z * r4.x + r5.w;
	r1.w = -r0.w + r1.w;
	r4.x = off.x * r4.y;
	o = r4.x * r1 + r0.xzyw;

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
	float3 texcoord2 : TEXCOORD2;
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
	r4.xyz = i.position.xyz;
	r4.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[3].w;
	r2 = transpose(view)[3] * transpose(projection)[2].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[0].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[0].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[2].z + r2.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[3].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[3].y + r0.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[2].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[0].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[0].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[2].x + r2;
	r6 = transpose(view)[0].xwyz * transpose(projection)[3].x + r0;
	r3 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r2 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r2 = r8.w * transpose(world)[2] + r2.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r3 = r6.w * transpose(world)[2] + r3.xzyw;
	r3 = r6.z * transpose(world)[1] + r3;
	r5 = r7.z * transpose(world)[1] + r5;
	r2 = r8.z * transpose(world)[1] + r2;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r2 = r8.x * transpose(world)[0] + r2;
	r5 = r7.x * transpose(world)[0] + r5;
	r3 = r6.x * transpose(world)[0] + r3;
	r3.w = dot(r3.zxyw, r4.zxyw);
	r3.z = dot(r5.zxyw, r4.zxyw);
	r3.y = dot(r2.zxyw, r4.zxyw);
	r3.x = dot(r0.zxyw, r4.zxyw);
	o.position = r3;
	r0.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r0.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r2.x = 1 / uvB.x;
	r0.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r2.y = 1 / uvB.z;
	r0.xyz = r0.xyz + -campos.xyz;
	r2.z = 1 / uvA.x;
	r0.x = dot(r0.zxy, r0.zxy);
	r2.w = 1 / uvA.z;
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord3 = r3.xyz;
	o.texcoord2 = (0 < -abs(r0.xxx)) ? 1 : 0;
	o.texcoord = r2.zw * r1.zw + uvA.yw;
	o.texcoord1 = r2.xy * r1.xy + uvB.yw;
	o.fog = saturate(r0.x * 0.0009090909 + -0.27272728);

	return o;
}

struct ps_flint_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
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
	float3 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r6;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.texcoord2.xyz;
	r3 = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r0.xy);
	r5 = r8;
	r8 = tex2D(roomcolor, r1.xy);
	r0 = r8;
	r4.zw = r0.xy * float2(1.8, 1.6);
	r5 = r5.xzyw * r0.xzyw;
	r13 = r0.w;
	r4.y = r13.x;
	r0.xy = -r4.zw * r4.xx + r4.zw;
	r5.xyz = -r5.xzy * r4.xxx + r5.xzy;
	r6.w = -r5.w + 1;
	r13 = 1 + -r4.y;
	r4.z = r13.x;
	r4.w = r6.w;
	r4.zw = r4.zw * r4.xx;
	r13 = -r0.z;
	r5.w = r5.w + r4.w;
	r13 = r4.x * r13.x;
	r4.y = r13.x;
	r0.zw = r4.yz + r0.zw;
	r4 = r0 + -r5;
	o = r4 * 0.8 + r5;

	return o;
}

struct vs_flint2_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
};

struct vs_flint2_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_flint2_P0_VS_OUT vs_flint2_P0(vs_flint2_P0_VS_IN i)
{
	vs_flint2_P0_VS_OUT o;

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
	r4.xyz = i.position.xyz;
	r4.w = 1;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[3].w;
	r2 = transpose(view)[3] * transpose(projection)[2].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[0].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[0].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[2].z + r2.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[3].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[3].y + r0.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[2].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[0].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[0].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[2].x + r2;
	r6 = transpose(view)[0].xwyz * transpose(projection)[3].x + r0;
	r3 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r2 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r2 = r8.w * transpose(world)[2] + r2.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r3 = r6.w * transpose(world)[2] + r3.xzyw;
	r3 = r6.z * transpose(world)[1] + r3;
	r5 = r7.z * transpose(world)[1] + r5;
	r2 = r8.z * transpose(world)[1] + r2;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r2 = r8.x * transpose(world)[0] + r2;
	r5 = r7.x * transpose(world)[0] + r5;
	r3 = r6.x * transpose(world)[0] + r3;
	r3.w = dot(r3.zxyw, r4.zxyw);
	r3.z = dot(r5.zxyw, r4.zxyw);
	r3.y = dot(r2.zxyw, r4.zxyw);
	r3.x = dot(r0.zxyw, r4.zxyw);
	o.position = r3;
	r0.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r0.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r2.x = 1 / uvB.x;
	r0.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r2.y = 1 / uvB.z;
	r0.xyz = r0.xyz + -campos.xyz;
	r2.z = 1 / uvA.x;
	r0.x = dot(r0.zxy, r0.zxy);
	r2.w = 1 / uvA.z;
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord3 = r3.xyz;
	o.texcoord2 = (0 < -abs(r0.xxx)) ? 1 : 0;
	o.texcoord = r2.zw * r1.zw + uvA.yw;
	o.texcoord1 = r2.xy * r1.xy + uvB.yw;
	o.fog = saturate(r0.x * 0.00032258066 + -0.3870968);

	return o;
}

struct ps_flint2_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float3 texcoord3 : TEXCOORD3;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_flint2_P0(ps_flint2_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float3 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float r6;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.texcoord2.xyz;
	r3.xyz = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r0.xy);
	r5 = r8;
	r8 = tex2D(roomcolor, r1.xy);
	r1 = r8;
	r4.w = -r1.w + 1;
	r0 = r5 * r1;
	r5.xyz = r1.xyz * float3(2.2, 0.6, 0.6);
	r13 = r0.w;
	r4.y = r13.x;
	r3.xy = -r0.yz * r4.xx + r0.yz;
	r5.xyw = -r5.yxz * r4.xxx + r5.yxz;
	r5.z = -r0.x * r4.x + r0.x;
	r3.zw = r5.wx + -r3.yx;
	r13 = 1 + -r4.y;
	r4.z = r13.x;
	o.yz = r3.wz * 0.8 + r3.xy;
	r4.xy = r4.wz * r4.xx;
	r4.x = r4.x + r1.w;
	r6 = r0.w + r4.y;
	r13 = r5.y;
	r5.x = r6.x;
	r4.x = r4.x + -r5.x;
	r13 = -r5.z + r13.x;
	r4.y = r13.x;
	r5.yw = r4.xy * 0.8;
	o.xw = r5.zx + r5.wy;

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

technique flint2
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_flint2_P0();
		PixelShader = compile ps_3_0 ps_flint2_P0();
	}
}

technique stairFog
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_insideDark_P0();
		PixelShader = compile ps_3_0 ps_insideDark_P0();
	}
}
