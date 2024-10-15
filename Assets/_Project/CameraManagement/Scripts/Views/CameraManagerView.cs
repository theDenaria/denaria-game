using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;

namespace _Project.CameraManagement.Scripts.Views
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