#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 WorldViewProjection;
float4x4 InvViewProjection;

texture2D DepthTexture;
texture2D NormalTexture;

sampler2D depthSampler = sampler_state
{
	texture = <DepthTexture>;
		minfilter = point;
	magfilter = point;
	mipfilter = point;
};

sampler2D normalSampler = sampler_state
{
	texture = <NormalTexture>;
		minfilter = point;
	magfilter = point;
	mipfilter = point;
};
float3 LightColor;
float3 LightPosition; 
float LightAttenuation;

#include "PPShared.vsi"

struct VertexShaderInput
{
	float4 Position : POSITION0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 LightPosition : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;
	
	output.Position = mul(input.Position, WorldViewProjection);  
	output.LightPosition = output.Position;   
	
	return output;

}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	 float2 texCoord = postProjToScreen(input.LightPosition) + halfPixel();
	 float4 depth = tex2D(depthSampler, texCoord);
	 float4 position;
	 position.x = texCoord.x * 2 - 1; 
	 position.y = (1 - texCoord.y) * 2 - 1; 
	 position.z = depth.r;  
	 position.w = 1.0f;

	 position = mul(position, InvViewProjection);
	 position.xyz /= position.w;

	 float4 normal = (tex2D(normalSampler, texCoord) - .5) * 2;
	 float3 lightDirection = normalize(LightPosition - position);
	 float lighting = clamp(dot(normal, lightDirection), 0, 1);
	 float d = distance(LightPosition, position);
	 float att = 1 - pow(d / LightAttenuation, 6);

	 return float4(LightColor * lighting * att, 1);
}
technique Technique1
{
	pass Pass1
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};