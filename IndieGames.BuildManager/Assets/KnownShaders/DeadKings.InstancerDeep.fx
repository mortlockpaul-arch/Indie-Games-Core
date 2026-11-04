// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
float4x4 View;
float4x4 Projection;
float4x4 World;
float4x4 ReflectionView;
float3 CameraPosition;
float slide;
float mult = 3;
float3 LightDirection;
float3 DiffuseLight;
float3 AmbientLight;
texture Texture;

sampler Sampler = sampler_state
{
	Texture = <Texture>;
};

texture ReflectionMap;

sampler ReflectionSampler = sampler_state
{
	Texture = <ReflectionMap>;
	MagFilter = Linear;
	MinFilter = Linear;
	MipFilter = Linear;
	AddressU = Mirror;
	AddressV = Mirror;
};

struct vs_HardwareInstancing_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog : FOG;
};

struct vs_HardwareInstancing_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
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
	float4 r11;
	float4 r12;
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
	r7 = i.position;
	r2.xyw = i.normal.yzx;
	r3.zw = i.texcoord.xy;
	r4 = i.blendweight;
	r6 = i.blendweight1;
	r8 = i.blendweight2;
	r9 = i.blendweight3;
	r0.x = i.fog.x;
	r0.x = r0.x * 0.01010101 + 0.004;
	r5.xyz = r2.yyy * r8.xzy;
	r0.y = 0.004;
	r11.xyz = r0.xyy;
	r11.w = 0;
	r12 = tex2Dlod(Sampler, r11);
	r1.xyz = r12.xyz;
	r0 = r7.w * r9;
	r0 = r7.z * r8 + r0;
	r0 = r7.y * r6.xzwy + r0.xzwy;
	r0 = r7.x * r4.xwyz + r0.xzwy;
	r7.x = dot(r0.ywxz, transpose(View)[0].wzxy);
	r7.y = dot(r0.ywxz, transpose(View)[1].wzxy);
	r7.z = dot(r0.ywxz, transpose(View)[2].wzxy);
	r7.w = dot(r0.ywxz, transpose(View)[3].wzxy);
	r0.y = dot(r7.wzxy, transpose(Projection)[3].wzxy);
	r0.w = dot(r7.wzxy, transpose(Projection)[2].wzxy);
	r0.x = dot(r7.wzxy, transpose(Projection)[1].wzxy);
	r0.z = dot(r7.wzxy, transpose(Projection)[0].wzxy);
	o.position = r0.zxwy;
	r2.xyz = r2.xxx * r6.xzy + r5.xyz;
	r2.xyz = r2.www * r4.xzy + r2.xyz;
	r4.w = r0.w + -transpose(Projection)[2].w;
	r4.y = r0.z + -transpose(Projection)[0].w;
	r4.x = -transpose(Projection)[1].w + r0.x;
	r3.xy = mult.xx * float2(1400, 2200);
	r4.z = -transpose(Projection)[3].w + r0.y;
	o.texcoord = r3.zw;
	o.texcoord1 = (0 < -abs(r0.xxx)) ? 1 : 0;
	r0.z = dot(r4.zwyx, r4.zwyx);
	r0.y = 1 / r3.y;
	r10.x = dot(r2.yxz, r2.yxz);
	r11 = rsqrt(abs(r0.z));
	r0.z = 1 / r11.x;
	r0.x = r10.x;
	r2.w = -r3.x + r0.z;
	r0.x = rsqrt(abs(r0.x));
	r0 = r2.wxzy * r0.yxxx;
	r1.w = dot(r0.wyz, -LightDirection.zxy);
	r10.y = max(r1.w, 0.1);
	r0.x = r0.x;
	r0.y = r10.y;
	r0.yzw = r0.yyy * DiffuseLight.xyz + AmbientLight.xyz;
	r1.xyz = r0.yzw * r1.xyz;
	r0.w = saturate(1 + -r0.x);
	r0.xyz = r1.xyz * r0.www;
	o.color = r0;

	return o;
}

struct ps_HardwareInstancing_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_HardwareInstancing_Pass1(ps_HardwareInstancing_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r0;
	float3 r1;
	float4 r2;
	float4 r5;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1.xyz;
	r2 = i.color;
	r5 = tex2D(Sampler, r0.xy);
	r0 = r5;
	o = r0 * r2;

	return o;
}

struct vs_Reflection1_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog : FOG;
};

