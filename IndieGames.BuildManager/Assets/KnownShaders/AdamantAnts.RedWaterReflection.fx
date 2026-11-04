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
	float4 r2;
	float4 r0;
	float4 r5;
	float r6;
	float r10;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r5 = tex2D(g_samSrcColor, r0.xy);
	r1.xyw = r5.xyz;
	r0.w = waterline.x + -0.5;
	r0.z = (r0.w < r0.y) ? 1 : 0;
	r6 = r0.z;
	if (r6.x != 0) {
		r2.x = (waterline.x < r0.y) ? 1 : 0;
		r10 = -waterline.x + r0.y;
		r0.z = r10.x;
		r0.z = -r0.z * 1.1 + waterline.x;
		r5 = tex2D(g_samSrcColor, r0.xz);
		r0.z = r5.x;
		r0.x = -r0.w + r0.y;
		r0.xy = r0.xx * float2(5, 3.5) + 1;
		r1.z = r0.z * 6 + r1.x;
		r0.xy = r0.xy * r1.xy;
		r1.xy = (-abs(r2.xx) >= 0) ? r0.xy : r1.zy;
	}
	o.xyz = r1.xyw;
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
