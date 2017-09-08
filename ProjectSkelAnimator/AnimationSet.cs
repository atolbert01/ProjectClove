using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectSkelAnimator
{
    /// <summary>
    /// Defines a set of Animations.
    /// </summary>
    class AnimationGroup
    {
        public string GroupName { get; set; }
        public Animation[] Animations { get; set; }
        public Animation CurrentAnimation { get; set; }

        public AnimationGroup(){}

        public AnimationGroup(Animation newAnim)
        {
            CurrentAnimation = newAnim;
            Animations = new Animation[1];
            Animations[0] = CurrentAnimation;
        }

        public void AddAnimation(Animation newAnim)
        {
            Animation[] newAnims = new Animation[Animations.Length + 1];
            for (int i = 0; i < Animations.Length; i++)
            {
                newAnims[i] = Animations[i];
            }
            newAnims[Animations.Length] = newAnim;
            Animations = newAnims;
            CurrentAnimation = Animations[Animations.Length - 1];
        }

        public void RemoveAnimation(Animation removedAnim)
        {
            if (Animations.Length != 0)
            {
                int newSize = Animations.Length - 1;
                Animation[] resizedAnims = new Animation[newSize];

                int j = 0;
                for (int i = 0; i < Animations.Length; i++)
                {
                    if (Animations[i] != removedAnim)
                    {
                        resizedAnims[j] = Animations[i];
                        //CurrentFrameIndex = j;
                        CurrentAnimation = Animations[j];
                        j++;
                    }
                }
                Animations = resizedAnims;

                if (Animations.Length == 0)
                {
                    CurrentAnimation = null;
                    //CurrentFrameIndex = 0;
                }
            }
        }
    }
}
