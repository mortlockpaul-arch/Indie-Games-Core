float val;

sampler samplerState = sampler_state
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Mirror;
	AddressV = Mirror;
};

texture ColorMap;

sampler colormap = sampler_state
{
	Texture = <ColorMap>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
};

float4 ps_normalfx_P0(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r0;
	float4 r6;
	float4 r5;
	float r7;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r0.xy = texcoord.xy;
	r6 = tex2D(samplerState, r0.xy);
	r2 = r6.wzyx;
	r6 = tex2D(colormap, r0.xy);
	r3.xyz = r6.yzx;
	r5 = (r3.z >= 1) ? 1 : 0;
	r6 = (1 >= r3.z) ? 1 : 0;
	r0.z = r5.z * r6.z;
	r7 = r0.z;
	if (r7.x != 0) {
		r0.zw = val.xx * float2(0.01, 0.03) + r0.xy;
		r6 = tex2D(samplerState, r0.zw);
		r2 = r6.wzyx;
	}
	r0.zw = val.xx * float2(-0.022, 0.04) + r0.xy;
	r6 = tex2D(samplerState, r0.zw);
	r1 = r6;
	r5 = (r3.xxxy >= 1) ? 1 : 0;
	r6 = (1 >= r3.xxxy) ? 1 : 0;
	r0.zw = r5.zw * r6.zw;
	r1 = (-abs(r0.z) >= 0) ? r2.wzyx : r1;
	r7 = r0.w;
	if (r7.x != 0) {
		r0.xy = val.xx * float2(0.035, -0.02) + r0.xy;
		r6 = tex2D(samplerState, r0.xy);
		r1 = r6;
	}
	o = r1;

	return o;
}

technique normalfx
{
	pass P0
	{
		PixelShader = compile ps_3_0 ps_normalfx_P0();
	}
}
