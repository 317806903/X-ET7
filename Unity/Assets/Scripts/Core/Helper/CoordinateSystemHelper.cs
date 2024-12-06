using Unity.Mathematics;

namespace ET
{
	public static class CoordinateSystemHelper
	{
		public struct CoordinateSystem
		{
			public float3 position;
			public quaternion rotation;
			public float3 scale;
		}

		public static float3 TransformPointFromAToB(float3 point, CoordinateSystem from, CoordinateSystem to)
		{
			// 从from坐标系转换到世界坐标系
			//float3 worldPoint = from.rotation * (point * from.scale) + from.position;
			float3 worldPoint = math.mul(from.rotation, (point * from.scale)) + from.position;

			// 从世界坐标系转换到to坐标系
			//float3 toPoint = to.rotation * (worldPoint - to.position) / to.scale;
			float3 toPoint = math.mul(to.rotation, (worldPoint - to.position) / to.scale);
			return toPoint;
		}

		public static float3 TransformPointFromBToA(float3 point, CoordinateSystem from, CoordinateSystem to)
		{
			// 从to坐标系转换到世界坐标系
			// float3 worldPoint = to.rotation * (point * to.scale) + to.position;
			float3 worldPoint = math.mul(to.rotation, (point * to.scale)) + to.position;

			// 从世界坐标系转换到from坐标系
			// float3 fromPoint = from.rotation * (worldPoint - from.position) / from.scale;
			float3 fromPoint = math.mul(from.rotation, (worldPoint - from.position) / from.scale);
			return fromPoint;
		}

		public static void Test()
		{
			// 定义两个坐标系A和B
			CoordinateSystem A = new CoordinateSystem
			{
				position = new float3(0, 0, 0),
				rotation = quaternion.identity,
				scale = new float3(1, 1, 1)
			};

			CoordinateSystem B = new CoordinateSystem
			{
				position = new float3(5, 3, 2),
				rotation = quaternion.EulerXYZ(math.radians(new float3(45, 30, 60))),
				scale = new float3(2, 2, 2)
			};

			// 需要转换的点
			float3 pointInA = new float3(1, 1, 1);

			// 从A坐标系转换到B坐标系
			float3 pointInB = TransformPointFromAToB(pointInA, A, B);

			// 从B坐标系转换回A坐标系
			float3 pointInAAgain = TransformPointFromBToA(pointInB, B, A);
		}
	}
}