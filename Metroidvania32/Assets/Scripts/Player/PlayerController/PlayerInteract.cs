using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    private PlayerInputHandler input;

    private readonly List<IInteractable> interactablesInRange = new();

    private IInteractable currentInteractable;

    private void OnEnable()
    {
        input = MasterInputHandler.Instance.playerInput;
        input.OnInteract += Interact;
    }

    private void OnDisable()
    {
        if (input != null)
            input.OnInteract -= Interact;
    }

    private void Update()
    {
        if (interactablesInRange.Count == 0)
            return;

        ShowOnlyClosestInteractable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactablesInRange.Contains(interactable))
                return;

            interactablesInRange.Add(interactable);

            // Re-evaluate immediately when something enters
            ShowOnlyClosestInteractable();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            if (interactablesInRange.Remove(interactable))
            {
                interactable.HideInteractIcon();

                if (currentInteractable == interactable)
                    currentInteractable = null;

                ShowOnlyClosestInteractable();
            }
        }
    }

    private void Interact()
    {
        Debug.Log("Pressed Interact");
        currentInteractable?.Interact();
    }

    private void ShowOnlyClosestInteractable()
    {
        IInteractable closest = GetClosestInteractable();

        if (closest == currentInteractable)
            return;

        // Hide previous
        if (currentInteractable != null)
            currentInteractable.HideInteractIcon();

        currentInteractable = closest;

        // Show new
        if (currentInteractable != null)
            currentInteractable.ShowInteractIcon();
    }

    private IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        Vector2 origin = transform.position;

        for (int i = 0; i < interactablesInRange.Count; i++)
        {
            var interactable = interactablesInRange[i];
            if (interactable == null) continue;

            var mb = interactable as MonoBehaviour;
            if (mb == null) continue;

            float dist = Vector2.Distance(origin, mb.transform.position);

            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = interactable;
            }
        }

        return closest;
    }
}