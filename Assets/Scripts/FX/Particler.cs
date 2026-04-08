using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Particler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dustParticle;
    [SerializeField] private ParticleSystem _attackParticle;
    [SerializeField] private ParticleSystem _regenerationParticle;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private int _occurAfterVelocity = 1;
    [SerializeField] private float _dustFormationPeriod = 0.2f;

    private float _counter;
    private bool _isAttack = false;
    private bool _isRegeneration = false;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isAttack)
        {
            _attackParticle.Play();
        }

        if (_isRegeneration)
        {
            _regenerationParticle.Play();
            _isRegeneration = false;
        }

        if (!_groundDetector || Mathf.Abs(_rigidbody.linearVelocity.x) < _occurAfterVelocity)
            return;

        _counter += Time.deltaTime;

        if (_counter >= _dustFormationPeriod)
        {
            _dustParticle.Play();
            _counter = 0;
        }
    }

    public void EnableAttack()
    {
        _isAttack = true;
    }

    public void DisableAttack()
    {
        _isAttack = false;
    }

    public void Regeneration()
    {
        _isRegeneration = true;
    }
}
