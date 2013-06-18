using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ChildBirth.Rendering.Defaults
{
    class DefaultShader : Shader
    {
        private static String vSource =
            @"#version 120

            uniform mat4 projection_matrix;
            uniform mat4 modelview_matrix;
            uniform mat4 model_matrix;
            uniform mat4 rotation_matrix;
            uniform mat4 mesh_matrix;
            uniform mat4 shadow_bias;
            uniform mat4 light_view;
            uniform mat4 light_proj;

            uniform vec3 in_light;
            uniform vec3 in_eyepos;

            attribute vec3 in_normal;
            attribute vec3 in_position;
            attribute vec2 in_texture;
            attribute vec3 in_tangent;

            varying vec4 g_pos;
            varying vec3 v_eyedirection;
            varying vec3 v_normal;
            varying vec2 v_texture;
            varying vec3 v_tangent;
            varying vec3 v_bnormal;
            varying vec3 light;
            varying float v_depth;


            void main(void)
            {
	            mat4 toWorld = model_matrix * mesh_matrix;
	            g_pos = toWorld * vec4(in_position, 1);
	
	            gl_Position = projection_matrix * modelview_matrix * g_pos;
	
	            v_texture = in_texture;
	
	            v_eyedirection = normalize(g_pos.xyz - in_eyepos);
	
	            light = normalize(-g_pos.xyz + in_light);
	
	            v_normal =  (rotation_matrix * vec4(in_normal, 1.0)).xyz;
	            v_tangent = normalize(in_tangent);
	            v_bnormal = cross(v_normal, v_tangent);
            }";

        private static String fSource =
            @"#version 120

            uniform sampler2D baseTexture;
            uniform sampler2D normalTexture;

            varying vec4 g_pos;
            varying vec3 v_eyedirection;
            varying vec3 v_normal;
            varying vec2 v_texture;
            varying vec3 v_tangent;
            varying vec3 v_bnormal;
            varying vec3 light;
            varying float v_depth;

            void main(void)
            {
	            float diffuse = clamp(dot(normalize(v_normal), light), 0.0, 1.0);
	
	            float specular = pow(clamp(dot(normalize(reflect(v_eyedirection, v_normal)), light), 0.0, 1.0), 40);
	            vec3 texture = vec3(0.3, 0.3, 0.3) + vec3(1.0, 0.85, 0.85) * diffuse + vec3(1.0) * specular;
	
	            gl_FragColor = vec4(texture, 1.0);
            }";

        public DefaultShader()
        {
            this.VShader = vSource;
            this.FShader = fSource;

            Initialize();
        }
    }
}
