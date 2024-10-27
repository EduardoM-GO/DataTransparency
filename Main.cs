using DataTransparency.src;
using StardewModdingAPI;
using StardewValley.Menus;
using StardewValley;
using StardewModdingAPI.Events;

namespace DataTransparency
{

    internal sealed class ModEntry : Mod
    {

        public override void Entry(IModHelper helper)
        {
            Monitor.Log("Hello, Stardew Valley!", LogLevel.Debug);

            helper.Events.Display.RenderedActiveMenu += OnRenderedActiveMenu;
        }


        private void OnRenderedActiveMenu(object? sender, RenderedActiveMenuEventArgs e)
        {
            if (Game1.activeClickableMenu is GameMenu gameMenu)
            {
                var currentPage = gameMenu.pages[gameMenu.currentTab];


                if (currentPage is SkillsPage skillsPage)
                {
                    Skills skills = new();

                    skills.ShowExpNeededNextsLevels(Game1.player, skillsPage);
                }
                else if (currentPage is SocialPage socialPage)
                {
                    Social social = new();

                    social.ShowExpNextHeartNpcs(Game1.player, socialPage);
                }

                return;
            }
        }

    }
}