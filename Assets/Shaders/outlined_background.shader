shader_type canvas_item;

uniform vec4 main_color : hint_color;
uniform vec4 outline_color : hint_color;

uniform float scale = 1;

float is_not_black(vec4 c) {
    return clamp(1000.0 * length(c.rgb), 0.0, 1.0);
}
float is_black(vec4 c) {
    return 1.0-is_not_black(c);
}

void fragment(){
    // COLOR = texture(TEXTURE, UV);
    COLOR = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0);
	float size_x = scale * SCREEN_PIXEL_SIZE.x;
	float size_y = scale * SCREEN_PIXEL_SIZE.y;
    float has_any_not_black_around = clamp(
        is_not_black(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(0, size_y), 0.0, 1.0), 0.0))
        + is_not_black(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(0, -size_y), 0.0, 1.0), 0.0))
        + is_not_black(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(size_x, 0), 0.0, 1.0), 0.0))
        + is_not_black(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(-size_x, 0), 0.0, 1.0), 0.0)), 0.0, 1.0);
    vec4 background_color = mix(main_color, outline_color, has_any_not_black_around);
    COLOR = mix(COLOR, background_color, is_black(COLOR));
}