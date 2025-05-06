using Unity.Mathematics;

namespace ET
{
	/// <summary>
	/// 坐标转换
	/// </summary>
	public static class CoordinateConversionHelper
	{
		public struct CoordinateConversion
		{
			public float3 position;
			public quaternion rotation;
			public float3 scale;
		}

        // 点从 A 坐标系转换到 B 坐标系
		public static float3 TransformPointFromAToB(float3 pointWhenA, CoordinateConversion fromA, CoordinateConversion toB)
		{
			// 从from坐标系转换到世界坐标系
			float3 pointWhenWorld = TransformPointFromAToWorld(pointWhenA, fromA);

			// 从世界坐标系转换到to坐标系
			float3 pointWhenB = TransformPointFromWorldToA(pointWhenWorld, toB);
			return pointWhenB;
		}

        // 点从 A 坐标系转换到世界坐标系
		public static float3 TransformPointFromAToWorld(float3 pointWhenA, CoordinateConversion fromA)
		{
			float3 pointWhenWorld = math.mul(fromA.rotation, (pointWhenA * fromA.scale)) + fromA.position;
			return pointWhenWorld;
		}

        // 点从世界坐标系转换到 A 坐标系
		public static float3 TransformPointFromWorldToA(float3 pointWhenWorld, CoordinateConversion toA)
		{
			float3 pointWhenA = math.mul(math.inverse(toA.rotation), (pointWhenWorld - toA.position)) / toA.scale;
			return pointWhenA;
		}

		// 向量从 A 坐标系转换到 B 坐标系
		public static float3 TransformVectorFromAToB(float3 vectorWhenA, CoordinateConversion fromA, CoordinateConversion toB)
		{
			// 从from坐标系转换到世界坐标系
			float3 vectorWhenWorld = TransformVectorFromAToWorld(vectorWhenA, fromA);

			// 从世界坐标系转换到to坐标系
			float3 vectorWhenB = TransformVectorFromWorldToA(vectorWhenWorld, toB);
			return vectorWhenB;
		}

        // 向量从 A 坐标系转换到世界坐标系
		public static float3 TransformVectorFromAToWorld(float3 vectorWhenA, CoordinateConversion fromA)
		{
			float3 vectorWhenWorld = math.rotate(fromA.rotation, vectorWhenA);
			return vectorWhenWorld;
		}

		// 向量从世界坐标系转换到 A 坐标系
		public static float3 TransformVectorFromWorldToA(float3 vectorWhenWorld, CoordinateConversion toA)
		{
			float3 vectorWhenA = math.rotate(math.inverse(toA.rotation), vectorWhenWorld);
			return vectorWhenA;
		}

		// 向量从 A 坐标系转换到 B 坐标系
		public static quaternion TransformRotationFromAToB(quaternion rotationWhenA, CoordinateConversion fromA, CoordinateConversion toB)
		{
			// 从from坐标系转换到世界坐标系
			quaternion rotationWhenWorld = TransformRotationFromAToWorld(rotationWhenA, fromA);

			// 从世界坐标系转换到to坐标系
			quaternion rotationWhenB = TransformRotationFromWorldToA(rotationWhenWorld, toB);
			return rotationWhenB;
		}

		// 旋转从 A 坐标系转换到世界坐标系
		public static quaternion TransformRotationFromAToWorld(quaternion rotationWhenA, CoordinateConversion fromA)
		{
			quaternion rotationWhenWorld = math.mul(fromA.rotation, rotationWhenA);
			return rotationWhenWorld;
		}

        // 旋转从世界坐标系转换到 A 坐标系
		public static quaternion TransformRotationFromWorldToA(quaternion rotationWhenWorld, CoordinateConversion toA)
		{
			quaternion rotationWhenA = math.mul(math.inverse(toA.rotation), rotationWhenWorld);
			return rotationWhenA;
		}

		public static void TestAll()
		{
			TestPointConversion();
			TestVectorConversion();
			TestRotationConversion();
		}

