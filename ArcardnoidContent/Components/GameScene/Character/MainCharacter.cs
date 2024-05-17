﻿using arcardnoid.Models.Framework.Tools;
using ArcardnoidContent.Components.Shared.Map.Models;
using ArcardnoidContent.Tools;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidContent.Components.GameScene.Character
{
    public class MainCharacter : GameComponent
    {
        #region Public Events

        public event Action<EncounterType, double> OnEncounter;

        #endregion Public Events

        #region Public Properties

        public Point CurrentCell { get; set; }

        #endregion Public Properties

        #region Private Properties

        private List<Point>? CurrentPath { get; set; } = null;
        private Point InitialCell { get; set; }
        private IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();
        private RandomMap RandomMap { get; set; }

        #endregion Private Properties

        #region Private Fields

        private Dictionary<CharacterDirection, Dictionary<AnimationState, List<Rectangle>>> _animations;
        private double _animationSpeed = 0.1;
        private double _animationTimer = 0;
        private int _currentFrame = 0;
        private int _currentPathIndex = 0;
        private CharacterDirection _direction = CharacterDirection.Right;
        private Rectangle _drawRectangle;
        private bool _forceDebug = false;
        private Rectangle _imageBounds;
        private MapHypothesis _mapHypotesis;
        private float _moveSpeed = 150;
        private AnimationState _state = AnimationState.Idle;
        private ITexture _texture;

        #endregion Private Fields

        #region Public Constructors

        public MainCharacter(RandomMap randomMap, MapHypothesis mapHypotesis, bool forceDebug = false) : base(((mapHypotesis.PlayerPositionX * 64) - 192 / 2) + 32, ((mapHypotesis.PlayerPositionY * 64) - 192 / 2) + 16, 64, 64)
        {
            _forceDebug = forceDebug;
            RandomMap = randomMap;
            _mapHypotesis = mapHypotesis;
            CurrentCell = new Point((int)mapHypotesis.PlayerPositionX, (int)mapHypotesis.PlayerPositionY);
            InitialCell = new Point((int)mapHypotesis.PlayerPositionX, (int)mapHypotesis.PlayerPositionY);
            _animations = new Dictionary<CharacterDirection, Dictionary<AnimationState, List<Rectangle>>>();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (_imageBounds == null || _drawRectangle == null || _texture == null) return;
            _texture.DrawTexture(_imageBounds, _drawRectangle, Color, Rotation, Point.Zero);
            DrawDebug();
        }

        public override void Load()
        {
            base.Load();
            _texture = GameServiceProvider.GetService<ITextureService>().Load("map/units/player");
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

        public void SetCurrentPath(List<Point>? path)
        {
            CurrentPath = path;
            _currentPathIndex = 0;
        }

        public void ToggleDebug()
        {
            _forceDebug = !_forceDebug;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            UpdateMove(delta);
            _animationTimer += delta;
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

        #endregion Public Methods

        #region Private Methods

        private static Rectangle GetRealPosition(float x, float y)
        {
            return new Rectangle((x * 64) - 192 / 2 + 32, (y * 64) - 192 / 2 + 16, 64, 64);
        }

        private bool CheckCollisionsOnCell()
        {
            EncounterType collision = _mapHypotesis.FinalChunk.CheckCollision((int)CurrentCell.X, (int)CurrentCell.Y);
            if (collision == EncounterType.None) return false;
            OnEncounter?.Invoke(collision, InitialCell.Distance(CurrentCell));
            return true;
        }

        private void DrawDebug()
        {
            if (_forceDebug && CurrentPath != null && CurrentPath.Count > 1)
            {
                for (int i = 0; i < CurrentPath.Count - 1; i++)
                {
                    Point p1 = new Point(RandomMap.Bounds.X + (CurrentPath[i].X * 64) + 32, RandomMap.Bounds.Y + (CurrentPath[i].Y * 64) + 32);
                    Point p2 = new Point(RandomMap.Bounds.X + (CurrentPath[i + 1].X * 64) + 32, RandomMap.Bounds.Y + (CurrentPath[i + 1].Y * 64) + 32);
                    Primitives2D.DrawLine(ScreenManager.Scale(p1), ScreenManager.Scale(p2), GameColor.Red, 2f);
                }
            }
        }

        private List<Rectangle> GetAnimationRectangles(int y)
        {
            List<Rectangle> tmp = new List<Rectangle>();
            for (int x = 0; x < 1152; x = x + 192)
            {
                tmp.Add(new Rectangle(x, y, 192, 192));
            }
            return tmp;
        }

        private void UpdateMove(float delta)
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
                Point nextPosition = GetRealPosition(nextPoint.X, nextPoint.Y).Position;
                if (nextPosition.Distance(Bounds.Position) < 3f)
                {
                    _currentPathIndex++;
                    CurrentCell = nextPoint;
                    if (CheckCollisionsOnCell() || _currentPathIndex >= CurrentPath.Count - 1)
                    {
                        CurrentPath = null;
                        return;
                    }
                }
                Point direction = Point.Normalize(nextPosition - new Point(Bounds.Position.X, Bounds.Position.Y));

                float offsetX = direction.X * _moveSpeed * (float)delta;
                float offsetY = direction.Y * _moveSpeed * (float)delta;
                Bounds = new Rectangle(Bounds.Position.X + offsetX, Bounds.Position.Y + offsetY, 64, 64);
            }
        }

        #endregion Private Methods
    }
}