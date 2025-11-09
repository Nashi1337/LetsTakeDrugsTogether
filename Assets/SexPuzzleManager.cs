using UnityEngine;
using UnityEngine.Events;

public class SexPuzzleManager : MonoBehaviour
{
    public SexPuzzlePiece[] pieces;

    public UnityEvent onPuzzleSolved;

    private bool solved = false;

    public MeshRenderer mainRenderer;

    private void Update()
    {
        if (solved) return;
        if (pieces == null || pieces.Length == 0) return;

        foreach (var piece in pieces)
        {
            if (piece == null || !piece.IsInCorrectPosition)
                return;
        }

        solved = true;
        onPuzzleSolved?.Invoke();
    }

    public void OnPuzzleSolved()
    {
        mainRenderer.material.shader = Shader.Find("Particles/Standard Unlit");
    }
}