using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extended Math Functions
public static class ExdMath {

	public static float SignedAngle(Vector2 vec1, Vector2 vec2) {
		int signed = 1;
		if (Vector3.Cross(vec1, vec2).z < 0) signed = -1;
		return signed * Vector3.Angle(vec1, vec2);
	}
}