struct vs_Reflection1_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_Reflection1_Pass1_VS_OUT vs_Reflection1_Pass1(vs_Reflection1_Pass1_VS_IN i)
{
	vs_Reflection1_Pass1_VS_OUT o;

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
	float4 r11;
	float4 r12;
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
	r1 = i.position;
	r7.xyz = i.normal.xyz;
	r6.xy = i.texcoord.xy;
	r3 = i.blendweight;
	r9 = i.blendweight1;
	r2 = i.blendweight2;
	r5 = i.blendweight3;
	r0.x = i.fog.x;
	r0.x = r0.x * 0.01010101 + 0.004;
	r8.xyz = r7.zzz * r2.xzy;
	r0.y = 0.004;
	r11.xyz = r0.xyy;
	r11.w = 0;
	r12 = tex2Dlod(Sampler, r11);
	r4.xyz = r12.xyz;
	r0 = r1.w * r5;
	r0 = r1.z * r2 + r0;
	r0 = r1.y * r9.xzwy + r0.xzwy;
	r5 = r1.x * r3.xwyz + r0.xzwy;
	r0.x = dot(r5.ywxz, transpose(View)[0].wzxy);
	r0.y = dot(r5.ywxz, transpose(View)[1].wzxy);
	r0.z = dot(r5.ywxz, transpose(View)[2].wzxy);
	r0.w = dot(r5.ywxz, transpose(View)[3].wzxy);
	r2.w = dot(r0.wzxy, transpose(Projection)[3].wzxy);
	r2.z = dot(r0.wzxy, transpose(Projection)[2].wzxy);
	r2.y = dot(r0.wzxy, transpose(Projection)[1].wzxy);
	r2.x = dot(r0.wzxy, transpose(Projection)[0].wzxy);
	o.position = r2;
	r1.yz = mult.xx * float2(2200, 1400);
	r0.y = r2.z;
	r1.x = r2.x;
	r0.x = r2.y;
	r2.xyz = r7.yyy * r9.xzy + r8.xyz;
	r3.xyz = r7.xxx * r3.xyz + r2.xzy;
	r0.w = dot(r3.zxy, r3.zxy);
	r0.z = r2.w;
	r2.xyz = -r5.xzw + CameraPosition.xyz;
	r1.w = rsqrt(abs(r0.w));
	r0.w = dot(r2.zxy, r2.zxy);
	r5.x = -transpose(Projection)[0].w + r1.x;
	r3.xyz = r3.xyz * r1.www;
	r5.z = -transpose(Projection)[2].w + r0.y;
	r10.y = dot(r3.zxy, -LightDirection.zxy);
	r5.y = -transpose(Projection)[1].w + r0.x;
	r0.y = r10.y;
	r10.x = max(r0.y, 0.1);
	r5.w = -transpose(Projection)[3].w + r0.z;
	r0.x = r10.x;
	r0.xyz = r0.xxx * DiffuseLight.xyz + AmbientLight.xyz;
	o.texcoord = r6.xy;
	r10.x = dot(r5.wzxy, r5.wzxy);
	r1.y = 1 / r1.y;
	r1.x = r10.x;
	r0.xyz = r0.xyz * r4.xyz;
	r11 = rsqrt(abs(r1.x));
	r1.x = 1 / r11.x;
	r2.w = -r1.z + r1.x;
	r1.x = rsqrt(abs(r0.w));
	r1 = r2 * r1.xxxy;
	r2 = (float4(-0, -0, -0, -1) >= 0) ? -r1.zxyy : 1;
	r3.w = dot(-r1.zxy, r3.zxy);
	r0.w = saturate(-r1.w + 1);
	r0.xyz = r0.xyz * r0.www;
	r2.x = dot(r3.zxyw, r2);
	o.texcoord1 = -r3.xyz * r2.xxx + -r1.xyz;
	o.color = r0;

	return o;
}

struct ps_Reflection1_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_Reflection1_Pass1(ps_Reflection1_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r3;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r6;
	float3 r4;
	float r11;
	r3 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord1.xyz;
	r2 = i.color;
	r6 = tex2D(Sampler, r0.xy);
	r3 = r6;
	r0.xy = -r1.xy * 0.5;
	r1 = r3.xxyz * float4(0.16666667, 1, 1, 1);
	r1 = r1.yzwx * r2.xyzx;
	r4.z = r0.x + r1.w;
	r11 = slide.x + r0.y;
	r0.w = r11.x;
	r0.z = r4.z;
	r6 = tex2D(ReflectionSampler, r0.zw);
	r0.xyz = r6.xyz;
	r0.xyz = r0.xyz * 1.6;
	r0.xyz = saturate(r1.xyz * r0.xyz);
	r11 = r3.w;
	o.xyz = r1.xyz + r0.xyz;
	r11 = r2.w * r11.x;
	o.w = r11.x;

	return o;
}

struct ps_ReflectAlpha_Pass1_PS_IN
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
};

