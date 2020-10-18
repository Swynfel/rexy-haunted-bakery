shader_type canvas_item;

uniform vec4 escaped_wall_color : hint_color;
uniform vec4 escaped_outline_color : hint_color;
uniform vec4 main_color : hint_color;
uniform vec4 outline_color : hint_color;

float neq(vec4 a, vec4 b) {
    return clamp(100.0 * distance(a, b) - 1.0, 0.0, 1.0);
}

void fragment(){
    // COLOR = textureLod(TEXTURE, UV, 0.0);
    COLOR = mix(main_color, COLOR, neq(COLOR, escaped_wall_color));
    COLOR = mix(outline_color, MODULATE * COLOR, neq(COLOR, escaped_outline_color));
}