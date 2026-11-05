shader_type spatial;
stencil_mode write,1, compare_always;
uniform float visibility: hint_range(0.0, 1.0);
uniform vec3 colour: source_color;

void fragment() {
	ALBEDO = colour;
	ALPHA = visibility;
}