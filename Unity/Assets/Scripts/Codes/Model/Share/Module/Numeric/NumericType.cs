namespace ET
{
	// 这个可弄个配置表生成
	// 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
	// final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
    public static class NumericType
    {
	    public const int Max = 10000;

	    public const int Speed = 1000;
	    public const int SpeedBase = Speed * 10 + 1;
	    public const int SpeedAdd = Speed * 10 + 2;
	    public const int SpeedPct = Speed * 10 + 3;
	    public const int SpeedFinalAdd = Speed * 10 + 4;
	    public const int SpeedFinalPct = Speed * 10 + 5;

	    public const int RotationSpeed = 1001;
	    public const int RotationSpeedBase = RotationSpeed * 10 + 1;
	    public const int RotationSpeedAdd = RotationSpeed * 10 + 2;
	    public const int RotationSpeedPct = RotationSpeed * 10 + 3;
	    public const int RotationSpeedFinalAdd = RotationSpeed * 10 + 4;
	    public const int RotationSpeedFinalPct = RotationSpeed * 10 + 5;

	    public const int SkillCD = 1002;
	    public const int SkillCDBase = SkillCD * 10 + 1;
	    public const int SkillCDAdd = SkillCD * 10 + 2;
	    public const int SkillCDPct = SkillCD * 10 + 3;
	    public const int SkillCDFinalAdd = SkillCD * 10 + 4;
	    public const int SkillCDFinalPct = SkillCD * 10 + 5;

	    public const int SkillDis = 1003;
	    public const int SkillDisBase = SkillDis * 10 + 1;
	    public const int SkillDisAdd = SkillDis * 10 + 2;
	    public const int SkillDisPct = SkillDis * 10 + 3;
	    public const int SkillDisFinalAdd = SkillDis * 10 + 4;
	    public const int SkillDisFinalPct = SkillDis * 10 + 5;

	    public const int AOI = 1004;
	    public const int AOIBase = AOI * 10 + 1;
	    public const int AOIAdd = AOI * 10 + 2;
	    public const int AOIPct = AOI * 10 + 3;
	    public const int AOIFinalAdd = AOI * 10 + 4;
	    public const int AOIFinalPct = AOI * 10 + 5;
	    
	    public const int Hp = 1005;
	    public const int HpBase = Hp * 10 + 1;

	    public const int MaxHp = 1006;
	    public const int MaxHpBase = MaxHp * 10 + 1;
	    public const int MaxHpAdd = MaxHp * 10 + 2;
	    public const int MaxHpPct = MaxHp * 10 + 3;
	    public const int MaxHpFinalAdd = MaxHp * 10 + 4;
	    public const int MaxHpFinalPct = MaxHp * 10 + 5;

	    public const int PhysicalAttack = 1007;
	    public const int PhysicalAttackBase = PhysicalAttack * 10 + 1;
	    public const int PhysicalAttackAdd = PhysicalAttack * 10 + 2;
	    public const int PhysicalAttackPct = PhysicalAttack * 10 + 3;
	    public const int PhysicalAttackFinalAdd = PhysicalAttack * 10 + 4;
	    public const int PhysicalAttackFinalPct = PhysicalAttack * 10 + 5;
	    
	    // PhysicalDamage = 0, //物理伤害
	    // FireDamage = 1, //元素-火伤害
	    // IceDamage = 2, //元素-冰伤害
	    // ThunderDamage = 3, //元素-雷伤害
	    
    }
}
