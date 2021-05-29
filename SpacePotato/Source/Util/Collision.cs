using Microsoft.Xna.Framework;

namespace SpacePotato {
    public static class Collision {
        public static bool rect(Vector2 pos1, Vector2 dimen1, Vector2 pos2, Vector2 dimen2) {
            
            return (pos1.X - dimen1.X/2F <= pos2.X + dimen2.X/2F &&
                    pos1.X + dimen1.X/2F >= pos2.X - dimen2.X/2F &&
                    pos1.Y - dimen1.Y/2F <= pos2.Y + dimen2.Y/2F &&
                    pos1.Y + dimen1.Y/2F >= pos2.Y - dimen2.Y/2F);
        }

        public static bool circle(Vector2 pos1, float radius1, Vector2 pos2, float radius2) {
            return Util.mag(pos2 - pos1) <= radius1 + radius2;
        }

        public static bool pointCircle(Vector2 point, Vector2 circlePos, float radius) {
            return Util.mag(point - circlePos) <= radius;
        }

        public static bool rectCircle(Vector2 rectPos, Vector2 rectDimen, Vector2 circlePos, float radius) {
            Vector2 tl = rectPos - rectDimen / 2F;
            return (pointCircle(tl, circlePos, radius) ||
                    pointCircle(tl + rectDimen * Vector2.UnitX, circlePos, radius) ||
                    pointCircle(tl + rectDimen * Vector2.UnitY, circlePos, radius) ||
                    pointCircle(tl + rectDimen, circlePos, radius));
        }
    }
}