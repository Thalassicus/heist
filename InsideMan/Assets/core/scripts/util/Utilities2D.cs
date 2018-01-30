using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities2D {

	/*
	 * Conversion functions for 3d and 2d vectors bounded to the 2D worldspace.
	*/
	public static Vector3 ConvertTo3D(Vector2 vector, float layer = 0f) {
		return new Vector3(vector.x, vector.y, layer);
	}
	public static Vector2 ConvertTo2D(Vector3 vector) {
		return new Vector2(vector.x, vector.y);
	}

	/*
	 * Removes z from 3D vectors to keep them on the 2D worldspace plane.
	*/
	public static Vector3 CleanVector3(Vector3 vector, float layer = 0f) {
		vector.z = layer;
		return vector;
	}

	/*
	 * Sets the layer of the vector as a position in z space. Useful to give priorities to MouseDown events.
	*/
	public static Vector3 SetLayer(Vector3 vector, float layer = 0f) {
		vector.z = layer;
		return vector;
	}

	/*
	 * returns direction vector, can also preserve the distance as vector magnitude
	*/
	public static Vector2 GetDirection2D(Vector2 start, Vector2 end, bool getNormalized = true) {
		if (getNormalized) {
			Vector2 vector = new Vector2(end.x - start.x, end.y - start.y);
			vector.Normalize();
			return vector;
		} else {
			return new Vector2(end.x - start.x, end.y - start.y);
		}
	}
	public static Vector2 GetDirection2D(Vector3 start, Vector3 end, bool getNormalized = true) {
		return Utilities2D.GetDirection2D(Utilities2D.ConvertTo2D(start), Utilities2D.ConvertTo2D(end), getNormalized);
	}
	public static Vector2 GetDirection2D(Vector2 start, Vector3 end, bool getNormalized = true) {
		return Utilities2D.GetDirection2D(start, Utilities2D.ConvertTo2D(end), getNormalized);
	}
	public static Vector2 GetDirection2D(Vector3 start, Vector2 end, bool getNormalized = true) {
		return Utilities2D.GetDirection2D(Utilities2D.ConvertTo2D(start), end, getNormalized);
	}

	public static Vector3 GetDirection3D(Vector3 start, Vector3 end, bool getNormalized = true) {
		if (getNormalized) {
			Vector3 vector = new Vector3(end.x - start.x, end.y - start.y, 0f);
			vector.Normalize();
			return vector;
		} else {
			return new Vector3(end.x - start.x, end.y - start.y, 0f);
		}
	}
	public static Vector3 GetDirection3D(Vector2 start, Vector2 end, bool getNormalized = true) {
		return Utilities2D.GetDirection3D(Utilities2D.ConvertTo3D(start), Utilities2D.ConvertTo3D(end), getNormalized);
	}
	public static Vector3 GetDirection3D(Vector3 start, Vector2 end, bool getNormalized = true) {
		return Utilities2D.GetDirection3D(start, Utilities2D.ConvertTo3D(end), getNormalized);
	}
	public static Vector3 GetDirection3D(Vector2 start, Vector3 end, bool getNormalized = true) {
		return Utilities2D.GetDirection3D(Utilities2D.ConvertTo3D(start), end, getNormalized);
	}

	/*
	 * Camera to World position
	*/
	public static Vector2 CameraToWorldPosition2D(Camera camera, Vector2 position, bool isScreenSpacePosition = true) {
		if (isScreenSpacePosition) {
			return Utilities2D.ConvertTo2D(camera.ScreenToWorldPoint(position));
		} else {
			return Utilities2D.ConvertTo2D(camera.ViewportToWorldPoint(position));
		}
	}
	public static Vector3 CameraToWorldPosition3D(Camera camera, Vector2 position, bool isScreenSpacePosition = true) {
		return Utilities2D.ConvertTo3D(CameraToWorldPosition2D(camera, position, isScreenSpacePosition));
	}

	/*
	 * World to Camera position
	*/
	public static Vector2 WorldToCameraPosition(Camera camera, Vector3 position, bool returnScreenSpacePosition = true) {
		if (returnScreenSpacePosition) {
			return camera.WorldToScreenPoint(Utilities2D.CleanVector3(position));
		} else {
			return camera.WorldToViewportPoint(Utilities2D.CleanVector3(position));
		}
	}
	public static Vector2 WorldToCameraPosition(Camera camera, Vector2 position, bool returnScreenSpacePosition = true) {
		return WorldToCameraPosition(camera, Utilities2D.ConvertTo3D(position), returnScreenSpacePosition);
	}

	/*
	 * Get rotation
	*/
	public static Quaternion CleanRotation(Quaternion rotation) {
		return Quaternion.Euler(0f, 0f, rotation.eulerAngles.y);
	}
	public static Quaternion GetRotation(float angle) {
		return Quaternion.Euler(0f, 0f, angle);
	}
	public static Quaternion GetRotation(Vector3 origin, Vector3 target) {
		Vector3 direction = Utilities2D.GetDirection3D(origin, target, true);
		return Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x), Vector3.forward);
	}
	public static Quaternion GetRotation(Vector2 origin, Vector2 target) {
		return Utilities2D.GetRotation(Utilities2D.ConvertTo3D(origin), Utilities2D.ConvertTo3D(target));
	}
	public static Quaternion GetRotation(Vector3 origin, Vector2 target) {
		return Utilities2D.GetRotation(origin, Utilities2D.ConvertTo3D(target));
	}
	public static Quaternion GetRotation(Vector2 origin, Vector3 target) {
		return Utilities2D.GetRotation(Utilities2D.ConvertTo3D(origin), target);
	}

	public static Quaternion GetRotation(Vector3 direction) {
		direction.Normalize();
		return Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x), Vector3.forward);
	}
	public static Quaternion GetRotation(Vector2 direction) {
		return Utilities2D.GetRotation(Utilities2D.ConvertTo3D(direction));
	}

	public static float GetAngle(Quaternion rotation) {
		return rotation.eulerAngles.z;
	}

	/*
	 * Get if target position is within range of the origin
	*/
	public static bool IsTargetInRange(Vector3 origin, Vector3 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(origin, target, false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Vector2 origin, Vector2 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(origin, target, false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Vector3 origin, Vector2 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(origin, target, false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Vector2 origin, Vector3 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(origin, target, false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Camera camera, Vector3 origin, Vector2 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(origin, Utilities2D.CameraToWorldPosition2D(camera, target), false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Camera camera, Vector2 origin, Vector3 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(Utilities2D.CameraToWorldPosition2D(camera, origin), target, false).magnitude < Mathf.Abs(range);
	}
	public static bool IsTargetInRange(Camera camera, Vector2 origin, Vector2 target, float range = 0.01f) {
		return Utilities2D.GetDirection2D(Utilities2D.CameraToWorldPosition2D(camera, origin), Utilities2D.CameraToWorldPosition2D(camera, target), false).magnitude < Mathf.Abs(range);
	}

	/*
	 * Get if target angle is within range of the origin
	*/
	public static bool IsTargetAngleInRange(Quaternion origin, Quaternion target, float range = 0.1f) {
		return Mathf.Abs(target.eulerAngles.z - origin.eulerAngles.z) < Mathf.Abs(range);
	}
	public static bool IsTargetAngleInRange(Vector3 origin, Vector3 target, float range = 0.1f) {
		return Utilities2D.IsTargetAngleInRange(Utilities2D.GetRotation(origin), Utilities2D.GetRotation(target), range);
	}
	public static bool IsTargetAngleInRange(Quaternion origin, Vector3 target, float range = 0.1f) {
		return Utilities2D.IsTargetAngleInRange(origin, Utilities2D.GetRotation(target), range);
	}
	public static bool IsTargetAngleInRange(Vector3 origin, Quaternion target, float range = 0.1f) {
		return Utilities2D.IsTargetAngleInRange(Utilities2D.GetRotation(origin), target, range);
	}
}
