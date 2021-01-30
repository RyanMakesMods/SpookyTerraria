using Microsoft.Xna.Framework;
using Terraria;

namespace SpookyTerraria.Utilities
{
    public class SlenderMain
    {
        public static short updateLogMode;
    }
    public class TransparencyHelper
    {
        public const short Visible = 0;
        public const short SlightlyTransparent = 15;
        public const short SomewhatTransparent = 30;
        public const short DecentlyTransparent = 60;
        public const short SemiTransparent = 100;
        public const short MostlyTransparent = 125;
        public const short HighlyTransparent = 155;
        public const short SuperTransparent = 180;
        public const short MegaTransparent = 205;
        public const short NearlyInvisible = 230;
        public const short Invisible = 255;
    }
    public class SlenderMusicID
    {
        public const short TheEightPages = 0;
        public const short SlendersShadow = 1;
        public const short SeventhStreet = 2;
    }
    public class PageID
    {
        public const short Follows = 1;
        public const short CantRun = 2;
        public const short Trees = 3;
        public const short LeaveMeAlone = 4;
        public const short AlwaysWatchesNoEyes = 5;
        public const short HelpMe = 6;
        public const short DontLook = 7;
        public const short NoX12 = 8;
    }
    public class SlenderMenuModeID
    {
        public const short SlenderExtras = 505;
        public const short SlenderChangeLogs = 1010;
    }
    public class TimeCreator
    {
        public static int AddSeconds(int seconds)
        {
            return seconds * 60;
        }
        public static int AddMinutes(int minutes)
        {
            return minutes * 60 * 60;
        }
        public static int AddHours(int hours)
        {
            return hours * 60 * 60 * 60;
        }
    }
    public class EaseOfAccess
    {
        /// <summary>
        /// Detects if <code>iniRect</code> and <code>collidingRect</code> are intersecting.
        /// </summary>
        /// <param name="iniRect"></param>
        /// <param name="collidingRect"></param>
        /// <returns></returns>
        public static bool Intersecting(Rectangle iniRect, Rectangle collidingRect)
        {
            return iniRect.Intersects(collidingRect);
        }
        /// <summary>
        /// checks <code>hoveringOver.Contains(Main.MouseScreen.ToPoint())</code> which results in checking if the mouse position is on top of said rectangle.
        /// </summary>
        /// <param name="hoveringOver"></param>
        /// <returns></returns>
        public static bool IsMouseOver(Rectangle hoveringOver)
        {
            return hoveringOver.Contains(Main.MouseScreen.ToPoint());
        }
    }
}