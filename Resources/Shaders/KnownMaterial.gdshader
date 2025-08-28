shader_type canvas_item;

varying vec3 pixel_color;

//void vertex() {
	//// Called for every vertex the material is visible on.
	//
//}

void fragment() {
	// Called for every pixel the material is visible on.
	pixel_color = COLOR.rgb;
}
//
void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
	LIGHT = vec4(0, 0, 0, 0);
}
