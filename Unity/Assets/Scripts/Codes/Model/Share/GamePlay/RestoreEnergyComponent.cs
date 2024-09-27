using System.Collections.Generic;
using System.Linq;
using ET.Ability;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
	[ComponentOf(typeof(GamePlayTowerDefenseComponent))]
	public class RestoreEnergyComponent : Entity, IAwake, IDestroy, IFixedUpdate
	{
		public List<EntityRef<SkillComponent>> skillComponentListClear = new();
		public List<EntityRef<SkillObj>> skillObjListClear = new();

		public HashSet<EntityRef<SkillComponent>> skillComponentList = new();
		public HashSet<EntityRef<SkillObj>> skillObjList = new();

		public int waitFrameChk = 100;
		public int curFrameChk = 0;
	}
}