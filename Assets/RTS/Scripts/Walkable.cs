using UnityEngine;
using System.Collections;
using UniRx;

public class Walkable : MonoBehaviour
{
    [SerializeField] float _speed = 5;

    Subject<Unit> _stop = new Subject<Unit>();

    public void WalkTo(Vector2 target)
    {
        _stop.OnNext(Unit.Default);
        Observable.EveryUpdate()
            .TakeUntilDestroy(this)
            .TakeUntil(_stop)
            .Select(_ => (target- (Vector2)transform.position ))
            .TakeWhile((delta) => 
            {
      
                return delta.magnitude > _speed* Time.deltaTime;
            }).Subscribe(delta => 
            {
               var translation = delta.normalized*_speed*Time.deltaTime;
                transform.Translate(translation);

            });

    }
}
