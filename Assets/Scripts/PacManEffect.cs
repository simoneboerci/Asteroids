using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManEffect : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Transform _oppositeBorder = null;
            float _offset = GameManager.instance.BorderOffset;

            switch (collision.gameObject.name)
            {
                case "TopBorder":
                    _oppositeBorder = GameManager.instance.GetBorderByName("BottomBorder");
                    transform.position = new Vector3(-transform.position.x, transform.position.y, _oppositeBorder.position.z + _offset);
                    break;
                case "BottomBorder":
                    _oppositeBorder = GameManager.instance.GetBorderByName("TopBorder");
                    transform.position = new Vector3(-transform.position.x, transform.position.y, _oppositeBorder.position.z - _offset);
                    break;
                case "LeftBorder":
                    _oppositeBorder = GameManager.instance.GetBorderByName("RightBorder");
                    transform.position = new Vector3(_oppositeBorder.position.x - _offset, transform.position.y, -transform.position.z);
                    break;
                case "RightBorder":
                    _oppositeBorder = GameManager.instance.GetBorderByName("LeftBorder");
                    transform.position = new Vector3(_oppositeBorder.position.x + _offset, transform.position.y, -transform.position.z);
                    break;
            }
        }
    }
}