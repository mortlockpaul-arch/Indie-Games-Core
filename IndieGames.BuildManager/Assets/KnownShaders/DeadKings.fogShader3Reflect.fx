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
	float4 normal : NORMAL;
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
	float4 r11;
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
	r1 = i.normal;
	r3.zw = i.texcoord.xy;
	r3.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r2 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[2].w;
	r6 = transpose(view)[3] * transpose(projection)[3].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[3].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[2].z + r5.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[1].z + r2.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[1].y + r2.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[2].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[3].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[3].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[2].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r2;
	r7 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r6 = r7.y * transpose(world)[3].xzyw;
	r5 = r8.y * transpose(world)[3].xzyw;
	r2 = r9.y * transpose(world)[3].xzyw;
	r0 = r10.y * transpose(world)[3].xzyw;
	r0 = r10.w * transpose(world)[2] + r0.xzyw;
	r2 = r9.w * transpose(world)[2] + r2.xzyw;
	r5 = r8.w * transpose(world)[2] + r5.xzyw;
	r6 = r7.w * transpose(world)[2] + r6.xzyw;
	r6 = r7.z * transpose(world)[1] + r6;
	r5 = r8.z * transpose(world)[1] + r5;
	r2 = r9.z * transpose(world)[1] + r2;
	r0 = r10.z * transpose(world)[1] + r0;
	r0 = r10.x * transpose(world)[0] + r0;
	r2 = r9.x * transpose(world)[0] + r2;
	r5 = r8.x * transpose(world)[0] + r5;
	r6 = r7.x * transpose(world)[0] + r6;
	o.position.x = dot(r6.zxyw, r4.zxyw);
	o.position.y = dot(r5.zxyw, r4.zxyw);
	o.position.z = dot(r2.zxyw, r4.zxyw);
	o.position.w = dot(r0.zxyw, r4.zxyw);
	r2.x = dot(r1.wzxy, transpose(world)[0].wzxy);
	r2.y = dot(r1.wzxy, transpose(world)[1].wzxy);
	r2.z = dot(r1.wzxy, transpose(world)[2].wzxy);
	r1.x = dot(r4.zxy, transpose(world)[0].zxy);
	r1.z = dot(r4.zxy, transpose(world)[1].zxy);
	r1.w = dot(r4.zxy, transpose(world)[2].zxy);
	r5.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r5.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r5.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r1.y = 1 / fe.x;
	r0.xzw = r5.xyz + -campos.xyz;
	r4.x = 1 / uvB.x;
	r6.xyz = r5.xyz + -ghostpos.xyz;
	r4.y = 1 / uvB.z;
	r0.y = dot(r6.zxy, r6.zxy);
	r4.z = 1 / uvA.x;
	r11.x = dot(r0.wxz, r0.wxz);
	r12 = rsqrt(abs(r0.y));
	r0.z = 1 / r12.x;
	r0.x = r11.x;
	r1.xzw = -r1.xzw + campos.xyz;
	r12 = rsqrt(abs(r0.x));
	r0.y = 1 / r12.x;
	r11.x = dot(r1.wxz, r1.wxz);
	r2.w = fs.x + r0.y;
	r0.x = r11.x;
	r0.z = r0.z + bs.x;
	r0.y = 1 / be.x;
	r0.y = saturate(r0.z * r0.y);
	r4.w = 1 / uvA.z;
	r11.w = dot(r2.zxy, r2.zxy);
	r0.x = rsqrt(abs(r0.x));
	r0.w = r11.w;
	o.fog1 = 1 + -r0.y;
	o.texcoord3 = r5.xyz + -campos.xyz;
	o.texcoord = r4.zw * r3.zw + uvA.yw;
	o.texcoord1 = r4.xy * r3.xy + uvB.yw;
	r11.xyz = r1.xzw * r0.xxx;
	r1.x = rsqrt(abs(r0.w));
	r0.xyz = r11.xyz;
	r1 = r2 * r1.xxxy;
	r0.w = dot(-r0.zxy, r1.zxy);
	r2.x = max(r1.w, 0.2);
	r0.w = r0.w + r0.w;
	o.fog = min(r2.x, 1);
	o.texcoord2 = -r1.xyz * r0.www + -r0.xyz;

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
	float4 r5;
	float4 r9;
	float r14;
	float4 r8;
	r6 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord2.xyz;
	r3.xyz = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r4.yz = -r2.xy * 15;
	r9 = tex2D(ReflectionSampler, r4.yz);
	r5.yzw = r9.xyz;
	r9 = tex2D(roomcolor, r0.xy);
	r0 = r9.yzwx;
	r9 = tex2D(roomcolor, r1.xy);
	r1 = r9.wyzx;
	r4.y = dot(r3.zxy, r3.zxy);
	r0.xyz = r0.zxy * r1.xyz;
	r3 = r1.wywz * float4(1.1, 1.1, 1, 1);
	r14 = r1.x;
	r4.z = r14.x;
	r3 = r3.wxyz * lightColor.zxyx;
	r8 = rsqrt(abs(r4.y));
	r14 = 1 / r8.x;
	r4.y = r14.x;
	r4.yw = saturate(r4.yy * float2(0.007246377, 0.0016528926) + float2(-0.9782609, -0.9917355));
	r6.x = r3.w * r0.w;
	r14 = 1 + -r4.z;
	r2.x = r14.x;
	r2.yz = r2.xx * r5.zw;
	r14 = 1 + -r0.x;
	r4.z = r14.x;
	r4.z = r4.z * r4.x + r0.x;
	r2.yzw = r2.xzy * ref1.xxx;
	r14 = 1 + r4.z;
	r1.w = r14.x;
	r3.w = -r1.w + r1.x;
	r14 = r4.w + -r4.y;
	r0.w = r14.x;
	r6.yzw = r0.yzw * lightColor.yzw;
	r14 = r2.y;
	r4.z = r4.y + r6.w;
	r14 = r5.y * r14.x;
	r2.x = r14.x;
	r4.xyw = -r6.xzy * r4.xxx + r6.xzy;
	r1.xyz = r4.xwy + r2.xwz;
	r14 = 1 + -r4.z;
	r4.y = r14.x;
	r3.xyz = -r1.xyz + r3.yzx;
	r14 = off.x * r4.y;
	r4.x = r14.x;
	r0 = r4.x * r3 + r1;
	r4.xyz = -r0.xyz * r5.xxx;
	r14 = 1 + -r0.w;
	r4.w = r14.x;
	r4.w = r4.w * r5.x;
	o = r0 + r4;

	return o;
}

