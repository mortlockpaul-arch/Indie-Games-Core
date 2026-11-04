// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float2 campos;
float4x4 View;
float4x4 Projection;
float4 LightDirection = float4(0.5, -0.4, 1, 0);
float3 DiffuseLight = float3(0.8, 0.9, 0.8);
float3 AmbientLight = float3(0.2, 0.2, 0.4);
float BoneDelta;
float RowDelta;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

texture AnimationTexture;

sampler AnimationSampler = sampler_state
{
	Texture = <AnimationTexture>;
	MinFilter = Point;
	MagFilter = Point;
	MipFilter = Point;
};

struct vs_HardwareInstancing_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendindices : BLENDINDICES;
	float4 blendweight : BLENDWEIGHT;
	float4 fog : FOG;
	float4 fog1 : FOG1;
	float4 fog2 : FOG2;
	float4 fog3 : FOG3;
	float4 fog4 : FOG4;
	float4 fog5 : FOG5;
};

struct vs_HardwareInstancing_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_HardwareInstancing_Pass1_VS_OUT vs_HardwareInstancing_Pass1(vs_HardwareInstancing_Pass1_VS_IN i)
{
	vs_HardwareInstancing_Pass1_VS_OUT o;

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
	float4 r10;
	float4 r11;
	float4 r12;
	float4 r13;
	float4 r14;
	float4 r15;
	float4 r16;
	float4 r17;
	float4 r19;
	float r25;
	float r21;
	float4 r20;
	float4 r18;
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
	r10 = 0;
	r11 = 0;
	r12 = 0;
	r13 = 0;
	r14 = 0;
	r15 = 0;
	r16 = 0;
	r17 = 0;
	r1 = i.position.wxyz;
	r9.xyz = i.normal.xyz;
	r4.xy = i.texcoord.xy;
	r11 = i.blendindices;
	r8 = i.blendweight;
	r6.xyz = i.fog.xyz;
	r3.x = i.fog1.x;
	r0.z = i.fog2.x;
	r0.y = i.fog3.x;
	r0.w = i.fog4.x;
	r0.x = i.fog5.x;
	r2.xy = r6.xz + -campos.xy;
	r2.x = dot(r2.xy, r2.xy) + 0;
	r5.xyz = (0 < -abs(r0.xxx)) ? 1 : 0;
	r19 = rsqrt(abs(r2.x));
	r25 = 1 / r19.x;
	r3.y = r25.x;
	r3.z = (r3.y < 1500) ? 1 : 0;
	r2 = (0 < -abs(r0.x)) ? 1 : 0;
	r21 = (-abs(r3.z) >= 0) ? 0 : 1;
	if (r21.x == 0) {
	} else {
		r2.x = (r0.x < 1) ? 1 : 0;
		r21 = (-abs(r2.x) >= 0) ? 0 : 1;
		if (r21.x == 0) {
			r19.x = (-0 >= 0) ? 1 : 0;
			r20.x = (-r8.x >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = 0 + 1;
			r19.y = (-0 >= 0) ? 1 : 0;
			r20.y = (-r8.x >= 0) ? 0 : 1;
			r18.w = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			r25 = (abs(r0.x) >= 0) ? 0 : 1;
			r7.x = r25.x;
			r0.w = r18.w;
			if (r21.x != 0) {
				r3.zw = BoneDelta.xx * float2(3, 0.5);
				r25 = RowDelta.x * r0.y;
				r0.y = r25.x;
				r2.xy = r11.xx * 3 + float2(1, 2);
				r2.xz = r2.xy * BoneDelta.xx + r3.ww;
				r0.x = r3.z * r11.x + r3.w;
				r0.y = RowDelta.x * 0.5 + r0.y;
				r25 = r0.y;
				r2.y = r25.x;
				r19.xyz = r2.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r5 = r20;
				r19.xyz = r2.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r2 = r20;
				r19.xyz = r0.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r7 = r20;
				r7 = r8.x * r7;
				r2 = r8.x * r2;
				r10 = r8.x * r5;
			}
			r19.x = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.x = (-r8.y >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r0.w + 1;
			r19.y = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.y = (-r8.y >= 0) ? 0 : 1;
			r0.w = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r25 = r0.y;
				r12.y = r25.x;
				r0.x = r3.z * r11.y + r3.w;
				r4.zw = r11.yy * 3 + float2(1, 2);
				r12.xz = r4.zw * BoneDelta.xx + r3.ww;
				r19.xyz = r12.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r5 = r20;
				r19.xyz = r12.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r19.xyz = r0.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r7 = r8.y * r13 + r7;
				r2 = r8.y * r12 + r2;
				r10 = r8.y * r5 + r10;
			}
			r19.x = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.x = (-r8.z >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r0.w + 1;
			r19.y = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.y = (-r8.z >= 0) ? 0 : 1;
			r0.w = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r25 = r0.y;
				r12.y = r25.x;
				r0.x = r3.z * r11.z + r3.w;
				r4.zw = r11.zz * 3 + float2(1, 2);
				r12.xz = r4.zw * BoneDelta.xx + r3.ww;
				r19.xyz = r12.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r5 = r20;
				r19.xyz = r12.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r19.xyz = r0.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r7 = r8.z * r13 + r7;
				r2 = r8.z * r12 + r2;
				r10 = r8.z * r5 + r10;
			}
			r19.x = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.x = (-r8.w >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r0.w + 1;
			r19.y = (-abs(r0.w) >= 0) ? 1 : 0;
			r20.y = (-r8.w >= 0) ? 0 : 1;
			r0.w = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r25 = r0.y;
				r8.y = r25.x;
				r0.x = r3.z * r11.w + r3.w;
				r4.zw = r11.ww * 3 + float2(1, 2);
				r8.xz = r4.zw * BoneDelta.xx + r3.ww;
				r19.xyz = r8.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r5 = r20;
				r19.xyz = r8.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r11 = r20;
				r19.xyz = r0.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r7 = r8.w * r12 + r7;
				r2 = r8.w * r11 + r2;
				r10 = r8.w * r5 + r10;
			}
			r19.x = r0.w + -1;
			r0.w = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r0.w + -1;
			r0.w = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r0.w + -1;
			r0.w = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r0.w + -1;
			r20.x = (-abs(r0.w) >= 0) ? 1 : r0.w;
			r0.w = (-abs(r19.x) >= 0) ? 0 : r20.x;
			r21 = (-abs(r19.x) >= 0) ? 1 : 0;
			if (r21.x != 0) {
				r7.yzw = (0 < -abs(r0.xxx)) ? 1 : 0;
				r2 = (0 < -abs(r0.x)) ? 1 : 0;
				r10 = (0 < -abs(r0.x)) ? 1 : 0;
			}
			r5.x = dot(r1.xwyz, r7.wzxy);
			r0.y = dot(r1.xwyz, r2.wzxy);
			r5.w = dot(r1.xwyz, r10.wzxy);
			r5.y = dot(r9.zxy, r7.zxy);
			r0.x = dot(r9.zxy, r2.zxy);
			r5.z = dot(r9.zxy, r10.zxy);
		} else {
			r19.x = (-0 >= 0) ? 1 : 0;
			r20.x = (-r8.x >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = 0 + 1;
			r19.y = (-0 >= 0) ? 1 : 0;
			r20.y = (-r8.x >= 0) ? 0 : 1;
			r4.z = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			r25 = (abs(r0.x) >= 0) ? 0 : 1;
			r10.x = r25.x;
			if (r21.x != 0) {
				r3.zw = BoneDelta.xx * float2(3, 0.5);
				r2.xy = r11.xx * 3 + float2(1, 2);
				r10.xz = r2.xy * BoneDelta.xx + r3.ww;
				r7.x = r2.y * BoneDelta.x + r3.w;
				r5.x = r3.z * r11.x + r3.w;
				r0.yw = r0.yw * RowDelta.xx;
				r5.yz = RowDelta.xx * 0.5 + r0.yw;
				r10.yw = r5.yz;
				r25 = r5.z;
				r7.y = r25.x;
				r19.xyz = r10.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r14 = r20;
				r19.xyz = r10.xww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r2 = r20;
				r19.xyz = r10.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r19.xyz = r7.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r7 = r20;
				r19.xyz = r5.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r19.xyz = r5.xzz;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r10 = r20;
				r12 = r12.wzyx + -r10.wzyx;
				r13 = r13.wzyx + -r7.wzyx;
				r14 = r14.wzyx + -r2.wzyx;
				r2 = r14 * r0.x + r2.wzyx;
				r7 = r13 * r0.x + r7.wzyx;
				r10 = r12 * r0.x + r10.wzyx;
				r10 = r10.wzyx * r8.x;
				r7 = r7.wzyx * r8.x;
				r2 = r2.wzyx * r8.x;
			}
			r19.x = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.x = (-r8.y >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r4.z + 1;
			r19.y = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.y = (-r8.y >= 0) ? 0 : 1;
			r4.z = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r5.x = r3.z * r11.y + r3.w;
				r0.yw = r11.yy * 3 + float2(1, 2);
				r13.xz = r0.yw * BoneDelta.xx + r3.ww;
				r0.y = r0.w * BoneDelta.x + r3.w;
				r13.yw = r5.yz;
				r25 = r5.z;
				r0.w = r25.x;
				r19.xyz = r13.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r17 = r20;
				r19.xyz = r13.xww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r19.xyz = r13.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r16 = r20;
				r19.xyz = r0.yww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r19.xyz = r5.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r15 = r20;
				r19.xyz = r5.xzz;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r14 = r20;
				r15 = r15.wzyx + -r14.wzyx;
				r16 = r16.wzyx + -r13.wzyx;
				r17 = r17.wzyx + -r12.wzyx;
				r12 = r17 * r0.x + r12.wzyx;
				r13 = r16 * r0.x + r13.wzyx;
				r14 = r15 * r0.x + r14.wzyx;
				r10 = r14.wzyx * r8.y + r10;
				r7 = r13.wzyx * r8.y + r7;
				r2 = r12.wzyx * r8.y + r2;
			}
			r19.x = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.x = (-r8.z >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r4.z + 1;
			r19.y = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.y = (-r8.z >= 0) ? 0 : 1;
			r4.z = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r5.x = r3.z * r11.z + r3.w;
				r0.yw = r11.zz * 3 + float2(1, 2);
				r13.xz = r0.yw * BoneDelta.xx + r3.ww;
				r0.y = r0.w * BoneDelta.x + r3.w;
				r13.yw = r5.yz;
				r25 = r5.z;
				r0.w = r25.x;
				r19.xyz = r13.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r17 = r20;
				r19.xyz = r13.xww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r19.xyz = r13.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r16 = r20;
				r19.xyz = r0.yww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r19.xyz = r5.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r15 = r20;
				r19.xyz = r5.xzz;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r14 = r20;
				r15 = r15.wzyx + -r14.wzyx;
				r16 = r16.wzyx + -r13.wzyx;
				r17 = r17.wzyx + -r12.wzyx;
				r12 = r17 * r0.x + r12.wzyx;
				r13 = r16 * r0.x + r13.wzyx;
				r14 = r15 * r0.x + r14.wzyx;
				r10 = r14.wzyx * r8.z + r10;
				r7 = r13.wzyx * r8.z + r7;
				r2 = r12.wzyx * r8.z + r2;
			}
			r19.x = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.x = (-r8.w >= 0) ? 0 : 1;
			r19.x = r19.x * r20.x;
			r20.x = r4.z + 1;
			r19.y = (-abs(r4.z) >= 0) ? 1 : 0;
			r20.y = (-r8.w >= 0) ? 0 : 1;
			r4.z = -r19.x * r20.x + r20.x;
			r21 = r19.y * r20.y;
			if (r21.x != 0) {
				r5.x = r3.z * r11.w + r3.w;
				r0.yw = r11.ww * 3 + float2(1, 2);
				r11.xz = r0.yw * BoneDelta.xx + r3.ww;
				r0.y = r0.w * BoneDelta.x + r3.w;
				r11.yw = r5.yz;
				r25 = r5.z;
				r0.w = r25.x;
				r19.xyz = r11.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r16 = r20;
				r19.xyz = r11.xww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r15 = r20;
				r19.xyz = r11.zyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r14 = r20;
				r19.xyz = r0.yww;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r11 = r20;
				r19.xyz = r5.xyy;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r13 = r20;
				r19.xyz = r5.xzz;
				r19.w = 0;
				r20 = tex2Dlod(AnimationSampler, r19);
				r12 = r20;
				r13 = r13.wzyx + -r12.wzyx;
				r14 = r14.wzyx + -r11.wzyx;
				r5 = r16.wzyx + -r15.wzyx;
				r5 = r5 * r0.x + r15.wzyx;
				r11 = r14 * r0.x + r11.wzyx;
				r12 = r13 * r0.x + r12.wzyx;
				r10 = r12.wzyx * r8.w + r10;
				r7 = r11.wzyx * r8.w + r7;
				r2 = r5.wzyx * r8.w + r2;
			}
			r19.x = r4.z + -1;
			r4.z = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r4.z + -1;
			r4.z = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r4.z + -1;
			r4.z = (-r19.x >= 0) ? 0 : r19.x;
			r21 = (-r19.x >= 0) ? 1 : 0;
			r19.x = r4.z + -1;
			r20.x = (-abs(r4.z) >= 0) ? 1 : r4.z;
			r4.z = (-abs(r19.x) >= 0) ? 0 : r20.x;
			r21 = (-abs(r19.x) >= 0) ? 1 : 0;
			if (r21.x != 0) {
				r10.yzw = (0 < -abs(r0.xxx)) ? 1 : 0;
				r2 = (0 < -abs(r0.x)) ? 1 : 0;
				r7 = (0 < -abs(r0.x)) ? 1 : 0;
			}
			r5.x = dot(r1.xwyz, r10.wzxy);
			r0.y = dot(r1.xwyz, r2.wzxy);
			r5.w = dot(r1.xwyz, r7.wzxy);
			r5.y = dot(r9.zxy, r10.zxy);
			r0.x = dot(r9.zxy, r2.zxy);
			r5.z = dot(r9.zxy, r7.zxy);
		}
		r25 = transpose(Projection)[2].y * r0.z;
		r2.x = r25.x;
		r2.z = r0.z * transpose(Projection)[3].y;
		r25 = transpose(Projection)[1].y * r0.z;
		r2.y = r25.x;
		r3.xy = r3.xy * float2(0.15915494, 0.0011111111) + float2(0.5, -0.6111111);
		r18.w = frac(r3.x);
		r25 = transpose(Projection)[0].y * r0.z;
		r2.w = r25.x;
		r0.w = r18.w;
		r0.w = r0.w * 6.2831855 + -3.1415927;
		r1.yzw = r0.zzz * r5.xyz;
		r19.x = cos(r0.w);
		r25 = r19.x;
		r4.z = r25.x;
		r3.y = saturate(-r3.y + 1);
		r19.y = sin(r0.w);
		r25 = r19.y;
		r4.w = r25.x;
		r2 = r2.wzxy * r0.x;
		r25 = -r4.w;
		r3.xz = r1.yw * r4.zw;
		r25 = r0.z * r25.x;
		r3.w = r25.x;
		r0.xw = r4.wz * r0.zz;
		r25 = r3.w;
		r7 = r0.xwww * r5.wwyz;
		r25 = r5.x * r25.x;
		r3.w = r25.x;
		r1.yw = r3.xw + r7.xy;
		r25 = r0.z;
		r0.z = r1.z * -r4.w + r7.w;
		r18.x = r7.z + r3.z;
		r25 = r0.y * r25.x;
		r1.z = r25.x;
		r0.x = r18.x;
		r5.x = dot(r0.xz, transpose(Projection)[0].xz) + r2.x;
		r5.y = dot(r0.xz, transpose(Projection)[1].xz) + r2.w;
		r5.z = dot(r0.xz, transpose(Projection)[2].xz) + r2.z;
		r5.w = dot(r0.xz, transpose(Projection)[3].xz) + r2.y;
		r0.xyz = r1.xxx * r6.xyz + r1.yzw;
		r2.x = dot(r0.zxy, transpose(View)[0].zxy);
		r2.y = dot(r0.zxy, transpose(View)[2].zxy);
		r2.z = dot(r0.zxy, transpose(View)[3].zxy);
		r0 = (float4(-0, -0, -0, -1) >= 0) ? r0.zxyy : r1.x;
		r0.y = dot(r0, transpose(View)[1].zxyw);
		r25 = transpose(View)[0].w * r1.x;
		r0.x = r25.x;
		r0.w = dot(r5.wzxy, LightDirection.wzxy);
		r25 = transpose(View)[2].w * r1.x;
		r0.z = r25.x;
		r18.y = max(-r0.w, 0);
		r25 = transpose(View)[3].w * r1.x;
		r0.w = r25.x;
		r1.y = r18.y;
		r0.xzw = r2.xyz + r0.xzw;
		r5.xyz = saturate(r1.yyy * DiffuseLight.xyz + AmbientLight.xyz);
		r2.x = dot(r0.wzxy, transpose(Projection)[0].wzxy);
		r2.y = dot(r0.wzxy, transpose(Projection)[1].wzxy);
		r2.z = dot(r0.wzxy, transpose(Projection)[2].wzxy);
		r2.w = dot(r0.wzxy, transpose(Projection)[3].wzxy);
	}
	o.position = r2;
	r3.xzw = r5.xyz * r3.yyy;
	o.texcoord = r4.xy;
	o.color = r3.xzwy;

	return o;
}

struct ps_HardwareInstancing_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass1(ps_HardwareInstancing_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	float4 r4;
	r0.xy = i.texcoord.xy;
	r1 = i.color;
	r4 = tex2D(Sampler, r0.xy);
	r0 = r4;
	o = r0 * r1;

	return o;
}

technique HardwareInstancing
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass1();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass1();
	}
}
