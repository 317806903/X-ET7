using Unity.Mathematics;

namespace ET
{
	public static class MathHelper
	{
		public static float3 QuaternionToEulerAngles(quaternion q)
		{
			// 从四元数计算欧拉角
			float3 euler = new float3(
				(math.asin(math.clamp(2.0f * (q.value.w * q.value.x - q.value.y * q.value.z), -1, 1))),
				(math.atan2(2.0f * (q.value.w * q.value.y + q.value.x * q.value.z),
					1.0f - 2.0f * (q.value.y * q.value.y + q.value.z * q.value.z))),
				(math.atan2(2.0f * (q.value.w * q.value.z + q.value.x * q.value.y),
					1.0f - 2.0f * (q.value.z * q.value.z + q.value.x * q.value.x)))
			);
			return math.degrees(euler);
		}

		public static float CalculateAngle(float3 pointA, float3 pointB)
		{
			// 计算两个点之间的向量
			float3 vectorA = math.normalize(pointA);
			float3 vectorB = math.normalize(pointB);

			// 计算点积
			float dotProduct = math.dot(vectorA, vectorB);

			// 计算夹角（弧度）
			float angleRadians = math.acos(dotProduct);

			// 将弧度转换为度数
			float angleDegrees = math.degrees(angleRadians);

			return angleDegrees;
		}

		public static quaternion ForwardToQuaternion(float3 forward)
		{
			quaternion rotation = quaternion.LookRotation(forward, math.up());
			return rotation;
		}

		public static quaternion EulerAnglesToQuaternion(float3 eulerAngles)
		{
			float3 radians = math.radians(eulerAngles);
			quaternion rotation = quaternion.EulerXYZ(radians);
			return rotation;
		}

		public static quaternion AngleToQuaternion(float angle, float3 axis)
		{
			float radians = math.radians(angle);
			quaternion rotation = quaternion.AxisAngle(axis, radians);
			return rotation;
		}

		public static quaternion CalculateLookRotationSafe(float3 pointA, float3 pointB)
		{
			// 计算点B相对于点A的方向向量
			float3 forward = pointB - pointA;

			// 确保forward是单位向量
			forward = math.normalize(forward);

			// 使用默认的向上方向（通常是Vector3.up）
			float3 up = math.up();

			// 计算旋转四元数
			return quaternion.LookRotationSafe(forward, up);
		}

		public static bool IsEqualByteArrays(byte[] array1, byte[] array2)
		{
			if (array1 == null || array2 == null)
			{
				return array1 == array2;
			}

			if (array1.Length != array2.Length)
			{
				return false;
			}

			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsEqualFloat(float data1, float data2, float dis = 0.001f)
		{
			if (math.abs(data1 - data2) < dis)
			{
				return true;
			}

			return false;
		}

		public static bool IsEqualFloat2(float2 data1, float2 data2, float dis = 0.001f)
		{
			if (math.abs(data1.x - data2.x) < dis
			    && math.abs(data1.y - data2.y) < dis
			    )
			{
				return true;
			}

			return false;
		}

		public static bool IsEqualFloat3(float3 data1, float3 data2, float dis = 0.001f)
		{
			if (math.abs(data1.x - data2.x) < dis
			    && math.abs(data1.y - data2.y) < dis
			    && math.abs(data1.z - data2.z) < dis
			    )
			{
				return true;
			}

			return false;
		}

		public static bool IsEqualQuaternion(quaternion data1, quaternion data2, float dis = 0.001f)
		{
			if (math.abs(data1.value.x - data2.value.x) < dis
			    && math.abs(data1.value.y - data2.value.y) < dis
			    && math.abs(data1.value.z - data2.value.z) < dis
			    && math.abs(data1.value.w - data2.value.w) < dis
			    )
			{
				return true;
			}

			return false;
		}

	}
}