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

	float4 r0;
	float r4;
	float4 r3;
	r0.xy = texcoord.xy;
	r0.z = (waterline.x < r0.y) ? 1 : 0;
	r4 = r0.z;
	if (r4.x != 0) {
		r0.y = -waterline.x + r0.y;
		r0.z = 1.1 * r0.y;
		r0.y = waterline.x + -r0.z;
		r3 = tex2D(g_samSrcColor, r0.xy);
		r0.xyw = r3.xyz;
		r0.xyz = -r0.zzz * 0.6 + r0.yxw;
		r0.w = r0.z * 0.7;
		r0.z = 0.8;
		r0.xy = r0.yx * 0.7 + float2(0.0175, 0.07);
	} else {
		r0.z = 1;
		r3 = tex2D(g_samSrcColor, r0.xy);
		r0.xyw = r3.xyz;
	}
	o = r0.xywz * 1.3 + -0.05;

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