		public static void TestPointConversion()
		{
			Log.Debug("=== ET.CoordinateConversionHelper Testing Point Conversion ===");

			// 定义两个坐标系 A 和 B
			CoordinateConversionHelper.CoordinateConversion A = new CoordinateConversionHelper.CoordinateConversion
			{
				position = new float3(0, 0, 0),
				rotation = quaternion.identity,
				scale = new float3(1, 1, 1)
			};

			CoordinateConversionHelper.CoordinateConversion B = new CoordinateConversionHelper.CoordinateConversion
			{
				position = new float3(5, 3, 2),
				rotation = quaternion.EulerXYZ(math.radians(new float3(45, 30, 60))),
				scale = new float3(2, 2, 2)
			};

            // 测试点的转换
            float3 pointInA = new float3(1, 1, 1);
            float3 pointInB = CoordinateConversionHelper.TransformPointFromAToB(pointInA, A, B);
            float3 pointInAAgain = CoordinateConversionHelper.TransformPointFromAToB(pointInB, B, A);

            Log.Debug($"Point in A: {pointInA}");
            Log.Debug($"Converted to B: {pointInB}");
            Log.Debug($"Converted back to A: {pointInAAgain}");

            // 验证转换是否正确
            if (math.all(math.abs(pointInAAgain - pointInA) < 1e-5f))
            {
                Log.Debug("Point conversion test passed!");
            }
            else
            {
                Log.Error($"Point conversion test failed! pointInA[{pointInA}], pointInAAgain[{pointInAAgain}]");
            }
        }

		public static void TestVectorConversion()
        {
            Log.Debug("=== ET.CoordinateConversionHelper Testing Vector Conversion ===");

            // 定义两个坐标系 A 和 B
            CoordinateConversionHelper.CoordinateConversion A = new CoordinateConversionHelper.CoordinateConversion
            {
                position = new float3(0, 0, 0),
                rotation = quaternion.identity,
                scale = new float3(1, 1, 1)
            };

            CoordinateConversionHelper.CoordinateConversion B = new CoordinateConversionHelper.CoordinateConversion
            {
                position = new float3(5, 3, 2),
                rotation = quaternion.EulerXYZ(math.radians(new float3(45, 30, 60))),
                scale = new float3(1, 1, 1)
            };

            // 测试向量的转换
            float3 vectorInA = new float3(1, 1, 1);
            float3 vectorInB = CoordinateConversionHelper.TransformVectorFromAToB(vectorInA, A, B);
            float3 vectorInAAgain = CoordinateConversionHelper.TransformVectorFromAToB(vectorInB, B, A);

            Log.Debug($"Vector in A: {vectorInA}");
            Log.Debug($"Converted to B: {vectorInB}");
            Log.Debug($"Converted back to A: {vectorInAAgain}");

            // 验证转换是否正确
            if (math.all(math.abs(vectorInAAgain - vectorInA) < 1e-5f))
            {
                Log.Debug("Vector conversion test passed!");
            }
            else
            {
                Log.Error($"Vector conversion test failed! vectorInA[{vectorInA}], vectorInAAgain[{vectorInAAgain}]");
            }
        }

		public static void TestRotationConversion()
        {
            Log.Debug("=== ET.CoordinateConversionHelper Testing Rotation Conversion ===");

            // 定义两个坐标系 A 和 B
            CoordinateConversionHelper.CoordinateConversion A = new CoordinateConversionHelper.CoordinateConversion
            {
                position = new float3(0, 0, 0),
                rotation = quaternion.identity,
                scale = new float3(1, 1, 1)
            };

            CoordinateConversionHelper.CoordinateConversion B = new CoordinateConversionHelper.CoordinateConversion
            {
                position = new float3(5, 3, 2),
                rotation = quaternion.EulerXYZ(math.radians(new float3(45, 30, 60))),
                scale = new float3(1, 1, 1)
            };

            // 测试旋转的转换
            quaternion rotationInA = quaternion.EulerXYZ(math.radians(new float3(10, 20, 30)));
            quaternion rotationInB = CoordinateConversionHelper.TransformRotationFromAToB(rotationInA, A, B);
            quaternion rotationInAAgain = CoordinateConversionHelper.TransformRotationFromAToB(rotationInB, B, A);

            Log.Debug($"Rotation in A: {rotationInA}");
            Log.Debug($"Converted to B: {rotationInB}");
            Log.Debug($"Converted back to A: {rotationInAAgain}");

            // 验证转换是否正确
            if (AreQuaternionsEqual(rotationInAAgain, rotationInA, 1e-5f))
            {
                Log.Debug("Rotation conversion test passed!");
            }
            else
            {
                Log.Error($"Rotation conversion test failed! rotationInAAgain[{rotationInAAgain}], rotationInA[{rotationInA}]");
            }
        }

		public static bool AreQuaternionsEqual(quaternion q1, quaternion q2, float tolerance)
		{
			return math.distance(q1.value, q2.value) < tolerance;
		}
    }
}