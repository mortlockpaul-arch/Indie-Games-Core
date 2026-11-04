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
float3 ghostpos;
float4 lightColor = float4(1, 1, 1, 1);
float4 uvA;
float4 uvB;
float4 uvC;
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

struct vs_insideDark_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_insideDark_P0_VS_OUT
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
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
	r1.yz = i.texcoord.xy;
	r0 = transpose(view)[3] * transpose(projection)[0].w;
	r2 = transpose(view)[3] * transpose(projection)[1].w;
	r3 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[1].z + r2.xywz;
	r0 = transpose(view)[2].xywz * transpose(projection)[0].z + r0.xywz;
	r0 = transpose(view)[1].xwyz * transpose(projection)[0].y + r0.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[1].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r2;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r0;
	r5 = r6.y * transpose(world)[3].xzyw;
	r3 = r7.y * transpose(world)[3].xzyw;
	r2 = r8.y * transpose(world)[3].xzyw;
	r0 = r9.y * transpose(world)[3].xzyw;
	r0 = r9.w * transpose(world)[2] + r0.xzyw;
	r2 = r8.w * transpose(world)[2] + r2.xzyw;
	r3 = r7.w * transpose(world)[2] + r3.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r3 = r7.z * transpose(world)[1] + r3;
	r2 = r8.z * transpose(world)[1] + r2;
	r0 = r9.z * transpose(world)[1] + r0;
	r0 = r9.x * transpose(world)[0] + r0;
	r2 = r8.x * transpose(world)[0] + r2;
	r3 = r7.x * transpose(world)[0] + r3;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r4.zxyw);
	o.position.y = dot(r3.zxyw, r4.zxyw);
	o.position.z = dot(r2.zxyw, r4.zxyw);
	o.position.w = dot(r0.zxyw, r4.zxyw);
	r3.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r3.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r2.x = 1 / uvC.x;
	r3.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r2.y = 1 / uvC.z;
	r4.xyz = r3.xyz + -ghostpos.xyz;
	r2.z = 1 / uvA.x;
	r0.yzw = r3.xyz + -campos.xyz;
	r0.x = 1 / uvB.x;
	r0.w = dot(r0.wyz, r0.wyz);
	r0.z = 1 / uvB.z;
	r10.y = dot(r4.zxy, r4.zxy);
	r11 = rsqrt(abs(r0.w));
	r0.w = 1 / r11.x;
	r0.y = r10.y;
	r10.w = r0.w + fs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r0.w = r10.w;
	r1.x = r0.y + bs.x;
	r0.y = 1 / be.x;
	r0.y = saturate(r1.x * r0.y);
	r1.x = 1 / fe.x;
	r0.xzw = r0.wxz * r1.xyz;
	r2.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.normal = r3.xyz + -campos.xyz;
	o.texcoord2 = r2.xy * r1.yz + uvC.yw;
	o.texcoord1 = r0.zw + uvB.yw;
	o.texcoord = r2.zw * r1.yz + uvA.yw;
	r0.x = max(r0.x, 0.2);
	o.fog = min(r0.x, 1);

	return o;
}

struct ps_insideDark_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_insideDark_P0(ps_insideDark_P0_PS_IN i) : COLOR
{
	float4 o;

	float3 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r7;
	r0 = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2.xy = i.texcoord1.xy;
	r3.xy = i.texcoord2.xy;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r1.xy);
	r1 = r8;
	r8 = tex2D(roomcolor, r2.xy);
	r2 = r8;
	r8 = tex2D(roomcolor, r3.xy);
	r3 = r8;
	r4.y = dot(r0.zxy, r0.zxy);
	r13 = 1 + -r3.w;
	r4.z = r13.x;
	r2 = r4.z * r2;
	r7 = rsqrt(abs(r4.y));
	r13 = 1 / r7.x;
	r4.y = r13.x;
	r4.yz = saturate(r4.yy * float2(0.0016528926, 0.008264462) + -0.9917355);
	r2 = r3 * r3.w + r2;
	r1 = r2 * r1;
	r13 = r4.y + -r4.z;
	r3.x = r13.x;
	r3.yzw = r2.zxy * float3(0.8, 1, 1);
	r13 = r1.w;
	r4.y = r13.x;
	r5.yzw = r1.xyz * lightColor.xyz;
	r13 = 1 + -r4.y;
	r4.y = r13.x;
	r4.w = r3.x * lightColor.w + r4.z;
	r1.xyz = -r5.ywz * r4.xxx + r5.ywz;
	r1.w = r4.y * r4.x + r1.w;
	r4.xyz = r3.zwy * lightColor.xyz + -r1.xzy;
	r4.w = -r4.w + 1;
	r13 = -r1.w;
	r5.y = r4.w * off.x;
	r13 = r2.w + r13.x;
	r4.w = r13.x;
	r1 = r5.y * r4 + r1.xzyw;
	r4.xyz = -r1.xzy * r5.xxx;
	r13 = 1 + -r1.w;
	r4.w = r13.x;
	r4.w = r4.w * r5.x;
	o = r1 + r4.xzyw;

	return o;
}

