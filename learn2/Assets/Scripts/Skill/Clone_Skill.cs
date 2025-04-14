using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    [Header("Clone duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private int chanceToDuplicate;

    public void CreateClone(Transform _clonePosition,Vector3 _offset)
    {
        GameObject newClone = Instantiate(clonePrefab);

        newClone.transform.position=_clonePosition.position+_offset;

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }

    public IEnumerator CreateCloneWithDelay(Transform _transform,Vector3 _offset,float _delay)
    {
        yield return new WaitForSeconds(_delay);
        CreateClone(_transform, _offset);
    }
}
