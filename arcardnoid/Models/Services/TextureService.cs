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
            return (textureType) switch
            {
                TextureType.LOGO_LOGO => "logo/logo",
                TextureType.MAINMENU_LEFT => "mainmenu/left",
                TextureType.MAINMENU_RIGHT => "mainmenu/right",
                TextureType.MAINMENU_CLOUD1 => "mainmenu/cloud1",
                TextureType.MAINMENU_CLOUD2 => "mainmenu/cloud2",
                TextureType.UI_CURSOR => "ui/cursors/01",
                TextureType.UI_VICTORY => "ui/victory",
                TextureType.UI_DEFEATED => "ui/defeated",
                TextureType.UI_BANNER => "ui/banner",
                TextureType.UI_BANDEAU => "ui/bandeau",
                TextureType.UI_PAGE => "ui/page",
                TextureType.UI_COIN => "ui/coin",
                TextureType.UI_HEART => "ui/heart",
                TextureType.UI_HEADER => "ui/header",
                TextureType.UI_INPUT => "ui/input",
                TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL => "ui/buttons/button-green-normal",
                TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER => "ui/buttons/button-green-hover",
                TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED => "ui/buttons/button-green-pressed",
                TextureType.UI_BUTTONS_BUTTON_RED_NORMAL => "ui/buttons/button-red-normal",
                TextureType.UI_BUTTONS_BUTTON_RED_HOVER => "ui/buttons/button-red-hover",
                TextureType.UI_BUTTONS_BUTTON_RED_PRESSED => "ui/buttons/button-red-pressed",
                TextureType.UI_BUTTONS_BUTTON_BLUE_NORMAL => "ui/buttons/button-blue-normal",
                TextureType.UI_BUTTONS_BUTTON_BLUE_HOVER => "ui/buttons/button-blue-hover",
                TextureType.UI_BUTTONS_BUTTON_BLUE_PRESSED => "ui/buttons/button-blue-pressed",
                TextureType.UI_PROGRESS_PROGRESS_NORMAL => "ui/progress/progress-normal",
                TextureType.UI_PROGRESS_PROGRESS_HOVER => "ui/progress/progress-hover",
                TextureType.UI_PROGRESS_PROGRESS_PRESSED => "ui/progress/progress-pressed",
                TextureType.UI_BUTTONS_BUTTON_YELLOW_NORMAL => "ui/buttons/button-yellow-normal",
                TextureType.UI_BUTTONS_BUTTON_YELLOW_HOVER => "ui/buttons/button-yellow-hover",
                TextureType.UI_BUTTONS_BUTTON_YELLOW_PRESSED => "ui/buttons/button-yellow-pressed",
                TextureType.MAP_UNITS_FACE_BACK => "map/units/face-back",
                TextureType.MAP_UNITS_ARCHER_FACE => "map/units/archer-face",
                TextureType.MAP_UNITS_WARRIOR_FACE => "map/units/warrior-face",
                TextureType.MAP_UNITS_TORCH_FACE => "map/units/torch-face",
                TextureType.MAP_UNITS_TNT_FACE => "map/units/tnt-face",
                TextureType.MAP_UNITS_SHEEP_FACE => "map/units/sheep-face",
                TextureType.MAP_UNITS_PLAYER_FACES => "map/units/player-faces",
                TextureType.MAP_UNITS_PLAYER => "map/units/player",
                TextureType.MAP_UNITS_ARCHER_BLUE_IDLE => "map/units/archer-blue-idle",
                TextureType.MAP_UNITS_WARRIOR_BLUE_IDLE => "map/units/warrior-blue-idle",
                TextureType.MAP_UNITS_TORCH_RED_IDLE => "map/units/torch-red-idle",
                TextureType.MAP_UNITS_TNT_RED_IDLE => "map/units/tnt-red-idle",
                TextureType.MAP_UNITS_PLAYER_BATTLE => "map/units/player-battle",
                TextureType.MAP_UNITS_DEAD_1 => "map/units/dead-1",
                TextureType.MAP_UNITS_DEAD_2 => "map/units/dead-2",
                TextureType.MAP_HALT => "map/halt",
                TextureType.BATTLE_BAR_PLAYER => "battle/bar-player",
                TextureType.BATTLE_BAR_OPONENT => "battle/bar-opponent",
                TextureType.BATTLE_PLAYER_BALL => "battle/player-ball",
                TextureType.BATTLE_OPONENT_BALL => "battle/oponent-ball",
                TextureType.PARTICLES_PARTICLEWHITE_4 => "particles/particleWhite_4",
                TextureType.MAP_WATER_SPLASH => "map/water_splash",
                TextureType.MAP_SAND => "map/sand",
                TextureType.MAP_ELEVATION => "map/elevation",
                TextureType.MAP_GRASS => "map/grass",
                TextureType.MAP_DECO_ROCKS_WATER_BIG => "map/deco/rocks_water_big",
                TextureType.MAP_DECO_05 => "map/deco/05",
                TextureType.MAP_DECO_04 => "map/deco/04",
                TextureType.MAP_DECO_10 => "map/deco/10",
                TextureType.MAP_DECO_11 => "map/deco/11",
                TextureType.MAP_DECO_ROCKS_WATER_MEDIUM => "map/deco/rocks_water_medium",
                TextureType.MAP_DECO_ROCKS_WATER_SMALL => "map/deco/rocks_water_small",
                TextureType.MAP_BUILDING_WOOD_TOWER_RED => "map/building/wood-tower-red",
                TextureType.MAP_BUILDING_GOBLIN_HOUSE => "map/building/goblin_house",
                TextureType.MAP_BUILDING_TOWER_BLUE => "map/building/tower_blue",
                TextureType.MAP_UNITS_SHEEP_IDLE => "map/units/sheep-idle",
                TextureType.MAP_SHADOWS => "map/shadows",
                TextureType.MAP_BRIDGE => "map/bridge",
                TextureType.MAP_DECO_TREE => "map/deco/tree",
                TextureType.MAP_UNITS_GOLD => "map/units/gold",
                TextureType.MAP_UNITS_MEAT => "map/units/meat",
                TextureType.CARDS_11_UNITS => "cards/11-units",
                TextureType.CARDS_22_UNITS => "cards/22-units",
                TextureType.CARDS_200_GOLD => "cards/200-gold",
                TextureType.CARDS_CATACLYSM => "cards/cataclysm",
                TextureType.CARDS_MANA_STORM => "cards/mana-storm",
                TextureType.CARDS_SUPER_SPEED => "cards/super-speed",
                _ => null
            };
        }

        #endregion Private Methods
    }
}