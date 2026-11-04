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
	float4 r2;
	float4 r0;
	float3 r3;
	float r10;
	float2 r4;
	float4 r5;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r3.z = -waterline.x + -0.02;
	r10 = r0.x;
	r0.z = r3.z;
	r1.x = r0.y + -waterline.x;
	r10 = timeraslow.x + r10.x;
	r1.z = r10.x;
	r0.w = waterline.x * 2 + 2;
	r2.y = -r0.y * 2 + r0.w;
	r3.y = r0.z + r0.y;
	r10 = 250 * r1.x;
	r1.w = r10.x;
	r1.y = r3.y;
	r0.zw = r1.zy + r1.wy;
	r0.z = r0.z * 0.15915494 + 0.5;
	r1.w = -r0.w + 1;
	r10 = frac(r0.z);
	r0.z = r10.x;
	r0.z = r0.z * 6.2831855 + -3.1415927;
	r1.y = r0.x + -0.5;
	r4.y = sin(r0.z);
	r10 = r4.y;
	r1.z = r10.x;
	r2.xz = r1.zx * float2(0.05, 2.8);
	r1.xyz = r2.xyz * r1.xxy;
	r0.z = -r1.z + r0.x;
	r10 = waterline.x + -r1.y;
	r0.w = r10.x;
	r1.xy = r0.zw + r1.xx;
	r5 = tex2D(g_samSrcColor, r0.xy);
	r0.xzw = r5.xyz;
	r5 = tex2D(g_samSrcColor, r1.xy);
	r1.xyz = r5.xyz;
	r0.y = (waterline.x < r0.y) ? 1 : 0;
	r1.xyz = r1.www * r1.xyz;
	r1.xyz = r1.zyx * 1.3 + float3(-0.117, -0.065, -0.13);
	o.xyz = (-abs(r0.yyy) >= 0) ? r0.xzw : r1.zyx;
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
