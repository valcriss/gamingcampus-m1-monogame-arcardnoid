using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace arcardnoid.Models.Services
{
    public class TextureService : ITextureService
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public TextureService(ArCardNoidGame game)
        {
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

        public ITexture Load(TextureType textureType)
        {
            string assetPath = GetAssetPath(textureType);
            Texture2D texture = Game.Content.Load<Texture2D>(assetPath);
            return new MonoGameTexture(Game, texture, textureType);
        }

        #endregion Public Methods

        #region Private Methods

        private string GetAssetPath(TextureType textureType)
        {
            switch (textureType)
            {
                case TextureType.LOGO_LOGO:
                    return "logo/logo";

                case TextureType.MAINMENU_LEFT:
                    return "mainmenu/left";

                case TextureType.MAINMENU_RIGHT:
                    return "mainmenu/right";

                case TextureType.MAINMENU_CLOUD1:
                    return "mainmenu/cloud1";

                case TextureType.MAINMENU_CLOUD2:
                    return "mainmenu/cloud2";

                case TextureType.UI_CURSOR:
                    return "ui/cursors/01";

                case TextureType.UI_VICTORY:
                    return "ui/victory";

                case TextureType.UI_DEFEATED:
                    return "ui/defeated";

                case TextureType.UI_BANNER:
                    return "ui/banner";

                case TextureType.UI_BANDEAU:
                    return "ui/bandeau";

                case TextureType.UI_PAGE:
                    return "ui/page";

                case TextureType.UI_COIN:
                    return "ui/coin";

                case TextureType.UI_HEART:
                    return "ui/heart";

                case TextureType.UI_HEADER:
                    return "ui/header";

                case TextureType.UI_INPUT:
                    return "ui/input";

                case TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL:
                    return "ui/buttons/button-green-normal";

                case TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER:
                    return "ui/buttons/button-green-hover";

                case TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED:
                    return "ui/buttons/button-green-pressed";

                case TextureType.UI_BUTTONS_BUTTON_RED_NORMAL:
                    return "ui/buttons/button-red-normal";

                case TextureType.UI_BUTTONS_BUTTON_RED_HOVER:
                    return "ui/buttons/button-red-hover";

                case TextureType.UI_BUTTONS_BUTTON_RED_PRESSED:
                    return "ui/buttons/button-red-pressed";

                case TextureType.UI_BUTTONS_BUTTON_BLUE_NORMAL:
                    return "ui/buttons/button-blue-normal";

                case TextureType.UI_BUTTONS_BUTTON_BLUE_HOVER:
                    return "ui/buttons/button-blue-hover";

                case TextureType.UI_BUTTONS_BUTTON_BLUE_PRESSED:
                    return "ui/buttons/button-blue-pressed";

                case TextureType.UI_PROGRESS_PROGRESS_NORMAL:
                    return "ui/progress/progress-normal";

                case TextureType.UI_PROGRESS_PROGRESS_HOVER:
                    return "ui/progress/progress-hover";

                case TextureType.UI_PROGRESS_PROGRESS_PRESSED:
                    return "ui/progress/progress-pressed";

                case TextureType.UI_BUTTONS_BUTTON_YELLOW_NORMAL:
                    return "ui/buttons/button-yellow-normal";

                case TextureType.UI_BUTTONS_BUTTON_YELLOW_HOVER:
                    return "ui/buttons/button-yellow-hover";

                case TextureType.UI_BUTTONS_BUTTON_YELLOW_PRESSED:
                    return "ui/buttons/button-yellow-pressed";

                case TextureType.MAP_UNITS_FACE_BACK:
                    return "map/units/face-back";

                case TextureType.MAP_UNITS_ARCHER_FACE:
                    return "map/units/archer-face";

                case TextureType.MAP_UNITS_WARRIOR_FACE:
                    return "map/units/warrior-face";

                case TextureType.MAP_UNITS_TORCH_FACE:
                    return "map/units/torch-face";

                case TextureType.MAP_UNITS_TNT_FACE:
                    return "map/units/tnt-face";

                case TextureType.MAP_UNITS_SHEEP_FACE:
                    return "map/units/sheep-face";

                case TextureType.MAP_UNITS_PLAYER_FACES:
                    return "map/units/player-faces";

                case TextureType.MAP_UNITS_PLAYER:
                    return "map/units/player";

                case TextureType.MAP_UNITS_ARCHER_BLUE_IDLE:
                    return "map/units/archer-blue-idle";

                case TextureType.MAP_UNITS_WARRIOR_BLUE_IDLE:
                    return "map/units/warrior-blue-idle";

                case TextureType.MAP_UNITS_TORCH_RED_IDLE:
                    return "map/units/torch-red-idle";

                case TextureType.MAP_UNITS_TNT_RED_IDLE:
                    return "map/units/tnt-red-idle";

                case TextureType.MAP_UNITS_PLAYER_BATTLE:
                    return "map/units/player-battle";

                case TextureType.MAP_UNITS_DEAD_1:
                    return "map/units/dead-1";

                case TextureType.MAP_UNITS_DEAD_2:
                    return "map/units/dead-2";

                case TextureType.MAP_HALT:
                    return "map/halt";

                case TextureType.BATTLE_BAR_PLAYER:
                    return "battle/bar-player";

                case TextureType.BATTLE_BAR_OPONENT:
                    return "battle/bar-opponent";

                case TextureType.BATTLE_PLAYER_BALL:
                    return "battle/player-ball";

                case TextureType.BATTLE_OPONENT_BALL:
                    return "battle/oponent-ball";

                case TextureType.PARTICLES_PARTICLEWHITE_4:
                    return "particles/particleWhite_4";

                case TextureType.MAP_WATER_SPLASH:
                    return "map/water_splash";

                case TextureType.MAP_SAND:
                    return "map/sand";

                case TextureType.MAP_ELEVATION:
                    return "map/elevation";

                case TextureType.MAP_GRASS:
                    return "map/grass";

                case TextureType.MAP_DECO_ROCKS_WATER_BIG:
                    return "map/deco/rocks_water_big";

                case TextureType.MAP_DECO_05:
                    return "map/deco/05";

                case TextureType.MAP_DECO_04:
                    return "map/deco/04";

                case TextureType.MAP_DECO_10:
                    return "map/deco/10";

                case TextureType.MAP_DECO_11:
                    return "map/deco/11";

                case TextureType.MAP_DECO_ROCKS_WATER_MEDIUM:
                    return "map/deco/rocks_water_medium";

                case TextureType.MAP_DECO_ROCKS_WATER_SMALL:
                    return "map/deco/rocks_water_small";

                case TextureType.MAP_BUILDING_WOOD_TOWER_RED:
                    return "map/building/wood-tower-red";

                case TextureType.MAP_BUILDING_GOBLIN_HOUSE:
                    return "map/building/goblin_house";

                case TextureType.MAP_BUILDING_TOWER_BLUE:
                    return "map/building/tower_blue";

                case TextureType.MAP_UNITS_SHEEP_IDLE:
                    return "map/units/sheep-idle";

                case TextureType.MAP_SHADOWS:
                    return "map/shadows";

                case TextureType.MAP_BRIDGE:
                    return "map/bridge";

                case TextureType.MAP_DECO_TREE:
                    return "map/deco/tree";

                case TextureType.MAP_UNITS_GOLD:
                    return "map/units/gold";

                case TextureType.MAP_UNITS_MEAT:
                    return "map/units/meat";
            }
            return null;
        }

        #endregion Private Methods
    }
}