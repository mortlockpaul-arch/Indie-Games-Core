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
	float4 r3;
	float4 r0;
	float r7;
	float r11;
	float4 r6;
	float2 r4;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r0.xy = texcoord.xy;
	r0.z = (waterline.x < r0.y) ? 1 : 0;
	r7 = r0.z;
	if (r7.x != 0) {
		r11 = -waterline.x + r0.y;
		r0.y = r11.x;
		r11 = 1.2 * r0.y;
		r0.z = r11.x;
		r11 = waterline.x + -r0.z;
		r0.y = r11.x;
		r6 = tex2D(g_samSrcColor, r0.xy);
		r0.xyw = r6.xyz;
		r3.xyz = r0.wyx * 1.5;
		r2.xyz = r0.wyx + 0.7;
		r1.x = r2.x * r0.w;
		r1.w = dot(r3.zxzy, r2.zxzy);
		r11 = r3.y;
		r0.w = -r0.z * 0.4;
		r11 = r2.y * r11.x;
		r0.z = r11.x;
		r1.yz = r1.ww * 0.2 + r0.zw;
		r0.x = dot(r3.yx, r2.yx) + r1.y;
		r2.xyz = (float3(-0, -1, -1) >= 0) ? r0.xxx : r1.xxw;
		r0.y = dot(r2.yzx, float3(3, 0.2, 0.2));
		r0.xy = r0.xy * 0.2;
		r1.x = r0.x + r0.w;
		r11 = r0.y;
		r4 = r1.zx + float2(-0.05, -0.02);
		r11 = r0.w + r11.x;
		r0.z = r11.x;
		r0.xy = r4.xy;
	} else {
		r6 = tex2D(g_samSrcColor, r0.xy);
		r0.xyz = r6.xyz;
	}
	o.xyz = r0.xyz * 1.1 + -0.0825;
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
