// FNA SDL_GPU compatibility: standard interpolation replaces unsupported centroid.
// Recovered original is preserved in the analysis archive.
texture BasicTexture;
texture BasicTexture2;
texture BasicTexture3;
texture BasicTexture4;

sampler TextureSampler = sampler_state
{
	Texture = <BasicTexture>;
	MipFilter = Linear;
	MinFilter = Linear;
	MagFilter = Linear;
};

sampler TextureSampler2 = sampler_state
{
	Texture = <BasicTexture2>;
	MipFilter = Linear;
	MinFilter = Linear;
	MagFilter = Linear;
};

sampler TextureSampler3 = sampler_state
{
	Texture = <BasicTexture3>;
	MipFilter = Linear;
	MinFilter = Linear;
	MagFilter = Linear;
};

sampler TextureSampler4 = sampler_state
{
	Texture = <BasicTexture4>;
	MipFilter = Linear;
	MinFilter = Linear;
	MagFilter = Linear;
};

float3 EyePosition;
float fader;
float4 myrot1;
float4 myrot2;
float3 DiffuseColor = float3(1, 1, 1);
float Alpha = 1;
float3 EmissiveColor;
float3 SpecularColor = float3(1, 1, 1);
float SpecularPower = 16;
float3 AmbientLightColor;
float3 DirLight0Direction;
float3 DirLight0DiffuseColor;
float3 DirLight0SpecularColor;
float3 DirLight1Direction;
float3 DirLight1DiffuseColor;
float3 DirLight1SpecularColor;
float3 DirLight2Direction;
float3 DirLight2DiffuseColor;
float3 DirLight2SpecularColor;
float4x4 World;
float4x4 View;
float4x4 Projection;

struct vs_BasicEffect__VS_IN
{
	float4 position : POSITION;
	float4 texcoord : TEXCOORD;
	float4 normal : NORMAL;
};

struct vs_BasicEffect__VS_OUT
{
	float2 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float4 color : COLOR;
	float4 position : POSITION;
};

vs_BasicEffect__VS_OUT vs_BasicEffect_(vs_BasicEffect__VS_IN i)
{
	vs_BasicEffect__VS_OUT o;

	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	r0 = 0;
	r1 = 0;
	r2 = 0;
	r3 = 0;
	r1 = i.position;
	r0.yz = i.texcoord.xy;
	r3.xyz = i.normal.xyz;
	r2.x = dot(r1.wzxy, transpose(World)[0].wzxy);
	r2.y = dot(r1.wzxy, transpose(World)[1].wzxy);
	r2.z = dot(r1.wzxy, transpose(World)[2].wzxy);
	r2.w = dot(r1.wzxy, transpose(World)[3].wzxy);
	r1.x = dot(r2.wzxy, transpose(View)[0].wzxy);
	r1.y = dot(r2.wzxy, transpose(View)[1].wzxy);
	r1.z = dot(r2.wzxy, transpose(View)[2].wzxy);
	r1.w = dot(r2.wzxy, transpose(View)[3].wzxy);
	o.position.x = dot(r1.wzxy, transpose(Projection)[0].wzxy);
	o.position.y = dot(r1.wzxy, transpose(Projection)[1].wzxy);
	o.position.z = dot(r1.wzxy, transpose(Projection)[2].wzxy);
	o.position.w = dot(r1.wzxy, transpose(Projection)[3].wzxy);
	r1.x = dot(r3.zxy, transpose(World)[0].zxy);
	r1.y = dot(r3.zxy, transpose(World)[1].zxy);
	r1.z = dot(r3.zxy, transpose(World)[2].zxy);
	r1.w = dot(r3.zxy, transpose(World)[3].zxy);
	r0.x = dot(r1.wzxy, r1.wzxy);
	r0.x = rsqrt(abs(r0.x));
	o.color.w = Alpha.x;
	o.color.xyz = 1;
	o.texcoord = r0.yz;
	o.texcoord1.xyz = r2.xyz;
	o.texcoord1.w = 0;
	o.texcoord2 = r1.xyz * r0.xxx;

	return o;
}

struct ps_BasicEffect__PS_IN
{
	float2 texcoord : TEXCOORD;
	float4 texcoord1 : TEXCOORD1;
	float3 texcoord2 : TEXCOORD2;
	float4 color : COLOR;
};