float4 ps_ReflectAlpha_Pass1(ps_ReflectAlpha_Pass1_PS_IN i) : COLOR
{
	float4 o;

	float4 r3;
	float4 r0;
	float4 r1;
	float4 r2;
	float r11;
	float4 r6;
	float2 r4;
	r3 = 0;
	r0.xy = i.texcoord.xy;
	r1.xyz = i.texcoord1.xyz;
	r2 = i.color;
	r11 = -r1.x;
	r0.z = r11.x;
	r0.w = -r1.y * 0.4 + slide.x;
	r6 = tex2D(ReflectionSampler, r0.zw);
	r3.xyz = r6.xyz;
	r6 = tex2D(Sampler, r0.xy);
	r1 = r6;
	o.w = r1.w * r2.w;
	r11 = 1 + -r1.w;
	r0.w = r11.x;
	r0.z = -r1.w * 0.3 + 0.3;
	r4 = r0.ww * r3.yz;
	r11 = r0.z;
	r0.xy = r4.xy;
	r0.yz = saturate(r0.xy * 0.3);
	r11 = r3.x * r11.x;
	r0.x = saturate(r11.x);
	r0.xyz = r0.www * r2.xyz + r0.xyz;
	r0.xyz = r1.xyz * r1.www + r0.xyz;
	o.xyz = r0.xyz * r2.www;

	return o;
}

struct vs_teleport_Pass1_VS_IN
{
	float4 position : POSITION;
	float4 normal : NORMAL;
	float4 texcoord : TEXCOORD;
	float4 blendweight : BLENDWEIGHT;
	float4 blendweight1 : BLENDWEIGHT1;
	float4 blendweight2 : BLENDWEIGHT2;
	float4 blendweight3 : BLENDWEIGHT3;
	float4 fog : FOG;
};

struct vs_teleport_Pass1_VS_OUT
{
	float2 texcoord : TEXCOORD;
	float3 texcoord1 : TEXCOORD1;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_teleport_Pass1_VS_OUT vs_teleport_Pass1(vs_teleport_Pass1_VS_IN i)
{
	vs_teleport_Pass1_VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float r16;
	float4 r10;
	float4 r11;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r7 = i.position;
	r2.xyw = i.normal.yzx;
	r1.xy = i.texcoord.xy;
	r3 = i.blendweight;
	r5 = i.blendweight1;
	r8 = i.blendweight2;
	r6 = i.blendweight3;
	r0.x = i.fog.x;
	r0.x = r0.x * 0.01010101 + 0.004;
	r4.xyz = r2.yyy * r8.xzy;
	r16 = 0.004;
	r0.y = r16.x;
	r10.xyz = r0.xyy;
	r10.w = 0;
	r11 = tex2Dlod(Sampler, r10);
	r0.xyz = r11.xyz;
	r6 = r7.w * r6;
	r6 = r7.z * r8 + r6;
	r6 = r7.y * r5.xzwy + r6.xzwy;
	r7 = r7.x * r3.xwyz + r6.xzwy;
	r6.x = dot(r7.ywxz, transpose(View)[0].wzxy);
	r6.y = dot(r7.ywxz, transpose(View)[1].wzxy);
	r6.z = dot(r7.ywxz, transpose(View)[2].wzxy);
	r6.w = dot(r7.ywxz, transpose(View)[3].wzxy);
	o.position.x = dot(r6.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r6.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r6.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r6.wzxy, transpose(Projection)[3].wzxy);
	r2.xyz = r2.xxx * r5.xzy + r4.xyz;
	r2.xyz = r2.www * r3.xyz + r2.xzy;
	r0.w = dot(r2.zxy, r2.zxy);
	r16 = rsqrt(abs(r0.w));
	r0.w = r16.x;
	r2.xyz = r2.xyz * r0.www;
	r0.w = dot(r2.zxy, -LightDirection.zxy);
	o.texcoord = r1.xy;
	o.texcoord1 = (0 < -abs(r0.xxx)) ? 1 : 0;
	r0.w = max(r0.w, 0.1);
	r1.xyz = r0.www * DiffuseLight.xyz + AmbientLight.xyz;
	o.color.xyz = r1.xyz * r0.xyz;
	o.color.w = 1;

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

technique Reflection1
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_Reflection1_Pass1();
		PixelShader = compile ps_3_0 ps_Reflection1_Pass1();
	}
}

technique ReflectAlpha
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_Reflection1_Pass1();
		PixelShader = compile ps_3_0 ps_ReflectAlpha_Pass1();
	}
}

technique SoftInstancing
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_HardwareInstancing_Pass1();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass1();
	}
}

technique teleport
{
	pass Pass1
	{
		VertexShader = compile vs_3_0 vs_teleport_Pass1();
		PixelShader = compile ps_3_0 ps_HardwareInstancing_Pass1();
	}
}
