using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace ChildBirth.Simulation
{
    // This is a base class for all the objects participating in the simulation
    public class SimObject
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// Private object fields
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// A vector representing the position of the object
        /// </summary>
        protected Vector3 position = new Vector3();

        /// <summary>
        /// A vector representing the scale of the object in the respective axes
        /// </summary>
        protected Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);

        /// <summary>
        /// A quaternion representation of the object's orientation
        /// </summary>
        protected Quaternion orientation = Quaternion.Identity;

        /// <summary>
        /// A matrix containing all the above transformations combined
        /// </summary>
        protected Matrix4 transform = Matrix4.Identity;

        /// <summary>
        /// Private boolean indicating if the transform matrix needs to be updated
        /// </summary>
        private bool transformNeedsUpdate = true;

        /// <summary>
        /// Private field to store the name of the object
        /// </summary>
        private String name;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// Public access properties and methods
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// A property providing access to the position of the object
        /// Note: the setter initiates transform update
        /// </summary>
        public virtual Vector3 Position
        {
            get { return position; }
            set
            {
                if (position == value)
                    return;

                position = value;
                transformNeedsUpdate = true;
            }
        }

        /// <summary>
        /// A property providing access to the scale of the object
        /// Note: the setter initiates transform update
        /// </summary>
        public virtual Vector3 Scale
        {
            set
            {
                if (scale == value)
                    return;
                scale = value;
                transformNeedsUpdate = true;
            }
            get { return scale; }
        }

        /// <summary>
        /// A property proving access to the orientation of the object
        /// Note: the setter initiates transform update
        /// </summary>
        public virtual Quaternion Orientation
        {
            get { return orientation; }
            set
            {
                if (orientation == value)
                    return;
                orientation = value;
                transformNeedsUpdate = true;
            }
        }

        /// <summary>
        /// A property proving access to the combined transformation matrix of the object
        /// Note: in the getter a check is performed if the matrix needs to updated
        /// due to the modification of the position, scale and orientation values
        /// </summary>
        public virtual Matrix4 Transform
        {
            get { 
                if(transformNeedsUpdate)
                    UpdateTransform();
                return transform; 
            }
            set { transform = value; }
        }

        /// <summary>
        /// A method to perform matrix udpate
        /// </summary>
        public void UpdateTransform()
        {
            transform =
                Matrix4.Rotate(orientation) *
                Matrix4.Scale(scale) *
                Matrix4.CreateTranslation(position);
            transformNeedsUpdate = false;
        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// Virtual methods
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// A virtual method to be overriden by subclasses, which is used to render the object
        /// </summary>
        public virtual void Render()
        {

        }
        
        
        /// <summary>
        /// A virtual method to be overriden by subclasses, which is used to render the object using a shader
        /// </summary>
        /// <param name="shader">A shader object to be used while rendering the object</param>
        public virtual void Render(Shader shader)
        {

        }

        /// <summary>
        /// A virtual method to be overriden by subclasses, which is used to render the object when performing a multipass rendering
        /// </summary>
        /// <param name="pass">A render pass object to indicate which pass is currently being rendered</param>
        public virtual void Render(RenderPass pass)
        {

        }

        /// <summary>
        /// A virtual method to be overriden by subclasses, which is used to update the state of the object as required
        /// </summary>
        public virtual void Update(double time)
        {

        }

    }
}