struct vs_darkBlue_P0_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
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
	float4 r10;
	float4 r11;
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
	r2 = i.normal;
	r1.zw = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[3].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[1].w;
	r6 = transpose(view)[3] * transpose(projection)[0].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[0].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[1].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[3].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[3].y + r0.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[1].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[0].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[0].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[3].x + r0;
	r4 = r7.y * transpose(world)[3].xzyw;
	r6 = r8.y * transpose(world)[3].xzyw;
	r5 = r9.y * transpose(world)[3].xzyw;
	r0 = r10.y * transpose(world)[3].xzyw;
	r0 = r10.w * transpose(world)[2] + r0.xzyw;
	r5 = r9.w * transpose(world)[2] + r5.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r4 = r7.z * transpose(world)[1] + r4;
	r6 = r8.z * transpose(world)[1] + r6;
	r5 = r9.z * transpose(world)[1] + r5;
	r0 = r10.z * transpose(world)[1] + r0;
	r0 = r10.x * transpose(world)[0] + r0;
	r5 = r9.x * transpose(world)[0] + r5;
	r6 = r8.x * transpose(world)[0] + r6;
	r4 = r7.x * transpose(world)[0] + r4;
	r4.w = dot(r4.zxyw, r3.zxyw);
	r4.z = dot(r6.zxyw, r3.zxyw);
	r4.y = dot(r5.zxyw, r3.zxyw);
	r4.x = dot(r0.zxyw, r3.zxyw);
	o.position = r4;
	r5.x = dot(r2.wzxy, transpose(world)[0].wzxy);
	r5.y = dot(r2.wzxy, transpose(world)[1].wzxy);
	r5.z = dot(r2.wzxy, transpose(world)[2].wzxy);
	r0.x = dot(r3.zxy, transpose(world)[0].zxy);
	r0.y = dot(r3.zxy, transpose(world)[1].zxy);
	r0.z = dot(r3.zxy, transpose(world)[2].zxy);
	r2.x = dot(transpose(world)[0].zxyw, r3.zxyw);
	r2.y = dot(transpose(world)[1].zxyw, r3.zxyw);
	r2.z = dot(transpose(world)[2].zxyw, r3.zxyw);
	r6.xyz = r2.xyz + -campos.xyz;
	r7.xyz = r2.xyz + -ghostpos.xyz;
	r3.xyz = -r0.xyz + campos.xyz;
	r2.x = 1 / uvB.x;
	r0.y = dot(r5.zxy, r5.zxy);
	r2.y = 1 / uvB.z;
	r11.x = dot(r3.zxy, r3.zxy);
	r0.z = rsqrt(abs(r0.y));
	r0.x = r11.x;
	r11.w = dot(r7.zxy, r7.zxy);
	r0.x = rsqrt(abs(r0.x));
	r0.w = r11.w;
	r11.y = dot(r6.zxy, r6.zxy);
	r12 = rsqrt(abs(r0.w));
	r0.w = 1 / r12.x;
	r0.y = r11.y;
	r11.w = r0.w + bs.x;
	r12 = rsqrt(abs(r0.y));
	r0.y = 1 / r12.x;
	r0.w = r11.w;
	r6.xyz = r5.xyz * r0.zzz;
	r0.z = ds.x + r0.y;
	r5.xyz = r3.xyz * r0.xxx;
	r3.x = 1 / de.x;
	r7 = (float4(-0, -0, -0, -1) >= 0) ? -r5.zxyy : 1;
	r6.w = dot(-r5.zxy, r6.zxy);
	r3.y = 1 / be.x;
	r0.x = saturate(r0.w * r3.y);
	r2.z = 1 / uvA.x;
	r0.y = dot(r6.zxyw, r7);
	r2.w = 1 / uvA.z;
	o.texcoord2 = -r6.xyz * r0.yyy + -r5.xyz;
	o.fog1 = 1 + -r0.x;
	o.texcoord3 = r4.xyz;
	o.fog = saturate(r0.z * r3.x);
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
	float3 r1;
	float3 r2;
	float3 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r6;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.texcoord2.xyz;
	r3 = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r4.yz = -r2.xy * 15;
	r8 = tex2D(ReflectionSampler, r4.yz);
	r5.yzw = r8.xyz;
	r8 = tex2D(roomcolor, r1.xy);
	r0 = r8;
	r13 = 0.2 * r0.w;
	r4.z = r13.x;
	r4.w = dot(r0.zxy, 0.33333334);
	r13 = r0.w;
	r4.y = r13.x;
	r1.xy = r4.ww * float2(0.2, 0.26);
	r13 = 1 + -r4.y;
	r0.y = r13.x;
	r0.zw = r0.yy * r5.zw;
	r13 = 1 + -r4.z;
	r0.x = r13.x;
	r4.yw = -r1.xy * r4.xx + r1.xy;
	r6.yzw = r0.ywz * ref1.xxx;
	r13 = r0.x;
	r0.yzw = r6.yzw;
	r1.yz = -r4.yw * r5.xx;
	r13 = r4.x * r13.x;
	r1.x = r13.x;
	r4.xyz = r4.zyw + r1.xyz;
	r13 = r0.y;
	r4.w = -r4.x + 1;
	r13 = r5.y * r13.x;
	r0.x = r13.x;
	o.xyz = r4.yyz + r0.xwz;
	r4.x = r4.w * r5.x + r4.x;
	r13 = 1 + r4.x;
	o.w = r13.x;

	return o;
}

