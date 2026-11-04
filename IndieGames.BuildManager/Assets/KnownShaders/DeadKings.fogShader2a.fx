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
	r2 = transpose(view)[3] * transpose(projection)[0].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[0].z + r2.xywz;
	r2 = transpose(view)[1].xwyz * transpose(projection)[0].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r2;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r2 = r9.y * transpose(world)[3].xzyw;
	r2 = r9.w * transpose(world)[2] + r2.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r3 = r8.z * transpose(world)[1] + r3;
	r2 = r9.z * transpose(world)[1] + r2;
	r2 = r9.x * transpose(world)[0] + r2;
	r3 = r8.x * transpose(world)[0] + r3;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r1.zxyw);
	o.position.y = dot(r4.zxyw, r1.zxyw);
	o.position.z = dot(r3.zxyw, r1.zxyw);
	o.position.w = dot(r2.zxyw, r1.zxyw);
	r2.x = dot(transpose(world)[0].zxyw, r1.zxyw);
	r2.y = dot(transpose(world)[1].zxyw, r1.zxyw);
	r2.z = dot(transpose(world)[2].zxyw, r1.zxyw);
	r3.xyz = r2.xyz + -ghostpos.xyz;
	r1.yzw = r2.xyz + -campos.xyz;
	r1.x = 1 / uvB.x;
	r0.w = dot(r1.wyz, r1.wyz);
	r1.y = 1 / uvB.z;
	r10.y = dot(r3.zxy, r3.zxy);
	r11 = rsqrt(abs(r0.w));
	r0.w = 1 / r11.x;
	r0.y = r10.y;
	r1.z = r0.w + fs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r0.w = r0.y + bs.x;
	r0.y = 1 / be.x;
	r0.y = saturate(r0.w * r0.y);
	r0.w = 1 / fe.x;
	r0.w = r1.z * r0.w;
	r1.z = 1 / uvA.x;
	r0.w = max(r0.w, 0.2);
	r1.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.fog = min(r0.w, 1);
	o.normal = r2.xyz + -campos.xyz;
	o.texcoord = r1.zw * r0.xz + uvA.yw;
	o.texcoord1 = r1.xy * r0.xz + uvB.yw;

	return o;
}

