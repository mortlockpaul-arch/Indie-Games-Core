sampler TextureSampler = sampler_state
{
};

float2 Center = float2(0.5, 0.5);
float BlurStart = 1;
float BlurWidth = -0.1;

float4 ps_AdvancedRadialBlur_p0(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r0;
	float3 r3;
	float4 r5;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r1 = (0 < -abs(r0.x)) ? 1 : 0;
	r3.yz = r0.xy + -Center.xy;
	r0.x = (abs(r0.x) >= 0) ? 0 : 1;
	r0.yz = r3.yz;
	for (int _trip0 = 0, i0 = 0; _trip0 < 16; _trip0++, i0 += 0) {
		r0.w = 0.06666667 * r0.x;
		r0.w = r0.w * BlurWidth.x + BlurStart.x;
		r2.xy = r0.yz * r0.ww + Center.xy;
		r5 = tex2D(TextureSampler, r2.xy);
		r2 = r5;
		r1 = r2 + r1;
		r0.x = 1 + r0.x;
	}
	o = r1 * 0.0625;

	return o;
}

technique AdvancedRadialBlur
{
	pass p0
	{
		CullMode = None;
		ZEnable = FALSE;
		PixelShader = compile ps_3_0 ps_AdvancedRadialBlur_p0();
	}
}