struct vs_outdoors_P0_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
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
	r2.xyz = i.position.xyz;
	r2.w = 1;
	r4 = i.normal;
	r3.zw = i.texcoord.xy;
	r3.xy = i.texcoord1.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r1 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[2].w;
	r6 = transpose(view)[3] * transpose(projection)[3].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[3].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[2].z + r5.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[1].z + r1.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r1 = transpose(view)[1].xwyz * transpose(projection)[1].y + r1.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[2].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[3].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[3].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[2].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r1;
	r7 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r6 = r7.y * transpose(world)[3].xzyw;
	r5 = r8.y * transpose(world)[3].xzyw;
	r1 = r9.y * transpose(world)[3].xzyw;
	r0 = r10.y * transpose(world)[3].xzyw;
	r0 = r10.w * transpose(world)[2] + r0.xzyw;
	r1 = r9.w * transpose(world)[2] + r1.xzyw;
	r5 = r8.w * transpose(world)[2] + r5.xzyw;
	r6 = r7.w * transpose(world)[2] + r6.xzyw;
	r6 = r7.z * transpose(world)[1] + r6;
	r5 = r8.z * transpose(world)[1] + r5;
	r1 = r9.z * transpose(world)[1] + r1;
	r0 = r10.z * transpose(world)[1] + r0;
	r0 = r10.x * transpose(world)[0] + r0;
	r1 = r9.x * transpose(world)[0] + r1;
	r5 = r8.x * transpose(world)[0] + r5;
	r6 = r7.x * transpose(world)[0] + r6;
	o.position.x = dot(r6.zxyw, r2.zxyw);
	o.position.y = dot(r5.zxyw, r2.zxyw);
	o.position.z = dot(r1.zxyw, r2.zxyw);
	o.position.w = dot(r0.zxyw, r2.zxyw);
	r5.x = dot(transpose(world)[0].zxyw, r2.zxyw);
	r5.y = dot(transpose(world)[1].zxyw, r2.zxyw);
	r5.z = dot(transpose(world)[2].zxyw, r2.zxyw);
	r1.x = dot(r4.wzxy, transpose(world)[0].wzxy);
	r1.y = dot(r4.wzxy, transpose(world)[1].wzxy);
	r1.z = dot(r4.wzxy, transpose(world)[2].wzxy);
	r0.x = dot(r2.zxy, transpose(world)[0].zxy);
	r0.y = dot(r2.zxy, transpose(world)[1].zxy);
	r0.z = dot(r2.zxy, transpose(world)[2].zxy);
	r2.xyz = -r0.xyz + campos.xyz;
	r0.y = dot(r1.zxy, r1.zxy);
	r4.x = 1 / uvB.x;
	r0.xzw = r5.xyz + -campos.xyz;
	r4.y = 1 / uvB.z;
	r0.w = dot(r0.wxz, r0.wxz);
	r4.z = 1 / uvA.x;
	r11 = dot(r2.zxy, r2.zxy);
	r0.y = rsqrt(abs(r0.y));
	r0.x = r11.x;
	r1.xyz = r1.xyz * r0.yyy;
	r0.x = rsqrt(abs(r0.x));
	r0.xyz = r2.xyz * r0.xxx;
	r4.w = 1 / uvA.z;
	r2 = (float4(-0, -0, -0, -1) >= 0) ? -r0.zxyy : 1;
	r1.w = dot(-r0.zxy, r1.zxy);
	r12 = rsqrt(abs(r0.w));
	r0.w = 1 / r12.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.texcoord3 = r5.xyz + -campos.xyz;
	o.texcoord = r4.zw * r3.zw + uvA.yw;
	o.texcoord1 = r4.xy * r3.xy + uvB.yw;
	o.fog = saturate(r0.w * 0.0004 + -0.44);
	r0.w = dot(r1.zxyw, r2);
	o.texcoord2 = -r1.xyz * r0.www + -r0.xyz;

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

	float4 r6;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r9;
	float r14;
	float4 r8;
	r6 = 0;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2.xyz = i.texcoord2.xyz;
	r3.xyz = i.texcoord3.xyz;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r4.yz = -r2.xy * 15;
	r9 = tex2D(ReflectionSampler, r4.yz);
	r5.xyz = r9.xyz;
	r9 = tex2D(roomcolor, r0.xy);
	r2 = r9;
	r9 = tex2D(roomcolor, r1.xy);
	r0 = r9;
	r4.z = dot(r3.zxy, r3.zxy);
	r14 = r0.w;
	r4.y = r14.x;
	r3.xyz = r2.yzw * r0.yzw;
	r14 = 1 + -r4.y;
	r4.y = r14.x;
	r2.yz = r3.xy * lightColor.yz;
	r8 = rsqrt(abs(r4.z));
	r14 = 1 / r8.x;
	r4.z = r14.x;
	r6.xy = saturate(r4.zz * float2(0.006666667, 0.0016528926) + float2(-0.8, -0.9917355));
	r4.zw = r4.yy * r5.yz;
	r14 = r6.y + -r6.x;
	r1.w = r14.x;
	r1.xyz = r0.zxy * float3(0.8, 1, 1);
	r14 = ref1.x * r4.y;
	r5.y = r14.x;
	r1 = r1.yzxw * lightColor;
	r14 = ref1.x * r4.w;
	r5.z = r14.x;
	r3.w = r6.x + r1.w;
	r14 = ref1.x * r4.z;
	r5.w = r14.x;
	r2.x = r1.x * r2.x;
	r14 = r5.y;
	r2.xyz = -r2.xzy * r4.xxx + r2.xzy;
	r4.yz = -r3.zw + 1;
	r14 = r5.x * r14.x;
	r5.x = r14.x;
	r5.xyz = r2.xzy + r5.xwz;
	r4.y = r4.y * r4.x + r3.z;
	r0.xyz = -r5.xyz + r1.xyz;
	r14 = 1 + r4.y;
	r5.w = r14.x;
	r0.w = -r5.w + r0.w;
	r14 = off.x * r4.z;
	r4.x = r14.x;
	o = r4.x * r0 + r5;

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