struct ps_insideDark_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
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
	r0.xyz = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2.xy = i.texcoord1.xy;
	r3.x = i.fog.x;
	r4 = i.fog1.x;
	r9 = tex2D(roomcolor, r1.xy);
	r1 = r9.yzwx;
	r9 = tex2D(roomcolor, r2.xy);
	r5 = r9;
	r3.y = dot(r0.zxy, r0.zxy);
	r1.xyz = r1.zxy * r5.wyz;
	r2 = r5.xyzx * float4(1.2, 1.1, 0.8, 1);
	r14 = 1 + -r1.x;
	r3.w = r14.x;
	r0 = r2.wxyz * lightColor.xxyz;
	r8 = rsqrt(abs(r3.y));
	r14 = 1 / r8.x;
	r3.y = r14.x;
	r3.yz = saturate(r3.yy * float2(0.008264462, 0.0016528926) + -0.9917355);
	r6.x = r0.x * r1.w;
	r14 = r3.z + -r3.y;
	r1.w = r14.x;
	r2.w = r3.w * r3.x + r1.x;
	r6.yzw = r1.yzw * lightColor.yzw;
	r3.y = r3.y + r6.w;
	r14 = -r2.w;
	r2.xyz = -r6.xzy * r3.xxx + r6.xzy;
	r3.y = -r3.y + 1;
	r14 = r5.w + r14.x;
	r1.w = r14.x;
	r1.xyz = -r2.xzy + r0.yzw;
	r14 = off.x * r3.y;
	r3.x = r14.x;
	r1 = r3.x * r1 + r2.xzyw;
	r3.w = -r1.w + 1;
	r3.xyz = -r1.xzy * r4.xxx;
	r3.w = r3.w * r4.x;
	o = r1 + r3.xzyw;

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
	r2 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r4 = r8.y * transpose(world)[3].xzyw;
	r3 = r9.y * transpose(world)[3].xzyw;
	r3 = r9.w * transpose(world)[2] + r3.xzyw;
	r4 = r8.w * transpose(world)[2] + r4.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r2 = r6.w * transpose(world)[2] + r2.xzyw;
	r2 = r6.z * transpose(world)[1] + r2;
	r5 = r7.z * transpose(world)[1] + r5;
	r4 = r8.z * transpose(world)[1] + r4;
	r3 = r9.z * transpose(world)[1] + r3;
	r3 = r9.x * transpose(world)[0] + r3;
	r4 = r8.x * transpose(world)[0] + r4;
	r5 = r7.x * transpose(world)[0] + r5;
	r2 = r6.x * transpose(world)[0] + r2;
	r2.w = dot(r2.zxyw, r1.zxyw);
	r2.z = dot(r5.zxyw, r1.zxyw);
	r2.y = dot(r4.zxyw, r1.zxyw);
	r2.x = dot(r3.zxyw, r1.zxyw);
	o.position = r2;
	r4.x = dot(transpose(world)[0].zxyw, r1.zxyw);
	r4.y = dot(transpose(world)[1].zxyw, r1.zxyw);
	r4.z = dot(transpose(world)[2].zxyw, r1.zxyw);
	r1.x = 1 / uvB.x;
	r3.xyw = r4.xyz + -campos.xyz;
	r1.y = 1 / uvB.z;
	r4.xyz = r4.xyz + -ghostpos.xyz;
	r1.z = 1 / uvA.x;
	r0.w = dot(r4.zxy, r4.zxy);
	r3.z = 1 / be.x;
	r10.y = dot(r3.wxy, r3.wxy);
	r11 = rsqrt(abs(r0.w));
	r0.w = 1 / r11.x;
	r0.y = r10.y;
	r3.x = r0.w + bs.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r3.y = r0.y + ds.x;
	r3.w = 1 / de.x;
	r0.y = saturate(r3.x * r3.z);
	r1.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.fog = saturate(r3.y * r3.w);
	o.normal = r2.xyz;
	o.texcoord = r1.zw * r0.xz + uvA.yw;
	o.texcoord1 = r1.xy * r0.xz + uvB.yw;

	return o;
}

struct ps_darkBlue_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_darkBlue_P0(ps_darkBlue_P0_PS_IN i) : COLOR
{
	float4 o;

