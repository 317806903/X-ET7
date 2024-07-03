namespace ET
{
	// 这个可弄个配置表生成
	// 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
	// final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
    public static class NumericType
    {
	    public const int Max = 10000;

	    //移动速度
	    public const int Speed = 1000;
	    public const int SpeedBase = Speed * 10 + 1;
	    public const int SpeedAdd = Speed * 10 + 2;
	    public const int SpeedPct = Speed * 10 + 3;
	    public const int SpeedFinalAdd = Speed * 10 + 4;
	    public const int SpeedFinalPct = Speed * 10 + 5;

	    //旋转速度
	    public const int RotationSpeed = 1001;
	    public const int RotationSpeedBase = RotationSpeed * 10 + 1;
	    public const int RotationSpeedAdd = RotationSpeed * 10 + 2;
	    public const int RotationSpeedPct = RotationSpeed * 10 + 3;
	    public const int RotationSpeedFinalAdd = RotationSpeed * 10 + 4;
	    public const int RotationSpeedFinalPct = RotationSpeed * 10 + 5;

	    //技能cd
	    public const int SkillCD = 1002;
	    public const int SkillCDBase = SkillCD * 10 + 1;
	    public const int SkillCDAdd = SkillCD * 10 + 2;
	    public const int SkillCDPct = SkillCD * 10 + 3;
	    public const int SkillCDFinalAdd = SkillCD * 10 + 4;
	    public const int SkillCDFinalPct = SkillCD * 10 + 5;

	    //技能施法距离
	    public const int SkillDis = 1003;
	    public const int SkillDisBase = SkillDis * 10 + 1;
	    public const int SkillDisAdd = SkillDis * 10 + 2;
	    public const int SkillDisPct = SkillDis * 10 + 3;
	    public const int SkillDisFinalAdd = SkillDis * 10 + 4;
	    public const int SkillDisFinalPct = SkillDis * 10 + 5;

	    //AOI识别范围
	    public const int AOI = 1004;
	    public const int AOIBase = AOI * 10 + 1;
	    public const int AOIAdd = AOI * 10 + 2;
	    public const int AOIPct = AOI * 10 + 3;
	    public const int AOIFinalAdd = AOI * 10 + 4;
	    public const int AOIFinalPct = AOI * 10 + 5;

	    public const int Hp = 1005;
	    public const int HpBase = Hp * 10 + 1;
	    public const int HpAdd = Hp * 10 + 2;
	    public const int HpPct = Hp * 10 + 3;
	    public const int HpFinalAdd = Hp * 10 + 4;
	    public const int HpFinalPct = Hp * 10 + 5;

	    public const int MaxHp = 1006;
	    public const int MaxHpBase = MaxHp * 10 + 1;
	    public const int MaxHpAdd = MaxHp * 10 + 2;
	    public const int MaxHpPct = MaxHp * 10 + 3;
	    public const int MaxHpFinalAdd = MaxHp * 10 + 4;
	    public const int MaxHpFinalPct = MaxHp * 10 + 5;

	    //物理攻击力
	    public const int PhysicalAttack = 1007;
	    public const int PhysicalAttackBase = PhysicalAttack * 10 + 1;
	    public const int PhysicalAttackAdd = PhysicalAttack * 10 + 2;
	    public const int PhysicalAttackPct = PhysicalAttack * 10 + 3;
	    public const int PhysicalAttackFinalAdd = PhysicalAttack * 10 + 4;
	    public const int PhysicalAttackFinalPct = PhysicalAttack * 10 + 5;

	    //暴击伤害(N%)
	    public const int CriticalHitDamage = 1008;
	    public const int CriticalHitDamageBase = CriticalHitDamage * 10 + 1;
	    public const int CriticalHitDamageAdd = CriticalHitDamage * 10 + 2;
	    public const int CriticalHitDamagePct = CriticalHitDamage * 10 + 3;
	    public const int CriticalHitDamageFinalAdd = CriticalHitDamage * 10 + 4;
	    public const int CriticalHitDamageFinalPct = CriticalHitDamage * 10 + 5;

	    //暴击率(N%)
	    public const int CriticalStrikeRate = 1009;
	    public const int CriticalStrikeRateBase = CriticalStrikeRate * 10 + 1;
	    public const int CriticalStrikeRateAdd = CriticalStrikeRate * 10 + 2;
	    public const int CriticalStrikeRatePct = CriticalStrikeRate * 10 + 3;
	    public const int CriticalStrikeRateFinalAdd = CriticalStrikeRate * 10 + 4;
	    public const int CriticalStrikeRateFinalPct = CriticalStrikeRate * 10 + 5;

	    //伤害加深系数(N%)
	    public const int DamageDeepening = 1010;
	    public const int DamageDeepeningBase = DamageDeepening * 10 + 1;
	    public const int DamageDeepeningAdd = DamageDeepening * 10 + 2;
	    public const int DamageDeepeningPct = DamageDeepening * 10 + 3;
	    public const int DamageDeepeningFinalAdd = DamageDeepening * 10 + 4;
	    public const int DamageDeepeningFinalPct = DamageDeepening * 10 + 5;

	    //伤害减免系数(N%)
	    public const int DamageRelief = 1011;
	    public const int DamageReliefBase = DamageRelief * 10 + 1;
	    public const int DamageReliefAdd = DamageRelief * 10 + 2;
	    public const int DamageReliefPct = DamageRelief * 10 + 3;
	    public const int DamageReliefFinalAdd = DamageRelief * 10 + 4;
	    public const int DamageReliefFinalPct = DamageRelief * 10 + 5;

	    // PhysicalDamage = 0, //物理伤害
	    // FireDamage = 1, //元素-火伤害
	    // IceDamage = 2, //元素-冰伤害
	    // ThunderDamage = 3, //元素-雷伤害

	    public const int TowerDefense_HomeMaxHp = 2001;
	    public const int TowerDefense_HomeMaxHpBase = TowerDefense_HomeMaxHp * 10 + 1;
	    public const int TowerDefense_HomeMaxHpAdd = TowerDefense_HomeMaxHp * 10 + 2;
	    public const int TowerDefense_HomeMaxHpPct = TowerDefense_HomeMaxHp * 10 + 3;
	    public const int TowerDefense_HomeMaxHpFinalAdd = TowerDefense_HomeMaxHp * 10 + 4;
	    public const int TowerDefense_HomeMaxHpFinalPct = TowerDefense_HomeMaxHp * 10 + 5;

	    public const int TowerDefense_HomeCurHp = 2002;
	    public const int TowerDefense_HomeCurHpBase = TowerDefense_HomeCurHp * 10 + 1;
	    public const int TowerDefense_HomeCurHpAdd = TowerDefense_HomeCurHp * 10 + 2;
	    public const int TowerDefense_HomeCurHpPct = TowerDefense_HomeCurHp * 10 + 3;
	    public const int TowerDefense_HomeCurHpFinalAdd = TowerDefense_HomeCurHp * 10 + 4;
	    public const int TowerDefense_HomeCurHpFinalPct = TowerDefense_HomeCurHp * 10 + 5;

	    public const int TowerDefense_PlayerInitGold = 2003;
	    public const int TowerDefense_PlayerInitGoldBase = TowerDefense_PlayerInitGold * 10 + 1;
	    public const int TowerDefense_PlayerInitGoldAdd = TowerDefense_PlayerInitGold * 10 + 2;
	    public const int TowerDefense_PlayerInitGoldPct = TowerDefense_PlayerInitGold * 10 + 3;
	    public const int TowerDefense_PlayerInitGoldFinalAdd = TowerDefense_PlayerInitGold * 10 + 4;
	    public const int TowerDefense_PlayerInitGoldFinalPct = TowerDefense_PlayerInitGold * 10 + 5;

	    public const int TowerDefense_PlayerLimitTowerCount = 2004;
	    public const int TowerDefense_PlayerLimitTowerCountBase = TowerDefense_PlayerLimitTowerCount * 10 + 1;
	    public const int TowerDefense_PlayerLimitTowerCountAdd = TowerDefense_PlayerLimitTowerCount * 10 + 2;
	    public const int TowerDefense_PlayerLimitTowerCountPct = TowerDefense_PlayerLimitTowerCount * 10 + 3;
	    public const int TowerDefense_PlayerLimitTowerCountFinalAdd = TowerDefense_PlayerLimitTowerCount * 10 + 4;
	    public const int TowerDefense_PlayerLimitTowerCountFinalPct = TowerDefense_PlayerLimitTowerCount * 10 + 5;

	    public const int TowerDefense_PlayerTowerPrice = 2005;
	    public const int TowerDefense_PlayerTowerPriceBase = TowerDefense_PlayerTowerPrice * 10 + 1;
	    public const int TowerDefense_PlayerTowerPriceAdd = TowerDefense_PlayerTowerPrice * 10 + 2;
	    public const int TowerDefense_PlayerTowerPricePct = TowerDefense_PlayerTowerPrice * 10 + 3;
	    public const int TowerDefense_PlayerTowerPriceFinalAdd = TowerDefense_PlayerTowerPrice * 10 + 4;
	    public const int TowerDefense_PlayerTowerPriceFinalPct = TowerDefense_PlayerTowerPrice * 10 + 5;

	    public const int TowerDefense_PlayerResurrectionTimes = 2006;
	    public const int TowerDefense_PlayerResurrectionTimesBase = TowerDefense_PlayerResurrectionTimes * 10 + 1;
	    public const int TowerDefense_PlayerResurrectionTimesAdd = TowerDefense_PlayerResurrectionTimes * 10 + 2;
	    public const int TowerDefense_PlayerResurrectionTimesPct = TowerDefense_PlayerResurrectionTimes * 10 + 3;
	    public const int TowerDefense_PlayerResurrectionTimesFinalAdd = TowerDefense_PlayerResurrectionTimes * 10 + 4;
	    public const int TowerDefense_PlayerResurrectionTimesFinalPct = TowerDefense_PlayerResurrectionTimes * 10 + 5;

	    public const int TowerDefense_PlayerRewardWhenGameEnd = 2007;
	    public const int TowerDefense_PlayerRewardWhenGameEndBase = TowerDefense_PlayerRewardWhenGameEnd * 10 + 1;
	    public const int TowerDefense_PlayerRewardWhenGameEndAdd = TowerDefense_PlayerRewardWhenGameEnd * 10 + 2;
	    public const int TowerDefense_PlayerRewardWhenGameEndPct = TowerDefense_PlayerRewardWhenGameEnd * 10 + 3;
	    public const int TowerDefense_PlayerRewardWhenGameEndFinalAdd = TowerDefense_PlayerRewardWhenGameEnd * 10 + 4;
	    public const int TowerDefense_PlayerRewardWhenGameEndFinalPct = TowerDefense_PlayerRewardWhenGameEnd * 10 + 5;

	    public const int TowerDefense_PlayerRewardWhenKillMonster = 2008;
	    public const int TowerDefense_PlayerRewardWhenKillMonsterBase = TowerDefense_PlayerRewardWhenKillMonster * 10 + 1;
	    public const int TowerDefense_PlayerRewardWhenKillMonsterAdd = TowerDefense_PlayerRewardWhenKillMonster * 10 + 2;
	    public const int TowerDefense_PlayerRewardWhenKillMonsterPct = TowerDefense_PlayerRewardWhenKillMonster * 10 + 3;
	    public const int TowerDefense_PlayerRewardWhenKillMonsterFinalAdd = TowerDefense_PlayerRewardWhenKillMonster * 10 + 4;
	    public const int TowerDefense_PlayerRewardWhenKillMonsterFinalPct = TowerDefense_PlayerRewardWhenKillMonster * 10 + 5;

    }
}
