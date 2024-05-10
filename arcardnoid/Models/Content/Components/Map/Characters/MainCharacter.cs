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
        private RandomMap RandomMap { get; set; }
        private List<Point> CurrentPath { get; set; } = null;
        private int _currentPathIndex = 0;
        private bool _forceDebug = false;
        public Point CurrentCell { get; set; }

        private float _moveSpeed = 150;

        #endregion Private Fields

        #region Public Constructors

        public MainCharacter(RandomMap randomMap, MapHypothesis mapHypotesis, bool forceDebug = false) : base("MainCharacter", ((mapHypotesis.PlayerPositionX * 64) - 192 / 2) + 32, ((mapHypotesis.PlayerPositionY * 64) - 192 / 2) + 16, 64, 64)
        {
            _forceDebug = forceDebug;
            RandomMap = randomMap;
            _mapHypotesis = mapHypotesis;
            CurrentCell = new Point((int)mapHypotesis.PlayerPositionX, (int)mapHypotesis.PlayerPositionY);
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

        public void ToggleDebug()
        {
            _forceDebug = !_forceDebug;
        }

        public void SetCurrentPath(List<Point> path)
        {
            CurrentPath = path;
            _currentPathIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateMove(gameTime);
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

        private MonoGame.Extended.RectangleF GetRealPosition(float x, float y)
        {
            return new MonoGame.Extended.RectangleF((x * 64) - 192 / 2 + 32, (y * 64) - 192 / 2 + 16, 64, 64);
        }

        private void UpdateMove(GameTime gameTime)
        {
            if (CurrentPath == null || CurrentPath.Count <= 1)
            {
                _state = AnimationState.Idle;
                Bounds = GetRealPosition(CurrentCell.X, CurrentCell.Y);
                return;
            }
            else
            {
                Point nextPoint = CurrentPath[_currentPathIndex + 1];
                if (nextPoint.X >= CurrentCell.X)
                {
                    _direction = CharacterDirection.Right;
                }
                else
                {
                    _direction = CharacterDirection.Left;
                }
                _state = AnimationState.Walking;
                Vector2 nextPosition = GetRealPosition(nextPoint.X, nextPoint.Y).Position;
                if (nextPosition.Distance(Bounds.Position) < 3f)
                {
                    _currentPathIndex++;
                    CurrentCell = nextPoint;
                    if (_currentPathIndex >= CurrentPath.Count - 1)
                    {
                        CurrentPath = null;                        
                        return;
                    }
                }
                Vector2 direction = Vector2.Normalize(nextPosition - new Vector2(Bounds.Position.X, Bounds.Position.Y));

                float offsetX = direction.X * _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                float offsetY = direction.Y * _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Bounds = new MonoGame.Extended.RectangleF(Bounds.Position.X + offsetX, Bounds.Position.Y + offsetY, 64, 64);
            }
        }

        public override void Draw()
        {
            base.Draw();
            Game.SpriteBatch.Draw(_texture, ScreenManager.Scale(_imageBounds), _drawRectangle, Color, MathHelper.ToRadians(Rotation), Vector2.Zero, SpriteEffects.None, 0);
            DrawDebug();
        }

        private void DrawDebug()
        {
            if (_forceDebug && CurrentPath != null && CurrentPath.Count > 1)
            {
                for (int i = 0; i < CurrentPath.Count - 1; i++)
                {
                    Vector2 p1 = new Vector2(RandomMap.Bounds.X + (CurrentPath[i].X * 64) + 32, RandomMap.Bounds.Y + (CurrentPath[i].Y * 64) + 32);
                    Vector2 p2 = new Vector2(RandomMap.Bounds.X + (CurrentPath[i + 1].X * 64) + 32, RandomMap.Bounds.Y + (CurrentPath[i + 1].Y * 64) + 32);
                    Primitives2D.DrawLine(Game.SpriteBatch, ScreenManager.Scale(p1), ScreenManager.Scale(p2), Color.Red, 2f);
                }
            }
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