	float3 r0;
	float4 r1;
	float2 r2;
	float4 r3;
	float4 r4;
	float4 r7;
	float r12;
	float4 r5;
	r0 = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2 = i.texcoord1.xy;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r7 = tex2D(roomcolor, r2.xy);
	r1 = r7;
	r3.z = dot(r1.zxy, 0.33333334);
	r12 = 0.2 * r1.w;
	r3.y = r12.x;
	r5.zw = r3.zz * float2(0.2, 0.26);
	r12 = 1 + -r3.y;
	r4.y = r12.x;
	r3.zw = r5.zw;
	r5.zw = -r3.zw * r3.xx;
	r12 = r4.y;
	r4.zw = r5.zw;
	r1.x = r3.w + r4.w;
	r12 = r3.x * r12.x;
	r4.y = r12.x;
	o.z = -r1.x * r4.x + r1.x;
	r3.xz = r3.yz + r4.yz;
	r5.w = -r3.z * r4.x;
	r12 = 1 + -r3.x;
	r3.y = r12.x;
	r3.w = r5.w;
	r3.y = r3.y * r4.x;
	o.xyw = r3.zzx + r3.wwy;

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
	r0.yz = i.texcoord.xy;
	r1 = transpose(view)[3] * transpose(projection)[3].w;
	r2 = transpose(view)[3] * transpose(projection)[2].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[0].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[0].z + r5.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r2 = transpose(view)[2].xywz * transpose(projection)[2].z + r2.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[3].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[3].y + r1.xzyw;
	r2 = transpose(view)[1].xwyz * transpose(projection)[2].y + r2.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[0].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[0].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r7 = transpose(view)[0].xwyz * transpose(projection)[2].x + r2;
	r6 = transpose(view)[0].xwyz * transpose(projection)[3].x + r1;
	r2 = r6.y * transpose(world)[3].xzyw;
	r5 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r1 = r9.y * transpose(world)[3].xzyw;
	r1 = r9.w * transpose(world)[2] + r1.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r5 = r7.w * transpose(world)[2] + r5.xzyw;
	r2 = r6.w * transpose(world)[2] + r2.xzyw;
	r2 = r6.z * transpose(world)[1] + r2;
	r5 = r7.z * transpose(world)[1] + r5;
	r3 = r8.z * transpose(world)[1] + r3;
	r1 = r9.z * transpose(world)[1] + r1;
	r1 = r9.x * transpose(world)[0] + r1;
	r3 = r8.x * transpose(world)[0] + r3;
	r5 = r7.x * transpose(world)[0] + r5;
	r2 = r6.x * transpose(world)[0] + r2;
	r2.w = dot(r2.zxyw, r4.zxyw);
	r2.z = dot(r5.zxyw, r4.zxyw);
	r2.y = dot(r3.zxyw, r4.zxyw);
	r2.x = dot(r1.zxyw, r4.zxyw);
	o.position = r2;
	r3.x = dot(transpose(world)[0].zxyw, r4.zxyw);
	r3.y = dot(transpose(world)[1].zxyw, r4.zxyw);
	r1.x = 1 / uvB.x;
	r3.z = dot(transpose(world)[2].zxyw, r4.zxyw);
	r1.y = 1 / uvB.z;
	r3.xyz = r3.xyz + -campos.xyz;
	r1.z = 1 / uvA.x;
	r0.x = dot(r3.zxy, r3.zxy);
	r1.w = 1 / uvA.z;
	r11 = rsqrt(abs(r0.x));
	r0.x = 1 / r11.x;
	o.fog1 = (abs(r0.x) >= 0) ? 0 : 1;
	o.normal = r2.xyz;
	o.texcoord = r1.zw * r0.yz + uvA.yw;
	o.texcoord1 = r1.xy * r0.yz + uvB.yw;
	o.fog = saturate(r0.x * 0.0009090909 + -0.27272728);

	return o;
}

struct ps_flint_P0_PS_IN
{
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
	float fog1 : FOG1;
};

float4 ps_flint_P0(ps_flint_P0_PS_IN i) : COLOR
{
	float4 o;

	float3 r0;
	float4 r1;
	float2 r2;
	float4 r3;
	float4 r4;
	float4 r7;
	float r12;
	float4 r5;
	r0 = i.normal.xyz;
	r1.xy = i.texcoord.xy;
	r2 = i.texcoord1.xy;
	r3.x = i.fog.x;
	r4.x = i.fog1.x;
	r7 = tex2D(roomcolor, r1.xy);
	r4 = r7;
	r7 = tex2D(roomcolor, r2.xy);
	r1 = r7;
	r3.zw = r1.xy * float2(1.8, 1.6);
	r4 = r4.xzyw * r1.xzyw;
	r12 = r1.w;
	r3.y = r12.x;
	r1.xy = -r3.zw * r3.xx + r3.zw;
	r4.xyz = -r4.xzy * r3.xxx + r4.xzy;
	r5.w = -r4.w + 1;
	r12 = 1 + -r3.y;
	r3.z = r12.x;
	r3.w = r5.w;
	r3.zw = r3.zw * r3.xx;
	r12 = -r1.z;
	r4.w = r4.w + r3.w;
	r12 = r3.x * r12.x;
	r3.y = r12.x;
	r1.zw = r3.yz + r1.zw;
	r3 = r1 + -r4;
	o = r3 * 0.8 + r4;

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
