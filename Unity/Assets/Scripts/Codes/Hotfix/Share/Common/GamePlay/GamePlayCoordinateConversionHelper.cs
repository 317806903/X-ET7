using ET.Ability;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class GamePlayCoordinateConversionHelper
    {
	    public static bool IsSetCoordinateConversion { get; set; }
	    public static ET.CoordinateConversionHelper.CoordinateConversion _coordinateConversion { get; set; }

	    public static void SetCoordinateConversion(float3 position, quaternion rotation, float3 scale)
	    {
		    float3 eulerAngles = ET.MathHelper.QuaternionToEulerAngles(rotation);
		    Log.Error($"--ET.GamePlayCoordinateConversionHelper.SetCoordinateConversion position[{position}], rotation[{rotation}], eulerAngles[{eulerAngles}], scale[{scale}]");
		    _coordinateConversion = new CoordinateConversionHelper.CoordinateConversion()
		    {
			    position = position,
			    rotation = rotation,
			    scale = scale,
		    };
		    IsSetCoordinateConversion = true;
	    }

	    public static float3 TranClientPos2ServerPos(Scene scene, float3 clientPos)
		{
			if (IsSetCoordinateConversion == false)
			{
				float clientGameResScale = ET.GamePlayHelper.GetGameResScale(scene, true);

				float3 serverPos = clientPos / clientGameResScale;
				return serverPos;
			}
			else
			{
				float clientGameResScale = ET.GamePlayHelper.GetGameResScale(scene, true);

				float3 clientPosChg = ET.CoordinateConversionHelper.TransformPointFromWorldToA(clientPos, _coordinateConversion);

				float3 serverPos = clientPosChg / clientGameResScale;
				return serverPos;
			}
		}

		public static float3 TranServerPos2ClientPos(Scene scene, float3 serverPos)
		{
			if (IsSetCoordinateConversion == false)
			{
				float clientGameResScale = ET.GamePlayHelper.GetGameResScale(scene, true);

				float3 clientPos = serverPos * clientGameResScale;
				return clientPos;
			}
			else
			{
				float clientGameResScale = ET.GamePlayHelper.GetGameResScale(scene, true);

				float3 clientPos = serverPos * clientGameResScale;

				float3 clientPosChg = ET.CoordinateConversionHelper.TransformPointFromAToWorld(clientPos, _coordinateConversion);

				return clientPosChg;
			}
		}

		public static float3 TranClientForward2ServerForward(float3 clientForward)
		{
			if (IsSetCoordinateConversion == false)
			{
				float3 serverForward = clientForward;
				return serverForward;
			}
			else
			{
				float3 clientForwardChg = ET.CoordinateConversionHelper.TransformVectorFromWorldToA(clientForward, _coordinateConversion);
				float3 serverForward = clientForwardChg;
				return serverForward;
			}
		}

		public static float3 TranServerForward2ClientForward(float3 serverForward)
		{
			if (IsSetCoordinateConversion == false)
			{
				float3 clientForward = serverForward;
				return clientForward;
			}
			else
			{
				float3 clientForward = serverForward;
				float3 clientForwardChg = ET.CoordinateConversionHelper.TransformVectorFromAToWorld(clientForward, _coordinateConversion);
				return clientForwardChg;
			}
		}

		public static quaternion TranClientQuaternion2ServerQuaternion(quaternion clientQuaternion)
		{
			if (IsSetCoordinateConversion == false)
			{
				quaternion serverQuaternion = clientQuaternion;
				return serverQuaternion;
			}
			else
			{
				quaternion clientQuaternionChg = ET.CoordinateConversionHelper.TransformRotationFromWorldToA(clientQuaternion, _coordinateConversion);
				quaternion serverQuaternion = clientQuaternionChg;
				return serverQuaternion;
			}
		}

		public static quaternion TranServerQuaternion2ClientQuaternion(quaternion serverQuaternion)
		{
			if (IsSetCoordinateConversion == false)
			{
				quaternion clientQuaternion = serverQuaternion;
				return clientQuaternion;
			}
			else
			{
				quaternion clientQuaternion = serverQuaternion;
				quaternion clientQuaternionChg = ET.CoordinateConversionHelper.TransformRotationFromAToWorld(clientQuaternion, _coordinateConversion);
				return clientQuaternionChg;
			}
		}
	}
}