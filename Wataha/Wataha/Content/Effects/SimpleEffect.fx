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
texture BasicTexture;
bool TextureEnabled = false;
float3 DiffuseColor = float3(1, 1, 1);
float3 AmbientColor = float3(.1, .1, .1);
float3 LightDirection = float3(2, 2, 2);
float3 LightColor = float3 (0.9, 0.9, 0.9);
float SpecularPower = 124;
float3 SpecularColor = float3(1, 1, 1);

float3 CameraPosition;


struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Normal : TEXCOORD1;
	float3 ViewDirection : TEXCOORD2;
};

sampler BasicTextureSampler = sampler_state {
	texture = <BasicTexture>;
};



VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4x4 viewProjection = mul(View, Projection);

	output.Position = mul(worldPosition, viewProjection);
	output.UV = input.UV;
	output.Normal = mul(input.Normal, World);
	output.ViewDirection = worldPosition - CameraPosition;
	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 color = DiffuseColor;

	if (TextureEnabled)
		color *= tex2D(BasicTextureSampler, input.UV);

	float3 lighting = AmbientColor;

	float3 lightDir = normalize(LightDirection);
	float3 normal = normalize(input.Normal);

	lighting += saturate(dot(lightDir, normal)) * LightColor;

	float3 refl = reflect(lightDir, normal);
	float3 view = normalize(input.ViewDirection);

	lighting += pow(saturate(dot(refl, view)), SpecularPower) * SpecularColor;


	float3 output = saturate(lighting) * color;

	return float4(output, 1);
}

technique Technique1
{
	pass Pass1
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};