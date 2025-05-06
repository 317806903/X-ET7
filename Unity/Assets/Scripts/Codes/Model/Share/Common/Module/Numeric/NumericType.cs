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

	    //受击物理伤害减免系数(N%)
	    public const int DamageReliefWhenPhysical = 1011;
	    public const int DamageReliefWhenPhysicalBase = DamageReliefWhenPhysical * 10 + 1;
	    public const int DamageReliefWhenPhysicalAdd = DamageReliefWhenPhysical * 10 + 2;
	    public const int DamageReliefWhenPhysicalPct = DamageReliefWhenPhysical * 10 + 3;
	    public const int DamageReliefWhenPhysicalFinalAdd = DamageReliefWhenPhysical * 10 + 4;
	    public const int DamageReliefWhenPhysicalFinalPct = DamageReliefWhenPhysical * 10 + 5;

	    //受击魔法伤害减免系数(N%)
	    public const int DamageReliefWhenMagic = 1012;
	    public const int DamageReliefWhenMagicBase = DamageReliefWhenMagic * 10 + 1;
	    public const int DamageReliefWhenMagicAdd = DamageReliefWhenMagic * 10 + 2;
	    public const int DamageReliefWhenMagicPct = DamageReliefWhenMagic * 10 + 3;
	    public const int DamageReliefWhenMagicFinalAdd = DamageReliefWhenMagic * 10 + 4;
	    public const int DamageReliefWhenMagicFinalPct = DamageReliefWhenMagic * 10 + 5;

	    //伤害按照距离变化(>0表示越远伤害越高,<0表示越近伤害越高)
	    public const int PhysicalAttackScaleByDis = 1013;
	    public const int PhysicalAttackScaleByDisBase = PhysicalAttackScaleByDis * 10 + 1;
	    public const int PhysicalAttackScaleByDisAdd = PhysicalAttackScaleByDis * 10 + 2;
	    public const int PhysicalAttackScaleByDisPct = PhysicalAttackScaleByDis * 10 + 3;
	    public const int PhysicalAttackScaleByDisFinalAdd = PhysicalAttackScaleByDis * 10 + 4;
	    public const int PhysicalAttackScaleByDisFinalPct = PhysicalAttackScaleByDis * 10 + 5;

	    //伤害按照高度变化(>0表示往上越远伤害越高,<0表示往下越远伤害越高)
	    public const int PhysicalAttackScaleByHeight = 1014;
	    public const int PhysicalAttackScaleByHeightBase = PhysicalAttackScaleByHeight * 10 + 1;
	    public const int PhysicalAttackScaleByHeightAdd = PhysicalAttackScaleByHeight * 10 + 2;
	    public const int PhysicalAttackScaleByHeightPct = PhysicalAttackScaleByHeight * 10 + 3;
	    public const int PhysicalAttackScaleByHeightFinalAdd = PhysicalAttackScaleByHeight * 10 + 4;
	    public const int PhysicalAttackScaleByHeightFinalPct = PhysicalAttackScaleByHeight * 10 + 5;

	    // PhysicalDamage = 0, //物理伤害
	    // FireDamage = 1, //元素-火伤害
	    // IceDamage = 2, //元素-冰伤害
	    // ThunderDamage = 3, //元素-雷伤害

	    //buff时间修改(BuffType==Buff)
	    public const int BuffTimeModify = 1020;
	    public const int BuffTimeModifyBase = BuffTimeModify * 10 + 1;
	    public const int BuffTimeModifyAdd = BuffTimeModify * 10 + 2;
	    public const int BuffTimeModifyPct = BuffTimeModify * 10 + 3;
	    public const int BuffTimeModifyFinalAdd = BuffTimeModify * 10 + 4;
	    public const int BuffTimeModifyFinalPct = BuffTimeModify * 10 + 5;

	    //buff时间修改(BuffType==Debuff)
	    public const int DebuffTimeModify = 1021;
	    public const int DebuffTimeModifyBase = DebuffTimeModify * 10 + 1;
	    public const int DebuffTimeModifyAdd = DebuffTimeModify * 10 + 2;
	    public const int DebuffTimeModifyPct = DebuffTimeModify * 10 + 3;
	    public const int DebuffTimeModifyFinalAdd = DebuffTimeModify * 10 + 4;
	    public const int DebuffTimeModifyFinalPct = DebuffTimeModify * 10 + 5;

	    //发起者的buff伤害修改
	    public const int BuffDamageModify = 1022;
	    public const int BuffDamageModifyBase = BuffDamageModify * 10 + 1;
	    public const int BuffDamageModifyAdd = BuffDamageModify * 10 + 2;
	    public const int BuffDamageModifyPct = BuffDamageModify * 10 + 3;
	    public const int BuffDamageModifyFinalAdd = BuffDamageModify * 10 + 4;
	    public const int BuffDamageModifyFinalPct = BuffDamageModify * 10 + 5;

	    //受击者的buff伤害修改
	    public const int BuffBeDamageModify = 1023;
	    public const int BuffBeDamageModifyBase = BuffBeDamageModify * 10 + 1;
	    public const int BuffBeDamageModifyAdd = BuffBeDamageModify * 10 + 2;
	    public const int BuffBeDamageModifyPct = BuffBeDamageModify * 10 + 3;
	    public const int BuffBeDamageModifyFinalAdd = BuffBeDamageModify * 10 + 4;
	    public const int BuffBeDamageModifyFinalPct = BuffBeDamageModify * 10 + 5;

	    //最大攻击范围
	    public const int MaxAttackDis = 1024;
	    public const int MaxAttackDisBase = MaxAttackDis * 10 + 1;
	    public const int MaxAttackDisAdd = MaxAttackDis * 10 + 2;
	    public const int MaxAttackDisPct = MaxAttackDis * 10 + 3;
	    public const int MaxAttackDisFinalAdd = MaxAttackDis * 10 + 4;
	    public const int MaxAttackDisFinalPct = MaxAttackDis * 10 + 5;

	    //技能释放时选择对象数量修改
	    public const int SkillSelectNumModify = 1025;
	    public const int SkillSelectNumModifyBase = SkillSelectNumModify * 10 + 1;
	    public const int SkillSelectNumModifyAdd = SkillSelectNumModify * 10 + 2;
	    public const int SkillSelectNumModifyPct = SkillSelectNumModify * 10 + 3;
	    public const int SkillSelectNumModifyFinalAdd = SkillSelectNumModify * 10 + 4;
	    public const int SkillSelectNumModifyFinalPct = SkillSelectNumModify * 10 + 5;

	    //技能能量点修改
	    public const int TotalEnergyModify = 1026;
	    public const int TotalEnergyModifyBase = TotalEnergyModify * 10 + 1;
	    public const int TotalEnergyModifyAdd = TotalEnergyModify * 10 + 2;
	    public const int TotalEnergyModifyPct = TotalEnergyModify * 10 + 3;
	    public const int TotalEnergyModifyFinalAdd = TotalEnergyModify * 10 + 4;
	    public const int TotalEnergyModifyFinalPct = TotalEnergyModify * 10 + 5;

    }
}
