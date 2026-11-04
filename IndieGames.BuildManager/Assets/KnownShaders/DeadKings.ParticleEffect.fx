// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
float2 ViewportScale;
float CurrentTime;
float Duration;
float DurationRandomness;
float3 Gravity;
float EndVelocity;
float4 MinColor;
float4 MaxColor;
float2 RotateSpeed;
float2 StartSize;
float2 EndSize;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Point;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct vs_Particles_P0_VS_IN
{
	float4 position : POSITION;
	float4 position1 : POSITION1;
	float4 normal : NORMAL;
	float4 color : COLOR;
	float4 texcoord : TEXCOORD;
};

struct vs_Particles_P0_VS_OUT
{
	float4 color : COLOR;
	float2 color1 : COLOR1;
	float4 position : POSITION;
};

vs_Particles_P0_VS_OUT vs_Particles_P0(vs_Particles_P0_VS_IN i)
{
	vs_Particles_P0_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float4 r9;
	float r17;
	float4 r11;
	float2 r10;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r6.zw = i.position.xy;
	r4.xyz = i.position1.xyz;
	r5.yzw = i.normal.xyz;
	r1 = i.color.xywz;
	r0.y = i.texcoord.x;
	r0.w = EndSize.y + -EndSize.x;
	r17 = RotateSpeed.y + -RotateSpeed.x;
	r0.z = r17.x;
	r7.x = r1.x * DurationRandomness.x + 1;
	r2.z = dot(r5.wyz, r5.wyz);
	r17 = CurrentTime.x + -r0.y;
	r7.y = r17.x;
	r3 = MaxColor + -MinColor;
	r17 = StartSize.y + -StartSize.x;
	r0.x = r17.x;
	r0.xyz = r0.wxz * r1.yyz;
	r11 = rsqrt(abs(r2.z));
	r17 = 1 / r11.x;
	r9.x = r17.x;
	r9.z = r9.x * EndVelocity.x + -r9.x;
	r2.w = r0.x + EndSize.x;
	r17 = 1 / Duration.x;
	r7.z = r17.x;
	r2.xy = r7.xy * r7.zx;
	r17 = StartSize.x + r0.y;
	r0.w = r17.x;
	r9.y = r2.w + -r0.w;
	r0.x = saturate(r2.x * r7.y);
	r6.xy = r0.xx + float2(0.2, 0);
	r17 = 1 + -r0.x;
	r9.w = r17.x;
	r8 = r6.xyyy * r9.wxyz;
	r0.w = r0.w + r8.z;
	r7.xyz = r2.yyy * Gravity.xyz;
	r2.w = r8.x * r9.w;
	r17 = RotateSpeed.x + r0.z;
	r5.x = r17.x;
	r10 = r2.yw * float2(0.15915494, 3.3);
	r17 = rsqrt(abs(r2.z));
	r2.z = r17.x;
	r2.xy = r10.xy;
	r5 = r2.xzzz * r5;
	r0.y = r5.x + 0.5;
	r17 = r8.w;
	r6.x = frac(r0.y);
	r17 = r0.x * r17.x;
	r6.y = r17.x;
	r0.y = r6.y * 0.5 + r8.y;
	r1.xyz = r6.xzw * float3(6.2831855, 0.5, 0.5) + float3(-3.1415927, 0.5, 0.5);
	r0.z = r0.w * transpose(Projection)[1].y;
	r11.x = cos(r1.x);
	r17 = r11.x;
	r0.w = r17.x;
	r5.xyz = r5.yzw * r0.yyy;
	r11.y = sin(r1.x);
	r17 = r11.y;
	r0.y = r17.x;
	r4.xyz = r5.xyz * Duration.xxx + r4.xyz;
	r5.xzw = r0.wyw * r6.zww;
	r17 = (abs(r0.x) >= 0) ? 1 : 0;
	r7.w = r17.x;
	r7.xyz = r7.xyz * r0.xxx + r4.xyz;
	r4.x = dot(transpose(View)[0].zxyw, r7.zxyw);
	r4.y = dot(transpose(View)[1].zxyw, r7.zxyw);
	r4.z = dot(transpose(View)[2].zxyw, r7.zxyw);
	r17 = -r0.y;
	r4.w = dot(transpose(View)[3].zxyw, r7.zxyw);
	r17 = r6.z * r17.x;
	r5.y = r17.x;
	r0.xy = r5.xy + r5.zw;
	r0.zw = r0.zz * r0.xy;
	o.position.z = dot(r4.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r4.wzxy, transpose(Projection)[3].wzxy);
	r0.x = dot(r4.wzxy, transpose(Projection)[0].wzxy);
	r0.y = dot(r4.wzxy, transpose(Projection)[1].wzxy);
	o.position.xy = r0.zw * ViewportScale.xy + r0.xy;
	r0 = r3 * r1.w + MinColor;
	o.color.w = r2.y * r0.w;
	o.color1 = r1.yz;
	o.color.xyz = r0.xyz;

	return o;
}

struct ps_Particles_P0_PS_IN
{
	float4 color : COLOR;
	float2 color1 : COLOR1;
};

float4 ps_Particles_P0(ps_Particles_P0_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float4 r4;
	r0 = i.color;
	r1.xy = i.color1.xy;
	r4 = tex2D(Sampler, r1.xy);
	r1 = r4;
	o = r1 * r0;

	return o;
}

technique Particles
{
	pass P0
	{
		VertexShader = compile vs_3_0 vs_Particles_P0();
		PixelShader = compile ps_3_0 ps_Particles_P0();
	}
}
