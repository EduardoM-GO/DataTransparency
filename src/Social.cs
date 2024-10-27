using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;

namespace DataTransparency.src
{
    class Social
    {

        public void ShowExpNextHeartNpcs(Farmer player, SocialPage socialPage)
        {
            for (int i = 0; i < socialPage.SocialEntries.Count; i++)
            {
                SocialPage.SocialEntry social = socialPage.SocialEntries[i];

                if (!player.friendshipData.ContainsKey(social.InternalName))
                {
                    continue;
                }

                var (expCurrent, expNextHeart) = GetExpsWithNpc(player, social);

                DrawExp(expCurrent, expNextHeart, socialPage, i);
            }
        }


        private static (int, int) GetExpsWithNpc(Farmer player, SocialPage.SocialEntry social)
        {

            int expCurrent = player.friendshipData[social.InternalName].Points;

            int hearts = social.HeartLevel;

            if (hearts == 14)
            {
                return (expCurrent, 3500);
            }

            int expNextHeart = (hearts + 1) * 250;

            return (expCurrent, expNextHeart);
        }

        private static void DrawExp(int expCurrent, int expNextHeart, SocialPage socialPage, int index)
        {

            string message = $"{expCurrent}/{expNextHeart}";

            Vector2 position = GetPosition(message, socialPage, index);

            Game1.spriteBatch.DrawString(Game1.smallFont, message, position, Game1.textColor);

        }

        private static Vector2 GetPosition(string message, SocialPage socialPage, int index)
        {

            ClickableTextureComponent component = socialPage.characterSlots[index];

            float width = socialPage.xPositionOnScreen + (socialPage.width / 2);

            Vector2 position = new(width, component.bounds.Top + 15);
            return position;
        }



    }

}