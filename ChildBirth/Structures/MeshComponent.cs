using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using OpenTK;
using ChildBirth.Simulation;

namespace ChildBirth.Structures
{
    class MeshComponent : SimObject
    {
        private MeshComponent() { }

        public MeshComponent(SimObject parent, Mesh mesh) : this(parent, mesh, new BoundingBox(mesh.PositionData))
        { }

        public MeshComponent(SimObject parent, Mesh mesh, BoundingBox box)
        {
            this.Parent = parent;
            this.mesh = mesh;
            this.box = box;
        }

        public SimObject Parent
        {
            set;
            get;
        }

        /// <summary>
        /// Private field and public property to store and access the mesh of the compoment
        /// </summary>
        private Mesh mesh;
        public Mesh Mesh
        {
            get { return this.mesh;  }
            set { this.mesh = value; }
        }

        /// <summary>
        /// Private field and public property to store and access the AABB of the component
        /// </summary>
        private BoundingBox box;
        public BoundingBox Box
        {
            set { this.box = value; }
            get { return this.box; }
        }

        /// <summary>
        /// Overriding Position property to make sure that the position of the
        /// component is reflected on thebounding box position
        /// </summary>
        public override Vector3 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                Box.Center = value;
                base.Position = value;
            }
        }

        public Quaternion GlobalOrientation
        {
            get { return Parent.Orientation * this.orientation; }
        }

        public Matrix4 GlobalOrientationMatrix
        {
            get { return Matrix4.Rotate(GlobalOrientation); }
        }

        /// <summary>
        /// Overriding Scale property to make sure that the scale of the
        /// component is reflected on the bounding box size
        /// </summary>
        public override Vector3 Scale
        {
            get
            {
                return base.Scale;
            }
            set
            {
                Box.Scale(value);
                base.Scale = value;
            }
        }
    }
}
