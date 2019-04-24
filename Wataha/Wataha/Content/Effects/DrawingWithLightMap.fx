﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 World; 
float4x4 View;
float4x4 Projection;

texture2D BasicTexture; 

sampler2D basicTextureSampler = sampler_state
{
	texture = <BasicTexture>;
	addressU = wrap;
	addressV = wrap;
	minfilter = anisotropic;
	magfilter = anisotropic;
	mipfilter = linear;
};

bool TextureEnabled = true;

texture2D LightTexture;
sampler2D lightSampler = sampler_state
{
	texture = <LightTexture>;
	minfilter = point;
	magfilter = point;
	mipfilter = point;
};

float3 AmbientColor = float3(0.15, 0.15, 0.15);
float3 DiffuseColor;

#include "PPShared.vsi"


struct VertexShaderInput
{
	float4 Position : POSITION0;  
	float2 UV : TEXCOORD0;

};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float4 PositionCopy : TEXCOORD1;
};





VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4x4 worldViewProjection = mul(World, mul(View, Projection));
	output.Position = mul(input.Position, worldViewProjection);  
	output.PositionCopy = output.Position;
	output.UV = input.UV;
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{

	 float3 basicTexture = tex2D(basicTextureSampler, input.UV);  
	 if (!TextureEnabled)
	 basicTexture = float4(1, 1, 1, 1);

	 float2 texCoord = postProjToScreen(input.PositionCopy) + halfPixel();
	 float3 light = tex2D(lightSampler, texCoord);  
	 light += AmbientColor;
	 return float4(basicTexture * DiffuseColor * light, 1);
}

technique Technique1
{
	pass Pass1
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};