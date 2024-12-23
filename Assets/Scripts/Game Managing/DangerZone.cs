using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] GameObject warning;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            warning.SetActive(true);
        }
        if (collision == null)
        {
            // nothing
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null)
        {
            Debug.LogWarning("The 'other' object is null. It may have been destroyed.");
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            if (warning != null)
            {
                warning.SetActive(false);
            }
        }
    }
    private void Start()
    {
        warning.SetActive(false);
    }
}
