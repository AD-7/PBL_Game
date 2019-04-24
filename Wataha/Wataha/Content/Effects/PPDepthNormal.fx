#if OPENGL
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

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float3 Normal : NORMAL0;
};

struct VertexShaderOutput {
	float4 Position : POSITION0;
	float2 Depth : TEXCOORD0;
	float3 Normal : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input) 
{
	VertexShaderOutput output;
	float4x4 viewProjection = mul(View, Projection);
	float4x4 worldViewProjection = mul(World, viewProjection);

	output.Position = mul(input.Position, worldViewProjection);  
	output.Normal = mul(input.Normal, World);
	output.Depth.xy = output.Position.zw;

	return output;
}

struct PixelShaderOutput
{ 
	float4 Normal : COLOR0; 
	float4 Depth : COLOR1;
};

PixelShaderOutput PixelShaderFunction(VertexShaderOutput input) {

	PixelShaderOutput output;

	output.Depth = input.Depth.x / input.Depth.y;

	output.Normal.xyz = (normalize(input.Normal).xyz / 2) + .5;

	output.Depth.a = 1;
	output.Normal.a = 1;

	return output;
}

technique Technique1 {
	pass Pass1 {

		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();

	}
};