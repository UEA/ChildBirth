using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Drawing;
using ChildBirth.Rendering;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace ChildBirth.Loaders.Textures
{
    class TextureLoader : Loader
    {
        #region Singleton declaration
        private static TextureLoader instance = null;
        public static TextureLoader GetInstance()
        {
            if (instance == null)
                instance = new TextureLoader();
            return instance;
        }
        private TextureLoader() { } 
        #endregion

        public Texture GetTexture(String name)
        {
            return (Texture)base.GetObject(name);
        }

        protected override ContentObject Load(string name)
        {
            String uri = ConstructURIAndRefineName(ref name);

            Texture texture = new Texture();
			texture.Name = name;

			Bitmap bmp = new Bitmap(uri);

			texture.Id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, texture.Id);

			BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
			              OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
			
			bmp.UnlockBits(bmp_data);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

			library.Add(texture);

            return (ContentObject) texture;
        }

        protected override string ContentSubDirectory { get { return "Textures/"; } }
        protected override string FileExtension { get { return ".png"; } }
    }
}
