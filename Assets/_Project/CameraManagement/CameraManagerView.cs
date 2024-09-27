using _Project.StrangeIOCUtility;
using UnityEngine;

namespace _Project.CameraManagement
{
	public class CameraManagerView : ViewZeitnot
	{
		[Header("References")]
		[SerializeField] private Camera mainCamera;
		
		public void EnableMainCamera()
		{
			mainCamera.gameObject.SetActive(true);
		}
	}
}