using arcardnoid.Models.Content.Components.Map.Models;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.Map.Characters
{
    public enum AnimationState
    {
        Idle,
        Walking,
        Attacking
    }

    public enum CharacterDirection
    {
        Left,
        Right
    }

    public class MainCharacter : Component
    {
        #region Private Fields

        private Dictionary<CharacterDirection, Dictionary<AnimationState, List<Rectangle>>> _animations;
        private CharacterDirection _direction = CharacterDirection.Right;
        private MapHypothesis _mapHypotesis;
        private AnimationState _state = AnimationState.Idle;
        private Texture2D _texture;
        private Rectangle _drawRectangle;
        private Rectangle _imageBounds;
        private int _currentFrame = 0;
        private double _animationSpeed = 0.1;
        private double _animationTimer = 0;

        #endregion Private Fields

        #region Public Constructors

        public MainCharacter(MapHypothesis mapHypotesis) : base("MainCharacter", ((mapHypotesis.PlayerPositionX * 64) - 192 / 2) + 32, ((mapHypotesis.PlayerPositionY * 64) - 192 / 2) + 16, 64, 64)
        {
            _mapHypotesis = mapHypotesis;
            _animations = new Dictionary<CharacterDirection, Dictionary<AnimationState, List<Rectangle>>>();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            _texture = Game.Content.Load<Texture2D>("map/units/player");
            _animations.Add(CharacterDirection.Left, new Dictionary<AnimationState, List<Rectangle>>
            {
                { AnimationState.Idle, GetAnimationRectangles(192) },
                { AnimationState.Walking,GetAnimationRectangles(576) },
                { AnimationState.Attacking, GetAnimationRectangles(960) }
            });
            _animations.Add(CharacterDirection.Right, new Dictionary<AnimationState, List<Rectangle>>
            {
                { AnimationState.Idle, GetAnimationRectangles(0) },
                { AnimationState.Walking,GetAnimationRectangles(384) },
                { AnimationState.Attacking, GetAnimationRectangles(768) }
            });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_animationTimer >= _animationSpeed)
            {
                _animationTimer = 0;
                _currentFrame++;
                if (_currentFrame >= _animations[_direction][_state].Count)
                {
                    _currentFrame = 0;
                }
            }
            _imageBounds = new Rectangle((int)Bounds.X + _mapHypotesis.PositionX, (int)Bounds.Y + _mapHypotesis.PositionY, 192, 192);
            _drawRectangle = _animations[_direction][_state][_currentFrame];
        }

        public override void Draw()
        {
            base.Draw();
            Game.SpriteBatch.Draw(_texture, ScreenManager.Scale(_imageBounds), _drawRectangle, Color, MathHelper.ToRadians(Rotation), Vector2.Zero, SpriteEffects.None, 0);
        }

        #endregion Public Methods

        #region Private Methods

        private List<Rectangle> GetAnimationRectangles(int y)
        {
            List<Rectangle> tmp = new List<Rectangle>();
            for (int x = 0; x < 1152; x = x + 192)
            {
                tmp.Add(new Rectangle(x, y, 192, 192));
            }
            return tmp;
        }

        #endregion Private Methods
    }
}