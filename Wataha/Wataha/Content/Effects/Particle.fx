﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 View;
float4x4 Projection;
texture ParticleTexture;
sampler2D texSampler = sampler_state
{ texture = <ParticleTexture>;
};
float Time;
float Lifespan;
float2 Size;
float3 Wind;
float3 Up;
float3 Side;
float FadeInTime;

struct VertexShaderInput {
	float3 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Direction : TEXCOORD1;
	float Speed : TEXCOORD2;
	float StartTime : TEXCOORD3;
};
struct VertexShaderOutput
{ 
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;  
	float RelativeTime : TEXCOORD1; 
};

VertexShaderOutput MainVS(VertexShaderInput input)
{
	VertexShaderOutput output;

	float3 position = input.Position;

	float2 offset = float2((input.UV.x - 0.5f) * 2.0f, -(input.UV.y - 0.5f) * 2.0f);
	
	position += offset.x * Size.x * Side + offset.y * Size.y * Up;

	float relativeTime = (Time - input.StartTime);
	output.RelativeTime = relativeTime;

	position += (input.Direction * input.Speed + Wind) * relativeTime;
	
	output.Position = mul(float4(position, 1), mul(View, Projection));
	output.UV = input.UV;

	return output;
}



float4 MainPS(VertexShaderOutput input) : COLOR0
{
	clip(input.RelativeTime);

	float4 color = tex2D(texSampler, input.UV);

	float d = clamp(1.0f - pow((input.RelativeTime / Lifespan), 10), 0, 1);

	d *= clamp((input.RelativeTime / FadeInTime), 0, 1);

	return float4(color * d);
}

technique Particle
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};