struct vs_darkBlue_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_darkBlue_P0_VS_OUT
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
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
	r1.xyz = i.position.xyz;
	r1.w = 1;
	r0.xz = i.texcoord.xy;
	r2 = transpose(view)[3] * transpose(projection)[3].w;
	r3 = transpose(view)[3] * transpose(projection)[2].w;
	r4 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[0].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[0].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[3].z + r2.xywz;
	r2 = transpose(view)[1].xwyz * transpose(projection)[3].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[0].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[0].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[3].x + r2;
	r3 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r4 = r8.y * transpose(world)[3].xzyw;
	r2 = r9.y * transpose(world)[3].xzyw;
	r2 = r9.w * transpose(world)[2] + r2.xzyw;
	r4 = r8.w * transpose(world)[2] + r4.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r3 = r6.w * transpose(world)[2] + r3.xzyw;
	r3 = r6.z * transpose(world)[1] + r3;
	r5 = r7.z * transpose(world)[1] + r5;
	r4 = r8.z * transpose(world)[1] + r4;
	r2 = r9.z * transpose(world)[1] + r2;
	r2 = r9.x * transpose(world)[0] + r2;
	r4 = r8.x * transpose(world)[0] + r4;
	r5 = r7.x * transpose(world)[0] + r5;
	r3 = r6.x * transpose(world)[0] + r3;
	r3.w = dot(r3.zxyw, r1.zxyw);
	r3.z = dot(r5.zxyw, r1.zxyw);
	r3.y = dot(r4.zxyw, r1.zxyw);
	r3.x = dot(r2.zxyw, r1.zxyw);
	o.position = r3;
	r5.x = dot(transpose(world)[0].zxyw, r1.zxyw);
	r2.x = 1 / uvB.x;
	r5.y = dot(transpose(world)[1].zxyw, r1.zxyw);
	r2.y = 1 / uvB.z;
	r5.z = dot(transpose(world)[2].zxyw, r1.zxyw);
	r1.x = 1 / uvC.x;
	r4.yzw = r5.xyz + -campos.xyz;
	r1.y = 1 / uvC.z;
	r5.xyz = r5.xyz + -ghostpos.xyz;
	r1.z = 1 / uvA.x;
	r0.w = dot(r5.zxy, r5.zxy);
	r4.x = 1 / be.x;
	r10.y = dot(r4.wyz, r4.wyz);
	r11 = rsqrt(abs(r0.w));
	r0.w = 1 / r11.x;
	r0.y = r10.y;
	r2.z = r0.w + bs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r2.w = r0.y + ds.x;
	r4.y = 1 / de.x;
	r0.y = saturate(r2.z * r4.x);
	r1.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.fog = saturate(r2.w * r4.y);
	o.normal = r3.xyz;
	o.texcoord = r1.zw * r0.xz + uvA.yw;
	o.texcoord1 = r2.xy * r0.xz + uvB.yw;
	o.texcoord2 = r1.xy * r0.xz + uvC.yw;

	return o;
}

struct ps_darkBlue_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_darkBlue_P0(ps_darkBlue_P0_PS_IN i) : COLOR
{
	float4 o;

	float3 r0;
	float4 r1;
	float2 r2;
	float2 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r6;
	r0 = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2 = i.texcoord1.xy;
	r3 = i.texcoord2.xy;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r2.xy);
	r1 = r8;
	r4.z = dot(r1.zxy, 0.33333334);
	r13 = 0.2 * r1.w;
	r4.y = r13.x;
	r6.zw = r4.zz * float2(0.2, 0.26);
	r13 = 1 + -r4.y;
	r5.y = r13.x;
	r4.zw = r6.zw;
	r6.zw = -r4.zw * r4.xx;
	r13 = r5.y;
	r5.zw = r6.zw;
	r1.x = r4.w + r5.w;
	r13 = r4.x * r13.x;
	r5.y = r13.x;
	o.z = -r1.x * r5.x + r1.x;
	r4.xz = r4.yz + r5.yz;
	r6.w = -r4.z * r5.x;
	r13 = 1 + -r4.x;
	r4.y = r13.x;
	r4.w = r6.w;
	r4.y = r4.y * r5.x;
	o.xyw = r4.zzx + r4.wwy;

	return o;
}