float4 ps_BasicEffect_(ps_BasicEffect__PS_IN i) : COLOR
{
	float4 o;

	float4 r4;
	float4 r5;
	float4 r6;
	float4 r7;
	float4 r8;
	float4 r9;
	float4 r0;
	float4 r1;
	float4 r2;
	float4 r3;
	float r17;
	float3 r10;
	float r13;
	float4 r12;
	r4 = 0;
	r5 = 0;
	r6 = 0;
	r7 = 0;
	r8 = 0;
	r9 = 0;
	r0.xy = i.texcoord.xy;
	r1 = i.texcoord1;
	r2.xyz = i.texcoord2.xyz;
	r3 = i.color;
	r1.yzw = -r1.zyx + EyePosition.zyx;
	r0.z = dot(r2.zxy, r2.zxy);
	r1.x = dot(r1.ywz, r1.ywz);
	r17 = rsqrt(abs(r0.z));
	r0.w = r17.x;
	r5.xyz = r0.www * r2.xyz;
	r8.xyz = r0.www * -DirLight2Direction.yxz;
	r4.xyz = r0.www * -DirLight0Direction.zyx;
	r0.z = dot(r4.xzy, r2.zxy);
	r17 = rsqrt(abs(r1.x));
	r1.x = r17.x;
	r4.xyz = r1.wzy * r1.xxx;
	r9.xyz = r4.xyz + -DirLight0Direction.xyz;
	r10.z = max(r0.z, 0);
	r17 = (abs(r0.x) >= 0) ? 0 : 1;
	r1.z = r17.x;
	r0.z = r10.z;
	r6.xyz = r0.zzz * DirLight0DiffuseColor.xyz + AmbientLightColor.xyz;
	r1.x = dot(r9.zxy, r9.zxy);
	r17 = (abs(r0.x) >= 0) ? 0 : 1;
	r1.y = r17.x;
	r7.xyz = r0.www * -DirLight1Direction.yxz;
	r17 = rsqrt(abs(r1.x));
	r1.x = r17.x;
	r9.xyz = r9.zyx * r1.xxx;
	r17 = (abs(r0.x) >= 0) ? 0 : 1;
	r1.x = r17.x;
	r9.xyz = r9.zyx * r0.www;
	r13 = r0.z;
	if (r13.x != 0) {
		r0.z = dot(r9.zxy, r2.zxy);
		r0.z = max(r0.z, 0);
		r17 = log2(abs(r0.z));
		r0.z = r17.x;
		r17 = SpecularPower.x * r0.z;
		r0.z = r17.x;
		r17 = exp2(r0.z);
		r0.z = r17.x;
		r1.xyz = r0.zzz * DirLight0SpecularColor.xyz;
	}
	r9.xyz = r4.xyz + -DirLight1Direction.xyz;
	r0.w = dot(r9.zxy, r9.zxy);
	r10.z = dot(r7.zyx, r2.zxy);
	r17 = rsqrt(abs(r0.w));
	r0.w = r17.x;
	r0.z = r10.z;
	r0.z = max(r0.z, 0);
	r7.xyz = r0.zzz * DirLight1DiffuseColor.xyz + r6.xyz;
	r6.xyz = r9.xyz * r0.www;
	r13 = r0.z;
	if (r13.x != 0) {
		r0.z = dot(r6.zxy, r5.zxy);
		r0.z = max(r0.z, 0);
		r17 = log2(abs(r0.z));
		r0.z = r17.x;
		r17 = SpecularPower.x * r0.z;
		r0.z = r17.x;
		r17 = exp2(r0.z);
		r0.z = r17.x;
		r1.xyz = r0.zzz * DirLight1SpecularColor.xyz + r1.xyz;
	}
	r6.xyz = r4.xyz + -DirLight2Direction.xyz;
	r0.w = dot(r6.zxy, r6.zxy);
	r10.z = dot(r8.zyx, r2.zxy);
	r17 = rsqrt(abs(r0.w));
	r0.w = r17.x;
	r0.z = r10.z;
	r0.z = max(r0.z, 0);
	r4.xyz = r0.zzz * DirLight2DiffuseColor.xyz + r7.xyz;
	r2.xyz = r6.xyz * r0.www;
	r13 = r0.z;
	if (r13.x != 0) {
		r0.z = dot(r2.zxy, r5.zxy);
		r0.z = max(r0.z, 0);
		r17 = log2(abs(r0.z));
		r0.z = r17.x;
		r17 = SpecularPower.x * r0.z;
		r0.z = r17.x;
		r17 = exp2(r0.z);
		r0.z = r17.x;
		r1.xyz = r0.zzz * DirLight2SpecularColor.xyz + r1.xyz;
	}
	r2.zw = r0.yx + -0.5;
	r2.y = dot(r2.zw, myrot2.wy) + 0.5;
	r2.x = dot(r2.zw, myrot2.zx) + 0.5;
	r0.w = dot(r2.zw, myrot1.wy) + 0.5;
	r0.z = dot(r2.zw, myrot1.zx) + 0.5;
	r12 = tex2D(TextureSampler, r0.xy);
	r9.xyz = r12.xyz;
	r12 = tex2D(TextureSampler4, r0.xy);
	r5 = r12;
	r12 = tex2D(TextureSampler3, r2.xy);
	r6 = r12;
	r12 = tex2D(TextureSampler2, r0.zw);
	r8 = r12;
	r0.xzw = r1.xyz * SpecularColor.xyz;
	r17 = r5.w;
	r0.y = r17.x;
	r1.xyz = r4.xyz * DiffuseColor.xyz + EmissiveColor.xyz;
	r2.xyz = r1.xyz * r3.xyz;
	r17 = fader.x * r0.y;
	r7.x = r17.x;
	r7.y = r6.w * fader.x;
	r17 = fader.x * r8.w;
	r7.z = r17.x;
	r6.xyz = r7.yyy * r6.xyz;
	r17 = r7.x;
	r8.xyz = r7.zzz * r8.xyz;
	r17 = r5.x * r17.x;
	r3.x = r17.x;
	r1.xyz = -r7.zxy + 1;
	r17 = r7.x;
	r4.xyz = r1.xxx * r9.xyz;
	r17 = r5.y * r17.x;
	r3.y = r17.x;
	r4.xyz = r8.xzy * 2 + r4.xzy;
	r4.xyz = r4.xzy * r1.zzz;
	r17 = r7.x;
	r4.xyz = r6.xzy * 2 + r4.xzy;
	r1.xyz = r4.xzy * r1.yyy;
	r17 = r5.z * r17.x;
	r3.z = r17.x;
	r1.xyz = r3.xzy * 2 + r1.xzy;
	r3.xyz = r2.xyz * r1.xzy + r0.xzw;
	o = r3;

	return o;
}

technique BasicEffect
{
	pass
	{
		VertexShader = compile vs_3_0 vs_BasicEffect_();
		PixelShader = compile ps_3_0 ps_BasicEffect_();
	}
}
