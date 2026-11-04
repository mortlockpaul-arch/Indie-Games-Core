float bs = -800;
float be = 900;
float Trans;
float Brite;
float4x4 world;
float4x4 view;
float4x4 projection;
float4 uvA;
float4 uvB;
float3 ghostpos;
texture rgbTexture1;

sampler rgb1 = sampler_state
{
	Texture = <rgbTexture1>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

struct vs_straight_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_straight_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog1 : FOG1;
	float4 position : POSITION;
};

vs_straight_P0_VS_OUT vs_straight_P0(vs_straight_P0_VS_IN i)
{
	vs_straight_P0_VS_OUT o;

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
	r0.xz = i.texcoord.xy;
	r1 = transpose(view)[3] * transpose(projection)[0].w;
	r3 = transpose(view)[3] * transpose(projection)[1].w;
	r4 = transpose(view)[3] * transpose(projection)[2].w;
	r5 = transpose(view)[3] * transpose(projection)[3].w;
	r5 = transpose(view)[2].xywz * transpose(projection)[3].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[2].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[1].z + r3.xywz;
	r1 = transpose(view)[2].xywz * transpose(projection)[0].z + r1.xywz;
	r1 = transpose(view)[1].xwyz * transpose(projection)[0].y + r1.xzyw;
	r3 = transpose(view)[1].xwyz * transpose(projection)[1].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[2].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[3].y + r5.xzyw;
	r9 = transpose(view)[0].xwyz * transpose(projection)[3].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[2].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[1].x + r3;
	r6 = transpose(view)[0].xwyz * transpose(projection)[0].x + r1;
	r5 = r6.y * transpose(world)[3].xzyw;
	r4 = r7.y * transpose(world)[3].xzyw;
	r3 = r8.y * transpose(world)[3].xzyw;
	r1 = r9.y * transpose(world)[3].xzyw;
	r1 = r9.w * transpose(world)[2] + r1.xzyw;
	r3 = r8.w * transpose(world)[2] + r3.xzyw;
	r4 = r7.w * transpose(world)[2] + r4.xzyw;
	r5 = r6.w * transpose(world)[2] + r5.xzyw;
	r5 = r6.z * transpose(world)[1] + r5;
	r4 = r7.z * transpose(world)[1] + r4;
	r3 = r8.z * transpose(world)[1] + r3;
	r1 = r9.z * transpose(world)[1] + r1;
	r1 = r9.x * transpose(world)[0] + r1;
	r3 = r8.x * transpose(world)[0] + r3;
	r4 = r7.x * transpose(world)[0] + r4;
	r5 = r6.x * transpose(world)[0] + r5;
	o.position.x = dot(r5.zxyw, r2.zxyw);
	o.position.y = dot(r4.zxyw, r2.zxyw);
	o.position.z = dot(r3.zxyw, r2.zxyw);
	o.position.w = dot(r1.zxyw, r2.zxyw);
	r1.y = dot(transpose(world)[0].zxyw, r2.zxyw);
	r1.z = dot(transpose(world)[1].zxyw, r2.zxyw);
	r1.w = dot(transpose(world)[2].zxyw, r2.zxyw);
	r1.x = 1 / uvB.x;
	r2.xyz = r1.yzw + -ghostpos.xyz;
	r1.y = 1 / uvB.z;
	r0.y = dot(r2.zxy, r2.zxy);
	r1.z = 1 / uvA.x;
	r11 = rsqrt(abs(r0.y));
	r0.y = 1 / r11.x;
	r0.w = r0.y + bs.x;
	r0.y = 1 / be.x;
	r0.y = saturate(r0.w * r0.y);
	r1.w = 1 / uvA.z;
	o.fog1 = 1 + -r0.y;
	o.texcoord = r1.zw * r0.xz + uvA.yw;
	o.texcoord1 = r1.xy * r0.xz + uvB.yw;

	return o;
}

struct ps_straight_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog1 : FOG1;
};

float4 ps_straight_P0(ps_straight_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float r2;
	float4 r5;
	float3 r3;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.fog1.x;
	r5 = tex2D(rgb1, r1.xy);
	r1 = r5;
	r5 = tex2D(rgb1, r0.xy);
	r0 = r5;
	r1 = r1 + -r0;
	r0 = r1 * Trans.x + r0;
	r3 = r0.xyz * Brite.xxx;
	r1.x = 1 + -r0.w;
	r0.xyz = r3.xyz;
	o.w = r1.x * r2.x + r0.w;
	o.xyz = -r0.xyz * r2.xxx + r0.xyz;

	return o;
}

struct ps_red_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog1 : FOG1;
};

float4 ps_red_P0(ps_red_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float r2;
	float4 r5;
	float r10;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.fog1.x;
	r5 = tex2D(rgb1, r1.xy);
	r1 = r5;
	r5 = tex2D(rgb1, r0.xy);
	r0 = r5;
	r1 = r1.xwyz + -r0.xwyz;
	r1 = r1 * Trans.x + r0.xwyz;
	r0.x = -r1.x * r2.x;
	r10 = 1 + -r1.y;
	r0.y = r10.x;
	r1 = r1.zwxy * float4(0.2, 0.1, 1, 1);
	r10 = r0.y;
	r0.yz = -r1.xy * r2.xx;
	r10 = r2.x * r10.x;
	r0.w = r10.x;
	o = r1.zxyw + r0;

	return o;
}

technique straight
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_straight_P0();
		PixelShader = compile ps_3_0 ps_straight_P0();
	}
}

technique red
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_straight_P0();
		PixelShader = compile ps_3_0 ps_red_P0();
	}
}
