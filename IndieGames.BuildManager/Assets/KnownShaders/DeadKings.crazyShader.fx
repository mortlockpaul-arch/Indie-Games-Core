float Fader;
float Reduct;
float Brite;
float delta;
float theta;

sampler samplerState = sampler_state
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Mirror;
	AddressV = Mirror;
};

float2 offsets[24] = { float2(-0.326212, -0.405805), float2(-0.840144, -0.07358), float2(-0.695914, 0.457137), float2(-0.203345, 0.620716), float2(0.96234, -0.194983), float2(0.473434, -0.480026), float2(0.519456, 0.767022), float2(0.185461, -0.893124), float2(0.507431, 0.064425), float2(0.89642, 0.412458), float2(-0.32194, -0.932615), float2(-0.791559, -0.597705), float2(0.326212, 0.405805), float2(0.840144, 0.07358), float2(0.695914, -0.457137), float2(0.203345, -0.620716), float2(-0.96234, 0.194983), float2(-0.473434, 0.480026), float2(-0.519456, -0.767022), float2(-0.185461, 0.893124), float2(-0.507431, -0.064425), float2(-0.89642, -0.412458), float2(0.32194, 0.932615), float2(0.791559, 0.597705) };

float4 ps_fly_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r0;
	float4 r7;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r0.xy = texcoord.xy;
	r2 = (0 < -abs(r0.x)) ? 1 : 0;
	r7 = tex2D(samplerState, r0.xy);
	r1 = r7;
	for (int _trip0 = 0, i0 = 0; _trip0 < 12; _trip0++, i0 += 1) {
		r0.z = Reduct.x + 0.015;
		r3.zw = offsets[i0].xy * 0.007;
		r3.xy = r0.zz * offsets[i0].xy;
		r4 = r3 + r0.xyxy;
		r7 = tex2D(samplerState, r4.zw);
		r3 = r7;
		r7 = tex2D(samplerState, r4.xy);
		r4 = r7;
		r2 = r4 + r2;
		r2 = r2 + r3;
	}
	r0 = r1 * float4(0.6, 0.6, 0.3, 0.6);
	r0 = r0 * Brite.x;
	o = r2 * float4(0.06666667, 0.06666667, 0.033333335, 0.06666667) + r0;

	return o;
}

float4 ps_learn_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r0;
	float4 r5;
	float r6;
	float4 r3;
	r1 = 0;
	r2 = 0;
	r0.xy = texcoord.xy;
	r5 = tex2D(samplerState, r0.xy);
	r1 = r5;
	r2.xyz = (0.1 >= r1.xzy) ? 1 : 0;
	r0.z = r2.x * r2.y;
	r0.z = (-abs(r0.z) >= 0) ? 0 : 1;
	r0.z = r2.z * r0.z;
	r6 = r0.z;
	if (r6.x == 0) {
		r0.xyz = r1.xyz;
	} else {
		r3.z = r1.y * 10;
		r0.x = 0.2 + r0.y;
		r0.z = r3.z;
		r0.yzw = -r0.zzz * float3(0.72, 0.7, 0.4) + float3(0.72, 0.7, 0.4);
		r0.xyz = r0.wzy * r0.xxx;
	}
	r0.xyz = -r0.xyz + 1;
	r0.x = log2(abs(r0.x));
	r0.y = log2(abs(r0.y));
	r0.z = log2(abs(r0.z));
	r0.xyz = r0.xyz * Fader.xxx;
	r0.x = exp2(r0.x);
	r3.w = r1.w * Brite.x;
	r0.y = exp2(r0.y);
	r0.w = r3.w;
	r2.x = -Brite.x + 1;
	r0.z = exp2(r0.z);
	r0.xyz = r0.xyz * Brite.xxx;
	o = r2.x * r1 + r0;

	return o;
}

