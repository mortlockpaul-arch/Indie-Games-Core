texture g_txSrcColor;

sampler2D g_samSrcColor = sampler_state
{
};

float rnum = 1;
float sinslow = 0.01;
float timeraslow;
float waterline = 0.6;

float4 ps_PostProcess_p1(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float3 r0;
	float r5;
	float4 r4;
	float r9;
	r1 = 0;
	r0.xy = texcoord.xy;
	r0.z = (waterline.x < r0.y) ? 1 : 0;
	r5 = r0.z;
	r4 = tex2D(g_samSrcColor, r0.xy);
	r1.xyz = r4.zyx;
	if (r5.x != 0) {
		r9 = -waterline.x + r0.y;
		r0.y = r9.x;
		r0.y = -r0.y * 1.1 + waterline.x;
		r4 = tex2D(g_samSrcColor, r0.xy);
		r0 = r4.xyz;
		r0 = r1.xyz + r0.zyx;
		r1.xyz = r0.xyz * 0.5;
	}
	o.xyz = r1.zyx;
	o.w = 1;

	return o;
}

technique PostProcess
{
	pass p1
	{
		PixelShader = compile ps_3_0 ps_PostProcess_p1();
		ZEnable = FALSE;
	}
}
