#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix xWorldViewProjection;

float4x4 xWorld;
float3 xLightPos;
float xLightPower;
float xAmbient;
Texture xTexture;

sampler TextureSampler = sampler_state
{
	texture = <xTexture>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror; 
};

struct VertexShaderInput
{
	float4 inPos : POSITION0;
	float3 inNormal : NORMAL0;
	float2 inTexCoords : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position     : POSITION;
	float2 TexCoords    : TEXCOORD0;
	float3 Normal        : TEXCOORD1;
	float3 Position3D    : TEXCOORD2;
};

struct PixelToFrame {
	float4 Color : COLOR0;
};
VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output;
	output.Position = mul(input.inPos, xWorldViewProjection);
	output.TexCoords = input.inTexCoords;
	output.Normal = normalize(mul(input.inNormal, (float3x3)xWorld));
	output.Position3D = mul(input.inPos, xWorld);

	return output;
	
}

float DotProduct(float3 lightPos, float3 pos3D, float3 normal) {
	float3 lightDir = normalize(pos3D - lightPos);
	return dot(-lightDir, normal);
}

PixelToFrame MainPS(VertexShaderOutput PSIn) 
{
	PixelToFrame output;
	float diffuseLightingFactor = DotProduct(xLightPos, PSIn.Position3D, PSIn.Normal);
	diffuseLightingFactor = saturate(diffuseLightingFactor);
	diffuseLightingFactor *= xLightPower;

	PSIn.TexCoords.y--;
	float4 baseColor = tex2D(TextureSampler, PSIn.TexCoords);
	output.Color = baseColor * (diffuseLightingFactor + xAmbient);

	return output;
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};