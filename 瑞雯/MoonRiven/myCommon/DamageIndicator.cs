﻿namespace MoonRiven.myCommon
{
    using System;
    using System.Linq;
    using System.Globalization;

    using SharpDX;
    using SharpDX.Direct3D9;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;

    using Color = System.Drawing.Color;
    using Line = EloBuddy.SDK.Rendering.Line;

    internal static class DamageIndicator
    {
        private static readonly Vector2 BarOffset = new Vector2(1.25f, 14.25f);
        private static readonly Vector2 PercentOffset = new Vector2(-31, 3);
        private static readonly Vector2 PercentOffset1 = new Vector2(-30, 3);
        private static readonly Vector2 PercentOffset2 = new Vector2(-29, 3);

        private static Font _font;

        private static Line _line;

        internal static bool Fill = true;
        internal static bool Enabled = true;

        internal static Color Color = Color.FromArgb(255, 255, 169, 4);

        private static DamageToUnitDelegate _damageToUnit;

        internal delegate float DamageToUnitDelegate(AIHeroClient hero);

        internal static void AddToMenu(Menu mainMenu, DamageToUnitDelegate Damage = null)
        {
            _font = new Font(Drawing.Direct3DDevice, 
                new FontDescription
                {
                    FaceName = "Calibri",
                    Height = 11,
                    Weight = FontWeight.Bold,
                    Quality = FontQuality.ClearType,
                    OutputPrecision = FontPrecision.TrueType
                });

            _line = new Line
            {
                Antialias = true,
                Width = 9
            };

            mainMenu.AddSeparator();
            mainMenu.AddText("Damage Indicator");
            mainMenu.AddBool("DrawComboDamage", "Draw Combo Damage");
            mainMenu.AddBool("DrawFillDamage", "Draw Fill Color");

            DamageToUnit = Damage ?? DamageCalculate.GetComboDamage;

            Enabled = mainMenu.GetBool("DrawComboDamage");
            Fill = mainMenu.GetBool("DrawFillDamage");

            Game.OnTick += delegate
            {
                Enabled = mainMenu.GetBool("DrawComboDamage");
                Fill = mainMenu.GetBool("DrawFillDamage");
            };
        }

        internal static DamageToUnitDelegate DamageToUnit
        {
            set
            {
                if (_damageToUnit == null)
                {
                    Drawing.OnEndScene += Drawing_OnEndScene;
                }

                _damageToUnit = value;
            }
        }

        private static void Drawing_OnEndScene(EventArgs Args)
        {
            if (!Enabled || _damageToUnit == null)
            {
                return;
            }

            foreach (var unit in EntityManager.Heroes.Enemies.Where(u => u.IsValidTarget() && u.IsHPBarRendered))
            {
                var damage = _damageToUnit(unit);

                if (damage <= 0)
                {
                    continue;
                }

                if (unit.Health > 0)
                {
                    var text = ((int)(unit.Health - damage)).ToString(CultureInfo.InvariantCulture);

                    if (text.Length == 4)
                    {
                        Drawing.DrawText(unit.HPBarPosition + PercentOffset, Color.Red, text, 10);
                    }
                    else if (text.Length == 3)
                    {
                        Drawing.DrawText(unit.HPBarPosition + PercentOffset1, Color.Red, text, 10);
                    }
                    else if (text.Length == 2)
                    {
                        Drawing.DrawText(unit.HPBarPosition + PercentOffset2, Color.Red, text, 10);
                    }
                    else
                    {
                        Drawing.DrawText(unit.HPBarPosition + PercentOffset, Color.Red, text, 10);
                    }
                }

                if (Fill)
                {
                    var damagePercentage = (unit.TotalShieldHealth() - damage > 0 ? unit.TotalShieldHealth() - damage : 0) / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var currentHealthPercentage = unit.TotalShieldHealth() / (unit.MaxHealth + unit.AllShield + unit.AttackShield + unit.MagicShield);
                    var startPoint = new Vector2(unit.HPBarPosition.X + BarOffset.X + damagePercentage * 104, unit.HPBarPosition.Y + BarOffset.Y - 5);
                    var endPoint = new Vector2(unit.HPBarPosition.X + BarOffset.X + currentHealthPercentage * 104 + 1, unit.HPBarPosition.Y + BarOffset.Y - 5);

                    _line.Draw(Color, startPoint, endPoint);
                    Drawing.DrawLine(startPoint, endPoint, 9, Color);
                }
            }
        }
    }
}
