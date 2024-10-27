using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace DataTransparency.src
{
    public enum Skill
    {
        Farming,
        Mining,
        Foraging,
        Fishing,
        Combat,
    }

    class Skills
    {
        readonly int[] expLevelsSkills = new int[]
                 {
                0,   // level 0
                100, // level 1
                380, // level 2
                770, // level 3
                1300,// level 4
                2150,// level 5
                3300,// level 6
                4800,// level 7
                6900,// level 8
                10000, // level 9
                15000 // level 10
                 };


        public void ShowExpNeededNextsLevels(Farmer player, SkillsPage skillsPage)
        {

            foreach (Skill skill in Enum.GetValues(typeof(Skill)))
            {

                int levelCurrent = GetLevelCurrent(player, skill);

                int expCurrent = GetExpCurrent(player, skill);

                int expNeeded = GetExpNeededNextLevel(levelCurrent, expCurrent);


                DrawExp(expNeeded, skill, skillsPage);
            }
        }

        private static int GetLevelCurrent(Farmer player, Skill skill)
        {
            return skill switch
            {
                Skill.Farming => player.FarmingLevel,
                Skill.Mining => player.MiningLevel,
                Skill.Fishing => player.FishingLevel,
                Skill.Combat => player.CombatLevel,
                Skill.Foraging => player.ForagingLevel,
                Skill => 0
            };
        }
        private static int GetExpCurrent(Farmer player, Skill skill)
        {
            int index = skill switch
            {
                Skill.Farming => 0,
                Skill.Mining => 3,
                Skill.Foraging => 2,
                Skill.Fishing => 1,
                Skill.Combat => 4,
                Skill => 5
            };

            return player.experiencePoints[index];
        }

        private int GetExpNeededNextLevel(int levelCurrent, int expCurrent)
        {
            if (levelCurrent >= 10)
            {
                return -1;
            }

            int expNeeded = expLevelsSkills[levelCurrent + 1] - expCurrent;

            return expNeeded;
        }


        private static void DrawExp(int expNeeded, Skill skill, SkillsPage skillsPage)
        {
            string? message = GetMessage(expNeeded);

            if (message is null)
            {
                return;
            }

            Vector2 position = GetPosition(message, skill, skillsPage);

            Game1.spriteBatch.DrawString(Game1.tinyFont, message, position, Game1.textColor);

        }

        private static Vector2 GetPosition(string message, Skill skill, SkillsPage skillsPage)
        {
            ClickableTextureComponent area = skillsPage.skillAreas[(int)skill];

            Vector2 messageSize = Game1.tinyFont.MeasureString(message);

            float width = skillsPage.xPositionOnScreen + skillsPage.width - messageSize.X - 100;

            Vector2 position = new(width, area.bounds.Top - 25);

            return position;
        }

        private static string? GetMessage(int expNeeded)
        {
            if (expNeeded >= 0)
            {
                return $"{expNeeded}";
            }

            return null;

        }
    }


}