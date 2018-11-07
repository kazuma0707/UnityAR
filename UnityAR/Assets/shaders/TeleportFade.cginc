// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

sampler2D _FadeTex;
half _FadeRate;
half _ObjectFadeHeight;
half _RisePower;
half _SpreadPower;
half _TwistPower;
fixed4 _ParticleColor;
half _ObjectHeight;
float4 _ObjectBasePos;

float teleport_custom_vertex(inout VertexInput v)
{
	float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
	float height = lerp(_ObjectHeight, -_ObjectFadeHeight, _FadeRate);
	float warpRate = saturate((worldPos.y - (height + _ObjectBasePos.y)) / _ObjectFadeHeight);
	warpRate *= warpRate;
	worldPos.y += warpRate * _RisePower;
	float2 dir = worldPos.xz - _ObjectBasePos.xz;
	float rot = warpRate * _TwistPower;
	worldPos.x = cos(rot) * dir.x - sin(rot) * dir.y;
	worldPos.z = sin(rot) * dir.x + cos(rot) * dir.y;
	worldPos.xz *= _SpreadPower * warpRate + 1.0;
	worldPos.xz += _ObjectBasePos.xz;
	v.vertex = mul(unity_WorldToObject, worldPos);
	return warpRate;
}

#define TELEPORT_PACKEDDATA(o, vertex, warpRate) \
{\
	o.teleportPackedData.z = warpRate;\
	o.teleportPackedData.xw = UnityObjectToClipPos(vertex).xw;\
	o.teleportPackedData.y = 0.0;\
}\

#define CLIP_TELEPORT_FADE(packedData) clip(tex2D(_FadeTex, float2(_ScreenParams.x * packedData.x / packedData.w / 128.0, saturate(packedData.z - 0.01))).r - 0.1)
