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
	float r10;
	float4 r3;
	float2 r4;
	float4 r5;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r0.w = r0.x + timeraslow.x;
	r10 = -waterline.x;
	r0.z = r0.y + -waterline.x;
	r10 = -0.02 + r10.x;
	r1.x = r10.x;
	r1.y = waterline.x + 1;
	r10 = r0.x;
	r1.zw = r0.zz * float2(150, 1.8);
	r10 = -0.5 + r10.x;
	r2.x = r10.x;
	r2.x = -r1.w * r2.x + r0.x;
	r0.w = r0.w + r1.z;
	r10 = r1.y;
	r0.w = r0.w * 0.15915494 + 0.5;
	r3.w = frac(r0.w);
	r10 = -r0.y + r10.x;
	r1.y = r10.x;
	r0.w = r3.w;
	r0.w = r0.w * 6.2831855 + -3.1415927;
	r1.z = r1.x + r0.y;
	r4.y = sin(r0.w);
	r10 = r4.y;
	r1.x = r10.x;
	r1.xyz = r1.zxy * float3(3, 0.05, 5);
	r0.zw = r1.yz * r0.zz;
	r10 = 1 + -r1.x;
	r1.w = r10.x;
	r2.y = -r0.w * waterline.x + waterline.x;
	r0.zw = r2.xy + r0.zz;
	r5 = tex2D(g_samSrcColor, r0.zw);
	r1.xyz = r5.xyz;
	r5 = tex2D(g_samSrcColor, r0.xy);
	r0.xzw = r5.xyz;
	r0.y = (waterline.x < r0.y) ? 1 : 0;
	r1.xyz = r1.www * r1.zyx + r0.wzx;
	r1.xyz = r1.xyz * 0.5;
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