float4 ps_holyoil_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float4 r9;
	float4 r10;
	float4 r11;
	float4 r12;
	float4 r13;
	float4 r14;
	float4 r15;
	float4 r0;
	float4 r18;
	float4 r17;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r10 = 0;
	r11 = 0;
	r12 = 0;
	r13 = 0;
	r14 = 0;
	r15 = 0;
	r0.xy = texcoord.xy;
	r1.xy = -r0.yx + 0.5;
	r0.z = dot(r1.yx, r1.yx) + 0;
	r0.w = rsqrt(abs(r0.z));
	r2.xy = r1.xy * r0.ww;
	r1 = r2.xyyx * float4(-0.042000003, -0.042000003, -0.054000005, -0.054000005) + r0.yxxy;
	r13 = r2.yxyx * float4(-0.030000001, -0.030000001, -0.018000001, -0.018000001) + r0.xyxy;
	r12 = r2.xyxy * float4(-0.012, -0.012, -0.024, -0.024) + r0.yxyx;
	r9 = r2.yxyx * float4(-0.006, -0.006, 0.012, 0.012) + r0.xyxy;
	r8 = r2.xyxy * float4(0.018000001, 0.018000001, 0.006, 0.006) + r0.yxyx;
	r5 = r2.yxyx * float4(0.024, 0.024, 0.042000003, 0.042000003) + r0.xyxy;
	r4 = r2.xyxy * float4(0.054000005, 0.054000005, 0.030000001, 0.030000001) + r0.yxyx;
	r18 = tex2D(samplerState, r4.yx);
	r2 = r18;
	r18 = tex2D(samplerState, r5.zw);
	r3 = r18;
	r18 = tex2D(samplerState, r4.wz);
	r4 = r18;
	r18 = tex2D(samplerState, r5.xy);
	r5 = r18;
	r18 = tex2D(samplerState, r8.yx);
	r6 = r18;
	r18 = tex2D(samplerState, r9.zw);
	r7 = r18;
	r18 = tex2D(samplerState, r8.wz);
	r8 = r18;
	r18 = tex2D(samplerState, r9.xy);
	r9 = r18;
	r18 = tex2D(samplerState, r12.yx);
	r10 = r18;
	r18 = tex2D(samplerState, r13.zw);
	r11 = r18;
	r18 = tex2D(samplerState, r12.wz);
	r12 = r18;
	r18 = tex2D(samplerState, r13.xy);
	r13 = r18;
	r18 = tex2D(samplerState, r1.yx);
	r14 = r18;
	r18 = tex2D(samplerState, r1.zw);
	r15 = r18;
	r18 = tex2D(samplerState, r0.xy);
	r1 = r18;
	r15 = r15 + r1;
	r14 = r15 + r14;
	r17 = rsqrt(abs(r0.z));
	r0.x = 1 / r17.x;
	r13 = r14 + r13;
	r12 = r13 + r12;
	r11 = r12 + r11;
	r10 = r11 + r10;
	r9 = r10 + r9;
	r8 = r9 + r8;
	r0.x = r0.x;
	r7 = r8 + r7;
	r0.y = 0.1;
	r6 = r7 + r6;
	r0.x = max(r0.x, r0.y);
	r5 = r6 + r5;
	r0.x = r0.x;
	r4 = r5 + r4;
	r0.y = 0.7;
	r3 = r4 + r3;
	r0.y = min(r0.x, r0.y);
	r2 = r3 + r2;
	r0.x = Brite.x * r0.y;
	r2 = r2 * 0.09090909 + -r1;
	r2.w = r0.x * r2.w;
	r0.xyz = r2.xyz * r0.yyy + r1.xyz;
	r0.xyz = r0.xyz * float3(0.625, 0.625, 1.2) + -r1.xyz;
	r2.xyz = r0.xyz * Brite.xxx;
	o = r2 + r1;

	return o;
}

float4 ps_dog_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r0;
	float4 r5;
	float2 r6;
	float4 r7;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r0.xy = texcoord.xy;
	r0.z = Brite.x + 0.001;
	r5.w = r0.x * 7;
	r0.z = 1 / r0.z;
	r0.w = r5.w;
	r0.w = delta.x * 2 + r0.w;
	r0.zw = r0.zw * float2(28, 0.15915494);
	r1.x = 0.5 + r0.w;
	r1.x = frac(r1.x);
	r0.w = 1 / r0.z;
	r1.x = r1.x * 6.2831855 + -3.1415927;
	r0.z = delta.x * 3;
	r6.y = sin(r1.x);
	r1.x = r6.y;
	r1.y = r0.w * r1.x + r0.y;
	r0.z = r1.y * 5 + r0.z;
	r0.z = r0.z * 0.15915494 + 0.5;
	r0.z = frac(r0.z);
	r0.z = r0.z * 6.2831855 + -3.1415927;
	r6.x = cos(r0.z);
	r0.z = r6.x;
	r1.x = r0.w * r0.z + r0.x;
	r2 = Fader.x * float4(0.0007142857, 0.0007142857, 0.00083333335, 0.00083333335) + r1.xyxy;
	r1.zw = -Fader.xx * 0.0007142857 + r1.xy;
	r7 = tex2D(samplerState, r0.xy);
	r0 = r7;
	r7 = tex2D(samplerState, r1.zw);
	r3.xyz = r7.yzw;
	r7 = tex2D(samplerState, r2.zw);
	r4.w = r7.x;
	r7 = tex2D(samplerState, r2.xy);
	r4.xyz = r7.yzw;
	r7 = tex2D(samplerState, r1.xy);
	r1 = r7;
	r2.zw = r4.xz * 8;
	r2.x = 0.6666667 * r1.x;
	r2.y = dot(r4.wy, float2(4, 8)) + 0;
	r2.yzw = -r3.yxz * 11 + r2.yzw;
	r2.yzw = r1.yzw * 0.5 + r2.zyw;
	r1 = r2 + r1;
	r1 = r1 + -r0;
	o = r1 * Brite.x + r0;

	return o;
}

