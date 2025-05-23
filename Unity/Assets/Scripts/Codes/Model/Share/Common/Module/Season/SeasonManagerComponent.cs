﻿using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class SeasonManagerComponent : Entity, IAwake, IDestroy, IFixedUpdate, ISerializeToEntity
	{
		private EntityRef<SeasonComponent> _SeasonComponent;
		public SeasonComponent SeasonComponent
		{
			get
			{
				return this._SeasonComponent;
			}
			set
			{
				this._SeasonComponent = value;
			}
		}
	}
}