using System;
using System.Collections.Generic;
using Unity.Mathematics;
namespace ET
{
    public class LineSimplifier
    {
        public static List<float3> SimplifyLineRDP(List<float3> points, float epsilon = 0.1f)
        {
            if (points == null || points.Count < 3)
                return points;

            List<int> pointIndicesToKeep = new List<int>
            {
                0,               // Always keep the first point
                points.Count - 1 // Always keep the last point
            };

            DouglasPeucker(points, 0, points.Count - 1, epsilon, pointIndicesToKeep);

            pointIndicesToKeep.Sort();

            // Create the result list
            List<float3> result = new List<float3>();
            foreach (int index in pointIndicesToKeep)
            {
                result.Add(points[index]);
            }
            Log.Debug($"Line simplified from {points.Count} to {result.Count}");
            return result;
        }

        private static void DouglasPeucker(List<float3> points, int first, int last, float epsilon, List<int> pointIndicesToKeep)
        {
            float maxDistance = 0;
            int indexFarthest = 0;

            // Find the point with the maximum distance
            for (int i = first + 1; i < last; i++)
            {
                float distance = PerpendicularDistance(points[i], points[first], points[last]);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    indexFarthest = i;
                }
            }

            // If max distance is greater than epsilon, recursively simplify
            if (maxDistance > epsilon)
            {
                // Keep the point at indexFarthest
                pointIndicesToKeep.Add(indexFarthest);

                // Recursively simplify the two sub-polylines
                DouglasPeucker(points, first, indexFarthest, epsilon, pointIndicesToKeep);
                DouglasPeucker(points, indexFarthest, last, epsilon, pointIndicesToKeep);
            }
            // Else, all points between first and last can be removed
        }

        private static float PerpendicularDistance(float3 pt, float3 lineStart, float3 lineEnd)
        {
            // If the line is a point, return the distance between pt and lineStart
            if (lineStart.Equals(lineEnd))
            {
                return math.distance(pt, lineStart);
            }

            // Compute the projection of pt onto the line segment
            float t = ((pt.x - lineStart.x) * (lineEnd.x - lineStart.x) +
                        (pt.y - lineStart.y) * (lineEnd.y - lineStart.y) +
                        (pt.z - lineStart.z) * (lineEnd.z - lineStart.z)) /
                    ((lineEnd.x - lineStart.x) * (lineEnd.x - lineStart.x) +
                        (lineEnd.y - lineStart.y) * (lineEnd.y - lineStart.y) +
                        (lineEnd.z - lineStart.z) * (lineEnd.z - lineStart.z));

            // Clamp t to the range [0,1]
            t = Math.Max(0, Math.Min(1, t));

            // Find the closest point on the line segment
            float closestX = lineStart.x + t * (lineEnd.x - lineStart.x);
            float closestY = lineStart.y + t * (lineEnd.y - lineStart.y);
            float closestZ = lineStart.z + t * (lineEnd.z - lineStart.z);

            // Compute the distance from pt to the closest point
            return math.distance(pt, new float3(closestX, closestY, closestZ));
        }
    }
}