float4 ps_teleport_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float4 r9;
	float4 r10;
	float4 r11;
	float4 r12;
	float4 r13;
	float4 r0;
	float r21;
	float4 r16;
	float4 r15;
	float3 r14;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r10 = 0;
	r11 = 0;
	r12 = 0;
	r13 = 0;
	r0.xy = texcoord.xy;
	r1.xy = -r0.yx + 0.5;
	r0.z = dot(r1.yx, r1.yx) + 0;
	r21 = rsqrt(abs(r0.z));
	r0.w = r21.x;
	r1.xy = r1.xy * r0.ww;
	r2 = r1.xyyx * float4(0.05, 0.05, 0.02, 0.02);
	r3 = r1.xyyx * float4(-0.07, -0.07, 0.04, 0.04);
	r4 = r1.xyyx * float4(0.03, 0.03, -0.01, -0.01);
	r5 = r1.xyyx * float4(0.01, 0.01, -0.03, -0.03);
	r6 = r1.xyyx * float4(-0.02, -0.02, -0.05, -0.05);
	r1 = r1.xyyx * float4(-0.04, -0.04, -0.09, -0.09);
	r1 = r1 * 2 + r0.yxxy;
	r11 = r6 * 2 + r0.yxxy;
	r9 = r5 * 2 + r0.yxxy;
	r7 = r4 * 2 + r0.yxxy;
	r12 = r3 * 2 + r0.yxxy;
	r5 = r2 * 2 + r0.yxxy;
	r16 = tex2D(samplerState, r5.yx);
	r2 = r16;
	r16 = tex2D(samplerState, r12.zw);
	r3 = r16;
	r16 = tex2D(samplerState, r7.yx);
	r4 = r16;
	r16 = tex2D(samplerState, r5.zw);
	r5 = r16;
	r16 = tex2D(samplerState, r9.yx);
	r6 = r16;
	r16 = tex2D(samplerState, r7.zw);
	r7 = r16;
	r16 = tex2D(samplerState, r11.yx);
	r8 = r16;
	r16 = tex2D(samplerState, r9.zw);
	r9 = r16;
	r16 = tex2D(samplerState, r1.yx);
	r10 = r16;
	r16 = tex2D(samplerState, r11.zw);
	r11 = r16;
	r16 = tex2D(samplerState, r12.yx);
	r12 = r16;
	r16 = tex2D(samplerState, r1.zw);
	r13 = r16;
	r16 = tex2D(samplerState, r0.xy);
	r1 = r16;
	r13 = r13.xwyz + r1.xwyz;
	r12 = r13 + r12.xwyz;
	r15 = rsqrt(abs(r0.z));
	r21 = 1 / r15.x;
	r0.x = r21.x;
	r11 = r12 + r11.xwyz;
	r10 = r11 + r10.xwyz;
	r9 = r10 + r9.xwyz;
	r8 = r9 + r8.xwyz;
	r21 = 4 * r0.x;
	r0.x = r21.x;
	r7 = r8 + r7.xwyz;
	r21 = r0.x;
	r0.x = r21.x;
	r6 = r7 + r6.xwyz;
	r21 = 0.3;
	r0.y = r21.x;
	r5 = r6 + r5.xwyz;
	r21 = max(r0.x, r0.y);
	r0.x = r21.x;
	r4 = r5 + r4.xwyz;
	r21 = r0.x;
	r0.x = r21.x;
	r3 = r4 + r3.xwyz;
	r21 = 1;
	r0.y = r21.x;
	r2 = r3 + r2.xwyz;
	r21 = min(r0.x, r0.y);
	r0.y = r21.x;
	r2 = r2 * 0.1 + -r1.xwyz;
	r2.xzw = r2.xzw * r0.yyy;
	r21 = Brite.x * r0.y;
	r0.w = r21.x;
	r0.x = r2.x * 2 + r1.x;
	r14.yz = r2.zw + r1.yz;
	r21 = r0.w;
	r0.yz = r14.yz;
	r0.yz = r0.yz * 0.5 + -r1.yz;
	r0.xyz = r0.xyz * Brite.xxx;
	r21 = r2.y * r21.x;
	r0.w = r21.x;
	o = r0 + r1;

	return o;
}

