using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : MonoBehaviour {
	/*
	 * Position
	 */
	protected float objectLayer = 0f;
	public void SetObjectLayer(float layer) {
		transform.position = Utilities2D.CleanVector3(transform.position, layer);
		objectLayer = layer;
		SetStartPosition(startPosition);
		SetTargetPosition(targetPosition);
	}

	protected Vector3 startPosition;
	public void SetStartPosition(Vector3 position) {
		startPosition = Utilities2D.CleanVector3(position, objectLayer);
	}
	public void SetStartPosition(Vector2 position) {
		startPosition = Utilities2D.ConvertTo3D(position, objectLayer);
	}

	protected bool hasNewTargetPosition;
	protected Vector3 targetPosition;
	public Vector3 GetTargetPosition() {
		return targetPosition;
	}
	public void SetTargetPosition(bool hasNewTarget = false) {
		targetPosition = GetTransformPosition();
		hasNewTargetPosition = hasNewTarget;
	}
	public void SetTargetPosition(Vector3 position) {
		targetPosition = Utilities2D.CleanVector3(position, objectLayer);
		hasNewTargetPosition = true;
	}
	public void SetTargetPosition(Vector2 position) {
		targetPosition = Utilities2D.ConvertTo3D(position, objectLayer);
		hasNewTargetPosition = true;
	}
	public void SetTargetPosition(Camera camera, Vector2 position) {
		targetPosition = Utilities2D.ConvertTo3D(Utilities2D.CameraToWorldPosition2D(camera, position), objectLayer);
		hasNewTargetPosition = true;
	}
	public Vector3 GetTransformPosition() {
		return Utilities2D.CleanVector3(transform.position, objectLayer);
	}
	

	public bool IsAtTargetPosition(float tolerance = 0.01f) {
		return IsAtTargetPosition(targetPosition, tolerance);
	}
	public bool IsAtTargetPosition(Vector3 target, float tolerance = 0.01f) {
		return Utilities2D.IsTargetInRange(transform.position, target, tolerance);
	}
	public bool IsAtTargetPosition(Vector2 target, float tolerance = 0.01f) {
		return Utilities2D.IsTargetInRange(transform.position, target, tolerance);
	}

	/*
	 * Rotation
	 */
	protected Quaternion startRotation;
	protected Quaternion targetRotation;
	protected bool hasNewTargetRotation;
	public Quaternion GetTargetRotation() {
		return targetRotation;
	}
	public void SetTargetRotation(bool hasNewTarget = false) {
		targetRotation = transform.rotation;
		hasNewTargetRotation = hasNewTarget;
	}
	public void SetTargetRotation(Quaternion rotation) {
		targetRotation = rotation;
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(Vector2 direction) {
		targetRotation = Utilities2D.GetRotation(direction);
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(Camera camera, Vector2 position) {
		targetRotation = Quaternion.Euler(0f, 0f, 90f) * Utilities2D.GetRotation(transform.position, Utilities2D.CameraToWorldPosition3D(camera, position));
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(float angle) {
		targetRotation = Utilities2D.GetRotation(angle);
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(Vector3 direction) {
		targetRotation = Utilities2D.GetRotation(direction);
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(Vector2 start, Vector2 end) {
		targetRotation = Utilities2D.GetRotation(start, end);
		hasNewTargetRotation = true;
	}
	public void SetTargetRotation(Vector3 start, Vector3 end) {
		targetRotation = Utilities2D.GetRotation(start, end);
		hasNewTargetRotation = true;
	}

	public bool IsAtTargetRotation(float tolerance = 0.01f) {
		return IsAtTargetRotation(targetRotation, tolerance);
	}
	public bool IsAtTargetRotation(Quaternion target, float tolerance = 0.01f) {
		return Utilities2D.IsTargetAngleInRange(transform.rotation, target, tolerance);
	}

	protected bool isControlled = false;
	public bool GetIsControlled() {
		return isControlled;
	}
	public virtual void SetIsControlled(bool newIsControlled) {
		isControlled = newIsControlled;
	}
}
