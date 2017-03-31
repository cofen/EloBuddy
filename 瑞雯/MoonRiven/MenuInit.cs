namespace MoonRiven
{
    using myCommon;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    using System.Linq;

    internal class MenuInit : Logic
    {
        internal static void Init()
        {
            mainMenu = MainMenu.AddMenu("EB中文社区汉化", "Moon" + Player.Instance.ChampionName);
            {
                mainMenu.AddGroupLabel("Please Set the Orbwalker");
                mainMenu.AddGroupLabel("Orbwalker -> Advanced -> Update Event Listening -> Enabled On Update(more fast)");
                mainMenu.AddGroupLabel("--------------------");
                mainMenu.AddGroupLabel("One Things You Need to Know");
                mainMenu.AddGroupLabel("Combo/Burst/Harass Mode");
                mainMenu.AddGroupLabel("Dont Forgot Use Left Click to Select Target");
                mainMenu.AddGroupLabel("It will make the Addon More Better");
                mainMenu.AddGroupLabel("---------------------");
                mainMenu.AddGroupLabel("My GitHub: https://github.com/aiRTako/EloBuddy");
                mainMenu.AddGroupLabel("If you have Feedback pls post to my topic");
                mainMenu.AddGroupLabel("----------------------");
                mainMenu.AddGroupLabel("Credit: NightMoon & aiRTako");
            }

            comboMenu = mainMenu.AddSubMenu("连招", "Combo");
            {
                comboMenu.AddLine("Q");
                comboMenu.AddBool("ComboQGap", "使用Q追击");
                comboMenu.AddLine("W");
                comboMenu.AddBool("ComboW", "使用 W");
                comboMenu.AddBool("ComboWLogic", "使用W取消后摇");
                comboMenu.AddLine("E");
                comboMenu.AddBool("ComboE", "使用 E");
                comboMenu.AddBool("ComboEGap", "使用E追击");
                comboMenu.AddLine("R");
                comboMenu.AddKey("ComboR1", "使用 R1: ", KeyBind.BindTypes.PressToggle, 'G', true);
                comboMenu.AddList("ComboR2", "使用 R2 模式: ", new[] { "智能判定", "可以杀死时", "刚开始接触", "关闭" });
                comboMenu.AddLine("Others");
                comboMenu.AddBool("ComboItem", "使用 装备");
                comboMenu.AddBool("ComboYoumuu", "使用 幽梦");
                comboMenu.AddBool("ComboDot", "使用 点燃");
                comboMenu.AddLine("Burst");
            }

            burstMenu = mainMenu.AddSubMenu("爆发", "Burst");
            {
                burstMenu.AddBool("BurstFlash", "使用 闪现");
                burstMenu.AddBool("BurstDot", "使用 点燃");
                burstMenu.AddList("BurstMode", "爆发模式: ", new[] { "Shy Mode", "EQ Mode" });
                burstMenu.AddKey("BurstEnabledKey", "爆发快捷键: ", KeyBind.BindTypes.PressToggle, 'T');
                burstMenu.AddSeparator();
                burstMenu.AddLabel("如何使用爆发");
                burstMenu.AddLabel("1.你需要解绑游戏预制的快捷键");
                burstMenu.AddLabel("2.选择需要攻击的目标 (也可以不选择，但是选择后会针对目标进行爆发)");
                burstMenu.AddLabel("3.按住快捷键即可");
            }

            harassMenu = mainMenu.AddSubMenu("骚扰", "Harass");
            {
                harassMenu.AddLine("Q");
                harassMenu.AddBool("HarassQ", "使用 Q");
                harassMenu.AddLine("W");
                harassMenu.AddBool("HarassW", "使用 W");
                harassMenu.AddLine("E");
                harassMenu.AddBool("HarassE", "使用 E");
                harassMenu.AddLine("Mode");
                harassMenu.AddList("HarassMode", "骚扰 模式: ", new[] { "智能", "正常" });
            }

            clearMenu = mainMenu.AddSubMenu("清线", "Clear");
            {
                clearMenu.AddText("清线 设置");
                clearMenu.AddLine("LaneClear Q");
                clearMenu.AddBool("LaneClearQ", "使用 Q");
                clearMenu.AddBool("LaneClearQSmart", "使用 Q 智能 Farm");
                clearMenu.AddBool("LaneClearQT", "使用 Q 重置攻击后摇");
                clearMenu.AddLine("LaneClear W");
                clearMenu.AddBool("LaneClearW", "使用 W", false);
                clearMenu.AddSlider("LaneClearWCount", "使用 W 最小命中数 >= x", 3, 1, 10);
                clearMenu.AddLine("LaneClear Items");
                clearMenu.AddBool("LaneClearItem", "使用 物品");

                clearMenu.AddSeparator();

                clearMenu.AddText("清野 设置");
                clearMenu.AddLine("JungleClear Q");
                clearMenu.AddBool("JungleClearQ", "使用 Q");
                clearMenu.AddLine("JungleClear W");
                clearMenu.AddBool("JungleClearW", "使用 W");
                clearMenu.AddLine("JungleClear E");
                clearMenu.AddBool("JungleClearE", "使用 E");
                clearMenu.AddLine("JungleClear Items");
                clearMenu.AddBool("JungleClearItem", "使用 物品");

                ManaManager.AddSpellFarm(clearMenu);
            }

            fleeMenu = mainMenu.AddSubMenu("逃跑", "Flee");
            {
                fleeMenu.AddText("Q");
                fleeMenu.AddBool("FleeQ", "使用 Q");
                fleeMenu.AddText("W");
                fleeMenu.AddBool("FleeW", "使用 W");
                fleeMenu.AddText("E");
                fleeMenu.AddBool("FleeE", "使用 E");
            }

            killStealMenu = mainMenu.AddSubMenu("抢人头", "KillSteal");
            {
                killStealMenu.AddText("R");
                killStealMenu.AddBool("KillStealR", "使用 R");
            }

            miscMenu = mainMenu.AddSubMenu("杂项", "Misc");
            {
                miscMenu.AddText("Q");
                miscMenu.AddBool("KeepQ", "保持Q连续性");
                miscMenu.AddList("QMode", "Q 模式: ", new[] { "指向 目标", "指向 鼠标" });

                miscMenu.AddSeparator();
                miscMenu.AddText("W");
                miscMenu.AddBool("AntiGapcloserW", "刺客 接近");
                miscMenu.AddBool("InterruptW", "中断危险法术");

                miscMenu.AddSeparator();
                miscMenu.AddText("Animation");
                miscMenu.AddBool("manualCancel", "取消动画");
                miscMenu.AddBool("manualCancelPing", "Cancel Animation Calculate Ping?");
            }

            eMenu = mainMenu.AddSubMenu("Evade", "Evade");
            {  
                //TODO: 
                //1.Add dodge not unit Spell

                eMenu.AddText("Evade Opinion");
                eMenu.AddBool("evadeELogic", "开启E闪避");

                if (EntityManager.Heroes.Enemies.Any())
                {
                    foreach (var target in EntityManager.Heroes.Enemies)
                    {
                        eMenu.AddText(target.ChampionName + " Skills");

                        #region
                        //SelfAOE (not include the Karma E and?)

                        //if (target.Spellbook.GetSpell(SpellSlot.Q).SData.TargettingType != SpellDataTargetType.Self &&
                        //    target.Spellbook.GetSpell(SpellSlot.Q).SData.TargettingType != SpellDataTargetType.SelfAndUnit)
                        //{
                        //    eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.Q).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.Q).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.Q).SData.TargettingType == SpellDataTargetType.Unit);
                        //}

                        //if (target.Spellbook.GetSpell(SpellSlot.W).SData.TargettingType != SpellDataTargetType.Self &&
                        //    target.Spellbook.GetSpell(SpellSlot.W).SData.TargettingType != SpellDataTargetType.SelfAndUnit)
                        //{
                        //    eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.W).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.W).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.W).SData.TargettingType == SpellDataTargetType.Unit);
                        //}

                        //if (target.Spellbook.GetSpell(SpellSlot.E).SData.TargettingType != SpellDataTargetType.Self &&
                        //    target.Spellbook.GetSpell(SpellSlot.E).SData.TargettingType != SpellDataTargetType.SelfAndUnit)
                        //{
                        //    eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.E).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.E).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.E).SData.TargettingType == SpellDataTargetType.Unit);
                        //}

                        //if (target.Spellbook.GetSpell(SpellSlot.R).SData.TargettingType != SpellDataTargetType.Self &&
                        //    target.Spellbook.GetSpell(SpellSlot.R).SData.TargettingType != SpellDataTargetType.SelfAndUnit)
                        //{
                        //    eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.R).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.R).SData.Name,
                        //        target.Spellbook.GetSpell(SpellSlot.R).SData.TargettingType == SpellDataTargetType.Unit);
                        //}
                        #endregion

                        if (target.Spellbook.GetSpell(SpellSlot.Q).SData.TargettingType == SpellDataTargetType.Unit)
                        {
                            eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.Q).SData.Name,
                                target.Spellbook.GetSpell(SpellSlot.Q).SData.Name);
                        }

                        if (target.Spellbook.GetSpell(SpellSlot.W).SData.TargettingType == SpellDataTargetType.Unit)
                        {
                            eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.W).SData.Name,
                                target.Spellbook.GetSpell(SpellSlot.W).SData.Name);
                        }

                        if (target.Spellbook.GetSpell(SpellSlot.E).SData.TargettingType == SpellDataTargetType.Unit)
                        {
                            eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.E).SData.Name,
                                target.Spellbook.GetSpell(SpellSlot.E).SData.Name);
                        }

                        if (target.Spellbook.GetSpell(SpellSlot.R).SData.TargettingType == SpellDataTargetType.Unit)
                        {
                            eMenu.AddBool(target.ChampionName + "Skill" + target.Spellbook.GetSpell(SpellSlot.R).SData.Name,
                                target.Spellbook.GetSpell(SpellSlot.R).SData.Name);
                        }
                    }
                }
            }

            drawMenu = mainMenu.AddSubMenu("线圈", "Drawings");
            {
                drawMenu.AddText("法术范围"); 
                drawMenu.AddBool("DrawW", "描绘 W 范围", false);
                drawMenu.AddBool("DrawE", "描绘 E 范围", false);
                drawMenu.AddBool("DrawR1", "描绘 R1 范围", false);
                drawMenu.AddBool("DrawR", "描绘 R 范围");
                drawMenu.AddBool("DrawBurst", "描绘 R 范围");
                ManaManager.AddDrawFarm(drawMenu);
                DamageIndicator.AddToMenu(drawMenu);
            }
        }

        internal static bool ComboQGap => comboMenu.GetBool("ComboQGap");
        internal static bool ComboW => comboMenu.GetBool("ComboW"); 
        internal static bool ComboWLogic => comboMenu.GetBool("ComboWLogic");
        internal static bool ComboE => comboMenu.GetBool("ComboE"); 
        internal static bool ComboEGap => comboMenu.GetBool("ComboEGap");
        internal static bool ComboR1 => comboMenu.GetKey("ComboR1");
        internal static int ComboR2 => comboMenu.GetList("ComboR2");
        internal static bool ComboItem => comboMenu.GetBool("ComboItem"); 
        internal static bool ComboYoumuu => comboMenu.GetBool("ComboYoumuu");
        internal static bool ComboDot => comboMenu.GetBool("ComboDot");

        internal static bool BurstFlash => burstMenu.GetBool("BurstFlash");
        internal static bool BurstDot => burstMenu.GetBool("BurstDot");
        internal static int BurstMode => burstMenu.GetList("BurstMode");
        internal static bool BurstEnabledKey => burstMenu.GetKey("BurstEnabledKey");
 
        internal static bool HarassQ => harassMenu.GetBool("HarassQ");
        internal static bool HarassW => harassMenu.GetBool("HarassW");
        internal static bool HarassE => harassMenu.GetBool("HarassE");
        internal static int HarassMode => harassMenu.GetList("HarassMode");

        internal static bool LaneClearQ => clearMenu.GetBool("LaneClearQ");
        internal static bool LaneClearQSmart => clearMenu.GetBool("LaneClearQSmart");
        internal static bool LaneClearQT => clearMenu.GetBool("LaneClearQT");
        internal static bool LaneClearW => clearMenu.GetBool("LaneClearW");
        internal static int LaneClearWCount => clearMenu.GetSlider("LaneClearWCount");
        internal static bool LaneClearItem => clearMenu.GetBool("LaneClearItem");

        internal static bool JungleClearQ => clearMenu.GetBool("JungleClearQ");
        internal static bool JungleClearW => clearMenu.GetBool("JungleClearW");
        internal static bool JungleClearE => clearMenu.GetBool("JungleClearE");
        internal static bool JungleClearItem => clearMenu.GetBool("JungleClearItem");

        internal static bool FleeQ => fleeMenu.GetBool("FleeQ");
        internal static bool FleeW => fleeMenu.GetBool("FleeW");
        internal static bool FleeE => fleeMenu.GetBool("FleeE");

        internal static bool KillStealR => killStealMenu.GetBool("KillStealR");

        internal static bool KeepQ => miscMenu.GetBool("KeepQ");
        internal static int QMode => miscMenu.GetList("QMode");

        internal static bool AntiGapcloserW => miscMenu.GetBool("AntiGapcloserW");
        internal static bool InterruptW => miscMenu.GetBool("InterruptW");

        internal static bool manualCancel => miscMenu.GetBool("manualCancel");
        internal static bool manualCancelPing => miscMenu.GetBool("manualCancelPing");

        internal static bool evadeELogic => miscMenu.GetBool("evadeELogic");

        internal static bool DrawW => drawMenu.GetBool("DrawW");
        internal static bool DrawE => drawMenu.GetBool("DrawE");
        internal static bool DrawR1 => drawMenu.GetBool("DrawR1");
        internal static bool DrawR => drawMenu.GetBool("DrawR");
        internal static bool DrawBurst => drawMenu.GetBool("DrawBurst");
    }
}
