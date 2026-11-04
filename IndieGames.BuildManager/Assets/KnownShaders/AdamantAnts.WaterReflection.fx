texture g_txSrcColor;

sampler2D g_samSrcColor = sampler_state
{
};

float rnum = 1;
float sinslow = 0.01;
float timeraslow;
float waterline = 0.7;

float4 ps_PostProcess_p1(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r0;
	float r6;
	float4 r3;
	float r10;
	float2 r4;
	float4 r5;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r0.z = (waterline.x < r0.y) ? 1 : 0;
	r6 = r0.z;
	if (r6.x != 0) {
		r3.w = -waterline.x + -0.02;
		r10 = -waterline.x + r0.y;
		r1.x = r10.x;
		r0.w = r3.w;
		r1.yz = r1.xx * float2(2.1, 300);
		r10 = -0.5 + r0.x;
		r0.z = r10.x;
		r1.y = -r1.y * r0.z + r0.x;
		r0.x = r1.z + timeraslow.x;
		r10 = waterline.x;
		r0.z = timeraslow.x * 0.5 + r1.z;
		r0.xz = r0.xz * 0.15915494 + 0.5;
		r0.xz = frac(r0.xz);
		r10 = 1 + r10.x;
		r1.z = r10.x;
		r0.xz = r0.xz * 6.2831855 + -3.1415927;
		r2.z = r1.z + -r0.y;
		r4.y = sin(r0.x);
		r10 = r4.y;
		r2.x = r10.x;
		r2.w = r0.w + r0.y;
		r4.x = cos(r0.z);
		r10 = r4.x;
		r2.y = r10.x;
		r0 = r2 * float4(0.049999997, 0.099999994, 3.8, 5);
		r0.xyz = r0.xzy * r1.xxx;
		r1.x = -r0.w + 1;
		r10 = waterline.x + -r0.y;
		r1.z = r10.x;
		r1.yz = r1.yz + r0.xz;
		r5 = tex2D(g_samSrcColor, r1.yz);
		r0.xyz = r5.xyz;
		r1.y = -r0.w * 1.1 + 1.1;
		r0.xyz = r1.xxy * r0.xyz;
		r10 = 1.1 * r0.x;
		r0.x = r10.x;
		r0.yz = r0.yz * float2(1.1, 1) + float2(0.022, 0.044);
	} else {
		r5 = tex2D(g_samSrcColor, r0.xy);
		r0.xyz = r5.xyz;
	}
	o.xyz = r0.xyz;
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
