float4x4 world;
float4x4 view;
float4x4 projection;
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

texture rgbTexture2;

sampler rgb2 = sampler_state
{
	Texture = <rgbTexture2>;
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
	float4 texcoord1 : TEXCOORD1;
};

struct vs_straight_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
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
	r1.xy = i.texcoord.xy;
	r0.yz = i.texcoord1.xy;
	r3 = transpose(view)[3] * transpose(projection)[0].w;
	r4 = transpose(view)[3] * transpose(projection)[1].w;
	r5 = transpose(view)[3] * transpose(projection)[2].w;
	r6 = transpose(view)[3] * transpose(projection)[3].w;
	r6 = transpose(view)[2].xywz * transpose(projection)[3].z + r6.xywz;
	r5 = transpose(view)[2].xywz * transpose(projection)[2].z + r5.xywz;
	r4 = transpose(view)[2].xywz * transpose(projection)[1].z + r4.xywz;
	r3 = transpose(view)[2].xywz * transpose(projection)[0].z + r3.xywz;
	r3 = transpose(view)[1].xwyz * transpose(projection)[0].y + r3.xzyw;
	r4 = transpose(view)[1].xwyz * transpose(projection)[1].y + r4.xzyw;
	r5 = transpose(view)[1].xwyz * transpose(projection)[2].y + r5.xzyw;
	r6 = transpose(view)[1].xwyz * transpose(projection)[3].y + r6.xzyw;
	r10 = transpose(view)[0].xwyz * transpose(projection)[3].x + r6;
	r9 = transpose(view)[0].xwyz * transpose(projection)[2].x + r5;
	r8 = transpose(view)[0].xwyz * transpose(projection)[1].x + r4;
	r7 = transpose(view)[0].xwyz * transpose(projection)[0].x + r3;
	r6 = r7.y * transpose(world)[3].xzyw;
	r5 = r8.y * transpose(world)[3].xzyw;
	r4 = r9.y * transpose(world)[3].xzyw;
	r3 = r10.y * transpose(world)[3].xzyw;
	r3 = r10.w * transpose(world)[2] + r3.xzyw;
	r4 = r9.w * transpose(world)[2] + r4.xzyw;
	r5 = r8.w * transpose(world)[2] + r5.xzyw;
	r6 = r7.w * transpose(world)[2] + r6.xzyw;
	r6 = r7.z * transpose(world)[1] + r6;
	r5 = r8.z * transpose(world)[1] + r5;
	r4 = r9.z * transpose(world)[1] + r4;
	r3 = r10.z * transpose(world)[1] + r3;
	r3 = r10.x * transpose(world)[0] + r3;
	r4 = r9.x * transpose(world)[0] + r4;
	r5 = r8.x * transpose(world)[0] + r5;
	r6 = r7.x * transpose(world)[0] + r6;
	o.position.x = dot(r6.zxyw, r2.zxyw);
	o.position.y = dot(r5.zxyw, r2.zxyw);
	o.position.z = dot(r4.zxyw, r2.zxyw);
	o.position.w = dot(r3.zxyw, r2.zxyw);
	r0.x = r2.y * 0.0002 + -0.39999998;
	o.fog = saturate(1 + -r0.x);
	o.texcoord = r1.xy;
	o.texcoord1 = r0.yz;

	return o;
}

struct ps_straight_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
};

float4 ps_straight_P0(ps_straight_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float r2;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1.xy = i.texcoord1.xy;
	r2 = i.fog.x;
	r5 = tex2D(rgb2, r1.xy);
	r1 = r5;
	r5 = tex2D(rgb1, r0.xy);
	r0 = r5;
	r0 = r1 * 0.9 + r0;
	r1 = -r0 + float4(0.32, 0.01, 0.01, 1);
	o = r1 * r2.x + r0;

	return o;
}

struct ps_multiply_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
};

float4 ps_multiply_P0(ps_multiply_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float4 r2;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2.x = i.fog.x;
	r5 = tex2D(rgb1, r0.xy);
	r2 = r5;
	r5 = tex2D(rgb2, r1.xy);
	r0 = r5;
	o = r2 * r0;

	return o;
}

struct ps_flash_P0_PS_IN
{
	float2 texcoord : TEXCOORD;
	float2 texcoord1 : TEXCOORD1;
	float fog : FOG;
};

float4 ps_flash_P0(ps_flash_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float2 r1;
	float r2;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xy;
	r2 = i.fog.x;
	r5 = tex2D(rgb2, r1.xy);
	r0 = r5;
	o = r0 * float4(1, 0.7, 0.6, 1);

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

technique multiply
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_straight_P0();
		PixelShader = compile ps_3_0 ps_multiply_P0();
	}
}

technique flash
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_straight_P0();
		PixelShader = compile ps_3_0 ps_flash_P0();
	}
}
