shader_type canvas_item;

uniform sampler2D heat_color;
uniform sampler2D select_color;
uniform vec4 symbol_color : hint_color;
uniform float speed = 1.0;
uniform float selected = 1.0;

const vec4 white = vec4(1);

float is_not_red(vec4 c) {
    return clamp(256.0 * (1.0 - c.r + c.g + c.b), 0.0, 1.0);
}

float neq(vec4 a, vec4 b) {
    return clamp(100.0 * distance(a, b) - 1.0, 0.0, 1.0);
}

float eq(vec4 a, vec4 b) {
    return 1.0 - neq(a, b);
}

float rand(float value) {
    return 0.5 + 0.5 * cos(5f * speed * value + cos(7f * speed * value));
}

void fragment(){
    vec4 original_color = texture(TEXTURE, UV);
    float heat_variation = 0.15 * (1.0 - selected) + (0.7 + 0.3 * selected) * rand(TIME);
    COLOR = original_color;
    COLOR.rgb = mix(original_color.rgb, white.rgb, 0.04*selected);
    COLOR = mix(texture(heat_color, vec2(heat_variation, 0)), COLOR, is_not_red(original_color));
    COLOR = mix(COLOR, texture(select_color, vec2(cos(24.0*TIME), 0)), eq(original_color, symbol_color) * selected);
}