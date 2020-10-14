shader_type canvas_item;

uniform vec4 inline_color : hint_color;
uniform vec4 outline_color : hint_color;
uniform vec4 background_color : hint_color;
uniform vec4 wall_color : hint_color;

float neq(vec4 a, vec4 b) {
    return clamp(100.0 * distance(a, b) - 1.0, 0.0, 1.0);
}

void fragment(){
    // COLOR = textureLod(SCREEN_TEXTURE, SCREEN_UV, 0.0);
	float size_x = 1.0/float(textureSize(TEXTURE, 0).x);
	float size_y = 1.0/float(textureSize(TEXTURE, 0).y);
    vec4 center = texture(TEXTURE, UV);
    vec4 left = texture(TEXTURE, UV + vec2(-size_x, 0));
    vec4 right = texture(TEXTURE, UV + vec2(size_x, 0));
    vec4 up = texture(TEXTURE, UV + vec2(0, -size_y));
    vec4 down = texture(TEXTURE, UV + vec2(0, size_y));
    float not_back_center = neq(background_color, center);
    float not_back_left = neq(background_color, left) * neq(inline_color, left);
    float not_back_right = neq(background_color, right) * neq(inline_color, right);
    float not_back_up = neq(background_color, up) * neq(inline_color, up);
    float not_back_down = neq(background_color, down) * neq(inline_color, down);
    float something_around = clamp(not_back_right + not_back_left + not_back_down + not_back_up, 0.0, 1.0);
    float wall_center = neq(wall_color, center);
    float not_back_around = not_back_right * not_back_left * not_back_down * not_back_up;
    // wall_center==0 && not_back_around==0 => outline_color
    // not_back_center==0 && something_around==1 => inline_color
    // else => center
    COLOR = mix(center, outline_color, (1.0 - wall_center) * (1.0 - not_back_around));
    COLOR = mix(COLOR, inline_color, (1.0 - not_back_center) * something_around);
}