using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cutttingCounter;

    private Animator _animator;
    private const string Cut = "Cut";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cutttingCounter.OnCut += _cutttingCounter_OnCut;
    }

    private void _cutttingCounter_OnCut(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Cut);
    }
}
