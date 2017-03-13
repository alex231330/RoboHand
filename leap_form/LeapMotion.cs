using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.Drawing;
using System.Drawing.Imaging;

namespace leap_form
{
    class LeapMotion : Listener
    {
        public float cordX, cordX1, cordX2, cordX3, cordX4, cordX5, cordX6, cordX7, cordX8, cordX9, cordX10, cordX11, cordZ11, cordZ17, cordX12, cordX13, cordX14, cordX15, cordX16, cordX17, cordX18, cordX19, cordX20;
        public float cordZ, cordY, cordY1,  cordY2, cordY3, cordY4, cordY5, cordY6, cordY7, cordY8, cordY9, cordY10, cordY11, cordY12, cordY13, cordY14, cordY15, cordY16, cordY17, cordY18, cordY19, cordY20;

        public float cordZ14, cordZ19;
        public float cordZ1, cordZ2, cordZ3, cordZN, cordZN2, cordZN3, cordZ4, cordZ8, cordZ12;                                                                                                                                                                                                                                                                                                                                            

        public float rotation = 0;

        public int[] h = new int[3]; 

        BitmapData bitmapData;

        public int[] t = new int[3];

        public Bitmap bitmap;

        public float[] wrist = new float[3];

        public override void OnFrame(Controller controller)
        {
            controller.SetPolicy(Controller.PolicyFlag.POLICY_IMAGES);
            controller.SetPolicy(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
            controller.SetPolicy(Controller.PolicyFlag.POLICYBACKGROUNDFRAMES);
            Frame frame = controller.Frame();
            foreach (Hand hand in frame.Hands)
            {
                foreach (Finger finger in hand.Fingers)
                {
                    Leap.Image image = frame.Images[0];
                    bitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    ColorPalette grayscale = bitmap.Palette;
                    for (int i = 0; i < 255; i++)
                    {
                        grayscale.Entries[i] = Color.FromArgb((int)255, i, i, i);
                    }
                    bitmap.Palette = grayscale;
                    Rectangle lockArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    bitmapData = bitmap.LockBits(lockArea, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    byte[] rawImageData = image.Data;
                    System.Runtime.InteropServices.Marshal.Copy(rawImageData, 0, bitmapData.Scan0, image.Width * image.Height);
                    bitmap.UnlockBits(bitmapData);
                    bitmap.Dispose();
                    Bone bone;

                    foreach (Bone.BoneType boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType)))
                    {
                        bone = finger.Bone(boneType);
                        if (hand.IsRight)
                        { 
                            t[0] = (int)hand.PalmPosition.x + 300;
                            t[1] = (int)hand.PalmPosition.z + 300;
                            t[2] = (int)hand.PalmPosition.y + 300;
                            h[0] = (int)hand.WristPosition.x + 300;
                            h[1] = (int)hand.WristPosition.y + 300;
                            h[2] = (int)hand.WristPosition.z + 300;
                            wrist[0] = hand.WristPosition.x + 300;
                            wrist[1] = hand.WristPosition.y + 300;
                            wrist[2] = hand.WristPosition.z + 300;
                            rotation = hand.RotationAngle(frame);

                            if (finger.Type == Finger.FingerType.TYPE_MIDDLE)
                            {
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX = bone.PrevJoint.x + 300;
                                    cordY = bone.PrevJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_INTERMEDIATE)
                                {
                                    cordX1 = bone.NextJoint.x + 300;
                                    cordY1 = bone.NextJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX2 = bone.PrevJoint.x + 300;
                                    cordY2 = bone.PrevJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_METACARPAL)
                                {
                                    cordX3 = bone.PrevJoint.x + 300;
                                    cordY3 = bone.PrevJoint.z + 300;
                                }
                            }

                            if (finger.Type == Finger.FingerType.TYPE_INDEX)
                            {
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX4 = bone.PrevJoint.x + 300;
                                    cordY4 = bone.PrevJoint.z + 300;
                                    cordZ4 = bone.PrevJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_INTERMEDIATE)
                                {
                                    cordX5 = bone.NextJoint.x + 300;
                                    cordY5 = bone.NextJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX6 = bone.PrevJoint.x + 300;
                                    cordY6 = bone.PrevJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_METACARPAL)
                                {
                                    cordX7 = bone.PrevJoint.x + 300;
                                    cordY7 = bone.PrevJoint.z + 300;
                                }
                            }

                            if (finger.Type == Finger.FingerType.TYPE_PINKY)
                            {
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX8 = bone.PrevJoint.x + 300;
                                    cordY8 = bone.PrevJoint.z + 300;
                                    cordZ8 = bone.PrevJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_INTERMEDIATE)
                                {
                                    cordX9 = bone.NextJoint.x + 300;
                                    cordY9 = bone.NextJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX10 = bone.PrevJoint.x + 300;
                                    cordY10 = bone.PrevJoint.z + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_METACARPAL)
                                {
                                    cordX11 = bone.PrevJoint.x + 300;
                                    cordY11 = bone.PrevJoint.z + 300;
                                    cordZ11 = bone.PrevJoint.y + 300;
                                }
                            }

                            if (finger.Type == Finger.FingerType.TYPE_RING)
                            {
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX12 = bone.PrevJoint.x + 300;
                                    cordY12 = bone.PrevJoint.z + 300;
                                    cordZ12 = bone.PrevJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_INTERMEDIATE)
                                {
                                    cordX13 = bone.NextJoint.x + 300;
                                    cordY13 = bone.NextJoint.z + 300;
                                    cordZ14 = bone.NextJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_PROXIMAL)
                                {
                                    cordX14 = bone.PrevJoint.x + 300;
                                    cordY14 = bone.PrevJoint.z + 300;
                                    cordZ14 = bone.NextJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_METACARPAL)
                                {
                                    cordX15 = bone.PrevJoint.x + 300;
                                    cordY15 = bone.PrevJoint.z + 300;
                                }
                            }
                            if (finger.Type == Finger.FingerType.TYPE_THUMB)
                            {
                                if (boneType == Bone.BoneType.TYPE_DISTAL)
                                {
                                    cordX16 = bone.PrevJoint.x + 300;
                                    cordY16 = bone.PrevJoint.z + 300;
                                    cordZ = bone.PrevJoint.y + 300;
                                    cordZN = bone.NextJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_INTERMEDIATE)
                                {
                                    cordX17 = bone.NextJoint.x + 300;
                                    cordY17 = bone.NextJoint.z + 300;
                                    cordZ2 = bone.PrevJoint.y + 300;
                                    cordZ17 = bone.NextJoint.y + 300;
                                    cordZ3 = bone.PrevJoint.y + 300;

                                }
                                if (boneType == Bone.BoneType.TYPE_PROXIMAL)
                                {
                                    cordX18 = bone.PrevJoint.x + 300;
                                    cordY18 = bone.PrevJoint.z + 300;
                                    cordZN2 = bone.NextJoint.y + 300;
                                }
                                if (boneType == Bone.BoneType.TYPE_METACARPAL)
                                {
                                    cordX19 = bone.PrevJoint.x + 300;
                                    cordZ19 = bone.PrevJoint.y + 300;
                                    cordY19 = bone.PrevJoint.z + 300;
                                    cordZ1 = bone.PrevJoint.y + 300;
                                    cordZN3 = bone.NextJoint.y + 300;
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
