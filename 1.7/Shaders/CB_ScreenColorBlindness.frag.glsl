#version 450

layout(location = 0) in vec2 vUv;
layout(location = 0) out vec4 outColor;

layout(set = 0, binding = 0) uniform sampler2D _MainTex;

layout(push_constant) uniform Push
{
    vec4 R; // @color @default = vec4(1.0, 0.0, 0.0, 1.0)
    vec4 G; // @color @default = vec4(0.0, 1.0, 0.0, 1.0)
    vec4 B; // @color @default = vec4(0.0, 0.0, 1.0, 1.0)
} push;

void main()
{
    vec4 color = texture(_MainTex, vUv);
    
    color.rgb = vec3(
            dot(color.rgb, push.R.rgb),
            dot(color.rgb, push.G.rgb),
            dot(color.rgb, push.B.rgb)
    );
    
    outColor = color;
}