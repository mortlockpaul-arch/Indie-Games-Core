texture g_txSrcColor;

sampler2D g_samSrcColor = sampler_state
{
};

float rnum = 1;
float sinslow = 0.01;
float timeraslow;
float waterline = 0.8;

float4 ps_PostProcess_p1(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r0;
	float r5;
	float r9;
	float4 r4;
	float3 r2;
	r1 = 0;
	r0.xy = texcoord.xy;
	r0.z = (waterline.x < r0.y) ? 1 : 0;
	r5 = r0.z;
	if (r5.x != 0) {
		r0.z = waterline.x + 1;
		r9 = -waterline.x;
		r0.z = r0.z + -r0.y;
		r9 = -0.02 + r9.x;
		r0.w = r9.x;
		r0.w = r0.w + r0.y;
		r1.xz = r0.zw * float2(1.8, 5);
		r9 = -waterline.x + r0.y;
		r0.y = r9.x;
		r0.y = -r1.x * r0.y + waterline.x;
		r4 = tex2D(g_samSrcColor, r0.xy);
		r0.xzw = r4.xyz;
		r1.y = -r1.z + 1;
		r9 = 1.1 * r0.x;
		r1.x = r9.x;
		r0.y = -r1.z * 0.5 + 0.5;
		r0.xyz = r1.xyy * r0.ywz;
		r9 = 0.55 * r0.z;
		r0.w = r9.x;
		r2.xz = r0.xw + float2(0.055000003, 0.11000001);
		r9 = 0.55 * r0.y;
		r0.y = r9.x;
		r0.xz = r2.xz;
	} else {
		r4 = tex2D(g_samSrcColor, r0.xy);
		r0.xyz = r4.xzy;
	}
	o.xyz = r0.xzy;
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
