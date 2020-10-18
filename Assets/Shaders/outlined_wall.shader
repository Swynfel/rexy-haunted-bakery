shader_type canvas_item;

uniform vec4 escaped_wall_color : hint_color;
uniform vec4 escaped_outline_color : hint_color;
uniform vec4 main_color : hint_color;
uniform vec4 outline_color : hint_color;

uniform float scale = 1;

float is_not_black(vec4 c) {
    return clamp(1000.0 * length(c.rgb), 0.0, 1.0);
}
float is_black(vec4 c) {
    return 1.0-is_not_black(c);
}

float neq(vec4 a, vec4 b) {
    return clamp(100.0 * distance(a, b) - 1.0, 0.0, 1.0);
}

float is_not_wall(vec4 c) {
    return neq(c, escaped_wall_color) * neq(c, escaped_outline_color);
}

float is_wall(vec4 c) {
    return 1.0 - is_not_wall(c);
}

void fragment(){
    COLOR = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0);
	float size_x = scale * SCREEN_PIXEL_SIZE.x;
	float size_y = scale * SCREEN_PIXEL_SIZE.y;
    float has_only_walls_around = 
        is_wall(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(0, size_y), 0.0, 1.0), 0.0))
        * is_wall(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(0, -size_y), 0.0, 1.0), 0.0))
        * is_wall(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(size_x, 0), 0.0, 1.0), 0.0))
        * is_wall(textureLod(SCREEN_TEXTURE, clamp(SCREEN_UV + vec2(-size_x, 0), 0.0, 1.0), 0.0));
    // vec4 wall_color = mix(escaped_outline_color, escaped_wall_color, has_only_walls_around);
    // COLOR = mix(wall_color, COLOR, neq(COLOR, escaped_wall_color));
    vec4 wall_color = mix(outline_color, main_color, has_only_walls_around);
    COLOR = mix(wall_color, COLOR, neq(COLOR, escaped_wall_color));
}