struct vs_flint_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_flint_P0_VS_OUT
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
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
	r5.xyz = i.position.xyz;
	r5.w = 1;
	r0.yz = i.texcoord.xy;
	r1 = transpose(view)[3] * transpose(projection)[2].w;
	r2 = transpose(view)[3] * transpose(projection)[1].w;
	r3 = transpose(view)[3] * transpose(projection)[0].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[0].z + r3.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r2.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[2].z + r1.xywz;
	r1 = transpose(view)[3] * transpose(projection)[3].w;
	r2.x = 1 / uvB.x;
	r1 = transpose(view)[2].xywz * transpose(projection)[3].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[3].y + r1.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[2].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[0].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[0].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[3].x + r1;
	r3 = r7.y * transpose(world)[3].xzyw;
	r6 = r8.y * transpose(world)[3].xzyw;
	r4 = r9.y * transpose(world)[3].xzyw;
	r1 = r10.y * transpose(world)[3].xzyw;
	r1 = r10.w * transpose(world)[2] + r1.xzyw;
	r4 = r9.w * transpose(world)[2] + r4.xzyw;
	r6 = r8.w * transpose(world)[2] + r6.xzyw;
	r3 = r7.w * transpose(world)[2] + r3.xzyw;
	r3 = r7.z * transpose(world)[1] + r3;
	r6 = r8.z * transpose(world)[1] + r6;
	r4 = r9.z * transpose(world)[1] + r4;
	r1 = r10.z * transpose(world)[1] + r1;
	r1 = r10.x * transpose(world)[0] + r1;
	r4 = r9.x * transpose(world)[0] + r4;
	r6 = r8.x * transpose(world)[0] + r6;
	r3 = r7.x * transpose(world)[0] + r3;
	r3.w = dot(r3.zxyw, r5.zxyw);
	r3.z = dot(r6.zxyw, r5.zxyw);
	r3.y = dot(r4.zxyw, r5.zxyw);
	r3.x = dot(r1.zxyw, r5.zxyw);
	o.position = r3;
	r4.x = dot(transpose(world)[0].zxyw, r5.zxyw);
	r2.y = 1 / uvB.z;
	r4.y = dot(transpose(world)[1].zxyw, r5.zxyw);
	r1.x = 1 / uvC.x;
	r4.z = dot(transpose(world)[2].zxyw, r5.zxyw);
	r1.y = 1 / uvC.z;
	r4.xyz = r4.xyz + -campos.xyz;
	r1.z = 1 / uvA.x;
	r0.x = dot(r4.zxy, r4.zxy);
	r1.w = 1 / uvA.z;
	r12 = rsqrt(abs(r0.x));
	r0.x = 1 / r12.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.normal = r3.xyz;
	o.texcoord = r1.zw * r0.yz + uvA.yw;
	o.texcoord1 = r2.xy * r0.yz + uvB.yw;
	o.texcoord2 = r1.xy * r0.yz + uvC.yw;
	o.fog = saturate(r0.x * 0.0009090909 + -0.27272728);

	return o;
}

struct ps_flint_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_flint_P0(ps_flint_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	float r13;
	float4 r6;
	r0.xyz = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2.xy = i.texcoord1.xy;
	r3.xy = i.texcoord2.xy;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r1.xy);
	r5 = r8;
	r8 = tex2D(roomcolor, r3.xy);
	r0 = r8;
	r8 = tex2D(roomcolor, r2.xy);
	r2 = r8;
	r4.y = -r0.w + 1;
	r4.zw = r2.xy * float2(1.8, 1.6);
	r1.xy = -r4.zw * r4.xx + r4.zw;
	r3 = r4.y * r2;
	r3 = r0.xwyz * r0.w + r3.xwyz;
	r5 = r3.xzwy * r5;
	r13 = r2.w;
	r4.y = r13.x;
	r5.xyz = -r5.xyz * r4.xxx + r5.xyz;
	r6.w = -r5.w + 1;
	r13 = 1 + -r4.y;
	r4.z = r13.x;
	r4.w = r6.w;
	r4.zw = r4.zw * r4.xx;
	r13 = -r2.z;
	r5.w = r5.w + r4.w;
	r13 = r4.x * r13.x;
	r4.y = r13.x;
	r1.zw = r4.yz + r2.zw;
	r4 = r1 + -r5;
	o = r4 * 0.8 + r5;

	return o;
}

struct ps_board_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_board_P0(ps_board_P0_PS_IN i) : COLOR
{
	float4 o;

	float3 r0;
	float2 r1;
	float2 r2;
	float2 r3;
	float4 r4;
	float4 r5;
	float4 r8;
	r0 = i.normal.xyz;
	r1 = i.texcoord.xy;
	r2 = i.texcoord1.xy;
	r3 = i.texcoord2.xy;
	r4.x = i.fog.x;
	r5.x = i.fog1.x;
	r8 = tex2D(roomcolor, r1.xy);
	r5 = r8;
	r4.yzw = r5.xyz * lightColor.xyz;
	r1.x = 1 + -r5.w;
	o.w = r1.x * r4.x + r5.w;
	o.xyz = -r4.yzw * r4.xxx + r4.yzw;

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

technique flint
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_flint_P0();
		PixelShader = compile ps_3_0 ps_flint_P0();
	}
}

technique board
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_insideDark_P0();
		PixelShader = compile ps_3_0 ps_board_P0();
	}
}
