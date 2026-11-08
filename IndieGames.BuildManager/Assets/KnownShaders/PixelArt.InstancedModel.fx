// FNA SDL_GPU compatibility: centroid replaced by standard interpolation.
float4x4 World;
float4x4 View;
float4x4 Projection;
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.2;
float3 DiffuseLightDirection = float3(1, 1, 1);
float3 DiffuseLightDirection2 = float3(-1, -1, -1);
float DiffuseIntensity = 1;
float4x4 WorldInverseTranspose;

struct vs_HardwareInstancing_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 color : COLOR;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
};

struct vs_HardwareInstancing_Pass1_VS_OUT
{
	float4 color : COLOR;
	float4 color1 : COLOR1;
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
	float3 r12;
	float r19;
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
	r4 = i.position;
	r2 = i.normal;
	r1 = i.color;
	r5 = i.blendweight.xzwy;
	r8 = i.blendweight1;
	r10 = i.blendweight2;
	r11 = i.blendweight3;
	r9 = r11.xxwz * transpose(World)[3].yzzz;
	r6 = r10.zzww * transpose(World)[2].xyyx;
	r3 = r10.xxwz * transpose(World)[2].xwww;
	r0 = r10.xxwz * transpose(World)[2].yzzz;
	r7 = r11.y * transpose(World)[3].xwzy;
	r0 = r8.xxwz * transpose(World)[1].yzzz + r0;
	r3 = r8.xxwz * transpose(World)[1].xwww + r3;
	r6 = r8.zwwz * transpose(World)[1].xxyy + r6.xwzy;
	r7 = r10.y * transpose(World)[2].xwzy + r7;
	r8 = r8.y * transpose(World)[1].xzyw + r7.xzwy;
	r10 = r5.yzyz * transpose(World)[0].xxyy + r6.xywz;
	r7 = r5.xxyz * transpose(World)[0].xwww + r3.xywz;
	r6 = r5.yxxz * transpose(World)[0].zyzz + r0.wxyz;
	r0.z = r6.w + r9.z;
	r3 = r11.xxzw * transpose(World)[3].xwww;
	r0.w = r7.w + r3.w;
	r0.xy = r11.ww * transpose(World)[3].xy + r10.yw;
	r5.xy = r11.zz * transpose(World)[3].xy + r10.xz;
	r12.yz = r6.yz + r9.xy;
	r19 = r6.x;
	r6.yz = r12.yz;
	r6.xw = r7.xy + r3.xy;
	r19 = r9.w + r19.x;
	r5.z = r19.x;
	r8 = r5.w * transpose(World)[0].xywz + r8.xzwy;
	r3.y = dot(r8.zwxy, r4.wzxy);
	r19 = r7.z;
	r12.x = dot(r6.wzxy, r4.wzxy);
	r19 = r3.z + r19.x;
	r5.w = r19.x;
	r3.x = r12.x;
	r3.z = dot(r5.wzxy, r4.wzxy);
	r3.w = dot(r0.wzxy, r4.wzxy);
	r0.x = dot(r3.wzxy, transpose(View)[0].wzxy);
	r0.y = dot(r3.wzxy, transpose(View)[1].wzxy);
	r0.z = dot(r3.wzxy, transpose(View)[2].wzxy);
	r0.w = dot(r3.wzxy, transpose(View)[3].wzxy);
	o.position.x = dot(r0.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r0.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r0.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r0.wzxy, transpose(Projection)[3].wzxy);
	r3.x = dot(r2.wzxy, transpose(WorldInverseTranspose)[0].wzxy);
	r3.y = dot(r2.wzxy, transpose(WorldInverseTranspose)[1].wzxy);
	r19 = r1.x;
	r0.y = r19.x;
	r3.z = dot(r2.wzxy, transpose(WorldInverseTranspose)[2].wzxy);
	r19 = DiffuseIntensity.x * r0.y;
	r2.x = r19.x;
	r0.x = dot(r3.zxy, DiffuseLightDirection.zxy);
	r19 = DiffuseIntensity.x * r1.y;
	r2.y = r19.x;
	r0.y = dot(r3.zxy, DiffuseLightDirection2.zxy);
	r19 = DiffuseIntensity.x * r1.z;
	r2.z = r19.x;
	r0.xy = r0.xy + float2(0.5, 0.7);
	r19 = DiffuseIntensity.x * r1.w;
	r2.w = r19.x;
	o.color = saturate(r2 * r0.x);
	o.color1 = saturate(r2 * r0.y);

	return o;
}

struct ps_HardwareInstancing_Pass1_PS_IN
{
	float4 color : COLOR;
	float4 color1 : COLOR1;
};

float4 ps_HardwareInstancing_Pass1(ps_HardwareInstancing_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float4 r1;
	r0 = i.color;
	r1 = i.color1;
	o = r0;

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
