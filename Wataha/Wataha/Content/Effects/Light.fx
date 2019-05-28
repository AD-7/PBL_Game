#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix xWorldViewProjection;
float4x4 xLightsWorldViewProjection;

float4x4 xWorld;
float3 xLightPos;
float xLightPower;
float xAmbient;
float xAlpha;

float4 LightColor = float4 (1, 1, 1,1);

Texture xTexture;
sampler TextureSampler = sampler_state
{
	texture = <xTexture>;
	magfilter = LINEAR; 
	minfilter = Anisotropic;
	mipfilter = LINEAR; 
	MaxAnisotropy = 16;
	//AddressU = mirror; AddressV = mirror;
};

Texture xShadowMap;
sampler ShadowMapSampler = sampler_state
{ 
	texture = <xShadowMap>; 
	magfilter = Point;
	minfilter = Point; 
	mipfilter = Point; 
	AddressU = wrap; AddressV = wrap;
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
	output.Normal = mul(input.inNormal, (float3x3)xWorld);
	output.Position3D = mul(input.inPos, xWorld);

	return output;
	
}

float DotProduct(float3 lightPos, float3 pos3D, float3 normal) {
	float3 lightDir = normalize(pos3D - lightPos);
	return saturate(dot(-lightDir, normal));
}

PixelToFrame MainPS(VertexShaderOutput PSIn) 
{
	PixelToFrame output;
	float diffuseLightingFactor = DotProduct(xLightPos, PSIn.Position3D, PSIn.Normal);
	diffuseLightingFactor = saturate(diffuseLightingFactor);
	diffuseLightingFactor *= xLightPower;

	PSIn.TexCoords.y--;
	float4 baseColor = tex2D(TextureSampler, PSIn.TexCoords);
	output.Color = baseColor * (diffuseLightingFactor + xAmbient) * LightColor;
	output.Color.a = xAlpha;
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

struct SMapVertexToPixel
{
	float4 Position : POSITION;
	float4 Position2D    : TEXCOORD0;
};
struct SMapPixelToFrame
{
	float4 Color : COLOR0;
};

SMapVertexToPixel ShadowMapVertexShader(float4 inPos : POSITION)
{
	SMapVertexToPixel Output = (SMapVertexToPixel)0;

	Output.Position = mul(inPos, xLightsWorldViewProjection);
	Output.Position2D = Output.Position;

	return Output;
}
SMapPixelToFrame ShadowMapPixelShader(SMapVertexToPixel PSIn)
{
	SMapPixelToFrame Output = (SMapPixelToFrame)0;
Output.Color.a = xAlpha;
	Output.Color =float4( PSIn.Position2D.z / PSIn.Position2D.w, 0.0f, 0.0f, 1.0f);
	

	return Output;
}

technique ShadowMap 
{
	pass Pass0 
	{
		VertexShader = compile VS_SHADERMODEL ShadowMapVertexShader();
		PixelShader = compile PS_SHADERMODEL ShadowMapPixelShader();
	}
};

struct SSceneVertexToPixel
{
	float4 Position             : POSITION;
	float4 Pos2DAsSeenByLight    : TEXCOORD0;

	float2 TexCoords			 :	TEXCOORD1;
	float3 Normal                : TEXCOORD2;
	float4 Position3D            : TEXCOORD3;
};

struct SScenePixelToFrame
{
	float4 Color : COLOR0;
};
SSceneVertexToPixel ShadowedSceneVertexShader(float4 inPos : POSITION, float2 inTexCoords : TEXCOORD0, float3 inNormal : NORMAL)
{
	SSceneVertexToPixel Output = (SSceneVertexToPixel)0;

	Output.Position = mul(inPos, xWorldViewProjection);
	Output.Pos2DAsSeenByLight = mul(inPos, xLightsWorldViewProjection);
	Output.Normal =normalize( mul(inNormal, (float3x3)xWorld));
	Output.TexCoords = inTexCoords;
	Output.Position3D = mul(inPos, xWorld);
	return Output;
}
SScenePixelToFrame ShadowedScenePixelShader(SSceneVertexToPixel PSIn)
{
	SScenePixelToFrame Output = (SScenePixelToFrame)0;

	float2 ProjectedTexCoords;
	ProjectedTexCoords[0] = PSIn.Pos2DAsSeenByLight.x / PSIn.Pos2DAsSeenByLight.w / 2.0f + 0.5f;
	ProjectedTexCoords[1] = -PSIn.Pos2DAsSeenByLight.y / PSIn.Pos2DAsSeenByLight.w / 2.0f + 0.5f;

	float diffuseLightingFactor = 0;
	if ((saturate(ProjectedTexCoords).x == ProjectedTexCoords.x) && (saturate(ProjectedTexCoords).y == ProjectedTexCoords.y))
	{
		float depthStoredInShadowMap = tex2D(ShadowMapSampler, ProjectedTexCoords).r;
		float realDistance = PSIn.Pos2DAsSeenByLight.z / PSIn.Pos2DAsSeenByLight.w;
		if ((realDistance - 1.0f / 350.0f) <= depthStoredInShadowMap)
		{
			diffuseLightingFactor = DotProduct(xLightPos, PSIn.Position3D, PSIn.Normal);
			diffuseLightingFactor = saturate(diffuseLightingFactor);
			diffuseLightingFactor *= xLightPower;
		}
	}

	float4 baseColor = tex2D(TextureSampler, PSIn.TexCoords);

	Output.Color = (diffuseLightingFactor + xAmbient)* baseColor  ;
	Output.Color.a = xAlpha;


	return Output;
}
technique ShadowedScene
{
	pass Pass0
	{
		VertexShader = compile VS_SHADERMODEL ShadowedSceneVertexShader();
		PixelShader = compile PS_SHADERMODEL ShadowedScenePixelShader();

	}
};