float4 ps_healthvial_x(float2 texcoord : TEXCOORD) : COLOR
{
	float4 o;

	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float4 r9;
	float4 r10;
	float4 r11;
	float4 r12;
	float4 r13;
	float4 r14;
	float4 r15;
	float4 r0;
	float4 r18;
	float4 r17;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r10 = 0;
	r11 = 0;
	r12 = 0;
	r13 = 0;
	r14 = 0;
	r15 = 0;
	r0.xy = texcoord.xy;
	r1.xy = -r0.yx + 0.5;
	r0.z = dot(r1.yx, r1.yx) + 0;
	r0.w = rsqrt(abs(r0.z));
	r2.xy = r1.xy * r0.ww;
	r1 = r2.xyyx * float4(-0.042000003, -0.042000003, -0.054000005, -0.054000005) + r0.yxxy;
	r13 = r2.yxyx * float4(-0.030000001, -0.030000001, -0.018000001, -0.018000001) + r0.xyxy;
	r12 = r2.xyxy * float4(-0.012, -0.012, -0.024, -0.024) + r0.yxyx;
	r9 = r2.yxyx * float4(-0.006, -0.006, 0.012, 0.012) + r0.xyxy;
	r8 = r2.xyxy * float4(0.018000001, 0.018000001, 0.006, 0.006) + r0.yxyx;
	r5 = r2.yxyx * float4(0.024, 0.024, 0.042000003, 0.042000003) + r0.xyxy;
	r4 = r2.xyxy * float4(0.054000005, 0.054000005, 0.030000001, 0.030000001) + r0.yxyx;
	r18 = tex2D(samplerState, r4.yx);
	r2 = r18;
	r18 = tex2D(samplerState, r5.zw);
	r3 = r18;
	r18 = tex2D(samplerState, r4.wz);
	r4 = r18;
	r18 = tex2D(samplerState, r5.xy);
	r5 = r18;
	r18 = tex2D(samplerState, r8.yx);
	r6 = r18;
	r18 = tex2D(samplerState, r9.zw);
	r7 = r18;
	r18 = tex2D(samplerState, r8.wz);
	r8 = r18;
	r18 = tex2D(samplerState, r9.xy);
	r9 = r18;
	r18 = tex2D(samplerState, r12.yx);
	r10 = r18;
	r18 = tex2D(samplerState, r13.zw);
	r11 = r18;
	r18 = tex2D(samplerState, r12.wz);
	r12 = r18;
	r18 = tex2D(samplerState, r13.xy);
	r13 = r18;
	r18 = tex2D(samplerState, r1.yx);
	r14 = r18;
	r18 = tex2D(samplerState, r1.zw);
	r15 = r18;
	r18 = tex2D(samplerState, r0.xy);
	r1 = r18;
	r15 = r15 + r1;
	r14 = r15 + r14;
	r17 = rsqrt(abs(r0.z));
	r0.y = 1 / r17.x;
	r13 = r14 + r13;
	r12 = r13 + r12;
	r11 = r12 + r11;
	r10 = r11 + r10;
	r9 = r10 + r9;
	r0.x = 3.1 * r0.y;
	r8 = r9 + r8;
	r0.x = r0.x;
	r7 = r8 + r7;
	r0.y = 0.1;
	r6 = r7 + r6;
	r0.x = max(r0.x, r0.y);
	r5 = r6 + r5;
	r0.x = r0.x;
	r4 = r5 + r4;
	r0.y = 1;
	r3 = r4 + r3;
	r0.y = min(r0.x, r0.y);
	r2 = r3 + r2;
	r0.x = Brite.x * r0.y;
	r2 = r2 * 0.09090909 + -r1;
	r2.w = r0.x * r2.w;
	r0.xyz = r2.xyz * r0.yyy + r1.xyz;
	r0.xyz = r0.xyz * float3(0.5555556, 1.4, 0.5555556) + -r1.xyz;
	r2.xyz = r0.xyz * Brite.xxx;
	o = r2 + r1;

	return o;
}

technique fly
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_fly_x();
	}
}

technique learn
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_learn_x();
	}
}

technique holyoil
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_holyoil_x();
	}
}

technique dog
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_dog_x();
	}
}

technique teleport
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_teleport_x();
	}
}

technique healthvial
{
	pass x
	{
		PixelShader = compile ps_3_0 ps_healthvial_x();
	}
}
