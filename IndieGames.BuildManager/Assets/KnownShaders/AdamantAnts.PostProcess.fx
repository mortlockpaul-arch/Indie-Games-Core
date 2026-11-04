sampler ColorMapSampler = sampler_state
{
};

float fTimer;

float4 ps_PostProcess_P0(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r0;
	float2 r2;
	float4 r3;
	r0.xy = texcoord.xy;
	r0.zw = r0.xy * 10 + fTimer.xx;
	r0.zw = r0.zw * 0.15915494 + 0.5;
	r0.zw = frac(r0.zw);
	r0.zw = r0.zw * 6.2831855 + -3.1415927;
	r2.y = sin(r0.z);
	r0.z = r2.y;
	r2.x = cos(r0.w);
	r0.w = r2.x;
	r0.xy = r0.zw * 0.01 + r0.xy;
	r3 = tex2D(ColorMapSampler, r0.xy);
	r0 = r3;
	o = r0;

	return o;
}

technique PostProcess
{
	pass P0
	{
		PixelShader = compile ps_3_0 ps_PostProcess_P0();
	}
}
