using UnityEngine;
using UnityEngine.Events;

public class SexPuzzleManager : MonoBehaviour
{
    public SexPuzzlePiece[] pieces;

    public UnityEvent onPuzzleSolved;

    private bool solved = false;

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
}