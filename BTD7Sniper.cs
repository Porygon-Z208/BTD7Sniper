using MelonLoader;
using BTD_Mod_Helper;
using BTD7Sniper;
using System;
using System.Runtime.CompilerServices;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using BTD_Mod_Helper.Api;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using BTD_Mod_Helper.Api.Display;
using System.Xml.Linq;
using UnityEngine.ResourceManagement.AsyncOperations;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using System.Diagnostics;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Models.Powers;
using BTD_Mod_Helper.Api.Enums;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppNewtonsoft.Json.Utilities;
using Il2CppSystem.Linq;
using BTD_Mod_Helper.Api.Towers;
using Octokit;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Api.ModOptions;
using UnityEngine.Playables;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper.Api.Helpers;
using Il2CppSystem.Collections;
using BTD_Mod_Helper.Api.Scenarios;
using Il2CppAssets.Scripts.Models.Difficulty;
using BTD_Mod_Helper.Api.Bloons;
using Il2CppAssets.Scripts.Models.Rounds;
using PathsPlusPlus;
using FuzzySharp.Extensions;

[assembly: MelonInfo(typeof(BTD7Sniper.BTD7Sniper), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BTD7Sniper;

public class BTD7Sniper : BloonsTD6Mod
{

    public override void OnApplicationStart()
    {
        MelonLogger.Msg(System.ConsoleColor.Magenta, "BTD7 Sniper loaded!");
    }

    private static readonly ModSettingBool SpecialPathRestrictions = new(true)
    {
        displayName = "Special Path Restrictions",
        description = "Whether or not to use the special path restrictions added by this mod for the sniper.",
    };

    public class SniperTopPath : PathPlusPlus
    {
        public override string Tower => TowerType.SniperMonkey;

        public override int UpgradeCount => 6;

        public override int ExtendVanillaPath => 0;

        public override bool ValidTiers(int[] tiers)
        {
            if (SpecialPathRestrictions)
            {
                return (tiers.Count(i => i > 5) == 1 && tiers.Count(i => i > 3) <= 1 && tiers.Count(i => i > 2) <= 2) && tiers.Count(i => i > 0) <= 3 || (tiers.Count(i => i > 5) == 0 && tiers.Count(i => i > 2) <= 1) && tiers.Count(i => i > 0) <= 3;
            }
            else
            {
                return DefaultValidTiers(tiers);
            }
        }
    }

    public class SniperMiddlePath : PathPlusPlus
    {
        public override string Tower => TowerType.SniperMonkey;

        public override int UpgradeCount => 6;

        public override int ExtendVanillaPath => 1;
        
        public override bool ValidTiers(int[] tiers)
        {
            if (SpecialPathRestrictions)
            {
                return (tiers.Count(i => i > 5) == 1 && tiers.Count(i => i > 3) <= 1 && tiers.Count(i => i > 2) <= 2) && tiers.Count(i => i > 0) <= 3 || (tiers.Count(i => i > 5) == 0 && tiers.Count(i => i > 2) <= 1) && tiers.Count(i => i > 0) <= 3;
            }
            else
            {
                return DefaultValidTiers(tiers);
            }
        }
    }

    public class SniperBottomPath : PathPlusPlus
    {
        public override string Tower => TowerType.SniperMonkey;

        public override int UpgradeCount => 6;

        public override int ExtendVanillaPath => 2;

        public override bool ValidTiers(int[] tiers)
        {
            if (SpecialPathRestrictions)
            {
                return (tiers.Count(i => i > 5) == 1 && tiers.Count(i => i > 3) <= 1 && tiers.Count(i => i > 2) <= 2) && tiers.Count(i => i > 0) <= 3 || (tiers.Count(i => i > 5) == 0 && tiers.Count(i => i > 2) <= 1) && tiers.Count(i => i > 0) <= 3;
            }
            else
            {
                return DefaultValidTiers(tiers);
            }
        }
    }
    public class AubreySniperStatusPath : PathPlusPlus
    {
        public override string Tower => TowerType.SniperMonkey;

        public override int UpgradeCount => 6;

        public override bool ValidTiers(int[] tiers)
        {
            if (SpecialPathRestrictions)
            {
                return (tiers.Count(i => i > 5) == 1 && tiers.Count(i => i > 3) <= 1 && tiers.Count(i => i > 2) <= 2) && tiers.Count(i => i > 0) <= 3 || (tiers.Count(i => i > 5) == 0 && tiers.Count(i => i > 2) <= 1) && tiers.Count(i => i > 0) <= 3;
            }
            else
            {
                return DefaultValidTiers(tiers);
            }
        }
    }

    public class DisableMOAB : UpgradePlusPlus<SniperTopPath>
    {
        public override int Cost => 92160;
        public override int Tier => 6;

        public override string DisplayName => "Disable MOAB";
        public override string Description => "+500 damage and moab-class bloons hit are vunerable to +50 damage and disabled.";
        public override string Icon => "DisableMOAB-Icon";
        public override string Portrait => "DisableMOAB-Portrait";
        public override string Container => UpgradeContainerDiamond;

        public override void ApplyUpgrade(TowerModel tower)
        {
            foreach (var weaponModel in tower.GetWeapons())
            {
                var crippleModel = weaponModel.projectile.GetBehavior<SlowMaimMoabModel>();
                weaponModel.projectile.GetDamageModel().damage += 500;
                crippleModel.bloonPerHitDamageAddition += 45;
            }
            if (IsHighestUpgrade(tower))
            {
                tower.ApplyDisplay<DisableMOABDisplay>();
            }
        }
        internal class DisableMOABDisplay : ModDisplay
        {
            public override string BaseDisplay => Game.instance.model.GetTowerFromId(TowerType.SniperMonkey + "-500").display.GUID;
            public override float Scale => 1.1f;
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                SetMeshTexture(node, "DisableMOAB-Display");
                SetMeshTexture(node, "DisableMOAB-Display", 1);
            }
        }
    }

    public class UltraSniper : UpgradePlusPlus<SniperMiddlePath>
    {
        public override int Cost => 67400;
        public override int Tier => 6;

        public override string Description => "All snipers get 2x attack speed. Ability: Gives +$2,500 cash, has -25% cooldown, and activates all other Supply Drop abilities.";
        public override string Icon => "UltraSniper-Icon";
        public override string Portrait => "UltraSniper-Portrait";
        public override string Container => UpgradeContainerDiamond;

        public override void ApplyUpgrade(TowerModel tower)
        {
            var weaponModel = tower.GetWeapon();
            var abilityModel = tower.GetAbilities().First(model => model.displayName == "Elite Supply Drop");
            var abilityCrate = abilityModel.GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile;

            tower.GetBehavior<RateSupportModel>().multiplier = 0.5f;
            tower.GetBehavior<RateSupportModel>().appliesToOwningTower = true;

            abilityModel.Cooldown *= 0.75f;
            abilityModel.displayName = "Ultra Supply Drop";
            abilityModel.icon = GetSpriteReference("UltraSniper-Icon");
            abilityModel.GetBehavior<ActivateAbilitiesOnAbilityModel>().abilityToFind = "Elite Supply Drop";

            abilityCrate.GetBehavior<CashModel>().minimum += 2500;
            abilityCrate.GetBehavior<CashModel>().maximum += 2500;


            if (IsHighestUpgrade(tower))
            {
                tower.ApplyDisplay<UltraSniperDisplay>();
            }
        }
        internal class UltraSniperDisplay : ModDisplay
        {
            public override string BaseDisplay => Game.instance.model.GetTowerFromId(TowerType.SniperMonkey + "-050").display.GUID;
            public override float Scale => 1.1f;
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                SetMeshTexture(node, "UltraSniper-Display");
            }
        }
    }

    public class EliteAssassin : UpgradePlusPlus<SniperBottomPath>
    {
        public override int Cost => 73490;
        public override int Tier => 6;

        public override string Description => "66% faster attack speed, +6 moab damage, and leaking lives makes the sniper attack 5x faster for 10 seconds.";
        public override string Icon => "EliteAssassin-Icon";
        public override string Portrait => "EliteAssassin-Portrait";
        public override string Container => UpgradeContainerDiamond;

        public override void ApplyUpgrade(TowerModel tower)
        {
            var abilityModel = tower.GetAbilities().First(model => model.displayName == "Retaliation");
            abilityModel.GetBehavior<TurboModel>().multiplier = 0.2f;
            abilityModel.GetBehavior<TurboModel>().Lifespan = 10;
            foreach (var weaponModel in tower.GetWeapons())
            {
                weaponModel.Rate *= .3333f;
                weaponModel.projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 1, 6, false, true));
            }
            if (IsHighestUpgrade(tower))
            {
                tower.ApplyDisplay<EliteAssassinDisplay>();
            }
        }
        internal class EliteAssassinDisplay : ModDisplay
        {
            public override string BaseDisplay => Game.instance.model.GetTowerFromId(TowerType.SniperMonkey + "-005").display.GUID;
            public override float Scale => 1.1f;
            public override void ModifyDisplayNode(UnityDisplayNode node)
            {
                SetMeshTexture(node, "EliteAssassin-Display");
            }
        }
    }

    public class XRayVision : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 610;
        public override int Tier => 1;

        public override string DisplayName => "X-Ray Vision";
        public override string Description => "Allows the sniper monkey to see through all obstacles.";
        public override string Icon => "XRayVision-Icon";
        public override string Portrait => "XRayVision-Portrait";

        public override void ApplyUpgrade(TowerModel tower)
        {
            tower.ignoreBlockers = true;
            tower.GetWeapon().projectile.ignoreBlockers = true;
            tower.GetWeapon().projectile.canCollisionBeBlockedByMapLos = false;
            tower.GetAttackModel().attackThroughWalls = true;
        }
    }

    public class IncendiaryBullets : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 270;
        public override int Tier => 2;

        public override string Description => "Sniper bullets set bloons on fire and can pop Lead and Frozen bloons.";
        public override string Icon => "IncendiaryBullets-Icon";
        public override string Portrait => "IncendiaryBullets-Portrait";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var burnyStuff = Game.instance.model.GetTowerFromId(TowerType.MortarMonkey + "-002").GetDescendant<AddBehaviorToBloonModel>().Duplicate();
            burnyStuff.isUnique = false;
            tower.GetWeapon().projectile.AddBehavior(burnyStuff);
            tower.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().filters = null;
            tower.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>().damage = 2;

            tower.GetDescendants<DamageModel>().ForEach(model => model.immuneBloonProperties = BloonProperties.None);
        }
    }

    public class ImpactRounds : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 1490;
        public override int Tier => 3;

        public override string Description => "Bullets push bloons back, do +1 damage, and crit every 10 shots.";
        public override string Icon => "ImpactRounds-Icon";
        public override string Portrait => "ImpactRounds-Portrait";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var knockback = Game.instance.model.GetTowerFromId(TowerType.SuperMonkey + "-001").GetDescendant<KnockbackModel>().Duplicate();
            var textOnHit = Game.instance.model.GetTowerFromId(TowerType.DartMonkey + "-004").GetDescendant<ShowTextOnHitModel>().Duplicate();
            var critModel = Game.instance.model.GetTowerFromId(TowerType.DartMonkey + "-004").GetDescendant<CritMultiplierModel>().Duplicate();
            var projectileModel = tower.GetWeapon().projectile;
            critModel.lower = 10;
            critModel.upper = 10;
            critModel.damage = (projectileModel.GetDamageModel().damage + 1) * 3;
            projectileModel.AddBehavior(knockback);
            tower.GetWeapon().AddBehavior(critModel);
            projectileModel.AddBehavior(textOnHit);
            projectileModel.GetBehavior<KnockbackModel>().Lifespan = 0.75f;
            projectileModel.GetBehavior<KnockbackModel>().lightMultiplier = 1.5f;
            projectileModel.GetDamageModel().damage += 1;
        }
    }

    public class BurstRifle : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 3540;
        public override int Tier => 4;

        public override string Description => "Shoots 2 sniper bullets in quick succession and bullets shock bloons.";
        public override string Icon => "BurstRifle-Icon";
        public override string Portrait => "BurstRifle-Portrait";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var laserShock = Game.instance.model.GetTowerFromId(TowerType.DartlingGunner + "-200").GetDescendant<AddBehaviorToBloonModel>().Duplicate();
            var laserShockDebuff = Game.instance.model.GetTowerFromId(TowerType.DartlingGunner + "-200").GetDescendant<DamageModifierForBloonStateModel>().Duplicate();
            var projectileModel = tower.GetWeapon().projectile;
            laserShock.isUnique = false;
            laserShockDebuff.applyOverMaxDamage = true;
            projectileModel.AddBehavior(laserShock);
            projectileModel.AddBehavior(laserShockDebuff);
            projectileModel.collisionPasses = new[] { -1, 0, 1 };
            tower.GetWeapon().emission = new EmissionOverTimeModel("EmissionOverTimeModel_", 2, 0.15f, null);
            projectileModel.AddBehavior(new InstantModel("InstantModel_", true));
        }
    }

    public class SlowBleedSniper : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 33700;
        public override int Tier => 5;

        public override string Description => "Sniper's bullets now gradually bleed bloons, with increased damage for bigger bloons and shoots +1 bullet per burst. Burn, shock, and knockback are more powerful.";
        public override string Icon => "SlowBleedSniper-Icon";
        public override string Portrait => "SlowBleedSniper-Portrait";

        public override void ApplyUpgrade(TowerModel tower)
        {
            var bloonBleed = Game.instance.model.GetTowerFromId(TowerType.Sauda + " 9").GetWeapon().projectile.GetBehaviors<AddBehaviorToBloonModel>().First().Duplicate();
            var moabBleed = Game.instance.model.GetTowerFromId(TowerType.Sauda + " 9").GetWeapon().projectile.GetBehaviors<AddBehaviorToBloonModel>().Last().Duplicate();
            var projectileModel = tower.GetWeapon().projectile;
            bloonBleed.GetBehavior<DamageOverTimeModel>().damage = 5;
            moabBleed.GetBehavior<DamageOverTimeModel>().damage = 70;
            bloonBleed.GetBehavior<DamageOverTimeModel>().Interval = 1.25f;
            moabBleed.GetBehavior<DamageOverTimeModel>().Interval = 1.25f;
            bloonBleed.lifespan = 10;
            moabBleed.lifespan = 10;
            tower.GetWeapon().emission = new EmissionOverTimeModel("EmissionOverTimeModel_", 3, 0.15f, null);
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().First().GetBehavior<DamageOverTimeModel>().damage = 20;
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().First().GetBehavior<DamageOverTimeModel>().Interval = 0.5f;
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().First().lifespan = 4;
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().Last().GetBehavior<DamageOverTimeModel>().damage = 10;
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().Last().GetBehavior<DamageOverTimeModel>().Interval = 0.35f;
            projectileModel.GetBehaviors<AddBehaviorToBloonModel>().Last().lifespan = 3.25f;
            projectileModel.GetBehavior<DamageModifierForBloonStateModel>().damageAdditive = 5;
            projectileModel.GetBehavior<KnockbackModel>().Lifespan *= 2;
            projectileModel.GetBehavior<KnockbackModel>().moabMultiplier *= 4;
            projectileModel.GetBehavior<KnockbackModel>().heavyMultiplier *= 4;
            projectileModel.GetBehavior<KnockbackModel>().lightMultiplier *= 4;
            projectileModel.AddBehavior(bloonBleed);
            projectileModel.AddBehavior(moabBleed);
        }
    }
    public class UnholyRetribution : UpgradePlusPlus<AubreySniperStatusPath>
    {
        public override int Cost => 106490;
        public override int Tier => 6;

        public override string Description => "Shoot +2 bullets per burst, and buffs burn and shock. Bullets leave a wall of fire on the ground, create arcing thunder that applies all tier 5 status effects, slows most bloons hit, and hex all bloons hit.";
        public override string Icon => "UnholyRetribution-Icon";
        public override string Portrait => "UnholyRetribution-Portrait";
        public override string Container => UpgradeContainerDiamond;

        public override void ApplyUpgrade(TowerModel tower)
        {
            var wallOfFire = Game.instance.model.GetTowerFromId(TowerType.MortarMonkey + "-005").GetWeapon().projectile.GetBehaviors<CreateProjectileOnExhaustFractionModel>().First(model => model.projectile.id == "WallOfFire").Duplicate();
            var thunder = Game.instance.model.GetTowerFromId(TowerType.Druid + "-200").GetWeapons()[1].projectile.Duplicate();
            var arcing = thunder.GetBehavior<LightningModel>();
            var bleedSlow = Game.instance.model.GetTowerFromId(TowerType.GlueGunner + "-003").GetWeapon().projectile.GetBehavior<SlowModel>().Duplicate();
            var moabSlow = Game.instance.model.GetTowerFromId(TowerType.GlueGunner + "-003").GetWeapon().projectile.GetBehavior<SlowModifierForTagModel>().Duplicate();
            var eziliHex = Game.instance.model.GetTowerFromId(TowerType.Ezili + " 20").GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().Duplicate();
            var hexDot = eziliHex.GetBehavior<DamageOverTimeModel>();
            var hexplosion = Game.instance.model.GetTowerFromId(TowerType.Ezili + " 20").GetWeapon().projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate();
            var projectileModel = tower.GetWeapon().projectile;
            var burnyStuff = projectileModel.GetBehaviors<AddBehaviorToBloonModel>().First(model => model.overlayType == "Fire");
            var laserShock = projectileModel.GetBehaviors<AddBehaviorToBloonModel>().First(model => model.overlayType == "LaserShock");
            wallOfFire.projectile.pierce = 5000;
            wallOfFire.projectile.filters = null;
            thunder.GetDamageModel().damage = 5;
            thunder.collisionPasses = new[] { -1, 0, 1 };
            arcing.splits = 2;
            arcing.splitRange = 130;
            moabSlow.slowMultiplier = 3;
            eziliHex.isUnique = false;
            eziliHex.applyOnlyIfDamaged = false;
            hexDot.damage = 15;
            hexDot.damageModifierModels.AddTo(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 1, 17, false, false));
            hexplosion.projectile.AddBehavior(eziliHex);
            burnyStuff.GetBehavior<DamageOverTimeModel>().damage = 35;
            burnyStuff.GetBehavior<DamageOverTimeModel>().Interval = 0.7f;
            laserShock.GetBehavior<DamageOverTimeModel>().Interval = 0.2f;
            foreach (var bloonEffect in projectileModel.GetDescendantsFast<AddBehaviorToBloonModel>())
            {
                thunder.AddBehavior(bloonEffect);
            }
            tower.GetWeapon().emission = new EmissionOverTimeModel("EmissionOverTimeModel_", 5, 0.1f, null);
            projectileModel.AddBehavior(wallOfFire);
            projectileModel.AddBehavior(bleedSlow);
            projectileModel.AddBehavior(moabSlow);
            projectileModel.AddBehavior(eziliHex);
            projectileModel.AddBehavior(hexplosion);
            projectileModel.AddBehavior(new CreateProjectileOnExhaustFractionModel("thundermodel_", thunder, new InstantDamageEmissionModel("instantmodelt", null), 1, -1, false, false, false));
        }
    }
}
