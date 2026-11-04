float4x4 world;
float4x4 view;
float4x4 projection;
float tail;
float scale;
texture rgbtexture;

sampler TextureSampler = sampler_state
{
	Texture = <rgbtexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct vs_straight_P0_VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
};

struct vs_straight_P0_VS_OUT
{
	float2 texcoord : TEXCOORD;
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
	r0.xy = i.texcoord.xy;
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
	o.texcoord = r0.xy;

	return o;
}

float4 ps_straight_P0(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r0;
	float4 r3;
	r0.xy = texcoord.xy;
	r0.z = r0.y * scale.x + tail.x;
	r3 = tex2D(TextureSampler, r0.xz);
	r0 = r3;
	o = r0;

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
